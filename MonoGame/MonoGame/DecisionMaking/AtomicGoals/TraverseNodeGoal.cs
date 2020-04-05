using Microsoft.Xna.Framework;
using MonoGame.DecisionMaking;
using MonoGame.Entity;

namespace MonoGame.GoalBehaviour.GoalBehaviours
{
    class TraverseNodeGoal : Goal
    {
        public GoalStatus GoalStatus { get; set; }

        private AwareEntity ME;
        private Vector2 Target;

        public TraverseNodeGoal(AwareEntity me, Vector2 target) 
        {
            ME = me;
            Target = target;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;

            // Give entity the behaviour needed to traverse through nodes
            ME.SB = new ArriveBehaviour(ME, Target, SteeringBehaviour.Deceleration.Fast);
        }

        public GoalStatus Process()
        {

            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            // Condition to complete the goal
            if (Vector2.Subtract(Target, ME.Pos).Length() < 30)
            {
                Terminate();
            }

            return GoalStatus;
        }

        public void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "\nTraverse node";
        }
    }
}
