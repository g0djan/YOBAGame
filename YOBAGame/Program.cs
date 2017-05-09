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
            Application.Run(new YobaWindow());
        }
    }

    class Unit
    {
        private PointF _coordinates;
        private Weapon _weapon;

        public Unit(PointF coordinates)
        {
            _coordinates = coordinates;
        }

        public void TakeWeapon(Weapon gun)
        {
            _weapon = gun;
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

    class Bullet
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
    }


}
