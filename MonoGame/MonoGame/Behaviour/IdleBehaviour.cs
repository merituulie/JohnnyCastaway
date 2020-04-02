using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Behaviour
{
    class IdleBehaviour : SteeringBehaviour
    {
        private Vector2 desiredVelocity;
        public IdleBehaviour(MovingEntity me) : base(me)
        {
        }
        public override Vector2 Calculate()
        {
                return new Vector2(0, 0);
        }
    }
}
