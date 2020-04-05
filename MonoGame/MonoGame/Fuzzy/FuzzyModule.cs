using System.Collections.Generic;

namespace MonoGame
{
    class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> variables;
        private List<FuzzyRule> rules;

        public FuzzyModule()
        {
            variables = new Dictionary<string, FuzzyVariable>();
            rules = new List<FuzzyRule>();
        }

        // Creates a new "empty" fuzzy variable and returns a reference to it.
        public FuzzyVariable CreateFLV(string name)
        {
            variables.Add(name, new FuzzyVariable());

            if (variables.TryGetValue(name, out FuzzyVariable fuzzyVariable))
                return fuzzyVariable;

            return null;
        }

        // Adds a rule to the module
        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence) => rules.Add(new FuzzyRule(antecedent, consequence));

        // Call the Fuzzify method of the named FLV
        public void Fuzzify(string nameFLV, double value)
        {
            if (variables.TryGetValue(nameFLV, out FuzzyVariable fuzzyVariable))
                fuzzyVariable.Fuzzify(value);
        }

        // Returns a crisp value
        public double DeFuzzify(string key)
        {
            if (variables.TryGetValue(key, out FuzzyVariable fuzzyVariable))
            {
                // Clear the DOMs of all the consequents of all the rules
                //SetConfidencesOfConsequentsToZero();

                foreach (FuzzyRule rule in rules)
                {
                    rule.SetConfidenceOfConsequentToZero();
                    rule.Calculate();
                }

                return fuzzyVariable.DeFuzzifyMaxAv();
            }

            return 0.0;
        }
    }
}
