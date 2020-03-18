using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    class NavEdge : Edge
    {
        /*
        public Enum{
        normal = 0,
        swim = 1 << 0,
            ...
        }; */

        int flagCounter;

        int IDofIntersectingObject;

        public NavEdge(int newFrom, int newTo, double newCost, int flags = 0, int newIDofInteresctingObject)
        {
            from = newFrom;
            to = newTo;
            cost = newCost;
            flagCounter = flags;
            IDofIntersectingObject = newIDofInteresctingObject;
        }

        public NavEdge(StreamReader ir)
        {
            from = int.Parse(ir.ReadLine());
            to = int.Parse(ir.ReadLine());
            cost = double.Parse(ir.ReadLine());
            flagCounter = int.Parse(ir.ReadLine());
            IDofIntersectingObject = int.Parse(ir.ReadLine());
        }

        public int Flags()
        {
            return flagCounter;
        }

        public void SetFlags(int flags)
        {
            flagCounter = flags;
        }

        public int IDofIntersectingEntity()
        {
            return IDofIntersectingObject;
        }

        public void SetIDofIntersectingEntity(int id)
        {
            IDofIntersectingObject = id;
        }

        public static StreamWriter operator <<(StreamWriter ow, NavEdge ne)
        {
            ow.WriteLine(ne.from);
            ow.WriteLine(ne.to);
            ow.WriteLine(ne.cost);
            ow.WriteLine(ne.flagCounter);
            ow.WriteLine(ne.IDofIntersectingObject);

            return ow;
        }
    }
}
