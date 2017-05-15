using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public HashSet<Keys> PressedKeys { get; }
        public Point MouseLocation { get; private set; }
        private Game _game;
        private DevicesHandler _devicesHandler;
        public bool LeftButtonPressed { get; private set; }
        public bool RightButtonPressed { get; private set; }

        public YOBAWindow()
        {
            PressedKeys = new HashSet<Keys>();
            MouseLocation = new Point();
            _devicesHandler = new DevicesHandler(this, _game.Player);
            
            var timer = new Timer {Interval = 1};
            _game = new Game();
            timer.Tick += TimerTick;
            timer.Start();

            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseMove += (sender, args) => MouseLocation = args.Location;
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

        int tickCount = 0;

        void TimerTick(object sender, EventArgs args)
        {
            _game.Step(dt);
            tickCount++;
            Invalidate();
        }

        
    }
}
