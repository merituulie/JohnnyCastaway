using Microsoft.Xna.Framework;
using MonoGame.Behaviour.GoalBasedBehaviour;
using MonoGame.DecisionMaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour.CompositeGoals
{
    class MakeDecisionGoal : CompositeGoal
    {
        private Vector2 Target;

        public MakeDecisionGoal(MovingEntity me) : base(me) 
        { 

        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed ||
            sg.GoalStatus == GoalStatus.Failed);

            if (Target != Game1.Instance.Target)
            {
                AddSubGoal(new FollowPathGoal(ME));
                Target = Game1.Instance.Target;
            }

            SubGoals.ForEach(sg => sg.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
        }

        public override string ToString()
        {
            return "Making decisions... " + base.ToString();
        }
    }
}
