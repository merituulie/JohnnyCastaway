using Microsoft.Xna.Framework;
using MonoGame.DecisionMaking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
