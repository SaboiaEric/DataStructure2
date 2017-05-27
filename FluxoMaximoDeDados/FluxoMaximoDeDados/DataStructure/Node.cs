using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoMaximoDeDados.DataStructure
{
    /// <summary>
    /// Classe que representa um nó dentro de um grafo.
    /// </summary>
    public class Node
    {

        #region Propriedades

        public int heuristicValue { get; set; }
        /// <summary>
        /// O nome do nó dentro do grafo.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A informação adicional armazenada no nó.
        /// </summary>
        public object Info { get; set; }
        /// <summary>
        /// A lista de arcos associada ao nó.
        /// </summary>
        public List<Edge> Edges { get; private set; }

        public int Nivel { get; set; }

        public bool Visited { get; set; }

        public Node Parent { get; set; }

        public double Length { get; set; }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria um novo nó.
        /// </summary>
        public Node()
        {
            this.Edges = new List<Edge>();
        }

        /// <summary>
        /// Cria um novo nó.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <param name="info">A informação armazenada no nó.</param>
        public Node(string name, object info, int nivel) : this()
        {
            this.Name = name;
            this.Info = info;
            this.Nivel = nivel;
        }

        public Node(string name)
        {
            this.Name = name;
        }
        

        #endregion

        #region Métodos

        /// <summary>
        /// Adiciona um arco com nó origem igual ao nó atual, e destino e custo de acordo com os parâmetros.
        /// </summary>
        /// <param name="to">O nó destino.</param>
        public void AddEdge(Node to)
        {
            AddEdge(to, 0);
            heuristicValue = -1;
        }

        /// <summary>
        /// Adiciona um arco com nó origem igual ao nó atual, e destino e custo de acordo com os parâmetros.
        /// </summary>
        /// <param name="to">O nó destino.</param>
        /// <param name="cost">O custo associado ao arco.</param>
        public void AddEdge(Node to, double cost)
        {
            this.Edges.Add(new Edge(this, to, cost));
            heuristicValue = -1;
        }

        public int HeuristicValue()
        {
            if (heuristicValue == -1)
                heuristicValue = Heuristic((int[])Info);
            return heuristicValue;
        }
        private int Heuristic(int[] v)
        {
            int value = 0;
            int size = (int)Math.Sqrt(v.Length);
            for (int i = 0; i < v.Length; i++)
                if (v[i] != 0)
                    value += Math.Abs((v[i] % size) - (i % size)) + Math.Abs((v[i] / size) - (i / size));
            return value;
        }
        #endregion

        #region Métodos Sobrescritos

        /// <summary>
        /// Apresenta a visualização do objeto em formato texto.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Info != null)
            {
                return String.Format("{0}({1})", this.Name, this.Info);
            }
            return this.Name;
        }

        #endregion

    }
}
