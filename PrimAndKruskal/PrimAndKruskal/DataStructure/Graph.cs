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
        #region Atributos 

        public List<Node> listNode;

        #endregion

        #region Constructor
        public Graph()
        {
            listNode = new List<Node>();
        }
        #endregion

        #region Methods
        public Node FindNode(string name)
        {
            foreach (Node item in listNode)
                if (item.Name.Equals(name))
                    return item;

            return null;
        }

        public void AddNode(string nome, object info)
        {
            if (FindNode(nome) == null)
                listNode.Add(new Node(nome, info));
        }

        public void AddNode(Node node)
        {
            if (FindNode(node.Name) == null)
                listNode.Add(node);
        }

        public void RemoveNode(string nome)
        {
            if (FindNode(nome) != null)
                listNode.Remove(FindNode(nome));
        }

        public void AddEdge(string nameFrom,string nameTo,double cost)
        {
            Node nodeFrom = new Node(nameFrom);
            Node nodeTo = new Node(nameTo);

            if (FindNode(nodeFrom.Name) != null && FindNode(nodeTo.Name) != null)
                nodeFrom.Edges.Add(new Edge(nodeFrom, nodeTo, cost));
        }

        #endregion

        #region Algoritmos
        public Graph Prim(string beginNode)
        {
            Graph solution = new Graph();
            Node node = FindNode(beginNode);
            solution.AddNode(node);
            Edge edgeAux = null;

            //Validação para o grafo solução
            while(solution.listNode.Count != listNode.Count)
            {
                foreach (Node nodeG in solution.listNode)
                {
                    foreach (Edge edgeG in nodeG.Edges)
                    {
                        if(solution.FindNode(edgeAux.To.Name) == null)
                        {
                            if (edgeAux == null)
                                edgeAux = edgeG;
                            else
                                if (edgeAux.Cost > edgeG.Cost)
                                    edgeAux = edgeG;
                        }
                    }
                }
                solution.AddNode(edgeAux.To.Name, "-");
                solution.AddEdge(edgeAux.From.Name, edgeAux.To.Name, Convert.ToInt32(edgeAux.Cost));
                solution.AddEdge(edgeAux.To.Name, edgeAux.From.Name, Convert.ToInt32(edgeAux.Cost));
                //graph.FindNode(edgeAux.To.Name).Parent = edgeAux.From;
                //graph.FindNode(edgeAux.From.Name).Visited = true;
                edgeAux = null;
            }

            return solution;
        }

        public Graph Kruskal()
        {
            Graph solution = new Graph();
            return solution;
        }

        #endregion
    }
}
