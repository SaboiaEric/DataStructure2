using ProjetoGrafos.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EP
{
    public class PriorityQueue
    {
        private Dictionary<object, Node> dicionario;
        private List<Node> lista;
        public int Count { get; set; }
        public PriorityQueue()
        {
            lista = new List<Node>();
            Count = 0;
        }
        public void Enqueue(Node n)
        {
            lista.Add(n);
           //lista = lista.OrderBy(x => x.heuristicValue).ToList();
            Count++;
        }
        public Node Dequeue()
        {
            Node n = lista[0];
            
            lista.Remove(n);
            Count--;
            return n;
        }

    }
}
