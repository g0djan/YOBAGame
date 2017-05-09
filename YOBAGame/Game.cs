using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YOBAGame
{
    class Game
    {
        private double MapWidth { get; set; }
        private double MapHeight { get; set; }

        public Game(double width, double height)
        {
            MapHeight = height;
            MapWidth = width;
        }

        public Keys KeyPressed { get; set; }
    }
}
