using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Entity;
using System;

namespace MonoGame
{
    public class Seagull : MovingEntity
    {
        private float angle = 0;
        private Rectangle sourceRectangle;
        private Vector2 origin;

        public Seagull(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Velocity = new Vector2(0, 0f);
            Scale = 1;

            SB = new FlockingBehaviour(this);

        }

        public override void Draw(SpriteBatch sb)
        {
            sourceRectangle = new Rectangle(0, 0, em.seagullTexture.Width, em.seagullTexture.Height);
            origin = new Vector2(em.seagullTexture.Width / 2, em.seagullTexture.Height / 2);

            sb.Draw(em.seagullTexture, Pos, sourceRectangle, Color.White, angle, origin, Scale, SpriteEffects.None, 1);
            sb.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2)), Color.Black, thickness:2) ;
        }

        public override void Update(float timeElapsed)
        {
            //angle = (float)(Math.Atan2(Heading.Y, Heading.X) * (180/Math.PI));

            base.Update(timeElapsed);
        }
    }
}
