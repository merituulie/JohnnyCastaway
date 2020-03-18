using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    public class Node
    {
        public int index;

        public int invalid_node_index = -1;

        public Node()
        {
            index = invalid_node_index;  
        }

        public Node(int idx)
        {
            index = idx;
        }

        public Node(StreamReader sr)
        {
            index = int.Parse(sr.ReadLine());
        }

        public int Index()
        {
            return index;
        }

        public void SetIndex(int newIdx)
        {
            index = newIdx;
        }

    }
}
