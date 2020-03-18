using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(200, 200), this);
            v.VColor = System.Drawing.Color.Black;
            v.SB = new FleeBehaviour(v);
            entities.Add(v);

            //Vehicle vg = new Vehicle(new Vector2D(60, 60), this);
            //vg.VColor = Color.Green;
            //entities.Add(vg);

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = System.Drawing.Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }

        public Vector2D WrapAround(Vector2D position)
        {
            return new Vector2D((position.X + Width) % Width, (position.Y + Height) % Height);
        }
    }
}
