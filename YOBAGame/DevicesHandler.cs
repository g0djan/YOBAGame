using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Archimedes.Geometry;
using Archimedes.Geometry.Units;
using YOBAGame.GameRules;
using YOBAGame.MapObjects;

namespace YOBAGame
{
    class DevicesHandler : IControlSource
    {
        private readonly YOBAWindow _window;
        private readonly Player _player;
        private readonly IGameRules _rules;

        public Angle Direction
        {
            get
            {
                var dy = _window.MouseLocation.Y - _player.Coordinates.Y;
                var dx = _window.MouseLocation.X - _player.Coordinates.X;
                return Angle.FromRadians(Math.Atan2(dy, dx));
            }
        }

        public Vector2 Speed
        {
            get
            {
                var velocity = Vector2.Zero;
                var keys = new List<Keys>{Keys.W, Keys.A, Keys.D, Keys.S, Keys.Up, Keys.Left, Keys.Right, Keys.Down};
                Vector2 currentVector = Vector2.FromAngleAndLenght(_player.Direction, 1);
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
                return _rules.MaxPlayerSpeed * currentVector.Normalize();
            }
        }

        public bool ShouldDropWeapon => _window.PressedKeys.Contains(Keys.F);

        public bool ShouldFire => _window.LeftButtonPressed;

        public bool ShouldPickUpWeapon => _window.PressedKeys.Contains(Keys.G);
        public bool ShouldWaveSword => _window.PressedKeys.Contains(Keys.E);

        public DevicesHandler(YOBAWindow window, Player player, IGameRules rules)
        {
            _window = window;
            _player = player;
            _rules = rules;
        }
    }
}
