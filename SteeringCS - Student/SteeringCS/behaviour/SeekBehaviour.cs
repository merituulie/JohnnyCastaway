using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {

        }
        public override Vector2D Calculate()
        {
            Vector2D targetpos = ME.MyWorld.Target.Pos;
            if (targetpos == null)
                return new Vector2D(0, 0);

            Vector2D desiredVelocity = targetpos.Clone();
            desiredVelocity.Sub(ME.Pos);
            desiredVelocity.Normalize();
            desiredVelocity.Multiply(ME.MaxSpeed);
            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
