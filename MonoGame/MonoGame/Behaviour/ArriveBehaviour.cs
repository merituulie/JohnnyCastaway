using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public ArriveBehaviour(MovingEntity me) : base(me)
        {

        }
        public override Vector2D Calculate()
        {
            Vector2D targetpos = ME.MyWorld.Target.Pos;
            if (targetpos == null)
                return new Vector2D(0, 0);

            Vector2D toTarget = targetpos.Clone().Sub(ME.Pos);
            double distance = toTarget.Length();

            if (distance <= 0)
            {
                return new Vector2D(0, 0);
            }

            double speed = distance / 0.3;
            speed = Math.Min(speed, ME.MaxSpeed);
            Vector2D desiredVelocity = toTarget.Multiply(speed).divide(distance);

            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
