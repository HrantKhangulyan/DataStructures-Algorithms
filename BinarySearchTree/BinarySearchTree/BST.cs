using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class BST
    {
        public List<Node> Elements;

        public BST()
        {
            Elements = new List<Node>();
        }

        private bool LeftNodeExists(Node node)
        {
            return node.leftnode != null;
        }

        private bool RightNodeExists(Node node)
        {
            return node.rightnode != null;
        }

        private void AddLeft(Node node, double value)
        {
            if (LeftNodeExists(node)) throw new Exception("Node already exists :( ");
            Node left = new Node(value);
            node.leftnode = left;
            Elements.Add(node.leftnode);
        }

        private void AddRight(Node node, double value)
        {
            if (RightNodeExists(node)) throw new Exception("Node already exists :( ");
            Node right = new Node(value);
            node.rightnode = right;
            Elements.Add(node.rightnode);
        }

        public Node GetNode(double val)
        {
            foreach (var item in Elements)
            {
                if (item.value == val)
                    return item;
            }
            throw new Exception("Node not found");
        }

        private Node GetParent(Node node)
        {
            foreach (var item in Elements)
            {
                if(item?.leftnode == node || item?.rightnode == node)
                {
                    return item;
                }
            }
            throw new Exception("Parent not found");
        }

        private double GetMinInSubOF(Node node) //minimum value of the right sub tree of node
        {
            Node current = node.rightnode;
            while (current.leftnode != null)
            {
                current = current.leftnode;
            }
            return current.value;
        }

        public void Print()
        {
            if(Elements.Count == 0)
            {
                Console.WriteLine("no elements in bst");
                return;
            }

            foreach (var item in Elements)
            {
                Console.Write(item.value + " ");
            }
            Console.WriteLine();
        }

        public Node BSearch(double inp)
        {
            if (Elements.Count == 0) throw new Exception("Sequence is empty");
            Node current = Elements.First();
            while ((LeftNodeExists(current) && inp <= current.value) || (RightNodeExists(current) && inp >= current.value))
            {
                if (current.value == inp) return current;
                if (current.value < inp)
                {
                    if(current.rightnode.value == inp)
                    {
                        return current.rightnode;
                    }
                    current = current.rightnode;
                    continue;
                }
                if (current.value > inp)
                {
                    if (current.leftnode.value == inp)
                    {
                        return current.leftnode;
                    }
                    current = current.leftnode;
                    continue;
                }
            }
            throw new Exception("Node not found");
        }

        public void Add(double inp)
        {
            if (Elements.Count == 0)
            {
                Node n = new Node(inp);
                Elements.Add(n);
                return;
            }

            Node current = Elements.First();
            while ((LeftNodeExists(current) && current.value > inp) || (RightNodeExists(current) && current.value < inp))
            {
                if (inp < current.value) current = current.leftnode;
                else current = current.rightnode;
            }
            if (inp > current.value) AddRight(current, inp);
            else AddLeft(current, inp);
        }

        public void Remove(Node NodeToRemove)
        {
            int childcount = 0;
            if (RightNodeExists(NodeToRemove)) childcount++;
            if (LeftNodeExists(NodeToRemove)) childcount++;

            switch (childcount)
            {
                case (0):
                    Node parent1 = GetParent(NodeToRemove);
                    if(parent1.rightnode == NodeToRemove) parent1.rightnode = null;
                    else parent1.leftnode = null;
                    Elements.Remove(NodeToRemove);
                    break;

                case (1):
                    Node parent = GetParent(NodeToRemove);
                    if(RightNodeExists(NodeToRemove))
                    {
                        if(parent.rightnode == NodeToRemove)
                        {
                            parent.rightnode = NodeToRemove.rightnode;
                            Elements.Remove(NodeToRemove);
                        }
                        if (parent.leftnode == NodeToRemove)
                        {
                            parent.leftnode = NodeToRemove.rightnode;
                            Elements.Remove(NodeToRemove);
                        }
                    }
                    else
                    {
                        if (parent.rightnode == NodeToRemove)
                        {
                            parent.rightnode = NodeToRemove.leftnode;
                            Elements.Remove(NodeToRemove);
                        }
                        if (parent.leftnode == NodeToRemove)
                        {
                            parent.leftnode = NodeToRemove.leftnode;
                            Elements.Remove(NodeToRemove);
                        }
                    }
                    break;

                case(2):
                    Node futorenodetoremove = GetNode(GetMinInSubOF(NodeToRemove));
                    NodeToRemove.value = futorenodetoremove.value;
                    Remove(futorenodetoremove);
                    break;
            }
        }


    }
}
