using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return Math.Pow(X, 2) + Math.Pow(Y, 2);
        }

        public Vector2D Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            X *= value;
            Y *= value;
            return this;
        }

        public Vector2D divide(double value)
        {
            X /= value;
            Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            double length = Length();
            X = X / length;
            Y = Y / length;
            return this;
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public Vector2D Perp()
        {
            return new Vector2D(Y,-X);
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }


}
