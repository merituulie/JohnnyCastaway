using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.GoalBehaviour
{
    public class GoalManager
    {
        private MovingEntity em;
        private Goal CurrentGoal;
        private Goal PreviousGoal;
        private Goal GlobalGoal;

        public GoalManager(MovingEntity entity)
        {
            em = entity;
            CurrentGoal = null;
            PreviousGoal = null;
            GlobalGoal = null;
        }

        public void SetCurrentGoal(Goal currentgoal)
        {
            CurrentGoal = currentgoal;
        }

        public void SetPreviousGoal(Goal previousgoal)
        {
            PreviousGoal = previousgoal;
        }

        public void SetGlobalGoal(Goal globalgoal)
        {
            GlobalGoal = globalgoal;
        }

        public Goal GetCurrentGoal()
        {
            return CurrentGoal;
        }
        public Goal GetPreviousGoal()
        {
            return PreviousGoal;
        }
        public Goal GetGlobalGoal()
        {
            return GlobalGoal;
        }

        public void Update()
        {
            if (GlobalGoal != null)
                GlobalGoal.Execute();
            if (CurrentGoal != null)
                CurrentGoal.Execute();
        }

        public void ChangeGoal(Goal newGoal)
        {
            if (newGoal == null)
                return;

            PreviousGoal = CurrentGoal;
            CurrentGoal.Execute();

            CurrentGoal = newGoal;
            CurrentGoal.Enter();
        }

        public void RevertToPreviousGoal()
        {
            ChangeGoal(PreviousGoal);
        }
    }
}
