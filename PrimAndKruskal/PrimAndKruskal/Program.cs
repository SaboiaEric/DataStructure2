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
        static void Main(string[] args)
        {
            Graph graphP = new Graph();
            int op = -1;
            string[] names;
            string name = string.Empty;
            while (op != 0)
            {
                Console.Clear();
                op = menu();
                switch (op)
                {
                    case 1:
                        Console.Write("Digite o nome do nó: ");
                        name = Console.ReadLine();
                        graphP.AddNode(new Node(name, "*"));
                        break;
                    case 2:
                        Console.Write("Digite o From|To|Cost: ");
                        names = Console.ReadLine().Split('|');
                        graphP.AddEdge(names[0], names[1], Convert.ToInt32(names[2]));
                        graphP.AddEdge(names[1], names[0], Convert.ToInt32(names[2]));
                        break;
                    case 3:
                        Console.Write("Digite o inicio: ");
                        ListGraph(graphP.Prim(Console.ReadLine()));
                        break;
                    case 4:
                        ListGraph(graphP.Kruskal());
                        break;
                    case 5:
                        ListGraph(graphP);
                        break;
                }
            }
        }

        public static int menu()
        {
            int ent = 0;
            string menu = "0 - Sair\n1 - Add Node\n2 - Add Arco\n3 - Prim\n4 - Kruskal\n5 - Ligacoes\n\n-->";
            Console.Write(menu);
            ent = Convert.ToInt32(Console.ReadLine());
            return ent;
        }

        public static void ListGraph(Graph graph)
        {
            foreach (Node node in graph.listNode)
            {
                Console.WriteLine("-----------------------");
                foreach (Edge edge in node.Edges)
                {
                    Console.WriteLine("Node: " + node.Name + " - Edge: " + edge.To.Name);
                }
            }
            Console.WriteLine("-----------------------");
            Console.ReadKey();
        }
    }
}
