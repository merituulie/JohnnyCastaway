namespace MonoGame
{
    class RightShoulder : FuzzySet
    {
        //the values that define the shape of this FLV
        private double peakPoint;
        private double leftOffset;
        private double rightOffset;
        private double midPoint;

        public RightShoulder(double peak, double left, double right) : base(peak + (right / 2))
        {
            peakPoint = peak;
            leftOffset = left;
            rightOffset = right;
            midPoint = peak + (right / 2);
        }

        //calculates the degree of membership for a particular value
        public override double CalculateDOM(double value)
        {
            if (0 == leftOffset && value == midPoint)
                return 1.0;

            //find DOM if left of center
            if (value <= midPoint && value > (midPoint - leftOffset))
            {
                double grad = 1.0 / leftOffset;
                return grad * (value - (midPoint - leftOffset));
            }

            //find DOM if right of center
            if (value > midPoint)
                return 1.0;

            //out of range of this FLV, return zero
            return 0.0;
        }
    }
}
