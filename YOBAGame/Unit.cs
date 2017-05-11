using System;
using System.Drawing;
using System.Windows.Forms;

namespace YOBAGame
{
    class Unit : GameObject<Bullet>
    {
        protected PointF Coordinates;
        protected float Direction;
        protected float Width;
        protected float Height;
        protected Weapon _weapon;


        public Unit(PointF coordinates)
        {
            Coordinates = coordinates;
        }

        public Tuple<float, float> Move(object sender, EventArgs args)
        {
            if (args is KeyEventArgs)

            else if (args is MouseEventArgs)

            throw new NotImplementedException();
        }

        public Bullet GenerateSomeObject(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}