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
using MonoGame.GoalBehaviour.GoalBehaviours;

namespace MonoGame.Behaviour.GoalBasedBehaviour
{
    class FollowPathGoal : Goal
    {
        private LinkedList<Vector2> PathToFollow;
        private Vector2 Target;

        public static FollowPathGoal instance = new FollowPathGoal();

        public FollowPathGoal() { }

        public static FollowPathGoal Instance => instance;

        public FollowPathGoal(Survivor s) : base (s)
        {
            Target = Game1.Instance.Target;
            PathToFollow = new LinkedList<Vector2>();
        }

        public override void Enter()
        {

            LinkedList<Node> newPathToFollow = Game1.Instance.navGraph.AStar(ME.Pos, Target);

            foreach (Node node in newPathToFollow)
                PathToFollow.AddFirst(node.coordinate);

            GoalCompleted = false;

            //Execute();

            //if (GoalCompleted == true)
            //    return;
            //else
            //    Execute();
        }

        public override bool Execute(GameTime gametime)
        {
            if (GoalCompleted == true)
                Exit();

            if (PathToFollow.Count == 0)
            {
                Exit();
                return GoalCompleted;
            }

            if (Target != Game1.Instance.Target)
            {
                Console.WriteLine("Pathfinding failed");
                Exit();
                return GoalCompleted;
            }

            SubGoals.RemoveAll(sg => sg.GoalCompleted == true);

            if (PathToFollow.Count != 0 && !SubGoals.Any())
            {
                while (PathToFollow.Count != 0)
                {
                    AddSubGoal(new TraverseNodeGoal(ME, PathToFollow.First(), PathToFollow.Count));
                    Console.WriteLine("Paths current first node: " + PathToFollow.First().ToString() + ", Number of nodes left on the list: " + PathToFollow.Count);
                    PathToFollow.RemoveFirst();
                }
            }

            SubGoals.Reverse();
            SubGoals.ForEach(sg => sg.Execute(gametime));

            return GoalCompleted;
        }

        public override void Exit()
        {
            GoalCompleted = true;
        }

        public override string ToString()
        {
            return $"Traverse Vertex {Target}";
        }
    }
}
