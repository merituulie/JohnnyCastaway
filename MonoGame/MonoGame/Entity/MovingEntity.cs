using Microsoft.Xna.Framework;
using MonoGame.Entity;
using MonoGame.Extended;
using System;

namespace MonoGame
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Heading { get; set; }
        public Vector2 Side { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public bool Tag { get; set; }
        public SteeringBehaviour SB { get; set; }

        public Vector2 origin;

        public float angle = 0;

        public Rectangle sourceRectangle;

        public MovingEntity(Vector2 pos, EntityManager em) : base(pos, em)
        {
            Mass = 10;
            MaxSpeed = 500;
            Velocity = new Vector2(0f, 0f);
        }

        public override void Update(float timeElapsed)
        {
            Vector2 SteeringForce = SB.Calculate();

            Vector2 acceleration = Vector2.Divide(SteeringForce, Mass);

            Velocity += Vector2.Multiply(acceleration, timeElapsed);

            Velocity = Velocity.Truncate(MaxSpeed);

            Pos += Vector2.Multiply(Velocity, timeElapsed);
            Pos = Game1.Instance.WrapAround(Pos);

            if (Velocity.LengthSquared() > 0.0000001)
            {
                Heading = Velocity.NormalizedCopy();
                Side = Heading.PerpendicularClockwise();
            }

            angle = (float)(Math.Atan2(Heading.Y, Heading.X));
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
