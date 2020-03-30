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
        private List<Survivor> movingEntities = new List<Survivor>();

        public Texture2D palmtreeTexture;
        public Texture2D survivorTexture;
        public Texture2D tentTexture;
        public Texture2D bushTexture;

        // public SpriteFont font;

        public EntityManager()
        {
            staticEntities.Add(new Palmtree(new Vector2(730, 290), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(710, 310), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(650, 220), this, 50, 50));
            staticEntities.Add(new Tent(new Vector2(480, 380), this, 50, 50));
            staticEntities.Add(new Bush(new Vector2(260, 540), this, 50, 50));
            staticEntities.Add(new Bush(new Vector2(380, 150), this, 50, 50));

            Survivor survivor = new Survivor(new Vector2(500, 500), this);
            movingEntities.Add(survivor);
        }

        public void LoadContent(ContentManager Content)
        {
            palmtreeTexture = Content.Load<Texture2D>("palmtree");
            tentTexture = Content.Load<Texture2D>("Tent");
            bushTexture = Content.Load<Texture2D>("bush");
                       
            survivorTexture = Content.Load<Texture2D>("Player");
        }

        public void Update(GameTime gt)
        {
            movingEntities.ForEach(m => m.Update((float)gt.ElapsedGameTime.TotalSeconds * 0.8F));
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            staticEntities.ForEach(s => s.Draw(sb));
            movingEntities.ForEach(survivor => survivor.Draw(sb));
            sb.End();
        }
    }
}
