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

    /*

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

        public abstract List<Bullet> Fire(double direction, PointF coordinates);
    }

    */
}
