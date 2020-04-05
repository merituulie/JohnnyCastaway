using MonoGame.DecisionMaking;

namespace MonoGame.GoalBehaviour
{
    interface Goal
    {

        GoalStatus GoalStatus { get; set; }

        void Activate();

        GoalStatus Process();

        void Terminate();
        
    }
}
