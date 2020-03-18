using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    class EdgeIterator
    {
        public EdgeList.iterator curEdge;

        Graph<Node, Edge> graph;

        int NodeIndex;

        EdgeIterator(Graph<Node, Edge> g, int node)
        {
            graph = g;
            NodeIndex = node;

            curEdge = g.EdgeList[NodeIndex].begin();
        }

        public EdgeIterator begin()
        {
            curEdge = graph.EdgeList[NodeIndex].begin();

            return curEdge;
        }

        public EdgeIterator next()
        {
            ++curEdge;

            if (end()) return null;

            return curEdge;
        }

        public bool end()
        { 
            return (curEdge == graph.EdgeList[NodeIndex].end());
        }


    }
}
