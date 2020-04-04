using Microsoft.Xna.Framework;
using MonoGame.Entity;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class FlockingBehaviour : SteeringBehaviour
    {
        private float maxSteeringForce;
        private float separationAmount;
        private float cohesionAmount;
        private float alignmentAmount;
        private float wanderAmount;


        public FlockingBehaviour(MovingEntity me) : base(me)
        {
            maxSteeringForce = 20.0F;
            separationAmount = 4.0F;
            cohesionAmount = 5.0F;
            alignmentAmount = 10.0F;
            wanderAmount = 20.0F;
        }

        public override Vector2 Calculate()
        {
            Vector2 steeringForce = new Vector2(0, 0);

            ME.em.TagNeighbours(ME, 100);
            ME.em.EnforceNonPenetrationConstraint(ME);
            List<MovingEntity> entities = ME.em.GetMovingEntities();

            steeringForce += Vector2.Multiply(Cohesion(ME, entities), cohesionAmount);
            steeringForce += Vector2.Multiply(Wander(ME, 30, 10, 5), wanderAmount);
            steeringForce += Vector2.Multiply(Alignment(ME, entities), alignmentAmount);
            steeringForce += Vector2.Multiply(Separation(ME, entities), separationAmount);

            return steeringForce.Truncate(maxSteeringForce);
        }

        private Vector2 Cohesion(MovingEntity me, List<MovingEntity> neighbours)
        {
            Vector2 centerOfMass = new Vector2(0, 0);
            Vector2 steeringForce = new Vector2(0, 0);
            int neighbourCount = 0;

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                centerOfMass += neighbour.Pos;
                    neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                centerOfMass /= neighbourCount;
                steeringForce = Seek(me, centerOfMass);
            }

            return steeringForce;
        }

        private Vector2 Alignment(MovingEntity entity, List<MovingEntity> neighbours)
        {
            Vector2 averageHeading = new Vector2(0, 0);
            int neighbourCount = 0;

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                averageHeading += neighbour.Heading;
                neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                // Average heading vectors
                averageHeading /= neighbourCount;
                averageHeading -= entity.Heading;
            }

            return averageHeading;
        }

        private Vector2 Separation(MovingEntity entity, List<MovingEntity> neighbours)
        {
            Vector2 steeringForce = new Vector2(0, 0);

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false || entity.Pos == neighbour.Pos)
                    continue;

                Vector2 ToAgent = Vector2.Subtract(entity.Pos, neighbour.Pos);

                //scale the force inversely proportional to the agent's distance from its neighbor
                steeringForce += Vector2.Divide(Vector2.Normalize(ToAgent), ToAgent.Length());
            }

            return steeringForce;
        }

        private Vector2 Wander(MovingEntity entity, float wanderRadius, float wanderDistance, float wanderJitter)
        {
            Random random = new Random();
            Vector2 wanderTarget = new Vector2(RandomDirection(random) * wanderJitter, RandomDirection(random) * wanderJitter);

            // Reproject this new vector back onto a unit circle.
            wanderTarget.Normalize();

            // Increase the length of the vector to the same as the radius of the wander circle.
            wanderTarget = Vector2.Multiply(wanderTarget, wanderRadius);

            // Add the wanderdistance 
            wanderTarget.X += wanderDistance;

            // rotation matrix from localheading to world
            Matrix2D matrix2D = new Matrix2D();
            matrix2D.M11 = entity.Heading.X;
            matrix2D.M12 = entity.Heading.Y;
            matrix2D.M21 = entity.Side.X;
            matrix2D.M22 = entity.Side.Y;

            // translate Matrix relative to local pos
            Matrix2D translateMatrix = new Matrix2D();
            translateMatrix.M11 = 1;
            translateMatrix.M22 = 1;
            translateMatrix.M31 = entity.Pos.X;
            translateMatrix.M32 = entity.Pos.Y;

            Vector2 targetWorld = translateMatrix.Transform(matrix2D.Transform(wanderTarget));

            return Vector2.Subtract(targetWorld, entity.Pos);
        }
        private Vector2 Seek(MovingEntity entity, Vector2 target)
        {
            Vector2 toTarget = Vector2.Subtract(target, entity.Pos);
            Vector2 desiredVelocity = Vector2.Multiply(Vector2.Normalize(toTarget), entity.MaxSpeed);
            return Vector2.Subtract(desiredVelocity, entity.Velocity);
        }

        private static float RandomDirection(Random random)
        {
            float next = (float)random.NextDouble();
            return -1 + (next * 2);
        }
    }
}
