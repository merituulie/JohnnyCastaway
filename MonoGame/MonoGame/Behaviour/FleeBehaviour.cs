using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity me) : base(me)
        {

        }
        public override Vector2D Calculate()
        {
            Vector2D targetpos = ME.MyWorld.Target.Pos;
            if (targetpos == null)
                return new Vector2D(0, 0);

            const double panicDistanceSquared = 100.0 * 100.0;
            if ((Math.Pow(ME.Pos.X - targetpos.X, 2) + Math.Pow(ME.Pos.Y - targetpos.Y, 2)) > panicDistanceSquared)
            {
                return new Vector2D(0, 0);
            }

            Vector2D desiredVelocity = ME.Pos.Clone();
            desiredVelocity.Sub(targetpos);
            desiredVelocity.Normalize();
            desiredVelocity.Multiply(ME.MaxSpeed);
            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
