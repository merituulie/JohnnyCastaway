using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public AwareEntity(Vector2 pos, EntityManager em) : base(pos, em)
        {
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
            if (Game1.Instance.showInfo)
                sb.DrawString(em.fontTexture, CompositeGoal.ToString() + "\n", new Vector2(Pos.X + 10, Pos.Y + 10), Color.Red);
        }
    }
}
