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
            staticEntities.Add(new Palmtree(new Vector2(730, 290), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(710, 310), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(650, 220), this, 50, 50));

            Survivor survivor = new Survivor(new Vector2(500, 500), this);
            survivor.SB = new SeekBehaviour(survivor, survivor.Pos);
            movingEntities.Add(survivor);
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
            sb.Begin();
            staticEntities.ForEach(s => s.Draw(sb));
            movingEntities.ForEach(m => m.Draw(sb));
            sb.End();
        }
    }
}
