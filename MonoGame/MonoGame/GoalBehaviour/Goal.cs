using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour
{
    abstract public class Goal
    {
        public MovingEntity ME { get; set; }

        public List<Goal> SubGoals;

        public Goal() { }

        public Goal(MovingEntity em) 
        {
            ME = em;
            SubGoals = new List<Goal>();
        }

        public abstract void Enter();

        public abstract void Execute();

        public abstract void Exit();

        public virtual void AddSubGoal(Goal goal)
        {
            SubGoals.Add(goal);
        }
    }
}
