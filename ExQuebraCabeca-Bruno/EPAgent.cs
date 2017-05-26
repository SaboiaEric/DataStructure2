using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using ProjetoGrafos.DataStructure;

namespace EP
{
    /// <summary>
    /// EPAgent - searchs solution for the eight puzzle problem
    /// </summary>
    public class EightPuzzle : Graph
    {
        private int[] initState;
        private int[] target;
        Dictionary<string, Node> dictionary;

        /// <summary>
        /// Creating the agent and setting the initialstate plus target
        /// </summary>
        /// <param name="InitialState"></param>
        public EightPuzzle(int[] InitialState, int[] Target)
        {
            initState = InitialState;
            target = Target;
            dictionary = new Dictionary<string, Node>();
        }

        /// <summary>
        /// Accessor for the solution
        /// </summary>
        public int[] GetSolution()
        {
            return FindSolution();
        }

        /// <summary>
        /// Função principal de busca
        /// </summary>
        /// <returns></returns>
        private string GeraNome(int[] v)
        {
            string n = "";
            for (int i = 0; i < v.Length; i++)
            {
                n += v[i] + ";";
            }
            return n;
        }
        private int[] FindSolution()
        {
            AddNode("", initState);
            PriorityQueue fila = new PriorityQueue();
            fila.Enqueue(nodes[0]);
            dictionary.Add(GeraNome((int[])nodes[0].Info), nodes[0]);
            while (fila.Count > 0)
            {
                Node atual = fila.Dequeue();
                if (TargetFound(atual))
                    return BuildAnswer(atual);

                List<Node> sucessors = GetSucessors(atual);

                foreach (Node sucessor in sucessors)
                {
                    if (!dictionary.ContainsKey(sucessor.Name))
                    {
                        fila.Enqueue(sucessor);
                        dictionary.Add(GeraNome((int[])sucessor.Info), sucessor);
                    }
                }
            }
            return null;

        }
        private int IndexOf(int[] info, int v)
        {
            for (int i = 0; i < info.Length; i++)
                if (info[i] == v)
                    return i;
            return -1;
        }
        private List<Node> GetSucessors(Node n)
        {
            List<Node> sucessors = new List<Node>();
            int[] info = (int[])n.Info;

            int pos = IndexOf(info, 0);
            int lado = (int)Math.Sqrt(target.Length);

            if (pos % lado > 0)
            {
                int[] vizinho = new int[info.Length];
                info.CopyTo(vizinho, 0);
                int aux = vizinho[pos - 1];
                vizinho[pos - 1] = vizinho[pos];
                vizinho[pos] = aux;
                Node novo = new Node(GeraNome(vizinho), vizinho, n.Nivel + 1);
                novo.Edges.Add(new Edge(novo, n, vizinho[pos]));
                sucessors.Add(novo);
            }
            if (pos % lado < lado - 1)
            {
                int[] vizinho = new int[info.Length];
                info.CopyTo(vizinho, 0);
                int aux = vizinho[pos + 1];
                vizinho[pos + 1] = vizinho[pos];
                vizinho[pos] = aux;
                Node novo = new Node(GeraNome(vizinho), vizinho, n.Nivel + 1);
                novo.Edges.Add(new Edge(novo, n, vizinho[pos]));
                sucessors.Add(novo);
            }
            if (pos - lado >= 0)
            {
                int[] vizinho = new int[info.Length];
                info.CopyTo(vizinho, 0);
                int aux = vizinho[pos - lado];
                vizinho[pos - lado] = vizinho[pos];
                vizinho[pos] = aux;
                Node novo = new Node(GeraNome(vizinho), vizinho, n.Nivel + 1);
                novo.Edges.Add(new Edge(novo, n, vizinho[pos]));
                sucessors.Add(novo);
            }

            if (pos + lado < info.Length)
            {
                int[] vizinho = new int[info.Length];
                info.CopyTo(vizinho, 0);
                int aux = vizinho[pos + lado];
                vizinho[pos + lado] = vizinho[pos];
                vizinho[pos] = aux;
                Node novo = new Node(GeraNome(vizinho), vizinho, n.Nivel + 1);
                novo.Edges.Add(new Edge(novo, n, vizinho[pos]));
                sucessors.Add(novo);
            }
            return sucessors;
        }

        private int[] BuildAnswer(Node n)
        {
            Stack<int> s = new Stack<int>();

            while (n.Edges.Count > 0)
            {
                s.Push((int)n.Edges[0].Cost);
                n = n.Edges[0].To;
            }
            List<int> seq = new List<int>();
            while (s.Count > 0)
            {
                seq.Add(s.Pop());
            }
            return seq.ToArray();
        }

        /// <summary>
        /// Verifica se o resultado foi encontrado, se encontrar retorna true
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool TargetFound(Node n)
        {
            int[] v = (int[])n.Info;
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] != target[i])
                    return false;
            }
            return true;
        }
    }
}
