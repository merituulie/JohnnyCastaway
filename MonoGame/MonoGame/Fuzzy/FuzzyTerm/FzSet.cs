namespace MonoGame
{
    class FzSet : FuzzyTerm
    {
        public FuzzySet set;

        public FzSet(FuzzySet set)
        {
            this.set = set;
        }

        public override void ClearDOM() => set.ClearDOM();

        public override FuzzyTerm Clone() => new FzSet(set);

        public override double GetDOM() => set.GetDOM();

        public override void ORwithDOM(double value) => set.ORwithDOM(value);
    }
}
