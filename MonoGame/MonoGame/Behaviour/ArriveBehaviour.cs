using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class ArriveBehaviour : SteeringBehaviour
    {
        Decelaration deceleration;

        public ArriveBehaviour(MovingEntity me, Decelaration deceleration) : base(me)
        {
            this.deceleration = deceleration;
        }
        public override Vector2 Calculate()
        {
            Vector2 targetpos = ME.MyWorld.Target.Pos;

            if (targetpos == null)
                return new Vector2(0, 0);

            Vector2 toTarget = Vector2.Subtract(targetpos, ME.Pos);
            float distance = toTarget.Length();

            if (distance <= 0)
            {
                return new Vector2(0, 0);
            }

            const float decelerationTweaker = 0.3f;

            float speed = distance / ((float)deceleration * decelerationTweaker);
            speed = Math.Min(speed, ME.MaxSpeed);
            Vector2 desiredVelocity = Vector2.Multiply(toTarget, speed / distance);

            return Vector2.Subtract(desiredVelocity, ME.Velocity);
        }
    }
}
