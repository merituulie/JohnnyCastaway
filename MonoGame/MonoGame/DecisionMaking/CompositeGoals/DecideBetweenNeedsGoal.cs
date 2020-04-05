using MonoGame.Entity;
using MonoGame.GoalBehaviour;

namespace MonoGame.DecisionMaking.CompositeGoals
{
    class DecideBetweenNeedsGoal : CompositeGoal
    {
        public DecideBetweenNeedsGoal(AwareEntity me) : base(me)
        {
            GoalStatus = GoalStatus.Inactive;
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed || sg.GoalStatus == GoalStatus.Failed);

            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            // Decide if there is a need for fulfillment and which need
            if (SubGoals.Count == 0)
                if (ME.Hunger >= 3 && ME.Fatique >= 5)
                    Terminate();
                else if (ME.Hunger < ME.Fatique)
                    AddSubGoal(new DealWithHungerGoal(ME, ME.em));
                else
                    AddSubGoal(new DealWithFatiqueGoal(ME, ME.em));

            SubGoals.ForEach(sg => sg.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }
    }
}
