using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Entity
{
    public class StaticGameEntity : BaseGameEntity
    {
        public int TextureWidth { get; set; }
        public int TextureHeight { get; set; }

        public StaticGameEntity(Vector2 pos, EntityManager em, int width, int height) : base(pos, em)
        {
            TextureWidth = width;
            TextureHeight = height;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
