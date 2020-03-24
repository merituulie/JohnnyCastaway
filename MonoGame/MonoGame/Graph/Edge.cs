using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Graph
{
    public class Edge : IComparable<Edge>
    {
        public Node toNode;
        public double fCost; // Cost of the edge
        public double gCost; // Distance between the start and the current node
        public double hCost; // Heuristic cost (Manhattan distance)

        public Edge(Node destinasionNode, double gCost, double hCost)
        {
            toNode = destinasionNode;
            this.gCost = gCost;
            this.hCost = hCost;
            fCost = gCost + hCost;
        }

        public int CompareTo(Edge rhs)
        {
            double rhsFCost = rhs.fCost;
            double rhshCost = rhs.hCost;

            if (fCost > rhsFCost)
            {
                return 1;
            }
            else if (fCost < rhsFCost)
            {
                return -1;
            }
            else
            {
                if (hCost < rhshCost)
                    return -1;
                else
                    return 0;
            }

        }
    }
}
