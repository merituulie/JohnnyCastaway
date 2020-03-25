using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGame
{
    public class World
    {
        public List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public GraphicsDeviceManager g;

        public World(int w, int h, GraphicsDeviceManager g)
        {
            Width = w;
            Height = h;
            this.g = g;
            populate();
        }

        private void populate()
        {
            /*
            Vehicle v = new Vehicle(new Vector2D(200, 200), this, g);
            v.VColor = Color.Blue;
            v.SB = new SeekBehaviour(v);

            //Vehicle vg = new Vehicle(new Vector2D(60, 60), this);
            //vg.VColor = Color.Green;
            //entities.Add(vg);

            Target = new Vehicle(new Vector2D(100, 60), this, g);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
            */
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            }
        }

        public void Render(SpriteBatch s)
        {
            entities.ForEach(e => e.Render(s));
            Target.Render(s);
        }

        public Vector2D WrapAround(Vector2D position)
        {
            return new Vector2D((position.X + Width) % Width, (position.Y + Height) % Height);
        }
    }
}
