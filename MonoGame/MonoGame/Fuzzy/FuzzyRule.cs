namespace MonoGame
{
    class FuzzyRule
    {
        private FuzzyTerm antecedent;
        private FuzzyTerm consequence;

        public FuzzyRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            this.antecedent = antecedent.Clone();
            this.consequence = consequence.Clone();
        }


        public void SetConfidenceOfConsequentToZero()
        {
            consequence.ClearDOM();
        }

        /**
         * this method updates the DOM (the confidence) of the consequent term with
         * the DOM of the antecedent term. 
         */
        public void Calculate()
        {
            consequence.ORwithDOM(antecedent.GetDOM());
        }
    }
}
