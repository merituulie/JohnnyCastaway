using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    class NavNode : Node
    {
        public Vector2D position;

        public extra_info extra_info;

        NavNode()
        {
            extra_info = Extra_Info();
        }

        NavNode(int idx, Vector2D pos)
        {
            index = idx;
            position = pos;
            extra_info = Extra_Info();
        }

        NavNode(StreamReader ir)
        {
            extra_info = Extra_Info();
            index = int.Parse(ir.ReadLine());
            position.X = int.Parse(ir.ReadLine());
            position.Y = int.Parse(ir.ReadLine());
        }

        public Vector2D Pos()
        {
            return position;
        }

        public void SetPos(Vector2D newPos)
        {
            position = newPos;
        }

        public extra_info Extra_Info()
        {
            return extra_info;
        }

        public void SetExtra_info(extra_info info)
        {
            extra_info = info;
        }

    }
}
