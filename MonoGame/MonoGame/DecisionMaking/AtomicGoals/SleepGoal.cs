using MonoGame.Entity;
using MonoGame.GoalBehaviour;
using System;
using System.Timers;

namespace MonoGame.DecisionMaking.AtomicGoals
{
    class SleepGoal : Goal
    {
        public GoalStatus GoalStatus { get; set; }
        private Timer timer;

        private AwareEntity ME;
        private float previousMaxSpeed;

        public SleepGoal(AwareEntity me)
        {
            ME = me;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;

            previousMaxSpeed = ME.MaxSpeed;
            ME.MaxSpeed = 0;

            // Set a timer to increase the fatique
            timer = new Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += Sleep;
            timer.Enabled = true;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            // Condition to complete the goal
            if (ME.Fatique >= 10f)
                Terminate();

            return GoalStatus;
        }

        public void Terminate()
        {
            timer.Stop();
            ME.MaxSpeed = previousMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        private void Sleep(Object source, ElapsedEventArgs e) => ME.Fatique += 1f;

        public override string ToString()
        {
            return "Z z z Z z...";
        }

    }
}
