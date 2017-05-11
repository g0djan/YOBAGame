using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YOBAGame
{
    class Unit : IMapObject
    {
        protected PointF Coordinates;
        protected float Direction;
        protected Weapon _weapon;
        public double MaxSpeed { get; set; }
        public PointF Speed { get; set; }

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

        public PointF Acceleration()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMapObject> GeneratedObjects()
        {
            throw new NotImplementedException();
        }

        public bool ShouldBeDeleted()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMapObject> DeleteResult()
        {
            throw new NotImplementedException();
        }
    }
}