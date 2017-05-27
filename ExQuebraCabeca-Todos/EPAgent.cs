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

        /// <summary>
        /// Creating the agent and setting the initialstate plus target
        /// </summary>
        /// <param name="InitialState"></param>
        public EightPuzzle(int[] InitialState, int[] Target)
        {
            initState = InitialState;
            target = Target;
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
        private int[] FindSolution()
        {
            this.AddNode(GetName(initState), initState);

            PriorityQueue<int, Node> fila = new PriorityQueue<int, Node>();

            Node n0 = this.Find(GetName(initState));
            n0.Nivel = 0;
            fila.Enqueue(n0.HeuristicValue(), 0, n0);

            while (fila.Count > 0)
            {
                Node n = fila.Dequeue();

                if (TargetFound(n))
                    return BuildAnswer(n);

                foreach (var ns in GetSucessors(n))
                {
                    if (this.Find(GetName((int[])ns.Value.Info)) == null)
                    {                      
                        this.AddNode(GetName((int[])ns.Value.Info), ns.Value.Info);
                        
                        //Find zero and get cost
                        int pb = FindSpace((int[])n.Info);
                        int mov = ((int[])ns.Value.Info)[pb];
                        this.AddEdge(ns.Value.Name, n.Name, mov);

                        Node nf = this.Find(ns.Value.Name);
                        nf.Nivel = n.Nivel + 1;
                        fila.Enqueue(nf.HeuristicValue(), nf.Nivel, nf);
                    }
                }
            }

            return null;
        }

        private Dictionary<int,Node> GetSucessors(Node n)
        {
            Dictionary<int, Node> res = new Dictionary<int, Node>();

            int[] vn = (int[])n.Info;

            int size = Convert.ToInt32(Math.Sqrt(vn.Length));

            int b = FindSpace(vn);
            int x = b / size;
            int y = b % size;


            if(x-1 >= 0)
            {
                int[] v1 = (int[])vn.Clone();
                int nb = (x - 1) * size + y;
                v1[b] = vn[nb];
                v1[nb] = 0;

                Node nf = new Node(GetName(v1), v1, n.Nivel + 1);
                res.Add(nf.GetHashCode(),nf);
            }
            if(x+1 < size)
            {
                int[] v2 = (int[])vn.Clone();
                int nb = (x + 1) * size + y;
                v2[b] = vn[nb];
                v2[nb] = 0;

                Node nf = new Node(GetName(v2), v2, n.Nivel + 1);
                res.Add(nf.GetHashCode(), nf);
            }
            if (y-1 >= 0)
            {
                int[] v3 = (int[])vn.Clone();
                int nb = x * size + (y - 1);
                v3[b] = vn[nb];
                v3[nb] = 0;

                Node nf = new Node(GetName(v3), v3, n.Nivel + 1);
                res.Add(nf.GetHashCode(), nf);
            }
            if(y+1 < size)
            {
                int[] v4 = (int[])vn.Clone();
                int nb = x * size + (y + 1);
                v4[b] = vn[nb];
                v4[nb] = 0;

                Node nf = new Node(GetName(v4), v4, n.Nivel + 1);
                res.Add(nf.GetHashCode(), nf);
            }

            return res;
        }
        
        private int[] BuildAnswer(Node n)
        {
            List<int> res = new List<int>();

            while(n.Edges.Count > 0)
            {
                res.Add(Convert.ToInt32(n.Edges[0].Cost));
                n = n.Edges[0].To;
            }

            res.Reverse();

            return res.ToArray();
        }

        private bool TargetFound(Node n)
        {
            return (GetName((int[])n.Info) == GetName(target));
        }

        private string GetName(int [] v)
        {
            string s = string.Empty;

            for (int i = 0; i < v.Length; i++)
            {
                s += v[i].ToString();
            }

            return s;
        }

        private int FindSpace(int[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] == 0) return i;
            } 
                       
            return -1;
        }

        private int CalcularHeuristica(Node n)
        {
            int cont = 0;
            int res = ((int[])n.Info).Length;
            var info = ((int[])n.Info);

            for(int i = 0; i < info.Length; i++)
            {
                if (info[i] == target[i])
                    cont++;
            }

            if (cont == res) return 0;

            cont = 0;

            foreach (var item in (int[])n.Info)
            {
                if (item == target[cont++])
                    res--;
            }

            return res + n.Nivel;
        }
    }
}

