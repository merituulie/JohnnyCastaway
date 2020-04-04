using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class FzOR : FuzzyTerm
    {
        private List<FuzzyTerm> fuzzyTerms = new List<FuzzyTerm>(4);

        private FzOR(FzOR fzAND)
        {
            fuzzyTerms.AddRange(fzAND.fuzzyTerms);
        }

        public FzOR(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2)
        {
            fuzzyTerms.Add(fuzzyTerm1.Clone());
            fuzzyTerms.Add(fuzzyTerm2.Clone());

        }

        public FzOR(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2, FuzzyTerm fuzzyTerm3)
            : this(fuzzyTerm1, fuzzyTerm2)
        {
            fuzzyTerms.Add(fuzzyTerm3.Clone());
        }

        public FzOR(FuzzyTerm fuzzyTerm1, FuzzyTerm fuzzyTerm2, FuzzyTerm fuzzyTerm3, FuzzyTerm fuzzyTerm4)
            : this(fuzzyTerm1, fuzzyTerm2, fuzzyTerm3)
        {
            fuzzyTerms.Add(fuzzyTerm4.Clone());
        }

        public override void ClearDOM()
        {
            throw new NotImplementedException();
        }

        public override FuzzyTerm Clone() => new FzOR(this);

        public override double GetDOM()
        {
            double largest = Double.MinValue;

            foreach (FuzzyTerm fuzzyTerm in fuzzyTerms)
                if (fuzzyTerm.GetDOM() > largest)
                    largest = fuzzyTerm.GetDOM();

            return largest;
        }

        public override void ORwithDOM(double value)
        {
            throw new NotImplementedException();
        }
    }
}
