using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour
{
    abstract public class Goal
    {
        public Survivor ME { get; set; }

        public List<Goal> SubGoals;

        public bool GoalCompleted = false;

        public Goal() { }

        public Goal(Survivor s) 
        {
            ME = s;
            SubGoals = new List<Goal>();
        }

        public abstract void Enter();

        public abstract bool Execute();

        public abstract void Exit();

        public virtual void AddSubGoal(Goal goal)
        {
            SubGoals.Add(goal);
        }
    }
}
