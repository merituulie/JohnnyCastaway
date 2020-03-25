using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public Game1 MyWorld { get; set; }

        public Texture2D texture { get; set; }

        public BaseGameEntity(Vector2D pos, Game1 w, GraphicsDeviceManager g)
        {
            Pos = pos;
            MyWorld = w;
            texture = new Texture2D(g.GraphicsDevice, 40, 40);
        }

        public abstract void Update(float delta);

        public virtual void Render(SpriteBatch s)
        {
            //g.Draw(Brushes.Blue, new Rectangle((int)Pos.X, (int)Pos.Y, 10, 10));
            //s.Draw(texture, new Vector2(200, 200));
            s.Draw(texture, new Rectangle(0, 0, 40, 40), Color.Red);
        }

    }
}
