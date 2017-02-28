using System;
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
        public bool AddEdge(string nameFrom, string nameTo, object info)
        {
            return true;

        }

        public bool AddNode(string name, object info)
        {
            return true;
        }

        public List<Node> DepthFirstSearch(string name)
        {
            Node node = new Node();
            List<Node> nodeList = new List<Node>();
            return nodeList;
        }

        public List<Node> BreadthFirstSearch(string name)
        {
            Node node = new Node();
            List<Node> nodeList = new List<Node>();

            return nodeList;
        }

        public List<Node> ShortestPath(string nameIn, string nameOut)
        {
            Node node = new Node();
            List<Node> nodeList = new List<Node>();

            return nodeList;
        }
    }
}
