using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class FzAND : FuzzyTerm
    {
        private List<FuzzyTerm> fuzzyTerms = new List<FuzzyTerm>(4);

        private FzAND(FzAND fzAND)
        {
            fuzzyTerms.AddRange(fzAND.fuzzyTerms);
        }

        public FzAND(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2)
        {
            fuzzyTerms.Add(fuzzyTerm1.Clone());
            fuzzyTerms.Add(fuzzyTerm2.Clone());

        }

        public FzAND(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2, FuzzyTerm fuzzyTerm3)
            : this(fuzzyTerm1, fuzzyTerm2)
        {
            fuzzyTerms.Add(fuzzyTerm3.Clone());
        }

        public FzAND(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2, FuzzyTerm fuzzyTerm3, FuzzyTerm fuzzyTerm4)
            : this(fuzzyTerm1, fuzzyTerm2, fuzzyTerm3)
        {
            fuzzyTerms.Add(fuzzyTerm4.Clone());
        }


        public override void ClearDOM() => fuzzyTerms.ForEach(term => term.ClearDOM());

        public override FuzzyTerm Clone() => new FzAND(this);

        public override double GetDOM()
        {
            double smallest = Double.MaxValue;

            foreach (FuzzyTerm fuzzyTerm in fuzzyTerms)
                if (fuzzyTerm.GetDOM() < smallest)
                    smallest = fuzzyTerm.GetDOM();

            return smallest;
        }

        public override void ORwithDOM(double value) => fuzzyTerms.ForEach(term => term.ORwithDOM(value));
    }
}
