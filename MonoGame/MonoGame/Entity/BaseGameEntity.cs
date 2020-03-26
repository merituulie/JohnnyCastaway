using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Entity;

namespace MonoGame
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public Game1 MyWorld { get; set; }

        public readonly EntityManager em;

        public Texture2D texture { get; set; }

        public BaseGameEntity(Vector2D pos, Game1 w, GraphicsDeviceManager g, EntityManager em)
        {
            this.em = em;
            Pos = pos;
            MyWorld = w;
            texture = new Texture2D(g.GraphicsDevice, 40, 40);
        }

        public abstract void Update(float delta);

        public virtual void Render(SpriteBatch s)
        {
            s.Draw(texture, new Rectangle(0, 0, 40, 40), Color.Red);
        }

    }
}
