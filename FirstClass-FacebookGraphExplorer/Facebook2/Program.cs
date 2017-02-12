using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Grafo facebook = new Grafo();
            No pessoa1 = new No("Ana");
            No pessoa2 = new No("Caio");
            No pessoa3 = new No("Fernanda");
            No pessoa4 = new No("Ana");

            Console.WriteLine(facebook.AdicionaPessoa(pessoa1.Info));
            Console.WriteLine(facebook.AdicionaPessoa(pessoa2.Info));
            Console.WriteLine(facebook.AdicionaPessoa(pessoa4.Info));
            Console.WriteLine(facebook.AdicionaPessoa(pessoa3.Info));

            Console.ReadKey();
        }
    }
}
