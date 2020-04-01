using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public abstract Vector2 Calculate();

        public Vector2 Target;

        public enum Decelaration { Slow = 3, Normal = 2, Fast = 1 }

        public SteeringBehaviour(MovingEntity me, Vector2 target)
        {
            ME = me;
            Target = target;
        }

        public SteeringBehaviour(MovingEntity me)
        {
            ME = me;
            Target = new Vector2(200,200);
        }
    }
}
