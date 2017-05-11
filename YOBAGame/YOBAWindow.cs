using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YOBAGame
{
    public partial class YOBAWindow : Form
    {
        private Game Game;

        public YOBAWindow()
        {
            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Game.KeyPressed = Keys.None;
        }

        int tickCount = 0;

        void TimerTick(object sender, EventArgs args)
        {
            tickCount++;
            Invalidate();
        }
    }
}
