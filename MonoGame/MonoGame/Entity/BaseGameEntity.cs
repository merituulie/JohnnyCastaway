﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int)Pos.X, (int)Pos.Y, 10, 10));
        }

    }
}