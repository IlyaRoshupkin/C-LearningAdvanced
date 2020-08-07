﻿using System;
using System.Collections.Generic;
using System.Drawing;
namespace MyGame
{
    class Asteroid : BaseObject, ICloneable, IComparable
    {
        static Random Rnd = new Random();
        public int Power { get; set; } = 3; // Начиная с версии C# 6.0 была
        //добавлена инициализация автосвойств
    public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public object Clone()
        {
            // Создаем копию нашего робота
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new
            Point(Dir.X, Dir.Y),
            new Size(Size.Width, Size.Height))
            { Power = Power };
            // Не забываем скопировать новому астероиду Power нашего астероида
            return asteroid;
        }
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            Dir.X = (Pos.X < 0 || Pos.X >= Game.Width) ? -Dir.X : Dir.X;
            Dir.Y = (Pos.Y < 0 || Pos.Y >= Game.Height) ? -Dir.Y : Dir.Y;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y,
            Size.Width, Size.Height);
        }
        int IComparable.CompareTo(object obj)
        {
            if (obj is Asteroid temp)
            {
                if (Power > temp.Power)
                    return 1;
                if (Power < temp.Power)
                    return -1;
                else
                    return 0;
            }
            throw new ArgumentException("Parameter is not а Asteroid!");
        }

        internal static void AddAsteropids(ref List<Asteroid> _asteroids, ref int asteroidsAmount, ref int destroyedAsters)
        {
            _asteroids = new List<Asteroid>();
            asteroidsAmount++;
            for (var a = 0; a < asteroidsAmount; a++)
            {
                int r = Rnd.Next(5, 50);
                _asteroids.Add(new Asteroid(new Point(400, Rnd.Next(1, Game.Height)),
                new Point(-r / 5, r), new Size(r, r)));
            }
            destroyedAsters = 0;
        }
    }
}