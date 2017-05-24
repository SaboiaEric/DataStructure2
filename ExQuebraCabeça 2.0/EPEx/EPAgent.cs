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
        private Node origin;
        private Dictionary<string, Node> nodes;

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
            Queue<Node> q = new Queue<Node>();
            nodes = new Dictionary<string, Node>();
            origin = new Node(GetStateName(initState), initState);
            origin.Antecessor = "Saboia";
            q.Enqueue(origin);
            nodes.Add(GetStateName((int[])origin.Info), origin);
            if (GetStateName(initState) == GetStateName(target)) return SolutionPath(origin);

            while(q.Count > 0)
            {
                Node newOrigin = q.Dequeue();
                List<Node> neighbors = GetSucessors(newOrigin);
                foreach (Node neighbor in neighbors)
                {
                    neighbor.AddEdge(newOrigin);
                    nodes.Add(GetStateName((int[])neighbor.Info), origin);
                    if (GetStateName((int[])neighbor.Info) == GetStateName(target))
                        return SolutionPath(neighbor);

                    q.Enqueue(neighbor);
                }
            }

            // Criar a fila e o no inicial e jogar o no na fila
            // Loop principal do passeio
            // Retira um no da fila e verifica se ele eh a solucao
            // Se é a solução, retorna o caminho da solução    
            // Senão, gera sucessores e colocando na fila os não visitados 
            return null;
        }

        private string GetStateName(int[] state)
        {
            string name = string.Empty;
            for (int i = 0; i < state.Length; i++)
                name += state[i].ToString();
            return name;
        }

        private int[] SolutionPath(Node n)
        {
            string path = string.Empty;
            while (n.Antecessor.Equals("Saboia"));
            {
                path += SearchForZero((int[])n.Info);
                n = n.Edges[0].To;
            }
            path += SearchForZero((int[])n.Info);
            int[] pathDois = new int[path.Length];
            char[] rev = path.ToCharArray();
            Array.Reverse(rev);
            path = new string(rev);
            for (int i = 0; i < path.Length; i++)
                pathDois[i] = Convert.ToInt32(path[i]) - 48;
            return pathDois;
        }

        private string SearchForZero(int[] state)
        {
            for (int i = 0; i < state.Length; i++)
                if (state[i] == 0) return i.ToString();
            return string.Empty;
        }

        /// <summary>
        /// Dado um nó ele te retorna a lista vizinhos
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private List<Node> GetSucessors(Node n)
        {
            List<Node> neighbors = new List<Node>();
            int[] currentState = (int[])n.Info;
            int[] newState = currentState;

            string returned = SearchForZero(currentState);
            int i = (string.IsNullOrEmpty(returned))? 0 : Convert.ToInt32(returned);
            //É possível ir para a esquerda.
            if(i != 0 && i!=3 && i!= 6)
            {
                newState = (int[])currentState.Clone();
                int a = newState[i - 1];
                newState[i] = a;
                if (InsideSearch(GetStateName(newState)) == null)
                    neighbors.Add(new Node(GetStateName(newState),newState));
            }

            //É possível ir para cima
            if (i != 0 && i != 1 && i != 2)
            {
                newState = (int[])currentState.Clone();
                int a = newState[i - 3];
                newState[i - 3] = newState[i];
                newState[i] = a;
                if (InsideSearch(GetStateName(newState)) == null)
                    neighbors.Add(new Node(GetStateName(newState), newState));
            }

            //É possível ir para direita
            if (i != 2 && i != 5 && i != 8)
            {
                newState = (int[])currentState.Clone();
                int a = newState[i + 1];
                newState[i + 1] = newState[i];
                newState[i] = a;
                if (InsideSearch(GetStateName(newState)) == null)
                {
                    neighbors.Add(new Node(GetStateName(newState), newState));
                }
            }

            //É possível ir para baixo
            if (i != 6 && i != 7 && i != 8)
            {
                newState = (int[])currentState.Clone();
                int a = newState[i + 3];
                newState[i + 3] = newState[i];
                newState[i] = a;
                if (InsideSearch(GetStateName(newState)) == null)
                {
                    neighbors.Add(new Node(GetStateName(newState), newState));
                }
            }

            return neighbors;
        }

        private Node InsideSearch(string name)
        {
            if (nodes.ContainsKey(name))
                return nodes[name];
            return null;
        }
        /// Dado um nó constrói a resposta final.
        private int[] BuildAnswer(Node n)
        {
            throw new NotImplementedException();
        }

        /// Dado um nó verifica se ele é onde você quer chegar
        private bool TargetFound(Node n)
        {
            throw new NotImplementedException();
        }
    }
}

