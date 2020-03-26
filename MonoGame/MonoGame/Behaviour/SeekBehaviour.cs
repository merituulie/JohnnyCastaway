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
        public SeekBehaviour(MovingEntity me) : base(me)
        {

        }
        public override Vector2 Calculate()
        {
            Vector2 targetpos = ME.MyWorld.Target.Pos;

            if (targetpos == null)
                return new Vector2(0, 0);

            Vector2 desiredVelocity = Vector2.Subtract(targetpos, ME.Pos);
            desiredVelocity.Normalize();
            desiredVelocity = Vector2.Multiply(desiredVelocity, ME.MaxSpeed);
            return desiredVelocity = Vector2.Subtract(desiredVelocity, ME.Velocity);
        }
    }
}
