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
        public Texture2D seagullTexture;

        // public SpriteFont font;

        public EntityManager()
        {
            staticEntities.Add(new Palmtree(new Vector2(730, 290), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(710, 310), this, 50, 50));
            staticEntities.Add(new Palmtree(new Vector2(650, 220), this, 50, 50));

            Survivor survivor = new Survivor(new Vector2(500, 500), this);
            movingEntities.Add(survivor);

            Seagull seagull1 = new Seagull(new Vector2(200, 200), this);
            Seagull seagull2 = new Seagull(new Vector2(230, 230), this);
            Seagull seagull3 = new Seagull(new Vector2(170, 230), this);
            Seagull seagull4 = new Seagull(new Vector2(160, 240), this);
            Seagull seagull5 = new Seagull(new Vector2(220, 250), this);

            movingEntities.Add(seagull1);
            movingEntities.Add(seagull2);
            movingEntities.Add(seagull3);
            movingEntities.Add(seagull4);
            movingEntities.Add(seagull5);


        }

        public void LoadContent(ContentManager Content)
        {
            palmtreeTexture = Content.Load<Texture2D>("palmtree");

            survivorTexture = Content.Load<Texture2D>("Player");

            seagullTexture = Content.Load<Texture2D>("Seagull");
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

        internal List<MovingEntity> GetMovingEntities() => movingEntities;

        public void TagNeighbours(MovingEntity centralEntity, double radius)
        {
            foreach (MovingEntity entity in movingEntities)
            {
                // Clear current tag.
                entity.Tag = false;

                // Calculate the difference in space
                Vector2 difference = Vector2.Subtract(entity.Pos, centralEntity.Pos);

                // When the entity is in range it gets tageed.
                if (entity != centralEntity && difference.LengthSquared() < radius * radius)
                    entity.Tag = true;
            }
        }
        public void EnforceNonPenetrationConstraint(MovingEntity centralEntity)
        {
            foreach (MovingEntity entity in movingEntities)
            {
                //make sure we don't check against the individual
                if (entity == centralEntity) continue;

                // calculate the distance between the positions of the entities
                Vector2 ToEntity = Vector2.Subtract(centralEntity.Pos, entity.Pos);

                float distFromEachOther = ToEntity.Length();

                //if this distance is smaller than the sum of their radii then this entity must be moved away in the direction parallel to the ToEntity vector
                float amountOfOverlap = 10 + 10 - distFromEachOther;

                //move the entity a distance away equivalent to the amount of overlap
                if (amountOfOverlap >= 0)
                    centralEntity.Pos += Vector2.Multiply(Vector2.Divide(ToEntity, distFromEachOther), amountOfOverlap);
            }
        }
    }
}
