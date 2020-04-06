using Microsoft.Xna.Framework;
using MonoGame.Graph;
using System;
using System.Collections.Generic;
using MonoGame.Entity;
using System.Linq;
using MonoGame.GoalBehaviour;
using MonoGame.GoalBehaviour.GoalBehaviours;
using MonoGame.DecisionMaking;
using MonoGame.DecisionMaking.CompositeGoals;

namespace MonoGame.Behaviour.GoalBasedBehaviour
{
    class FollowPathGoal : CompositeGoal
    {
        private LinkedList<Vector2> PathToFollow;
        private Vector2 Target;
        private bool PathFindingAsSubGoal;

        public FollowPathGoal(AwareEntity me) : base(me)
        {
            GoalStatus = GoalStatus.Inactive;
            PathToFollow = new LinkedList<Vector2>();
            Target = Game1.Instance.Target;
            PathFindingAsSubGoal = false;
        }

        // Overload of method to use if the pathfollowing is needed by another goal
        public FollowPathGoal(AwareEntity me, Vector2 target) : base(me)
        {
            GoalStatus = GoalStatus.Inactive;
            PathToFollow = new LinkedList<Vector2>();
            Target = target;
            PathFindingAsSubGoal = true;
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;

            LinkedList<Node> newPathToFollow = Game1.Instance.navGraph.AStar(ME.Pos, Target);

            foreach (Node node in newPathToFollow)
                PathToFollow.AddFirst(node.coordinate);
        }

        public override GoalStatus Process()
        {

            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            // Condition to complete the goal
            if (PathToFollow.Count == 0)
                Terminate();


            if (!SubGoals.OfType<DecideBetweenNeedsGoal>().Any())
            {
                // If target changes during pathfinding and pathfinding is not used by another goal, return from this goal
                if (Target != Game1.Instance.Target && !PathFindingAsSubGoal)
                {
                    Console.WriteLine("Pathfinding failed, calculating new path");
                    GoalStatus = GoalStatus.Failed;
                }
            }
            

            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed)
                return GoalStatus;

            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed);

            // Add traversing each node as subgoal to process them later
            if (PathToFollow.Count != 0 && !SubGoals.OfType<TraverseNodeGoal>().Any())
            {
                AddSubGoal(new TraverseNodeGoal(ME, PathToFollow.First()));
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
            return "\nFollow path " + base.ToString();
        }
    }
}
