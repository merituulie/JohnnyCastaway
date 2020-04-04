using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviour;
using MonoGame.Behaviour.GoalBasedBehaviour;
using MonoGame.Entity;
using MonoGame.GoalBehaviour;
using MonoGame.GoalBehaviour.CompositeGoals;
using System;

namespace MonoGame
{
    public class Survivor : AwareEntity
    {
        public Color VColor { get; set; }

        public Survivor(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Velocity = new Vector2(0, 0f);

            Scale = 1;
            VColor = Color.Black;
            Mass = 4;
            MaxSpeed = 8;

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sourceRectangle = new Rectangle(0, 0, em.survivorTexture.Width, em.survivorTexture.Height);
            origin = new Vector2(em.survivorTexture.Width / 2, em.survivorTexture.Height / 2);

            sb.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 4), (int)Pos.Y + (int)(Velocity.Y * 4)), VColor, thickness: 2);
            sb.Draw(em.survivorTexture, Pos, sourceRectangle, Color.White, angle, origin, Scale, SpriteEffects.None, 1);
        }
    }
}
