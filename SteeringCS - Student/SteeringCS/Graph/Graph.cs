using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SteeringCS.Graph
{
    public class Graph<Node, Edge>
    {
        private Edge edge;
        private Node node;

        private Vector2D NodeVector;
        public List<Edge> EdgeList;
        private Vector2D EdgeListVector;

        public List<Node> Nodes;
        public List<Edge> Edges;
        public int nextNodeIndex;

        private const string GRAPH_FILE_NAME = "GraphFile.txt";

        Graph()
        {
            nextNodeIndex = 0;
        }
        public bool UniqueEdge(int from, int to)
        {
            for (EdgeList.iterator curEdge = Edges[from].begin(); curEdge != Edges[from].end(); ++curEdge)
            {
                if (curEdge.To() == to)
                {
                    return false;
                }
            }

            return true;
        }
        public void CullInvalidEdges()
        {
            for (EdgeListVector.iterator curEdgeList = Edges.begin(); curEdgeList != Edges.end(); ++curEdgeList)
            {
                for (EdgeList.iterator curEdge = curEdgeList.begin(); curEdge != curEdgeList.end(); ++curEdge)
                {
                    if (Nodes[curEdge.To()].Index() == invalid_node_index || Nodes[curEdge.From()].Index() == invalid_node_index)
                        curEdge = curEdgeList.erase(curEdge);
                }
            }
        }

        public Node GetNode(int idx)
        {
            return Nodes[idx];
        }

        public Edge GetEdge(int from, int to)
        {
            for (EdgeList.iterator curEdge = Edges[from].begin(); curEdge != Edges[from].end(); ++curEdge)
            {
                if (curEdge.To() == to) 
                    return curEdge;
            }
            return null;
        }

        public int GetNextFreeNodeIndex()
        {
            return nextNodeIndex;
        }

        public int AddNode(Node node)
        {
            if (node.Index() <(int) Nodes.size())
            {
                Nodes[node.Index()] = node;
                return nextNodeIndex;
            }
            else 
            {
                Nodes.push_back(node);
                Edges.push_back(EdgeList());

                return nextNodeIndex++;
            }
        }

        public void RemoveNode(int node)
        {
            Nodes[node].SetIndex(invalid_node_index);

            for (EdgeList.iterator curEdge = Edges[node].begin(); curEdge != Edges[curEdge].end(); ++curEdge)
            {
                for (EdgeList.iterator curE = Edges[curEdge.To()].begin(); curE != Edges[curEdge.To()].end(); ++curE)
                {
                    if (curE.To() == node)
                    {
                        curE = Edges[curEdge.To()].erase(curE);
                        break;
                    }
                }
            }

            Edges[node].clear();
        }

        public void AddEdge(Edge edge)
        {
            if ((Nodes[edge.To()].Index() != invalid_node_index) && (Nodes[edge.From()].Index() != invalid_node_index))
            {
                if (UniqueEdge(edge.From(), edge.To()))
                    Edges[edge.From()].push_back(edge);
            }

            if (UniqueEdge(edge.To(), edge.From()))
            {
                Edge newEdge = edge;

                newEdge.SetTo(edge.From());
                newEdge.SetFRom(edge.To());

                Edges[edge.To()].push_back(newEdge);
            }
        }

        public void RemoveEdge(int from, int to)
        {
            EdgeList.iterator curEdge;

            for (curEdge = Edges[to].begin(); curEdge != Edges[to].end(); ++curEdge)
            {
                if (curEdge.To() == from)
                {
                    curEdge = Edges[to].erase(curEdge);
                    break;
                }
            }

            for (curEdge = Edges[from].begin(); curEdge != Edges[from].end(); ++curEdge)
            {
                if (curEdge.To() == to)
                {
                    curEdge = Edges[from].erase(curEdge);
                    break;
                }
            }
        }

        public void SetEdgeCost(int from, int to, double cost)
        {
            for (EdgeList.iterator curEdge = Edges[from].begin(); curEdge != Edges[from].end(); ++curEdge)
            {
                if (curEdge.To() == to)
                {
                    curEdge.SetCost(cost);
                    break;
                }
            }
        }

        public int NumNodes()
        {
            return Nodes.size();
        }

        public int NumActiveNodes()
        {
            int count = 0;

            for (int i = 0; i < Nodes.size(); i++)
            {
                if (Nodes[i].Index() != invalid_node_index)
                    ++count;
            }
            return count;
        }

        public int NumEdges()
        {
            int total = 0;

            for (EdgeListVector.iterator curEdge = Edges.begin(); curEdge != Edges.end(); ++curEdge)
            {
                total += curEdge.size();
            }

            return total;
        }

        public bool isEmpty()
        {
            return Nodes.empty();
        }

        public bool isNodePresent(int node)
        {
            if (node >= (int)Nodes.size() || (Nodes[node].Index() == invalid_node_index))
                return false;
            else 
                return true;
        }

        public bool isEdgePresent(int from, int to)
        {
            if (isNodePresent(from) && isNodePresent(to))
            {
                for (EdgeList.iterator curEdge = Edges[from].begin(); curEdge != Edges[from].end(); ++curEdge)
                {
                    if (curEdge.To() == to)
                        return true;
                }
                return false;
            }
            else 
            {
                return false;
            }
        }

        public bool Save(string filename)
        {
            StreamWriter fsout = new StreamWriter(filename);

            return Save(fsout);
        }

        public bool Save(StreamWriter fsout) {
            fsout.WriteLine(Nodes.size());

            NodeVector.iterator curNode = Nodes.begin();
            for (curNode; curNode != Nodes.end(); ++curNode)
            {
                fsout.WriteLine(curNode);
            }

            fsout.WriteLine(NumEdges() = "\n");

            for (int nodeIdx = 0; nodeIdx < Nodes.begin(); ++nodeIdx)
            {
                for (EdgeList.iterator curEdge = Edges[nodeIdx].begin(); curEdge != Edges[nodeIdx].end(); ++curEdge)
                {
                    fsout.WriteLine(curEdge);
                }
            }

            return true;
        }

        public bool Load(string filename)
        {
            StreamReader fsin = new StreamReader(filename);

            return Load(fsin);
        }

        public bool Load(StreamReader fsin)
        {
            Clear();

            int numNodes, numEdges;

            numNodes = int.Parse(fsin.ReadLine());

            for (int n = 0; n < numNodes; ++n)
            {
                Node NewNode = new Node(fsin); // mita mita

                if (NewNode.Index() != invalid_node_index)
                {
                    AddNode(NewNode);
                }
                else 
                {
                    Nodes.push_back(NewNode);
                    Edges.push_back(EdgeList());

                    ++nextNodeIndex;
                }
            }

            numEdges = int.Parse(fsin.ReadLine());

            for (int e = 0; e < numEdges; ++e)
            {
                Edge newEdge = new Edge(fsin);

                AddEdge(newEdge);
            }
            return true;
        }

        public void Clear()
        {
            nextNodeIndex = 0;
            Nodes.Clear();
            Edges.Clear();
        }

        public void RemoveEdges()
        {
            for (EdgeListVector.iterator i = Edges.begin(); i != Edges.end(); ++i)
                Edges[i].Clear();
        }
    }
}
