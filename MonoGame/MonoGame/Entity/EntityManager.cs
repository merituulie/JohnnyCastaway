using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Entity.StaticEntities;

namespace MonoGame.Entity
{
    public class EntityManager
    {
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();
        private List<MovingEntity> movingEntities = new List<MovingEntity>();

        public Texture2D palmtreeTexture;
        public Texture2D survivorTexture;

        // public SpriteFont font;

        public EntityManager()
        {
            staticEntities.Add(new Palmtree(new Vector2(100, 100), this, 50, 50));

            movingEntities.Add(new Survivor(new Vector2(80, 80), this));
        }

        public void LoadContent(ContentManager Content)
        {
            palmtreeTexture = Content.Load<Texture2D>("palmtree");

            survivorTexture = Content.Load<Texture2D>("Player");
        }

        public void Update(GameTime gt)
        {
            movingEntities.ForEach(m => m.Update((float)gt.ElapsedGameTime.TotalSeconds * 0.8F));
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Begin();
            staticEntities.ForEach(s => s.Render(sb));
            movingEntities.ForEach(m => m.Render(sb));
            //sb.End();
        }

        // internal List<StaticGameEntity> GetStaticEntities() => staticEntities;
    }
}
