using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook2
{
    public class No
    {
        public object Info { get; set; }

        public List<Arco> listArco = new List<Arco>();

        public No(Object info)
        {
            this.Info = info;
        }

        public void addArco(Arco arco)
        {
            listArco.Add(arco);
        }

    }
}
