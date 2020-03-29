using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Entity;
using MonoGame.Extended;

namespace MonoGame
{
    public abstract class BaseGameEntity
    {
        public Vector2 Pos { get; set; }
        public float Scale { get; set; }

        public readonly EntityManager em;

        public BaseGameEntity(Vector2 pos, EntityManager em)
        {
            this.em = em;
            Pos = pos;
        }

        public abstract void Update(float delta);

        public virtual void Render(SpriteBatch s)
        {
            s.DrawCircle(Pos, 5f, 64, Color.Red);
        }

    }
}
