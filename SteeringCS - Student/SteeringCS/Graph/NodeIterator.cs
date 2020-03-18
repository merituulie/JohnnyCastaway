using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    class NodeIterator
    {
        NodeVector curNode;

        Graph<Node, Edge> graph;

        public void GetNextValidNode(NodeVector.iterator i)
        { 
            if (curNode == graph.Nodes.end() || i.Index() != invalid_node_exception)
            {
                while (i.Index() == invalid_node_index)
                {
                    ++i;

                    if (curNode == graph.Nodes.end()) 
                        break;
                }
            }
        }

        public void ConstNodeIterator(Graph<Node, Edge> graph)
        {
            curNode = graph.Nodes.begin();
        }

        public NodeIterator begin()
        {
            curNode = graph.Nodes.begin();

            GetNextValidNode(curNode);

            return curNode;
        }

        public NodeIterator next()
        {
            if (end())
            {
                return null;
            }
            else
            {
                GetNextValidNode(curNode);

                return curNode;
            }
        }

        public bool end()
        {
            return (curNode == graph.Nodes.end());
        }
    }
}
