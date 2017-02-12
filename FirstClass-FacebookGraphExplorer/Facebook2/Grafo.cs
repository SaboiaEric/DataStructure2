using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook2
{
    public class Grafo
    {
        public List<No> listNode { get; set; }

        public Grafo()
        {
            listNode = new List<No>();
        }

        public string AdicionaPessoa(object pessoa)
        {

                //Verifica se a pessoa já existe
                var resultado = listNode.Find(item => item.Info == pessoa);
                if (resultado == null)
                {
                    No novaPessoa = new No(pessoa);
                    listNode.Add(novaPessoa);
                    return "Pessoa " + novaPessoa.Info + " cadastrado com sucesso!";
                }
                else
                    return "Pessoa já existente, crie uma nova pessoa";

        }

        public void CriaRelacionamento(object pessoa1, object pessoa2, object relacionamento)
        {
            //Verifica se o nós existe no grafo
            var primeiroResultado = listNode.Find(item => item.Info == pessoa1);
            var segundoResultado = listNode.Find(item => item.Info == pessoa2);

            //Cria o relacionameneto para ambas as pessoas
            if (primeiroResultado != null && segundoResultado != null)
            {
                No primeiraPessoa = listNode.Find(item => item.Info == pessoa1);
                No segundaPessoa = listNode.Find(item => item.Info == pessoa2);

                Arco novaRelacionamento = new Arco(primeiraPessoa, segundaPessoa);
                novaRelacionamento.Info = relacionamento;

                primeiraPessoa.listArco.Add(novaRelacionamento);
                segundaPessoa.listArco.Add(novaRelacionamento);
            }   
        }

        public bool ExcluirPessoa(object pessoa)
        {
            var pessoaExiste = listNode.Find(item => item.Info == pessoa);

            if (pessoaExiste != null)
            {

                No pessoaExclusao = listNode.Find(item => item.Info == pessoa);
                //Retirar os relacionamentos existentes
                
                //Remover a pessoa da lista do grafo
                
                return true;
            }
            else
                return false;
        }
    }
}
