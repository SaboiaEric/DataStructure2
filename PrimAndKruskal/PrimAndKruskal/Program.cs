using PrimAndKruskal.DataStructure;
using ProjetoGrafos.DataStructure;
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
            Graph graph = new Graph();
            var opcao = "1";
            while (!(opcao.Equals("1") || opcao.Equals("2") || opcao.Equals("3") || opcao.Equals("4")))
            {
                opcao = string.Empty;
                opcao = Console.ReadLine();
            }
            int op = Convert.ToInt32(opcao);
            //Creating graph

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
            while(solutionGraph.listNode.Exists(x => x.Name.Equals(allNodes.)))

            
            
        }
    }
}
