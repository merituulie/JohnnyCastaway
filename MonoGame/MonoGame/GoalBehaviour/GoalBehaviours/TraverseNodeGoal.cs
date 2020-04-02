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
        private Vector2 Target;
        private int NodeCount;
        private Goal PreviousGoal;

        public TraverseNodeGoal(Survivor s, Vector2 target, int nodeCount) : base(s)
        {
            Target = target;
            NodeCount = nodeCount;
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override bool Execute()
        {
            if (PreviousGoal == null)
                PreviousGoal = this;

            if (NodeCount == 0)
            {
                Exit();
                return GoalCompleted;
            }

            if (Vector2.Subtract(Target, ME.Pos).Length() < 32 && PreviousGoal.GoalCompleted == true)
            {
                Exit();
                return GoalCompleted;
            }

            if (NodeCount == 1)
            {
                ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Deceleration.Slow);
                Exit();
            }
            else
            {
                ME.SB = new SeekBehaviour(ME, Target);
                Exit();
            }

            //switch (NodeCount) // to simpler check
            //{
            //    case 5:
            //    case 4:
            //        ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Decelaration.Fast);
            //        break;
            //    case 3:
            //        ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Decelaration.Normal);
            //        break;
            //    case 2:
            //    case 1:
            //        ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Decelaration.Slow);
            //        break;
            //    default:
            //        ME.SB = new SeekBehaviour(ME, Target);
            //        break;
            //}
            return GoalCompleted;
        }

        public override void Exit()
        {
            PreviousGoal = this;
            GoalCompleted = true;
        }
    }
}
