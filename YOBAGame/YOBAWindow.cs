using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Archimedes.Geometry;
using Archimedes.Geometry.Primitives;
using Archimedes.Geometry.Units;
using YOBAGame.Extensions;
using YOBAGame.MapObjects;
using YOBAGame.GameRules;

namespace YOBAGame
{
    public class Resources
    {
        public List<Tuple<Bitmap, Point>[]> Images;

        public Resources(List<Tuple<Bitmap, Point>[]> images)
        {
            Images = images;
        }
    }

    public partial class YOBAWindow : Form
    {
        private readonly Timer _timer;
        private Game _game;
        private Player _player;
        private UsualBot _bot;
        private Sword _swordSample;
        private readonly UsualBullet _bulletSample;
        private readonly UsualWeapon _weaponSample;
        private readonly SwordSwing _swordSwingSample;

        private DevicesHandler _devicesHandler;
        private Point _cameraLeftUpper;

        public HashSet<Keys> PressedKeys { get; }
        public Point MouseLocation { get; private set; }
        public bool LeftButtonPressed { get; private set; }
        public bool RightButtonPressed { get; private set; }


        private const double _scaleBulletCoefficient = 5; //TODO: настроить
        public YOBAWindow()
        {
            this.Size = new Size(800, 480);
            _timer = new Timer { Interval = 30 };
            LoadResources();

            var rules = UsualRules.Default;

            _bulletSample = new UsualBullet(Vector2.Zero, Vector2.Zero, rules.DefaultBulletLength, null, rules, ExternalData["Bullet"]);
            _swordSwingSample = new SwordSwing(new Circle2(Vector2.Zero, rules.SwordSwingRadius), null, double.PositiveInfinity, rules, ExternalData["SwordSwing"]);
            _weaponSample = new UsualWeapon(new Circle2(Vector2.Zero, rules.WeaponDefaultRadius),rules,rules.DefaultReloadDuration,_bulletSample, 3, ExternalData["Weapon"], Angle.FromDegrees(30));
            _swordSample = new Sword(new Circle2(Vector2.Zero, rules.DefaultSwordRadius),rules , ExternalData["Sword"], _swordSwingSample);
            _player = new Player(rules.DefaultHP,_weaponSample, _swordSample, new Vector2(250, 250), new Circle2(Vector2.Zero, rules.DefaultPlayerRadius) ,null ,rules, ExternalData["Player"]);
            _bot = new UsualBot(rules.DefaultHP,_weaponSample, Vector2.Zero, new Circle2(Vector2.Zero, rules.DefaultPlayerRadius), ExternalData["Enemy"], rules);

            _game = new Game(rules);
            _game.LoadMap(System.IO.File.OpenText(Path.GetFullPath(@"..\..\Resources\Maps\map1.map")));
            _devicesHandler = new DevicesHandler(this, _player, _game.Rules);
            _player.Control = _devicesHandler;

            PressedKeys = new HashSet<Keys>();
            MouseLocation = new Point();
            
            _timer.Tick += TimerTick;
            _timer.Start();

            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseMove += (sender, args) => MouseLocation = PointToClient(args.Location);
        }

        private Dictionary<string, Resources> ExternalData;

        private void LoadResources()
        {
            var picFiles = new []
            {
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\enemy1_sprites.png"), "Enemy", 4) ,
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\player_sprites.png"), "Player", 4),
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\word_sprites.png"), "Sword", 2),
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\sword_swing_sprites.png"), "SwordSwing", 2),
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\bullet_sprites.png"), "Bullet", 1),
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\weapon1_sprites.png"), "Weapon", 2),
                Tuple.Create(Path.GetFullPath(@"..\..\Resources\Images\weapon1_droped_sprites.png"), "DroppedWeapon", 1)
            };
            foreach (var picFile in picFiles)
                ExternalData.Add(picFile.Item2, 
                    new Resources(ImageParser.ParsePicture(picFile.Item1, picFile.Item3)));
            ExternalData["Weapon"].Images.AddRange(ExternalData["DropedWeapon"].Images);
            var bullet = ExternalData["Bullet"].Images[0][0];
            ExternalData["Bullet"].Images[0][0] = Tuple.Create(
                bullet.Item1.ScaleImage(_scaleBulletCoefficient, 1),
                bullet.Item2);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    LeftButtonPressed = true;
                    break;
                case MouseButtons.Right:
                    RightButtonPressed = true;
                    break;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    LeftButtonPressed = false;
                    break;
                case MouseButtons.Right:
                    RightButtonPressed = false;
                    break;
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            PressedKeys.Add(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            PressedKeys.Remove(e.KeyCode);
        }

        protected override bool IsInputKey(Keys keyData) => 
            keyData == Keys.Escape || base.IsInputKey(keyData);

        protected override void OnPaint(PaintEventArgs e)
        {
            _cameraLeftUpper = new Point((int) _player.Coordinates.X - Width / 2,
                (int) _player.Coordinates.Y - Height / 2);
            foreach (var obj in _game.Objects)
            {
                if (obj is Wall)
                    DrawWall(e, obj as Wall);
                else if (obj is IDrawableObject)
                    DrawImage(e, obj);
            }
        }

        private void DrawWall(PaintEventArgs e, Wall wall)
        {
            e.Graphics.FillPolygon(
                wall.Color,
                wall.HitBox.ToVertices().Select(v => v.ToLocation()).ToArray());
        }

        private void DrawImage(PaintEventArgs e, IMapObject obj)
        {
            var forDrawing = (obj as IDrawableObject).ForDrawing;
            foreach (var tuple in forDrawing)
                e.Graphics.DrawImage(tuple.Item1, tuple.Item2.Sub(_cameraLeftUpper));
        }

        
        void TimerTick(object sender, EventArgs args)
        {
            _game.Step(_timer.Interval);
            Invalidate();
        }
    }
}
