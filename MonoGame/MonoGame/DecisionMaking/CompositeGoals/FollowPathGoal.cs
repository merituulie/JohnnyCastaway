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
using MonoGame.DecisionMaking;

namespace MonoGame.Behaviour.GoalBasedBehaviour
{
    class FollowPathGoal : CompositeGoal
    {
        private LinkedList<Vector2> PathToFollow;
        private Vector2 Target;

        public FollowPathGoal(MovingEntity me) : base(me)
        {
            GoalStatus = GoalStatus.Inactive;
            PathToFollow = new LinkedList<Vector2>();
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;

            Target = Game1.Instance.Target;

            LinkedList<Node> newPathToFollow = Game1.Instance.navGraph.AStar(ME.Pos, Target);

            foreach (Node node in newPathToFollow)
                PathToFollow.AddFirst(node.coordinate);
        }

        public override GoalStatus Process()
        {

            if (GoalStatus == GoalStatus.Inactive)
                Activate();


            if (PathToFollow.Count == 0)
                Terminate();

            if (Target != Game1.Instance.Target)
            {
                Console.WriteLine("Pathfinding failed");
                GoalStatus = GoalStatus.Failed;
            }

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed);

            if (PathToFollow.Count != 0 && !SubGoals.OfType<TraverseNodeGoal>().Any())
            {
                AddSubGoal(new TraverseNodeGoal(ME, PathToFollow.First(), PathToFollow.Count));
                Console.WriteLine("Paths current first node: " + PathToFollow.First().ToString() + ", Number of nodes left on the list: " + PathToFollow.Count);
                PathToFollow.RemoveFirst();
            }

            SubGoals.ForEach(sg => sg.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "\nFollow path, target: " + Target + " " + base.ToString();
        }
    }
}
