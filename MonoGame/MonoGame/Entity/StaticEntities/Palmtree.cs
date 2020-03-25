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
        public Palmtree(EntityManager em, Vector2D position, int width, int height) : base(em, position, width, height)
        { 
        }

        public override Draw(SpriteBatch sb)
        {
            sb.Draw(em.palmtreeTexture, Pos);
        }
    }
}
