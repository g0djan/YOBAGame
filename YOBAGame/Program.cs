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

    //TODO: inherite from IMapObject and move to proper places
    /*abstract class Unit
    {
        public PointF Coordinates { get; protected set; }
        public double Direction { get; protected set; }
        //        private Weapon _weapon;

        public Unit(PointF coordinates, double direction = 0)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

//        public void TakeWeapon(Weapon gun)
//        {
//            _weapon = gun;
//        }
    }

    internal abstract class Weapon
    {
        // TODO: все умирают от одного попадания, но на будущее можно оставить
        public double _damage { get; protected set; }
//        private double _velocity;
//        public string Name { get; protected set; }

        public Weapon(double damage /*, double velocity#1#)
        {
            _damage = damage;
//            _velocity = velocity;
//            Name = name;
        }

        public abstract List<Bullet> Fire(float direction, PointF coordinates);
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
    }*/
}