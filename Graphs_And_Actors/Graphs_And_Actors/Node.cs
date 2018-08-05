using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_And_Actors
{
    class Node<T>
    {
        public T value; //yndhanur depqeri hamar
        public bool IsMarked { get; set; } //for BFS and DFS
        public bool HasIndex { get; set; } //Dijkstra
        public int Index { get; set; }    //Dijkstra
        public List<string> PlayedMovieList; //for OMDB
        public List<Node<T>> NeighbourNodes; //child Nodes

        public Node()
        {
            NeighbourNodes = new List<Node<T>>();
            PlayedMovieList = new List<string>();
        }
    }
}
