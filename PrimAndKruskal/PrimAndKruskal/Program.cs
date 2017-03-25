using ProjetoGrafos.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimAndKruskal
{
    class Program
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
            while(!(opcao.Equals("1") || opcao.Equals("2") || opcao.Equals("3") || opcao.Equals("4"))){
                opcao = string.Empty;
                opcao= Console.ReadLine();
            }
            int op = Convert.ToInt32(opcao);
            //Creating graph
            
            switch (op)
            {
                case 1:
                    getNodes();
                    break;
                case 2:
                    break;  
                case 3 :
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
    }
}
