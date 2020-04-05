using Microsoft.Xna.Framework;
using MonoGame.Behaviour.GoalBasedBehaviour;
using MonoGame.DecisionMaking;
using MonoGame.DecisionMaking.AtomicGoals;
using MonoGame.DecisionMaking.CompositeGoals;
using MonoGame.Entity;
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

        public MakeDecisionGoal(AwareEntity me) : base(me) 
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

            if (ME.Hunger < 5 && !SubGoals.OfType<DealWithHungerGoal>().Any())
            {
                AddSubGoal(new DealWithHungerGoal(ME, ME.em));
            }

            if (ME.Fatique < 8 && !SubGoals.OfType<DealWithFatiqueGoal>().Any())
            {
                AddSubGoal(new DealWithFatiqueGoal(ME, ME.em));
            }

            if (Target != Game1.Instance.Target && !SubGoals.OfType<DealWithHungerGoal>().Any())
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
            if (base.ToString().Equals(""))
                return "Making decisions... " + "\nHunger: " + ME.Hunger + "\nFatique: " + ME.Fatique;
            else
                return base.ToString();
        }
    }
}
