using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Data.EntityClient;
using Newtonsoft.Json;
using System.IO;

namespace Graphs_And_Actors
{
    class Program
    {
        public static List<T> BFS<T>(Graph<T> graph, Node<T> source)
        {
            List<T> result = new List<T>(); //stugelu hamar
            Queue<Node<T>> myqueue = new Queue<Node<T>>();
            source.IsMarked = true;
            myqueue.Enqueue(source);
            Node<T> x;
            while (myqueue.Count != 0)
            {
                x = myqueue.First();
                result.Add(x.value);
                foreach (var item in x.NeighbourNodes)
                {
                    if (!item.IsMarked)
                    {
                        item.IsMarked = true;
                        myqueue.Enqueue(item);
                    }
                }
                myqueue.Dequeue();
            }
            return result;
        }

        public static List<T> DFS<T>(Graph<T> graph, Node<T> source)
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            List<T> result = new List<T>();
            source.IsMarked = true;
            stack.Push(source);
            result.Add(source.value);
            while (stack.Count != 0)
            {
                Node<T> x = stack.Peek();
                bool IsUnmarkedNodeFound = false;
                foreach (var item in x.NeighbourNodes)
                {
                    if (!item.IsMarked)
                    {
                        item.IsMarked = true;
                        stack.Push(item);
                        result.Add(item.value);
                        IsUnmarkedNodeFound = true;
                        break;
                    }
                }
                if (!IsUnmarkedNodeFound) stack.Pop();
            }

            return result;
        }

        public static bool HasCycle<T>(Graph<T> graph)
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            graph.VertexList[0].IsMarked = true;
            stack.Push(graph.VertexList[0]);
            bool IsUnmarkedNodeFound = false;
            while (stack.Count != 0)
            {
                Node<T> x = stack.Peek();
                foreach (var item in x.NeighbourNodes)
                {
                    if (!item.IsMarked)
                    {
                        item.IsMarked = true;
                        stack.Push(item);
                        IsUnmarkedNodeFound = true;
                        break;
                    }
                    else
                    {
                        if (stack.Contains(item))
                        {
                            foreach (var s in stack)
                            {
                                Console.WriteLine(s.value + " ");
                            }
                            return true;
                        }
                    }
                }
                if (!IsUnmarkedNodeFound) stack.Pop();
                IsUnmarkedNodeFound = false;
            }
            return false;
        }

        public static bool HasHamiltonianCycle<T>(Graph<T> graph) //UNFINISHED !!!!!!!!!!!!!
        {
            List<Node<T>> result = new List<Node<T>>();
            result.Add(graph.VertexList[0]);

            while (true)
            {
                for (int i = 0; i < graph.VertexList.Count; i++)
                {
                    if (graph.VertexList[i] == graph.VertexList.Last() && (result.Contains(graph.VertexList[i])) || (graph.VertexList[i] == graph.VertexList.Last() && (!graph.EdgeExists(result.Last().value, graph.VertexList[i].value) || !graph.EdgeExists(graph.VertexList[i].value, result.First().value))))
                    {
                        result.Remove(result.Last());
                    }

                    if (result.Count != graph.VertexList.Count && graph.EdgeExists(result.Last().value, result.First().value))
                    {
                        foreach (var item in result)
                        {
                            Console.Write(item.value + " ");
                        }
                        return true;
                    }

                    if (!result.Contains(graph.VertexList[i]) && graph.EdgeExists(result.Last().value, graph.VertexList[i].value))
                    {
                        result.Add(graph.VertexList[i]);
                        i = -1;
                    }
                }
                return false;
            }
        }

        public static int MinCut<T>(Graph<T> graph)
        {
            Graph<List<T>> NewGraph = new Graph<List<T>>();
            for (int i = 0; i < graph.VertexList.Count; i++)
            {
                List<T> node = new List<T>();
                node.Add(graph.VertexList[i].value);
                NewGraph.AddVertex(node);
            }

            for (int i = 0; i < graph.VertexList.Count; i++)
            {
                for (int k = 0; k < graph.VertexList.Count; k++)
                {
                    if (graph.VertexList[i] != graph.VertexList[k])
                    {
                        if (graph.EdgeExists(graph.VertexList[i].value, graph.VertexList[k].value))
                        {
                            NewGraph.CreateDirectionalConnection(NewGraph.VertexList[i].value, NewGraph.VertexList[k].value);
                        }
                    }
                }
            }

            while (NewGraph.VertexList.Count != 2)
            {
                Random rnd = new Random();

                int rnd1 = -1;
                int rnd2 = -1;

                while (rnd1 == rnd2)
                {
                    rnd1 = rnd.Next(0, NewGraph.VertexList.Count);
                    rnd2 = rnd.Next(0, NewGraph.VertexList.Count);
                }

                foreach (var item in NewGraph.VertexList)
                {
                    if (item != NewGraph.VertexList[rnd1])
                    {
                        if (NewGraph.EdgeExists(item.value, NewGraph.VertexList[rnd2].value))
                        {
                            NewGraph.RemoveDirectionalEdge(item.value, NewGraph.VertexList[rnd2].value);
                            NewGraph.CreateDirectionalConnectionMulty(item.value, NewGraph.VertexList[rnd1].value);
                        }
                    }
                    else
                    {
                        if (NewGraph.EdgeExists(item.value, NewGraph.VertexList[rnd2].value))
                        {
                            NewGraph.RemoveDirectionalEdge(item.value, NewGraph.VertexList[rnd2].value);
                        }
                    }
                }
                foreach (var item in NewGraph.VertexList[rnd2].value.ToList())
                {
                    NewGraph.VertexList[rnd2].value.Add(item);
                }
                NewGraph.RemoveVertex(NewGraph.VertexList[rnd2].value);
            }

            return NewGraph.VertexList[0].NeighbourNodes.Count;
        }

        public static int Dijkstra<T>(Graph<T> graph, T start, T finish)
        {
            Node<T> StartNode = graph.GetNode(start);
            Node<T> FinishNode = graph.GetNode(finish);
            Node<T> CurrentNode = StartNode;
            StartNode.IsMarked = true;
            StartNode.Index = 0;
            StartNode.HasIndex = true;
            foreach (var item in StartNode.NeighbourNodes)
            {
                item.Index = graph.DistanceBetween(StartNode.value, item.value);
                item.HasIndex = true;
            }
            int MarkedElements = 1;

            while (MarkedElements != graph.VertexList.Count - 1)
            {
                foreach (var item in graph.VertexList)
                {
                    if (item.HasIndex && !item.IsMarked && item != FinishNode)
                    {
                        CurrentNode = item;
                        break;
                    }
                }
                foreach (var item in CurrentNode.NeighbourNodes)
                {
                    if (!item.IsMarked && !item.HasIndex)
                    {
                        item.Index = CurrentNode.Index + graph.DistanceBetween(CurrentNode.value, item.value);
                        item.HasIndex = true;
                    }

                    if (!item.IsMarked && item.HasIndex)
                    {
                        if (CurrentNode.Index + graph.DistanceBetween(CurrentNode.value, item.value) <= item.Index)
                        {
                            item.Index = CurrentNode.Index + graph.DistanceBetween(CurrentNode.value, item.value);
                        }
                    }
                }
                CurrentNode.IsMarked = true;
                MarkedElements++;
            }
            return FinishNode.Index;
        }

        public static Graph<T> Prim<T>(Graph<T> graph)
        {
            Graph<T> result = new Graph<T>();
            List<Node<T>> MarkedElements = new List<Node<T>>();
            Random rnd = new Random();
            int r = rnd.Next(0, graph.VertexList.Count);
            graph.VertexList[r].IsMarked = true;
            MarkedElements.Add(graph.VertexList[r]);
            result.AddVertex(graph.VertexList[r].value);

            while (MarkedElements.Count != graph.VertexList.Count)
            {
                int mindistance = int.MaxValue;
                Node<T> start = new Node<T>();
                Node<T> finish = new Node<T>();

                foreach (var item in MarkedElements)
                {
                    foreach (var n in item.NeighbourNodes)
                    {
                        if (!n.IsMarked)
                        {
                            if (graph.DistanceBetween(item.value, n.value) < mindistance)
                            {
                                mindistance = graph.DistanceBetween(item.value, n.value);
                                start = item;
                                finish = n;
                            }
                        }
                    }
                }

                finish.IsMarked = true;
                MarkedElements.Add(finish);
                result.AddVertex(finish.value);
                result.MakeConnected(start.value, finish.value , mindistance);

            }
            return result;
        }

        public static Graph<string> GetActorsGraph() //OMDB Problem
        {
            WebClient webClient = new WebClient();
            List<string> files = new List<string>();
            List<ImdbEntity> MovieData = new List<ImdbEntity>();
            Graph<string> actorsGraph = new Graph<string>();
            string path = @"C:\Users\Lenovo\Desktop\testmovie.txt";
            string[] MoveieNamesArray = new string[File.ReadAllLines(path).Count()];

            for (int i = 0; i < MoveieNamesArray.Length; i++)
            {
                MoveieNamesArray[i] = File.ReadAllLines(path).Skip(i).Take(1).First().Substring(5);
                files.Add(webClient.DownloadString($"http://www.omdbapi.com/?t={MoveieNamesArray[i]}&apikey=3921b7ac"));
            }

            foreach (var item in files)
            {
                MovieData.Add(JsonConvert.DeserializeObject<ImdbEntity>(item));
            }

            foreach (var item in MovieData)
            {
                string[] tmpactorarr = item.Actors.Split(',');
                foreach (var actname in tmpactorarr)
                {
                    if (!actorsGraph.DoesNodeExist(actname))
                    {
                        actorsGraph.AddVertex(actname);
                        actorsGraph.AddMovieForActor(actname, item.Title);
                    }
                    else
                    {
                        actorsGraph.AddMovieForActor(actname, item.Title);
                    }
                }
            }

            foreach (var item1 in actorsGraph.VertexList)
            {
                foreach (var item2 in actorsGraph.VertexList)
                {
                    if (item1 != item2)
                    {
                        foreach (var item3 in item1.PlayedMovieList)
                        {
                            if (item2.PlayedMovieList.Contains(item3))
                            {
                                actorsGraph.MakeConnected(item1.value, item2.value);
                            }
                        }
                    }
                }
            }

            return actorsGraph;
        }

        static void Main(string[] args)
        {
            Graph<string> actorsGraph = GetActorsGraph();
            Console.WriteLine("Neighbours of Chris Patt are");
            foreach (var item in actorsGraph.VertexList[0].NeighbourNodes)
            {
                Console.WriteLine(item.value);
            }
            //Console.WriteLine(actorsGraph.AreConnected("Chris Pratt", " Bryce Dallas Howard")); //true
            //Console.WriteLine(actorsGraph.AreConnected("Harrison Ford", " Mark Hamill")); //true
            //Console.WriteLine(actorsGraph.AreConnected("Harrison Ford", " Bryce Dallas Howard")); //false
            //Console.WriteLine(actorsGraph.AreConnected("Chris Pratt", " Mark Hamill")); //false

            //Graph<int> gr = new Graph<int>();
            //Graph<int> res = new Graph<int>();
            //gr.AddVertex(1);
            //gr.AddVertex(2);
            //gr.AddVertex(3);
            //gr.AddVertex(4);
            //gr.AddVertex(5);
            //gr.AddVertex(6);
            //gr.MakeConnected(1, 2, 1);
            //gr.MakeConnected(2, 3, 20);
            //gr.MakeConnected(3, 4, 8);
            //gr.MakeConnected(4, 5, 5);
            //gr.MakeConnected(1, 5, 2);
            //gr.MakeConnected(6, 2, 15);
            //gr.MakeConnected(4, 6, 7);
            //res = Prim<int>(gr);
            //foreach (var item in res.VertexList)
            //{
            //    Console.Write(item.value + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine($"1 and 2 are connected : {res.AreConnected(1, 2)}");
            //Console.WriteLine($"2 and 3 are connected : {res.AreConnected(3, 2)}");
            //Console.WriteLine($"3 and 4 are connected : {res.AreConnected(3, 4)}");
            //Console.WriteLine($"3 and 4 are connected : {res.AreConnected(3, 4)}");
            //Console.WriteLine($"1 and 4 are connected : {res.AreConnected(1, 4)}");
            //Console.WriteLine($"6 and 4 are connected : {res.AreConnected(6, 4)}");
            //Console.WriteLine($"6 and 5 are connected : {res.AreConnected(5, 6)}");
            //Console.WriteLine($"1 and 3 are connected : {res.AreConnected(3, 1)}");
            //Console.WriteLine($"Distance between 1 and 2 : {res.DistanceBetween(1,2)}");
            //Console.WriteLine($"Distance between 3 and 4 : {res.DistanceBetween(3, 4)}");
            Console.ReadLine();
        }
    }
}