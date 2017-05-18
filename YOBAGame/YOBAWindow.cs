using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        private Game _game;
        private Player _player;
        private UsualBot _bot;

        private DevicesHandler _devicesHandler;
        private Point _cameraLeftUpper;

        public HashSet<Keys> PressedKeys { get; }
        public Point MouseLocation { get; private set; }
        public bool LeftButtonPressed { get; private set; }
        public bool RightButtonPressed { get; private set; }

        private const double _scaleBulletCoefficient = 5; //TODO: настроить
        public YOBAWindow()
        {
            var timer = new Timer { Interval = 1 };
            LoadResources();
            
            _player = new Player();
            _bot = new UsualBot();
            _game = new Game(, ,new UsualRules(), ExternalData);
            _devicesHandler = new DevicesHandler(this, _player, _game.Rules);
            _player.Control = _devicesHandler;

            PressedKeys = new HashSet<Keys>();
            MouseLocation = new Point();
            
            timer.Tick += TimerTick;
            timer.Start();

            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseMove += (sender, args) => MouseLocation = PointToClient(args.Location);
        }

        private Dictionary<string, Resources> ExternalData;

        private void LoadResources()
        {
            var picFiles = new []
            {
                Tuple.Create("enemy1_sprites.png", "Enemy", 4) ,
                Tuple.Create("player_sprites.png", "Player", 4),
                Tuple.Create("sword_sprites.png", "Sword", 2),
                Tuple.Create("sword_swing_sprites.png", "SwordSwing", 2),
                Tuple.Create("bullet_sprites.png", "Bullet", 1),
                Tuple.Create("weapon1_sprites.png", "Weapon", 2),
                Tuple.Create("weapon1_droped_sprites.png", "DroppedWeapon", 1)
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

        int tickCount = 0;

        void TimerTick(object sender, EventArgs args)
        {
            _game.Step(dt);
            tickCount++;
            Invalidate();
        }
    }
}
