using PrimAndKruskal.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAndKruskal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine("1 - Adicionar nó");
            Console.WriteLine("2 - Adicionar arco");
            Console.WriteLine("3 - Prim");
            Console.WriteLine("4 - Kruskal");
            Console.WriteLine("5 - Listar Nós");
            var opcao = "1";
            while ((opcao.Equals("1") || opcao.Equals("2") || opcao.Equals("3") || opcao.Equals("4") || opcao.Equals("5")))
            {
                opcao = string.Empty;
                opcao = Console.ReadLine();
            }
            int op = Convert.ToInt32(opcao);
            //Creating graph
            Graph graph = new Graph();
            switch (op)
            {
                case 1:
                    if (!addNode(graph))
                        Console.WriteLine("Nome do nó já existente!");
                    break;
                case 2:
                    if (!addEdge(graph))
                        Console.WriteLine("Impossível criar este arco!");
                    break;
                case 3:
                    getPrim(graph);
                    break;
                case 4:
                    break;
                case 5:
                    Console.WriteLine(graph.listNode.ToList());
                    Console.ReadKey();
                    break;
            }
        }

        public static void getNodes()
        {
            int firstNode = Convert.ToInt32(Console.ReadLine());
            int secondNode = Convert.ToInt32(Console.ReadLine());
            int edge = Convert.ToInt32(Console.ReadLine());

        }
        public static bool addNode(Graph currentGraph)
        {
            string nameNode = Console.ReadLine().ToUpper();

            if (currentGraph.listNode.Exists(x => x.Name.Equals(nameNode)))
                return false;
            else
                currentGraph.AddNode(nameNode, null);

            return true;
        }

        public static bool addEdge(Graph currentGraph)
        {
            string firstNodeName = Console.ReadLine().ToUpper();
            string secondNodeName = Console.ReadLine().ToUpper();
            int edgeValue = Convert.ToInt32(Console.ReadLine());
            //check nodes
            if (!(currentGraph.listNode.Exists(x => x.Name.Equals(firstNodeName)) && currentGraph.listNode.Exists(x => x.Name.Equals(secondNodeName))))
                currentGraph.AddEdge(firstNodeName, secondNodeName, edgeValue);
            else
                return false;

            return true;
        }

        public static void getPrim(Graph mainGraph)
        {
            //Creating solution graph
            Graph solutionGraph = new Graph();
            var allNodes = mainGraph.listNode.ToList();
            //Ordena em forma decrescente de nome
            allNodes = allNodes.OrderByDescending(noMain => noMain.Name).ToList();

            //Coloca o primeiro nó no grafo solução. A busca de prim será feita com base neste nó
            solutionGraph.AddNode(allNodes.First().Name, allNodes.First().Info);
            
            //Enquanto todos os nós não estiverem no nó solução busque o menor arco que ligue o nó atual a um próximo.
            while (VerifySolutionGraph(solutionGraph,allNodes)){
                Edge menorEdge = null;
                Node noMenorEdgeFrom = null;
                Node noMenorEdgeTo = null;
                //Para cada nó no 
                foreach (var item in solutionGraph.listNode)
                {
                    //Coleta o nó existente no grafo solução e verifica qual é o arco de menor valor
                    Node currentNode = allNodes.Find(noPrincipal => noPrincipal.Name.Equals(item.Name));

                    Edge curretEdge = currentNode.Edges.OrderByDescending(menorEgde => menorEgde.Cost).First();
                    //Pega o arco com o menor custo
                    if (menorEdge == null)
                        if (curretEdge.Cost < menorEdge.Cost)
                        {
                            //Recupera o arco e o nó deste menor arco
                            menorEdge = curretEdge;
                            noMenorEdgeFrom = menorEdge.From;
                            noMenorEdgeTo = menorEdge.To;
                        }
                            
                }
                //A partir deste momento já possuimos a menor arresta
                if (VerifyNodeOnSolutionGraph(solutionGraph, noMenorEdgeFrom))
                {
                    solutionGraph.AddNode(noMenorEdgeFrom.Name, noMenorEdgeFrom.Info);
                    solutionGraph.AddEdge(menorEdge.From.Name, menorEdge.To.Name,menorEdge.Cost);
                }

                if (VerifyNodeOnSolutionGraph(solutionGraph, noMenorEdgeTo))
                {
                    solutionGraph.AddNode(noMenorEdgeTo.Name, noMenorEdgeTo.Info);
                    solutionGraph.AddEdge(menorEdge.To.Name, menorEdge.From.Name, menorEdge.Cost);
                }
            }           
        }

        //Verify if all nodes are in solution graph
        public static bool VerifySolutionGraph(Graph solutionGraph, List<Node> allNodes)
        {
            //Se existir algum nó que não esteja no nó solução ele retorna true no método e o while que o chamou continua
            foreach (var item in allNodes)
            {
                if (solutionGraph.listNode.Exists(nodeSolution => nodeSolution.Name.Equals(item.Name)))
                    continue;
                else
                    return true;
            }
            //Se todos os nós do grafo princiapal estiverem no grafo solução o método retorna false, dizendo para o while para e encerrar,
            return false;
        }

        public static bool VerifyNodeOnSolutionGraph(Graph solutionGraph, Node node)
        {
            if (solutionGraph.listNode.Exists(solutionNode => solutionNode.Name.Equals(node.Name)))
                return true;
            return false;
        }
    }
}
