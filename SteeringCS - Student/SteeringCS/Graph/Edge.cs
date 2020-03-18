using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SteeringCS.Graph
{
    class Edge
    {
        public int FROM { get; set; }
        public int TO { get; set; }

        int invalid_node_index = -1;

        public double COST { get; set; }

        public Edge(int newfrom, int newto, double newcost)
        {
            COST = newcost;
            FROM = newfrom;
            TO = newto;
        }

        public Edge(int newfrom, int newto)
        {
            COST = 1.0;
            FROM= newfrom;
            TO = newto;
        }

        public Edge()
        {
            COST = 1.0;
            FROM = invalid_node_index;
            TO = invalid_node_index;
        }

        public Edge(StreamReader ir)
        {
            string buffer = "";
            buffer = ir.ReadLine();
            FROM = int.Parse(buffer);
            buffer = ir.ReadLine();
            TO = int.Parse(buffer);
            buffer = ir.ReadLine();
            COST = int.Parse(buffer);
        }

        public int From()
        {
            return FROM;
        }

        public void SetFrom(int newIdx)
        {
            FROM = newIdx;
        }

        public int To()
        {
            return TO;
        }

        public void SetTo(int newIdx)
        {
            TO = newIdx;
        }

        public double Cost()
        {
            return COST;
        }

        public void SetCost(double newCost)
        {
            COST = newCost;
        }
        public static bool operator != (Edge lhs, Edge rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator == (Edge lhs, Edge rhs)
        {
           if (lhs.FROM == rhs.FROM && lhs.TO == rhs.TO && lhs.COST == rhs.COST)
                return true;
            else
                return false;
        }
        }
    }