using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Entity.StaticEntities;

namespace MonoGame.Entity
{
    class EntityManager
    {
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();

        public Texture2D palmtreeTexture;

        public EntityManager()
        {
            staticEntities.Add(new Palmtree(this, new Vector2D(100, 100), 50, 50));
        }

        public void LoadContent(ContentManager Content)
        {
            palmtreeTexture = Content.Load<Texture2D>("Palmtree");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            staticEntities.ForEach(s => s.Draw(sb));
            sb.End();
        }

        // internal List<StaticGameEntity> GetStaticEntities() => staticEntities;
    }
}
