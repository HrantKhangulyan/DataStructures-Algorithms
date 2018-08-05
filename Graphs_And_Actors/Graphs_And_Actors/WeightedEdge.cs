using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_And_Actors
{
    class WeightedEdge<T>
    {
        public int weight;
        public Node<T> start;
        public Node<T> end;

        public WeightedEdge(Node<T> s , Node<T> e , int w)
        {
            start = s;
            end = e;
            weight = w;
        }
    }
}
