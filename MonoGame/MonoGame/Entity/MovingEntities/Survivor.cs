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
    public class Survivor : MovingEntity
    {
        public Color VColor { get; set; }

        private CompositeGoal CompositeGoal;

        private float TotalTimeElapsed;

        public Survivor(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Velocity = new Vector2(0, 0f);
            Scale = 5;
            VColor = Color.Black;

            CompositeGoal = new MakeDecisionGoal(this);
        }

        public override void Update(float timeElapsed)
        {
            CompositeGoal.Process();

            base.Update(timeElapsed);

            TotalTimeElapsed += timeElapsed;
        }

        public override void Draw(SpriteBatch sb)
        {
            //double leftCorner = Pos.X - Scale;
            //double rightCorner = Pos.Y - Scale;
            //double size = Scale * 2;

            //base.Draw(sb);
            sb.Draw(em.survivorTexture, Pos);
            if (Game1.Instance.showInfo) sb.DrawString(em.fontTexture, CompositeGoal.ToString(), new Vector2(Pos.X + 10, Pos.Y + 10), Color.Green);
            sb.DrawLine(new Vector2((int)Pos.X, (int)Pos.Y), new Vector2((int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2)), VColor, thickness: 2);
        }
    }
}
