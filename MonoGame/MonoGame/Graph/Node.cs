using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Graph
{
    public class Node
    {
        public Vector2 coordinate;
        public LinkedList<Edge> edges;
        public Node prev;
        public int scratch; // Helper to see, if the node is visited

        public double dist;
        public bool drawable; // Helper to see, if the node is drawable

        public Node(Vector2 coordinate)
        {
            this.edges = new LinkedList<Edge>();
            this.coordinate = coordinate;
            Reset();
        }

        public void Reset()
        {
            dist = Graph.INFINITY;
            prev = null;
            scratch = 0;
            drawable = false;
        }
    }
}
