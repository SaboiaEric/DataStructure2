using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        List<Node> listNode = new List<Node>();
 
        public List<Node> ShortestPath(string begin, string end)
        {
            ClearVisited();
            Node startNode = listNode.Find(x => x.Name.Equals(begin));

            double minimun_distance = 1000;
            Node shorterNode = null;

            Graph solutionGraph = new Graph();
            solutionGraph.AddNode(startNode);
            
            //Verifica se o nó 'end' está no grafo solução
            while (solutionGraph.listNode.Find(x => x.Name.Equals(end)) == null)
            {
                minimun_distance = 1000;
                shorterNode = null;
                //Visita todos os nós do grafo solução
                foreach (var node in solutionGraph.listNode)
                {                   
                    //Visita todos os arcos do nó atual que está no grafo solução
                    foreach (var edges in node.Edges)
                    {
                        //Verifica se o nome do nó 'PARA' já existe na lista de nós do grafo solução
                        if (solutionGraph.listNode.Find(x => x.Name.Equals(edges.To.Name)) == null)
                        {
                            //Caso não esteja, ele é enviado para um nó auxiliar
                            var auxNode = listNode.Find(x => x.Name.Equals(edges.To.Name));
                            //A distância até o nó auxiliar será a soma do custo do arco mais o tamanho até o nó atual
                            auxNode.Length = edges.Cost + node.Length;

                            //Se a distância até este nó auxiliar, que é o nó 'PARA' do arco for menor que a distância minima
                            if (auxNode.Length < minimun_distance)
                            {
                                //Distância minima recebe a distância do nó
                                minimun_distance = auxNode.Length;
                                //O menor nó recebe o nó auxiliar
                                shorterNode = auxNode;
                                //O menor nó recebe como pai o nó atual do grafo.
                                shorterNode.Parent = node;
                            }
                        }
                    }
                }
                //Adiciona o nó com menor distância no grafo solução.
                solutionGraph.AddNode(shorterNode);
            }

            //Cria a lista de nós
            List<Node> result = new List<Node>();
            Node rNode = (shorterNode == null)? startNode: shorterNode;

            //Enquanto o menor nó for diferente do nó inicial
            if (rNode == startNode)
                result.Add(rNode);
            while (rNode != startNode)
            {
                //Verifica se o nome do menor nó está na lista de resultado
                if (result.Find(x => x.Name.Equals(rNode.Name)) == null)
                    result.Add(rNode); //Se não estiver adiciona nó mais curto na lista de resultado
                //O nó mais curto recebe o teu parent
                rNode = rNode.Parent;
                //O nó mais curto atual, que era o pai do nó mais curto na linha superior é também adicionado na lista de resultado.
                result.Add(rNode);
            }

            //Reverte a lista de nós resultado
            result.Reverse();

            return result;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            ClearVisited();
            Node searchNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;

            List<Node> breadth = new List<Node>();
            Queue queue = new Queue();
            queue.Enqueue(searchNode);
            while (queue.Count > 0)
            {
                Node current = (Node)queue.Dequeue();
                if (current.Visited)
                    continue;
                current.Visited = true;
                breadth.Add(current);
                foreach (Node element in GetNeighborhood(current.Name))
                {
                    if (element.Visited)
                        continue;
                    element.Parent = current;
                    queue.Enqueue(element);
                }
            }

            return breadth;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            ClearVisited();
            Node searchNode = listNode.Find(x => x.Name.Equals(begin));
            if (begin == null)
                return null;

            List<Node> depth = new List<Node>();
            Stack stack = new Stack();
            stack.Push(searchNode);
            while (stack.Count > 0)
            {
                Node current = (Node)stack.Pop();
                if (current.Visited)
                    continue;
                current.Visited = true;
                depth.Add(current);
                foreach (Node element in GetNeighborhood(current.Name))
                {
                    if (element.Visited)
                        continue;
                    element.Parent = current;
                    stack.Push(element);
                }
            }

            return depth;
        }

        public void AddNode(string name, object info)
        {
            if (listNode.Find(x => x.Name.Equals(name)) != null)
                throw new Exception("Amigo, esse nó já existe. Tentou outro.");

            Node newNode = new Node(name,info);
            listNode.Add(newNode);
        }

        public void AddNode(Node node)
        {
            listNode.Add(node);
        }

        public void RemoveNode(string name)
        {
            // Search the node in the list and remove him
            if (listNode.Find(x => x.Name.Equals(name)) != null)
                listNode.Remove(listNode.Find(x => x.Name.Equals(name)));
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node nodeFrom  = listNode.Find(x => x.Name.Equals(nameFrom));
            Node nodeTo = listNode.Find(x => x.Name.Equals(nameTo));
            if (nodeFrom == null && nodeTo == null)
                throw new Exception("Os nomes informalidos são inválidos. Tente outros.");
            else
            {
                Edge edge = new Edge(nodeFrom, nodeTo, cost);
                nodeFrom.AddEdge(edge);
            }
            
        }
        
        public List<Node> GetNeighborhood(string name)
        {
            if (listNode.Find(x => x.Name.Equals(name)) == null)
                throw new Exception("Opa, este nome que nos informou não existe. Nos passe algum outro? :) ");

            Node current = new Node();
            current = listNode.Find(x => x.Name.Equals(name));
            List <Node> neighborhoods = new List<Node>();
            foreach (Edge element in current.Edges)
                neighborhoods.Add(element.To);

            return neighborhoods.ToList<Node>();
        }

        public void ClearVisited()
        {
            foreach (Node element in listNode)
                element.Visited = false;
        }
    }
}
