using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class SeekBehaviour : SteeringBehaviour
    {

        public SeekBehaviour(MovingEntity me, Vector2 target) : base(me, target)
        {
        }
        public override Vector2 Calculate()
        {
            Vector2 targetpos = Target;

            if (targetpos == null)
                return new Vector2(0, 0);

            Vector2 desiredVelocity = Vector2.Subtract(targetpos, ME.Pos);

            if (desiredVelocity.X > 0 || desiredVelocity.Y > 0)
            {
                desiredVelocity.Normalize();
                desiredVelocity = Vector2.Multiply(desiredVelocity, ME.MaxSpeed);
                return desiredVelocity = Vector2.Subtract(desiredVelocity, ME.Velocity);
            }
            else
                return new Vector2(0, 0);
        }
    }
}
