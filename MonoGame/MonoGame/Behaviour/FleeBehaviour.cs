using Microsoft.Xna.Framework;
using System;

namespace MonoGame
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity me, Vector2 target) : base(me, target)
        {
        }
        public override Vector2 Calculate()
        {
            Vector2 targetPos = Target;

            if (targetPos == null)
                return new Vector2(0, 0);

            const float panicDistanceSquared = 100f * 100f;

            if ((Math.Pow(ME.Pos.X - targetPos.X, 2) + Math.Pow(ME.Pos.Y - targetPos.Y, 2)) > panicDistanceSquared)
            {
                return new Vector2(0, 0);
            }

            Vector2 desiredVelocity = Vector2.Subtract(ME.Pos, targetPos);
            desiredVelocity.Normalize();
            desiredVelocity = Vector2.Multiply(desiredVelocity, ME.MaxSpeed);
            return Vector2.Subtract(desiredVelocity, ME.Velocity);
        }
    }
}
