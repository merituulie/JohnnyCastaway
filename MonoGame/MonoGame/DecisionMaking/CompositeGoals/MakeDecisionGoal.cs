using Microsoft.Xna.Framework;
using MonoGame.Behaviour.GoalBasedBehaviour;
using MonoGame.DecisionMaking;
using MonoGame.DecisionMaking.CompositeGoals;
using MonoGame.Entity;
using System.Linq;

namespace MonoGame.GoalBehaviour.CompositeGoals
{
    class MakeDecisionGoal : CompositeGoal
    {
        private Vector2 Target;
        private FuzzyModule fuzzyModule;

        public MakeDecisionGoal(AwareEntity me) : base(me) 
        {
            fuzzyModule = new FuzzyModule();

            FuzzyVariable hunger = fuzzyModule.CreateFLV("Hunger");
            FzSet hungry = hunger.AddLeftShoulder("Hungry", 0.0, 0.1, 0.3);
            FzSet content = hunger.AddTriangle("Content", 0.1, 0.5, 0.8);
            FzSet full = hunger.AddRightShoulder("Full", 0.5, 0.8, 1.0);

            FuzzyVariable fatique = fuzzyModule.CreateFLV("Fatique");
            FzSet sleepy = fatique.AddLeftShoulder("Sleepy", 0.0, 0.1, 0.3);
            FzSet awake = fatique.AddTriangle("Awake", 0.1, 0.3, 0.5);
            FzSet alert = fatique.AddRightShoulder("Alert", 0.3, 0.5, 1.0);

            FuzzyVariable desirability = fuzzyModule.CreateFLV("Desirability");
            FzSet unDesirable = desirability.AddLeftShoulder("Undesirable", 0, 0.25, 0.5);
            FzSet desirable = desirability.AddTriangle("Desirable", 0.25, 0.5, 0.75);
            FzSet veryDesirable = desirability.AddRightShoulder("VeryDesirable", 0.5, 0.75, 1.0);

            fuzzyModule.AddRule(new FzAND(hungry, sleepy), unDesirable);
            fuzzyModule.AddRule(new FzAND(hungry, awake), desirable);
            fuzzyModule.AddRule(new FzAND(hungry, alert), veryDesirable);
            fuzzyModule.AddRule(new FzAND(content, sleepy), unDesirable);
            fuzzyModule.AddRule(new FzAND(content, awake), desirable);
            fuzzyModule.AddRule(new FzAND(content, alert), veryDesirable);
            fuzzyModule.AddRule(new FzAND(full, sleepy), unDesirable);
            fuzzyModule.AddRule(new FzAND(full, awake), desirable);
            fuzzyModule.AddRule(new FzAND(full, alert), veryDesirable);
        }

        public double GetDesirability(float hunger, float fatique)
        {
            fuzzyModule.Fuzzify("Hunger", hunger);
            fuzzyModule.Fuzzify("Fatique", fatique);

            return fuzzyModule.DeFuzzify("Desirability");
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive)
                Activate();

            // Remove subgoals
            SubGoals.RemoveAll(sg => sg.GoalStatus == GoalStatus.Completed ||
            sg.GoalStatus == GoalStatus.Failed);

            // Calculate, if there is need to attend to fatique or hunger
            if (!SubGoals.OfType<DecideBetweenNeedsGoal>().Any() && GetDesirability(ME.Hunger, ME.Fatique) < 0.5)
                AddSubGoal(new DecideBetweenNeedsGoal(ME));

            // If the target changes and is not used by other goals, get a new path
            if (Target != Game1.Instance.Target && !SubGoals.OfType<DecideBetweenNeedsGoal>().Any())
            {
                AddSubGoal(new FollowPathGoal(ME));
                Target = Game1.Instance.Target;
            }

            // Process through all subgoals
            SubGoals.ForEach(sg => sg.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
        }

        public override string ToString()
        {
            if (base.ToString().Equals(""))
            {
                string fatiqueAsString = ME.Fatique.ToString("0.00");
                string hungerAsString = ME.Hunger.ToString("0.00");
                return "Making decisions... " + "\nHunger: " + hungerAsString + "\nFatique: " + fatiqueAsString + base.ToString();
            }
            else
                return base.ToString();
        }
    }
}
