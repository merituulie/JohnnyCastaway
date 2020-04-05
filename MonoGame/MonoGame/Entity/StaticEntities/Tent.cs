using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Entity.StaticEntities
{
    class Tent : StaticGameEntity
    {
        public Tent(Vector2 pos, EntityManager em, int width, int height) : base(pos, em, width, height)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(em.tentTexture, Pos);
        }
    }
}
