using Microsoft.Xna.Framework;
using MonoGame.DecisionMaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour
{
    abstract class CompositeGoal : Goal
    {
        public MovingEntity ME { get; set; }
        public GoalStatus GoalStatus { get; set; }

        public List<Goal> SubGoals { get; set; }

        public CompositeGoal(MovingEntity me)
        {
            ME = me;
            SubGoals = new List<Goal>();
        }

        public abstract void Activate();

        public abstract GoalStatus Process();

        public abstract void Terminate();

        public virtual void AddSubGoal(Goal goal)
        {
            SubGoals.Add(goal);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            SubGoals.ForEach(sg => stringBuilder.AppendLine(" " + sg.ToString()));
            return base.ToString();
        }
    }
}
