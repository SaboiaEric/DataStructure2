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
        List<Edge> listEdge = new List<Edge>();
        int visit = 0;


 
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
            Node beginNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;
            
            List<Node> depthList = new List<Node>();
            Stack stack = new Stack();

            return null;
        }

        public void AddNode(string name, object info)
        {
            Node newNode = new Node();
            newNode.Name = name;
            newNode.Info = info;

            listNode.Add(newNode);
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node nodeFrom  = listNode.Find(x => x.Name.Equals(nameFrom));
            Node nodeTo = listNode.Find(x => x.Name.Equals(nameTo));
            if (nodeFrom == null && nodeTo == null)
                return;
            else
            {
                Edge edge = new Edge(nodeFrom, nodeTo, cost);
            }
            
        }
    }
}
