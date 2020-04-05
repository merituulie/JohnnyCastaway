namespace MonoGame
{
    abstract class FuzzySet
    {
        private double DOM;
        private double RepresentativeValue;

        public FuzzySet(double representativeValue)
        {
            DOM = 0.0;
            RepresentativeValue = representativeValue;
        }

        //calculates the degree of membership for a particular value
        public abstract double CalculateDOM(double value);

        public virtual void ORwithDOM(double value)
        {
            if (value > DOM)
                DOM = value;
        }

        public virtual double GetRePresentativeVal() => RepresentativeValue;
        public virtual double GetDOM() => DOM;
        public virtual void SetDOM(double value) => DOM = value;
        public virtual void ClearDOM() => DOM = 0.0;
    }
}
