namespace MonoGame
{
    abstract class FuzzyTerm
    {
        public abstract FuzzyTerm Clone();

        public abstract double GetDOM();

        public abstract void ClearDOM();

        public abstract void ORwithDOM(double value);
    }
}
