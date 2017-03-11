using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        List<Node> listNode = new List<Node>();
 
        public List<Node> ShortestPath(string begin, string end)
        {
            return null;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            return null;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            Node searchNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;
            
            Stack stack = new Stack();
            stack.Push(searchNode);
            while (stack.Peek() != null)
            {
                Node current = (Node)stack.Pop();
                if (current.Visited)
                    continue;
                current.Visited = true;
            }

            return null;
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

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node nodeFrom  = listNode.Find(x => x.Name.Equals(nameFrom));
            Node nodeTo = listNode.Find(x => x.Name.Equals(nameTo));
            if (nodeFrom == null && nodeTo == null)
                throw new Exception("Os nomes informalidos são inválidos. Tente outros.");
            else
            {
                Edge edge = new Edge(nodeFrom, nodeTo, cost);
                nodeFrom.AddEdge(edge);
            }
            
        }
        
        public List<Node> GetNeighborhood(string name)
        {
            Node current = new Node();
            if (listNode.Find(x => x.Name.Equals(name)) != null)
                throw new Exception("Opa, este nome que nos informou não existe. Quer nos passar algum outro? :) ");

            current = listNode.Find(x => x.Name.Equals(name));
            List <Node> neighborhoods = new List<Node>();
            foreach (Edge element in current.Edges)
                neighborhoods.Add(element.To);

            return neighborhoods.ToList<Node>();
        }
    }
}
