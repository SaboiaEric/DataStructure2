﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoMaximoDeDados.DataStructure
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
        private Dictionary<string, Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>


        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new Dictionary<string, Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        protected Node Find(string name)
        {
            if(nodes.ContainsKey(name))
                return nodes[name];
            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            if (Find(name) != null)
            {
                throw new Exception("Um nó com o mesmo nome já foi adicionado a este grafo.");
            }
            this.nodes.Add(name, new Node(name, info, 0));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node existingNode = Find(name);
            if (existingNode == null)
            {
                throw new Exception("Não foi possível encontrar o nó a ser removido.");
            }
            this.nodes.Remove(name);
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node start = Find(from);
            Node end = Find(to);
            // Verifica se os nós existem..
            if (start == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            if (end == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            start.AddEdge(end, cost);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node node = Find(from);
            // Verifica se os nós existem..
            if (node == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            return node.Edges.Select(e => e.To).ToArray();
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            return false;
        }

        #endregion


       

        
        private bool Hamiltonian(Node n)
        {
            // Cria lista para armazenar o resultado..
            Queue<Node> queue = new Queue<Node>();
            // Arvore
            Graph arvore = new Graph();
            int id = 0;
            id++;
            arvore.AddNode(id.ToString(),n.Name);
            queue.Enqueue(arvore.Find(id.ToString()));

            // Realiza a busca..
            while (queue.Count > 0)
            {
                Node np = queue.Dequeue();
                Node currentNode = this.Find(np.Info.ToString());
                if (this.nodes.Count == CountNodes(np))
                    return true;

                foreach (Edge edge in currentNode.Edges)
                {
                    if (!ExistNode(np, edge.To.Name))
                    {
                        id++;
                        arvore.AddNode(id.ToString(), edge.To.Name);
                        Node nf = arvore.Find(id.ToString());
                        queue.Enqueue(nf);
                        arvore.AddEdge(nf.Name, np.Name, 1);
                    }
                }
            }
            
            return false;
        }

        private bool ExistNode(Node np, string p)
        {
            if (np == null) return false;
            while (np.Edges.Count > 0) 
            {
                if (np.Info.ToString() == p) return true;
                np = np.Edges[0].To; 
            }
            return np.Info.ToString() == p;
        }

        private int CountNodes(Node np)
        {
            if (np == null) return 0;
            int count = 1;
            while (np.Edges.Count > 0)
            { count++; np = np.Edges[0].To; }
            return count;
        }

        public List<Node> BreadthFirstSearch(string s)
        {
            ClearVisited();

            List<Node> ret = new List<Node>();
            Queue<Node> q = new Queue<Node>();

            Node a = Find(s);
            q.Enqueue(a);

            a.Visited = true;
            ret.Add(a);

            while (q.Count() > 0)
            {
                Node n = q.Dequeue();

                foreach (Node v in GetNeighbours(n.Name))
                {
                    if (!v.Visited)
                    {
                        v.Parent = n;
                        q.Enqueue(v);
                        v.Visited = true;
                        ret.Add(v);
                    }
                }
            }

            return ret;
        }

        public List<Node> DepthFirstSearch(string s)
        {
            ClearVisited();

            List<Node> ret = new List<Node>();
            Stack<Node> stack = new Stack<Node>();

            Node a = Find(s);
            stack.Push(a);

            while (stack.Count() > 0)
            {
                Node n = stack.Pop();

                if (!n.Visited)
                {
                    ret.Add(n);
                    n.Visited = true;
                }

                foreach (Node v in GetNeighbours(n.Name))
                {
                    if (!v.Visited)
                    {
                        v.Parent = n;
                        stack.Push(v);
                    }
                }
            }

            return ret;
        }

        //public List<Node> ShortestPath(string s, string ss)
        //{
        //    Node start = Find(s);
        //    Node end = Find(ss);

        //    double dist_min = 1000;
        //    Node shorter = null;

        //    Graph g = new Graph();
        //    g.AddNode(start.Name);

        //    while (g.Find(ss) == null)
        //    {
        //        dist_min = 1000;
        //        shorter = null;

        //        foreach (var n in g.Nodes)
        //        {
        //            foreach (var e in n.Edges)
        //            {
        //                if (g.Find(e.To.Name) == null)
        //                {
        //                    var a = Find(e.To.Name);
        //                    a.Length = e.Cost + n.Length;

        //                    if (a.Length < dist_min)
        //                    {
        //                        dist_min = a.Length;
        //                        shorter = a;
        //                        shorter.Parent = n;
        //                    }
        //                }
        //            }
        //        }

        //        g.AddNode(shorter.Name);
        //    }

        //    List<Node> res = new List<Node>();
        //    Node r = shorter;

        //    while (r != start)
        //    {
        //        if (res.Find(x => x.Name == r.Name) == null)
        //            res.Add(r);
        //        r = r.Parent;
        //        res.Add(r);
        //    }

        //    res.Reverse();

        //    return res;
        //}  

        public void ClearVisited()
        {
            //foreach (var n in Nodes)
            //    n.Visited = false;
        }


    }
}
