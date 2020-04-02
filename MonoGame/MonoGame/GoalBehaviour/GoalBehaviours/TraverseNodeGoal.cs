using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour.GoalBehaviours
{
    class TraverseNodeGoal : Goal
    {
        public Vector2 Target;

        private int NodeCount;

        private GameTime gameTime;

        public TraverseNodeGoal(Survivor s, Vector2 target, int nodeCount) : base(s)
        {
            Target = target;
            NodeCount = nodeCount;
        }

        public override void Enter()
        {
            GoalCompleted = false;
        }

        public override bool Execute(GameTime gametime)
        {
            //if (NodeCount == 0)
            //{
            //    Exit();
            //    return GoalCompleted;
            //}

            Console.WriteLine(Target);

            if (Vector2.Subtract(Target, ME.Pos).Length() < 3)
            {
                Exit();
                return GoalCompleted;
            }

            if (NodeCount == 1)
            {
                ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Deceleration.Slow);
                Game1.Instance.em.Update(gametime);
                Exit();
            }
            else
            {
                ME.SB = new SeekBehaviour(ME, Target);
                Game1.Instance.em.Update(gametime);
                Exit();
            }

            return GoalCompleted;
        }

        public override void Exit()
        {
            GoalCompleted = true;
        }
    }
}
