using System.Collections.Generic;

namespace MonoGame
{
    class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> members;
        private double minRange;
        private double maxRange;

        public FuzzyVariable()
        {
            members = new Dictionary<string, FuzzySet>();
            minRange = 0.0;
            maxRange = 0.0;
        }

        private void AdjustRangeToFit(double min, double max)
        {
            if (min < minRange)
                minRange = min;
            if (max > maxRange)
                maxRange = max;
        }

        public FzSet AddLeftShoulder(string name, double min, double peak, double max)
        {
            members.Add(name, new LeftShoulder(peak, peak - min, peak - max));
            AdjustRangeToFit(min, max);
            if (members.TryGetValue(name, out FuzzySet fuzzySet))
                return new FzSet(fuzzySet);
            return null;
        }

        public FzSet AddRightShoulder(string name, double min, double peak, double max)
        {
            members.Add(name, new RightShoulder(peak, peak - min, peak - max));
            AdjustRangeToFit(min, max);
            if (members.TryGetValue(name, out FuzzySet fuzzySet))
                return new FzSet(fuzzySet);
            return null;
        }

        public FzSet AddTriangle(string name, double min, double peak, double max)
        {
            members.Add(name, new Triangle(peak, peak - min, peak - max));
            AdjustRangeToFit(min, max);
            if (members.TryGetValue(name, out FuzzySet fuzzySet))
                return new FzSet(fuzzySet);
            return null;
        }

        public void Fuzzify(double value)
        {
            foreach (FuzzySet curSet in members.Values)
                curSet.SetDOM(curSet.CalculateDOM(value));
        }

        public double DeFuzzifyMaxAv()
        {
            double top = 0.0;
            double bottom = 0.0;

            foreach (FuzzySet curSet in members.Values)
            {
                top += curSet.GetRePresentativeVal() * curSet.GetDOM();
                bottom += curSet.GetDOM();
            }

            //make sure bottom is not equal to zero
            if (bottom == 0.0)
            {
                return 0.0;
            }

            return top / bottom;
        }
    }
}
