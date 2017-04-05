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
        public List<Node> Nodes { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public Node FindNode(string name)
        {
            // Percorre lista de nós
            foreach (Node node in Nodes)
            {
                // Se encontrou o nó
                if (node.Name.Equals(name.Trim()))
                    return node;
            }
            return null;
        }

        public void ClearVisited()
        {
            foreach (Node node in Nodes)
                node.Visited = false;
        }

        public void ClearParent()
        {
            foreach (Node node in Nodes)
                node.Parent = null;
        }

        public void AddNode(string name, string info)
        {
            if (FindNode(name) == null)
                Nodes.Add(new Node(name, info));
        }

        public void AddNode(Node node)
        {
            if (FindNode(node.Name) == null)
                Nodes.Add(node);
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node from = FindNode(nameFrom);
            Node to = FindNode(nameTo);

            if (from != null && to != null)
                from.Edges.Add(new Edge(from, to, cost));
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            List<Node> result = new List<Node>();
            Queue<Node> queue = new Queue<Node>();
            ClearVisited();

            Node node = FindNode(begin);
            if (node == null)
                return null;

            node.Visited = true;
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                result.Add(node);

                foreach (Edge edge in node.Edges)
                {
                    if (!edge.To.Visited)
                    {
                        edge.To.Parent = node;
                        edge.To.Visited = true;
                        queue.Enqueue(edge.To);
                    }
                }
            }
            return result;
        }

        private bool FindWay(List<Node> path, Edge se)
        {
            if (path != null && path.Count > 0)
            {
                Node end = null;
                foreach (Node node in path)
                {
                    if (node.Info != null && node.Name.Equals(se.To.Name))
                        end = node;
                }
                if (end != null)
                {
                    Node x = end;
                    while (x.Parent != null)
                    {
                        x = x.Parent;
                    }
                    if (x.Name.Equals(se.From.Name))
                        return true;
                }
            }
            return false;
        }

        public Graph Prim(string begin)
        {
            //ClearVisited();
            Graph graph = new Graph();
            Node node = FindNode(begin);
            graph.AddNode(node.Name, "I");
            Edge edgeAux = null;

            while (graph.Nodes.Count != this.Nodes.Count)
            {
                foreach (Node nodeG in graph.Nodes)
                {
                    foreach (Edge edgeG in this.FindNode(nodeG.Name).Edges)
                    {
                        if (graph.FindNode(edgeG.To.Name) == null)
                        {
                            if (edgeAux == null)
                            {
                                edgeAux = edgeG;
                            }
                            else
                            {
                                if (edgeAux.Cost > edgeG.Cost)
                                {
                                    edgeAux = edgeG;
                                }
                            }
                        }
                    }
                }

                graph.AddNode(edgeAux.To.Name, "-");
                graph.AddEdge(edgeAux.From.Name, edgeAux.To.Name, Convert.ToInt32(edgeAux.Cost));
                graph.AddEdge(edgeAux.To.Name, edgeAux.From.Name, Convert.ToInt32(edgeAux.Cost));
                //graph.FindNode(edgeAux.To.Name).Parent = edgeAux.From;
                //graph.FindNode(edgeAux.From.Name).Visited = true;
                edgeAux = null;
            }
            return graph;
        }

        public Graph Kruskal()
        {
            Graph graph = new Graph();
            List<Edge> edges = new List<Edge>();

            foreach (Node node in this.Nodes)
            {
                foreach (Edge edge in node.Edges)
                {
                    edges.Add(new Edge(new Node(edge.From.Name, "F"), new Node(edge.To.Name, "T"), edge.Cost));
                }
            }

            edges = edges.OrderBy(e => e.Cost).ToList();

            foreach (Edge edge in edges)
            {
                graph.ClearVisited();
                graph.ClearParent();
                if (!graph.FindWay(graph.BreadthFirstSearch(edge.From.Name), edge))
                {
                    if (graph.FindNode(edge.To.Name) == null)
                        graph.AddNode(new Node(edge.To.Name, "To"));
                    if (graph.FindNode(edge.From.Name) == null)
                        graph.AddNode(new Node(edge.From.Name, "From"));
                    graph.AddEdge(edge.From.Name, edge.To.Name, Convert.ToInt32(edge.Cost));
                }
            }
            return graph;
        }
    }
}
