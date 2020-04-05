using Microsoft.Xna.Framework;
using MonoGame.Behaviour.GoalBasedBehaviour;
using MonoGame.DecisionMaking.AtomicGoals;
using MonoGame.Entity;
using MonoGame.Entity.StaticEntities;
using MonoGame.GoalBehaviour;
using System.Linq;

namespace MonoGame.DecisionMaking.CompositeGoals
{
    class DealWithFatiqueGoal : CompositeGoal
    {
        private readonly EntityManager EM;

        private Vector2 TentAsTarget;

        public DealWithFatiqueGoal(AwareEntity me, EntityManager em) : base(me)
        {
            EM = em;

            GoalStatus = GoalStatus.Inactive;
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;

            // Get the decided position of the tent to use by the pathfinder
            foreach (StaticGameEntity entity in EM.GetStaticEntities())
            {
                if (entity.GetType() == typeof(Tent))
                    TentAsTarget = new Vector2(entity.Pos.X + 15, entity.Pos.Y + 54); 
            }
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            // Make sure that sleeping subgoal is terminated first and terminate, when fatique is good
            if (ME.Fatique >= 10f && SubGoals[0].GetType() == typeof(SleepGoal) && SubGoals[0].GoalStatus == GoalStatus.Completed)
            {
                Terminate();
            }

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed);

            // Get a path to traverse to the tent
            if (!SubGoals.OfType<FollowPathGoal>().Any())
            {
                FollowPathGoal comp = new FollowPathGoal(ME, TentAsTarget);
                AddSubGoal(comp);
            }

            SubGoals.ForEach(sg => sg.Process());

            // Once path is traversed, sleep
            if (Vector2.Subtract(TentAsTarget, ME.Pos).Length() < 10 && !SubGoals.OfType<SleepGoal>().Any())
            {

                SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed);
                AddSubGoal(new SleepGoal(ME));
            }

            SubGoals.ForEach(sg => sg.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            string valueAsString = ME.Fatique.ToString("0.00");
            return "\nDealing with fatique: " + valueAsString;
        }
    }
}
