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

        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        public List<Node> Nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        //public Node[] Nodes
        //{
        //    get { return this.nodes.ToArray(); }
        //}

        //public List<Node> Nodes
        //{
        //    get { return this.nodes; }
        //}

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.Nodes = new List<Node>();

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        public Node FindNode(string name)
        {
            foreach (Node n in Nodes)
            {
                if (n.Name == name) return n;
            }
            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            this.AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            if (FindNode(name) == null)
                Nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node n = FindNode(name);
            if (n != null) Nodes.Remove(n);
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node f = FindNode(from);
            Node t = FindNode(to);
            if (f != null && t != null)
                f.AddEdge(t, cost);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node f = FindNode(from);
            Node[] res = new Node[f.Edges.Count];
            int i = 0;
            foreach (Edge e in f.Edges)
            {
                res[i++] = e.To;
            }
            return res;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            nodes = new Node[path.Length];
            int i = 0;
            string origem = "";
            nodes[i++] = FindNode(path[0]);
            foreach (string s in path)
            {
                string destino = s;
                if (origem != "")
                {
                    Node o = FindNode(origem);
                    Node d = FindNode(destino);
                    if (o == null || d == null) return false;
                    if (!GetNeighbours(origem).Contains(d)) return false;
                    nodes[i++] = d;
                }
                origem = s;
            }

            return true;
        }

        public bool HasEulerianPath()
        {
            int grau = 0;
            foreach (Node n in Nodes)
            {
                string nameNode = n.Name;
                grau = n.Edges.Count;

                foreach (Node i in Nodes)
                {
                    if (!i.Name.Equals(nameNode))
                    {
                        foreach (Edge e in n.Edges)
                        {
                            if (e.To.Name.Equals(nameNode))
                            {
                                grau++;
                            }
                        }
                    }
                }
            }

            if ((grau / 2) <= 2)
                return true;
            else
                return false;
            //return ((grau/2) <= 2);
    }

        public List<string> verificaGrau(string Name)
        {
            List<string> n = new List<string>();

            Node node = FindNode(Name);

            string name = node.Name;

            int grau = 0;
            foreach (Node no in Nodes)
            {
                string nameNode = no.Name;
                grau = no.Edges.Count;

                foreach (Node i in Nodes)
                {
                    if (!i.Name.Equals(nameNode))
                    {
                        foreach (Edge e in no.Edges)
                        {
                            if (e.To.Name.Equals(nameNode))
                            {
                                grau++;
                            }
                        }
                    }
                }
            }

            if ((grau % 2) != 0 ||grau == 1)
                n.Add(Name);


            return n;
        }

        public Graph MinimumDistance()
        {
            Graph g = new Graph();
            foreach(Node n in Nodes)
            {
                g.Nodes.Add(n);
            }
            foreach(Node n in g.Nodes)
            {
                foreach(Edge e in n.Edges)
                {
                    List<Node> j = DepthFirstSearch(n.Name);
                    j.ToArray();
                    List<Node> ln = ShortestPath(j[0].Name, j[j.Count].Name);

                    foreach(Node n2 in Nodes)
                    {
                        foreach(Node e2 in ln)
                        {
                            AddEdge(n2.Name, e2.Name, Convert.ToDouble(e2.Info));
                        }
                        
                    }
                }
            }

            return g;
        }

        public List<string> EulerianPath()
        {
            List<string> l = new List<string>();
            List<string> aux = new List<string>();

            foreach(Node n in Nodes)
            {
                aux = verificaGrau(n.Name);
            }

            Nodes.ToArray();


            for (int i = 0; i < Nodes.Count; i++)
            {
                Node m = Nodes[i];
                if (!l.Contains(m.Name))
                {
                    if (aux.Contains(m.Name))
                    {
                        l.Add(m.Name);
                        aux.Remove(m.Name);
                        i = Nodes.Count;
                    }

                }
            }

            for(int i = 0; i < Nodes.Count; i++)
            {
                Node m = Nodes[i];

                for(int j = 0; j < m.Edges.Count; j++)
                {
                    Edge p = m.Edges[j];
                    if (!l.Contains(p.To.Name))
                    {
                        if (aux.Contains(p.To.Name) && (l.Count == Nodes.Count -1))
                        {
                            l.Add(p.To.Name);
                            aux.Remove(p.To.Name);
                            i = Nodes.Count;
                        }
                        else
                        {
                            l.Add(p.To.Name);
                        }

                    }
                }
               
            }

            Nodes.ToList();
            return l;
        }


    

        public void ClearVisited()
        {
            foreach (Node n in Nodes)
            {
                n.Visited = false;
                n.Parent = null;
            }
        }



        public List<Node> BreadthFirstSearch(string nome)
        {
            ClearVisited();

            List<Node> ret = new List<Node>();
            Queue<Node> Q = new Queue<Node>();

            Node o = FindNode(nome);
            Q.Enqueue(o);

            o.Visited = true;
            ret.Add(o);

            while (Q.Count > 0)
            {
                Node n = Q.Dequeue();


                foreach (Node v in GetNeighbours(n.Name))
                {
                    if (!v.Visited)
                    {
                        v.Parent = n;
                        Q.Enqueue(v);
                        v.Visited = true;
                        ret.Add(v);
                    }
                }
            }

            return ret;
        }

        //public List<Node> DepthFirstSearch(string nome)
        //{
        //    ClearVisited();

        //    List<Node> ret = new List<Node>();
        //    Stack<Node> Q = new Stack<Node>();

        //    Node o = Find(nome);
        //    Q.Push(o);

        //    o.Visited = true;

        //    while (Q.Count > 0)
        //    {
        //        Node n = Q.Pop();


        //        foreach (Node v in GetNeighbours(n.Name))
        //        {
        //            if (!v.Visited)
        //            {
        //                v.Parent = n;

        //                Q.Push(v);
        //                v.Visited = true;
        //                if (v.Info.ToString() == "S")
        //                {
        //                    Node x = v;
        //                    while (x.Parent != null)
        //                    {
        //                        ret.Add(x);
        //                        x = x.Parent;
        //                    }
        //                    ret.Add(x);
        //                    return ret;
        //                }
        //            }
        //        }
        //    }

        //    return ret;
        //}

        public List<Node> DepthFirstSearch(string begin)
        {

            Stack<Node> q = new Stack<Node>();
            List<Node> ln = new List<Node>();

            Node aux = FindNode(begin);


            if (aux != null)
            {
                q.Push(aux);

                while (q.Count > 0)
                {
                    Node n = q.Pop();

                    if (n.Visited != true)
                    {
                        n.Visited = true;
                        foreach (Edge e in n.Edges)
                        {
                            if (e.To.Visited != true)
                            {

                                q.Push(e.To);
                                e.To.Parent = n;
                            }
                        }
                        ln.Add(n);
                    }
                }
            }

            return ln;
        }

        public Graph Kruskal()
        {
            // Adiciona todos os arcos..
            List<Edge> edges = new List<Edge>();
            foreach (Node node in this.Nodes)
            {
                edges.AddRange(node.Edges);
            }
            // Orderna pelo custo..
            edges = edges.OrderBy(e => e.Cost).ToList();
            Graph solution = new Graph();
            foreach (Edge edge in edges)
            {
                AddKruskalEdge(edge, solution);
            }
            return solution;
        }

        private void AddKruskalEdge(Edge edge, Graph graph)
        {
            // Get nodes..
            Node from = graph.FindNode(edge.From.Name);
            Node to = graph.FindNode(edge.To.Name);
            if (from == null)
            {
                graph.AddNode(edge.From.Name);
                from = graph.FindNode(edge.From.Name);
            }
            if (to == null)
            {
                graph.AddNode(edge.To.Name);
                to = graph.FindNode(edge.To.Name);
            }
            Node parentTo = graph.FindParent(from);
            Node parentFrom = graph.FindParent(to);
            if (parentTo.Name != parentFrom.Name)
            {
                graph.AddEdge(edge.From.Name, edge.To.Name, edge.Cost);
                graph.AddEdge(edge.To.Name, edge.From.Name, edge.Cost);
                parentTo.Info = parentFrom;
            }
        }

        private Node FindParent(Node node)
        {
            while (node.Info != null && node.Info is Node)
            {
                node = node.Info as Node;
            }
            return node;
        }

        public List<Node> ShortestPath(string begin, string end)
        {
            ClearVisited();
            List<Node> nos = new List<Node>();
            Graph solucao = new Graph();
            Node inicio = FindNode(begin);
            solucao.AddNode(inicio.Name, Convert.ToString(0));
            Node parent = null;

            while (solucao.FindNode(end) == null)
            {
                double x = -1;
                foreach (Node n in solucao.Nodes)
                {
                    
                    foreach (Edge e in n.Edges)
                    {
                        if (solucao.Nodes.Find(f => f.Name.Equals(e.To.Name)) == null)
                        {
                            if (e.Cost + Convert.ToDouble(n.Info) < x || x == -1)
                            {
                                x = e.Cost + Convert.ToDouble(n.Info);
                                e.To.Parent = n;
                                parent = n;
                                inicio = e.To;
                            }
                        }
                    }
                }
                solucao.AddNode(inicio.Name, Convert.ToString(x));
                solucao.FindNode(inicio.Name).Parent = parent;
                nos.Add(FindNode(inicio.Name));
            }


            return nos;
        }

        public List<Node> ShortestPath2(string nome, string sai)
        {
            Graph solucao = new Graph();
            solucao.AddNode(nome);

            while (solucao.FindNode(sai) == null)
            {
                Node no_min = null;
                Node no_pai = null;
                double min_dist = -1;
                foreach (Node n in solucao.Nodes)
                {
                    // inicializar variaveis do minimo
                    Node no = this.FindNode(n.Name);
                    foreach (Edge e in no.Edges)
                    {
                        if (solucao.FindNode(e.To.Name) == null)
                        {
                            double dist = Convert.ToInt32(n.Info) + e.Cost;
                            if (min_dist == -1 || dist < min_dist)
                            {
                                no_pai = n;
                                min_dist = dist;
                                no_min = e.To;
                            }
                        }
                    }
                }
                solucao.AddNode(no_min.Name);
                solucao.AddEdge(no_min.Name, no_pai.Name, 0);
                solucao.FindNode(no_min.Name).Info = min_dist;
            }

            List<Node> ret = solucao.BreadthFirstSearch(sai);
            ret.Reverse();
            return ret;
        }

        public Graph Prim(string begin)
        {
            
            List<Node> nos = new List<Node>();
            Graph solucao = new Graph();
            Node inicio = FindNode(begin);
            Node fim = FindNode(begin);
            solucao.AddNode(inicio.Name, "");
           

            while (solucao.Nodes.Count != this.Nodes.Count)
            {
                double x = -1;
                foreach (Node n in solucao.Nodes)
                {
                    Node aux = FindNode(n.Name);
                    foreach (Edge e in aux.Edges)
                    {
                        if (solucao.Nodes.Find(f => f.Name.Equals(e.To.Name)) == null)
                        {
                            if (e.Cost < x || x == -1)
                            {
                                x = e.Cost;
                                fim = e.To;
                            }
                        }
                    }
                    inicio = aux;
                }
                solucao.AddNode(fim.Name, "");
                solucao.AddEdge(inicio.Name, fim.Name, x);
                solucao.AddEdge(fim.Name, inicio.Name, x);
            }


            return solucao;
        }

        #endregion



    }
}
