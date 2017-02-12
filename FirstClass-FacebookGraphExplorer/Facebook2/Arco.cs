using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook2
{
    public class Arco
    {
        public object Info { get; set; }
        public No Origem { get; set; }
        public No Destino { get; set; }

        public Arco(No origem, No destino){
            Origem = origem;
            Destino = destino;
        }
    }
}
