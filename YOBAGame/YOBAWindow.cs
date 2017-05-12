using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    public partial class YOBAWindow : Form
    {
        private Game _game;
        private Unit _player;

        public YOBAWindow()
        {
            var timer = new Timer {Interval = 1};
            _player = new Unit(Vector2.Zero); //temporary start coordinates
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKeyComands(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            HandleKeyComands(Keys.None);
        }

        private void HandleKeyComands(Keys key)
        {
            var force = Vector2.Zero;
            var playerNorm = _player.Speed.Normalize();
            switch (key)
            {
                case Keys.Up:
                    force = playerNorm;
                    break;
                case Keys.Down:
                    force = -1 * playerNorm;
                    break;
                case Keys.Left:
                    force = playerNorm.GetRotated(Angle.HalfRotation);
                    break;
                case Keys.Right:
                    force = -1 * playerNorm.GetRotated(Angle.HalfRotation);
                    break;
                case Keys.None:
                    force = Vector2.Zero;
                    break;
            }
            _player.ChangeAcceleration(force);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _player.ChangeDirection(e.Location);
        }

        protected override bool IsInputKey(Keys keyData) => 
            keyData == Keys.Escape || base.IsInputKey(keyData);

        int tickCount = 0;

        void TimerTick(object sender, EventArgs args)
        {
            tickCount++;
            Invalidate();
        }
    }
}
