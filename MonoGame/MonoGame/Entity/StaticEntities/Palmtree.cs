using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Entity.StaticEntities
{
    class Palmtree : StaticGameEntity
    {
        public Palmtree(Vector2 pos, EntityManager em, int width, int height) : base(pos, em, width, height)
        { 
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(em.palmtreeTexture, Pos);
        }
    }
}
