using Microsoft.Xna.Framework;
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

        public void Update(GameTime gametime)
        {
            if (GlobalGoal != null)
                GlobalGoal.Execute(gametime);
            if (CurrentGoal != null)
                CurrentGoal.Execute(gametime);
        }

        public void ChangeGoal(Goal newGoal, GameTime gametime)
        {
            if (newGoal == null)
                return;

            if (PreviousGoal != null)
            {
                PreviousGoal = CurrentGoal;
                CurrentGoal.Execute(gametime);
            }
            
            CurrentGoal = newGoal;
            CurrentGoal.Enter();
        }

        public void RevertToPreviousGoal(GameTime gameTime)
        {
            ChangeGoal(PreviousGoal, gameTime);
        }
    }
}
