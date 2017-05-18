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
                var speed = Vector2.Zero;

                if (_window.PressedKeys.Contains(Keys.W))
                    speed -= Vector2.UnitY;
                if (_window.PressedKeys.Contains(Keys.S))
                    speed += Vector2.UnitY;
                if (_window.PressedKeys.Contains(Keys.D))
                    speed += Vector2.UnitX;
                if (_window.PressedKeys.Contains(Keys.A))
                    speed -= Vector2.UnitX;
                if (speed == Vector2.Zero)
                    return Vector2.Zero;
                return _rules.MaxPlayerSpeed * speed.Normalize();
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