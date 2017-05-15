using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;

namespace YOBAGame.MapObjects
{
    class DevicesHandler : IControlSource
    {
        private YOBAWindow _window;
        private AbstractPlayer _player;

        public Angle Direction
        {
            get
            {
                var dy = _window.MouseLocation.Y - _player.Coordinates.Y;
                var dx = _window.MouseLocation.X - _player.Coordinates.X;
                return Angle.FromRadians(Math.Atan2(dy, dx));
            }
        }

        private const double addedSpeedLength = 1; //TODO: настроить
        public Vector2 Speed
        {
            get
            {
                var velocity = new Vector2();
                var keys = new List<Keys>{Keys.W, Keys.A, Keys.D, Keys.S, Keys.Up, Keys.Left, Keys.Right, Keys.Down};
                var currentVector = _player.Speed.Normalize();
                foreach (var key in keys)
                    if (_window.PressedKeys.Contains(key))
                        switch (key)
                        {
                            case Keys.W:
                            case Keys.Up:
                                velocity += currentVector;
                                break;
                            case Keys.A:
                            case Keys.Left:
                                velocity += currentVector.GetRotated(Angle.HalfRotation);
                                break;
                            case Keys.D:
                            case Keys.Right:
                                velocity -= currentVector.GetRotated(Angle.HalfRotation);
                                break;
                            case Keys.S:
                            case Keys.Down:
                                velocity -= currentVector;
                                break;
                        }
                return addedSpeedLength * currentVector.Normalize();
            }
        }

        public bool ShouldDropWeapon => _window.PressedKeys.Contains(Keys.F);

        public bool ShouldFire => _window.LeftButtonPressed;

        public bool ShouldPickUpWeapon => _window.PressedKeys.Contains(Keys.G);
        public bool ShouldWaveSword => _window.PressedKeys.Contains(Keys.E);

        public DevicesHandler(YOBAWindow window, AbstractPlayer player)
        {
            _window = window;
            _player = player;
        }
    }
}
