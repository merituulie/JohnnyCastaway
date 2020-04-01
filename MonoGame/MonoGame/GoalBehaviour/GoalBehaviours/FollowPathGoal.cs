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

        FollowPathGoal(MovingEntity me) : base (me)
        {
            // get an instance of the game -> target and the path nodes to the target
            // set path to this goals path
        }

        public override void Enter()
        { 
        
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }

        private void TraverseNode(Node FirstNode, int PathCount)
        {
            int nodeCount = PathCount;

            if (Vector2.Subtract(ME.SB.Target, ME.Pos).Length() < 32)
                return;

            switch (nodeCount)
            {
                case 5: case 4:
                    ME.SB = new ArriveBehaviour(ME, ME.SB.Target, SteeringBehaviour.Decelaration.Fast);
                    break;
                case 3:
                    ME.SB = new ArriveBehaviour(ME, ME.SB.Target, SteeringBehaviour.Decelaration.Normal);
                    break;
                case 2: case 1:
                    ME.SB = new ArriveBehaviour(ME, ME.SB.Target, SteeringBehaviour.Decelaration.Slow);
                    break;
                default:
                    ME.SB = new SeekBehaviour(ME, ME.SB.Target);
                    break;
            }


        }
    }
}
