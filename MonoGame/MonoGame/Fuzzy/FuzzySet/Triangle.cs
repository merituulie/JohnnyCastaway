using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    class Triangle : FuzzySet
    {
        //the values that define the shape of this FLV
        private double peakPoint;
        private double leftOffset;
        private double rightOffset;

        public Triangle(double mid, double left, double right) : base(mid)
        {
            peakPoint = mid;
            leftOffset = left;
            rightOffset = right;
        }

        //calculates the degree of membership for a particular value
        public override double CalculateDOM(double value)
        {
            if ((rightOffset == 0.0 && peakPoint == value) || (leftOffset == 0.0 && peakPoint == value))
                return 1.0;

            //find DOM if left of center
            if (value <= peakPoint && value >= (peakPoint - leftOffset))
            {
                double grad = 1.0 / leftOffset;
                return grad * (value - (peakPoint - leftOffset));
            }

            //find DOM if right of center
            if (value > peakPoint && value < (peakPoint + rightOffset))
            {
                double grad = 1.0 / -rightOffset;
                return grad * (value - peakPoint) + 1.0;
            }

            //out of range of this FLV, return zero
            return 0.0;
        }
    }
}
