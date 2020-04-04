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

            Scale = 5;
            VColor = Color.Black;
            Mass = 4;
            MaxSpeed = 7;

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.Draw(em.survivorTexture, Pos);
            sb.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2)), VColor, thickness: 2);
        }
    }
}
