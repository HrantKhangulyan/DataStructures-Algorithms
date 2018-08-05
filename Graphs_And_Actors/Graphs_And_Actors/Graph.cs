using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_And_Actors
{
    class Graph<T>
    {
        public List<Node<T>> VertexList;
        private List<WeightedEdge<T>> WeightedEdges;

        public Graph()
        {
            VertexList = new List<Node<T>>();
            WeightedEdges = new List<WeightedEdge<T>>();
        }

        private static bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        public Node<T> GetNode(T val)
        {
            foreach (var item in this.VertexList)
            {
                if (Compare(item.value, val)) return item;
            }
            throw new Exception("No Node Found :( ");
        }

        public void ShowVertexValues()
        {
            if (VertexList.Count == 0)
            {
                Console.WriteLine("No elenemts");
                return;
            }

            foreach (var item in VertexList)
            {
                Console.Write(item.value + " , ");
            }
            Console.WriteLine();
        }

        public void AddVertex(T vertex)
        {
            Node<T> node = new Node<T>();
            node.value = vertex;
            VertexList.Add(node);
        }

        public void RemoveVertex(T vertex)
        {
            int RemovedItems = 0;
            foreach (var item in VertexList.ToList()) // ??????????????????????????????????????????
            {
                if (Compare(item.value, vertex))
                {
                    foreach (var i in VertexList)
                    {
                        if (i.NeighbourNodes.Contains(item)) i.NeighbourNodes.Remove(item);
                    }
                    VertexList.Remove(item);
                    RemovedItems++;
                    break;
                }
            }
            if (RemovedItems == 0)
            {
                throw new Exception("No items found :( ");
            }
        }

        public bool DoesNodeExist(T val)
        {
            int FoundElements = 0;
            foreach (var item in VertexList)
            {
                if (Compare(item.value, val))
                {
                    FoundElements++;
                }
            }
            return FoundElements >= 1;
        }

        public bool AreConnected(T val1, T val2)  //determines if there is 2-way connection between nodes
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            return GetNode(val1).NeighbourNodes.Contains(GetNode(val2)) && GetNode(val2).NeighbourNodes.Contains(GetNode(val1));
        }

        public bool EdgeExists(T val1, T val2)  //determines if val1 -> val2 exists
        {
            Node<T> node1 = GetNode(val1);
            Node<T> node2 = GetNode(val2);
            return node1.NeighbourNodes.Contains(node2);
        }

        public void MakeConnected(T val1, T val2) //creates 2-way connection between nodes
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (this.AreConnected(val1, val2)) return;
            Node<T> n1 = GetNode(val1);
            Node<T> n2 = GetNode(val2);
            n1.NeighbourNodes.Add(n2);
            n2.NeighbourNodes.Add(n1);
        }

        public void MakeConnected(T val1, T val2, int weight) //created 2-way weighted connection
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (this.AreConnected(val1, val2)) return;
            Node<T> n1 = GetNode(val1);
            Node<T> n2 = GetNode(val2);
            n1.NeighbourNodes.Add(n2);
            n2.NeighbourNodes.Add(n1);
            WeightedEdge<T> e1 = new WeightedEdge<T>(n1, n2, weight);
            WeightedEdge<T> e2 = new WeightedEdge<T>(n2, n1, weight);
            WeightedEdges.Add(e1);
            WeightedEdges.Add(e2);
        }

        public int DistanceBetween(T val1, T val2)
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (!this.EdgeExists(val1, val2) && !this.EdgeExists(val2, val1)) throw new Exception("Nodes are not connected");

            Node<T> n1 = GetNode(val1);
            Node<T> n2 = GetNode(val2);
            foreach (var item in WeightedEdges)
            {
                if (item.start == n1 && item.end == n2)
                {
                    return item.weight;
                }
            }
            return -1;
        }

        public void CreateDirectionalConnection(T val1, T val2) // val1 -> val2
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (this.EdgeExists(val1, val2)) return;
            Node<T> node1 = GetNode(val1);
            Node<T> node2 = GetNode(val2);
            node1.NeighbourNodes.Add(node2);
        }

        public void CreateDirectionalConnectionMulty(T val1, T val2) // val1 -> val2 (several edges if necessary)
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            Node<T> node1 = GetNode(val1);
            Node<T> node2 = GetNode(val2);
            node1.NeighbourNodes.Add(node2);
        }

        public void CreateDirectionalConnection(T val1, T val2, int weight) // val1 -> val2 , with weighted edge
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (this.EdgeExists(val1, val2)) return;
            Node<T> node1 = GetNode(val1);
            Node<T> node2 = GetNode(val2);
            node1.NeighbourNodes.Add(node2);
            WeightedEdge<T> edge = new WeightedEdge<T>(node1, node2, weight);
            WeightedEdges.Add(edge);
        }

        public void RemoveAllConnections(T val1, T val2) //removes all connections between val1 and val2
        {
            if (!this.DoesNodeExist(val1) || !this.DoesNodeExist(val2)) throw new Exception("No elemets found :( ");
            if (!this.AreConnected(val1, val2)) throw new Exception("Nodes are not connected !");
            GetNode(val1).NeighbourNodes.Remove(GetNode(val2));
            GetNode(val2).NeighbourNodes.Remove(GetNode(val1));
        }

        public void RemoveDirectionalEdge(T val1, T val2) //removes val1 -> val2 edge
        {
            if (!EdgeExists(val1, val2)) throw new Exception("There is no such edge :(");
            if (!DoesNodeExist(val1) || !DoesNodeExist(val2)) throw new Exception("Nodes does not exist");
            Node<T> node1 = GetNode(val1);
            Node<T> node2 = GetNode(val2);
            node1.NeighbourNodes.Remove(node2);
        }

        public void AddMovieForActor(T actname, string moviename) //for OMDB
        {
            if (!DoesNodeExist(actname)) throw new Exception("Actor not found");
            foreach (var item in VertexList)
            {
                if (Compare(item.value, actname))
                {
                    item.PlayedMovieList.Add(moviename);
                    break;
                }
            }
        }

    }
}