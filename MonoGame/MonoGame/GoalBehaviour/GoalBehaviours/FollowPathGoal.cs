using Microsoft.Xna.Framework;
using MonoGame.Graph;
using System;
using System.Collections.Generic;
using MonoGame.Entity;
using MonoGame.Behaviour;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.GoalBehaviour;

namespace MonoGame.Behaviour.GoalBasedBehaviour
{
    class FollowPathGoal : Goal
    {
        private LinkedList<Vector2> PathToFollow;
        private Vector2 Target;

        public static FollowPathGoal instance = new FollowPathGoal();

        public FollowPathGoal() { }

        public static FollowPathGoal Instance => instance;

        public FollowPathGoal(MovingEntity me) : base (me)
        {
            PathToFollow = new LinkedList<Vector2>();
        }

        public override void Enter()
        {
            Target = Game1.Instance.Target; 

            LinkedList<Node> newPathToFollow = Game1.Instance.navGraph.AStar(ME.Pos, Target);

            foreach (Node node in newPathToFollow)
                PathToFollow.AddFirst(node.coordinate);

            Execute();
        }

        public override void Execute()
        {
            if (PathToFollow.Count == 0)
                Exit();

            if (Target != Game1.Instance.Target)
            {
                Console.WriteLine("Pathfinding failed");
                Exit();
            }

            TraverseNodes(PathToFollow.First(), PathToFollow.Count);
        }

        public override void Exit()
        {
        }

        private void TraverseNodes(Vector2 targetNode, int PathCount)
        {
            int nodeCount = PathCount;

            if (Vector2.Subtract(ME.SB.Target, ME.Pos).Length() < 32)
                Exit();

            switch (nodeCount)
            {
                case 5: case 4:
                    ME.SB = new ArriveBehaviour(ME, targetNode, SteeringBehaviour.Decelaration.Fast);
                    break;
                case 3:
                    ME.SB = new ArriveBehaviour(ME, targetNode, SteeringBehaviour.Decelaration.Normal);
                    break;
                case 2: case 1:
                    ME.SB = new ArriveBehaviour(ME, targetNode, SteeringBehaviour.Decelaration.Slow);
                    break;
                default:
                    ME.SB = new SeekBehaviour(ME, targetNode);
                    break;
            }
        }
    }
}
