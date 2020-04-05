using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Behaviour;
using MonoGame.DecisionMaking.AtomicGoals;
using MonoGame.GoalBehaviour;
using MonoGame.GoalBehaviour.CompositeGoals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Entity
{
    public class AwareEntity : MovingEntity
    {
        private CompositeGoal CompositeGoal;

        private float TotalTimeElapsed;

        public float Hunger { get; set; }

        public float Fatique { get; set; }

        public AwareEntity(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Hunger = 10f;
            Fatique = 20f;

            SB = new IdleBehaviour(this);
            CompositeGoal = new MakeDecisionGoal(this);
        }

        public override void Update(float timeElapsed)
        {
            CompositeGoal.Process();

            base.Update(timeElapsed);

            TotalTimeElapsed += timeElapsed;

            if (TotalTimeElapsed > 1f && !CompositeGoal.SubGoals.OfType<EatGoal>().Any() && !CompositeGoal.SubGoals.OfType<SleepGoal>().Any())
            {
                TotalTimeElapsed = 0;
                Hunger -= 0.05f;
                Fatique -= 0.02f;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Game1.Instance.showInfo)
                sb.DrawString(em.fontTexture, CompositeGoal.ToString() + "\n", new Vector2(Pos.X + 10, Pos.Y + 10), Color.Red);
        }
    }
}
