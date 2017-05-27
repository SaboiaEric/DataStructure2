using FluxoMaximoDeDados.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoMaximoDeDados
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string [] valores = Console.ReadLine().Split();

            Console.WriteLine($"Primeiro valor digitado: {valores[0]} e segundo valor digitado: {valores[1]}");

            Console.ReadKey();

            Graph solutiongraph = new Graph();
            //Inclui os os nós passados pelo usuário nos grafos
            for (int i = 0; i < Convert.ToInt32(valores[0]); i++)
                solutiongraph.AddNode(new Node(i.ToString()).ToString());

            for (int i = 0; i < Convert.ToInt32(valores[1]); i++)
            {
                string[] info = Console.ReadLine().Split();
                solutiongraph.AddEdge(info[0], info[1], Convert.ToDouble(info[2]));
            }

        }
    }
}
