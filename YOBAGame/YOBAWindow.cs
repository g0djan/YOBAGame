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

namespace YOBAGame
{
    public partial class YOBAWindow : Form
    {
        private Game _game;
        private Player _player;

        public YOBAWindow()
        {
            var timer = new Timer {Interval = 1};
            _player = new Player();
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _player.ChangeAcceleration(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _player.ChangeAcceleration(Keys.None);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _player.ChangeDirection(e.Location);
        }

        int tickCount = 0;

        void TimerTick(object sender, EventArgs args)
        {
            tickCount++;
            Invalidate();
        }
    }
}
