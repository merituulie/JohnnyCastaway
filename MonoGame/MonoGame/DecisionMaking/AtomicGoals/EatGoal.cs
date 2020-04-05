using MonoGame.Entity;
using MonoGame.GoalBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MonoGame.DecisionMaking.AtomicGoals
{
    class EatGoal : Goal
    {
        public GoalStatus GoalStatus { get; set; }
        private Timer timer;

        private AwareEntity ME;
        private float previousMaxSpeed;

        public EatGoal(AwareEntity me)
        {
            ME = me;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;

            previousMaxSpeed = ME.MaxSpeed;
            ME.MaxSpeed = 0;

            timer = new Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += Eat;
            timer.Enabled = true;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            if (ME.Hunger >= 3f)
                Terminate();

            return GoalStatus;
        }

        public void Terminate()
        {
            timer.Stop();
            ME.MaxSpeed = previousMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        private void Eat(Object source, ElapsedEventArgs e) => ME.Hunger += 1f;
    }
}
