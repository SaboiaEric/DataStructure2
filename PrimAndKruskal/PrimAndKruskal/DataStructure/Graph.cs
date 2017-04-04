using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAndKruskal.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        public List<Node> listNode = new List<Node>();
 
        public List<Node> ShortestPath(string begin, string end)
        {
            return null;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            ClearVisited();
            Node searchNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;

            List<Node> breadth = new List<Node>();
            Queue queue = new Queue();
            queue.Enqueue(searchNode);
            while (queue.Count > 0)
            {
                Node current = (Node)queue.Dequeue();
                if (current.Visited)
                    continue;
                current.Visited = true;
                breadth.Add(current);
                foreach (Node element in GetNeighborhood(current.Name))
                {
                    if (element.Visited)
                        continue;
                    element.Parent = current;
                    queue.Enqueue(element);
                }
            }

            return breadth;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            ClearVisited();
            Node searchNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;

            List<Node> depth = new List<Node>();
            Stack stack = new Stack();
            stack.Push(searchNode);
            while (stack.Count > 0)
            {
                Node current = (Node)stack.Pop();
                if (current.Visited)
                    continue;
                current.Visited = true;
                depth.Add(current);
                foreach (Node element in GetNeighborhood(current.Name))
                {
                    if (element.Visited)
                        continue;
                    element.Parent = current;
                    stack.Push(element);
                }
            }

            return depth;
        }

        public void AddNode(string name, object info)
        {
            if (listNode.Find(x => x.Name.Equals(name)) != null)
                throw new Exception("Amigo, esse nó já existe. Tentou outro.");

            Node newNode = new Node(name,info);
            listNode.Add(newNode);
        }


        public void RemoveNode(string name)
        {
            // Search the node in the list and remove him
            if (listNode.Find(x => x.Name.Equals(name)) != null)
                listNode.Remove(listNode.Find(x => x.Name.Equals(name)));
        }

        public void AddEdge(string nameFrom, string nameTo, double cost)
        {
            Node nodeFrom  = listNode.Find(x => x.Name.Equals(nameFrom));
            Node nodeTo = listNode.Find(x => x.Name.Equals(nameTo));
            if (nodeFrom == null && nodeTo == null)
                throw new Exception("Os nomes informalidos são inválidos. Tente outros.");
            else
            {
                Edge edgeFrom = new Edge(nodeFrom, nodeTo, cost);
                nodeFrom.AddEdge(edgeFrom);

                Edge edgeTo = new Edge(nodeTo, nodeFrom, cost);
                nodeTo.AddEdge(edgeTo);
            }
            
        }
        
        public List<Node> GetNeighborhood(string name)
        {
            if (listNode.Find(x => x.Name.Equals(name)) == null)
                throw new Exception("Opa, este nome que nos informou não existe. Nos passe algum outro? :) ");

            Node current = new Node();
            current = listNode.Find(x => x.Name.Equals(name));
            List <Node> neighborhoods = new List<Node>();
            foreach (Edge element in current.Edges)
                neighborhoods.Add(element.To);

            return neighborhoods.ToList<Node>();
        }

        public void ClearVisited()
        {
            foreach (Node element in listNode)
                element.Visited = false;
        }
    }
}
