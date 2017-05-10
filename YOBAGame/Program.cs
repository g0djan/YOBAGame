using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows;

namespace YOBAGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new YOBAWindow());
        }
    }

    class Weapon
    {
        private double _damage;
        private double _velocity;
        private string _name;

        public Weapon(double damage, double velocity, string name)
        {
            _damage = damage;
            _velocity = velocity;
            _name = name;
        }

        public void Fire(float direction, PointF coordinates) => 
            new Bullet(direction, coordinates).Fly();
    }

    class Bullet : GameObject<object>
    {
        private double _direction;
        private PointF _coordinates;

        private const double step = 0.1;

        public Bullet(double direction, PointF coordinates)
        {
            _direction = direction;
            _coordinates = coordinates;
        }

        public void Fly()
        {
            while (true) //здесь проверка пока не вылетели за поле 
                         //или не произошло столкновение
            {
                _coordinates = new PointF(
                    _coordinates.X + (float) Math.Cos(_direction),
                    _coordinates.Y + (float) Math.Cos(_direction));
            }
        }

        public Tuple<float, float> Move(object sender, EventArgs args)
        {
            MouseEventArgs mouseArgs;
            if (args is MouseEventArgs)
                mouseArgs = args as MouseEventArgs;
            ;
            float direction = Math.Atan2(_coordinates.Y - mouseArgs.Y, _coordinates.X - mouseArgs.X);
            throw new NotImplementedException();
        }

        public object GenerateSomeObject(object sender, EventArgs args) => null;
    }


}
