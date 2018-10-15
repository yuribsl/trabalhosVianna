using PedidosDominio.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestePersistencia
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dao = new ClienteDAO();

            //var c = new Cliente { Nome = "Ana Maria", Email = "ana@maria.com.br" };

            //dao.Inserir(c);

            //Console.WriteLine("Cliente inserido");

            //Console.ReadKey();

            //c = dao.ConsultarPorId(c.Id);

            //c.Nome += " (alterado)";

            //dao.Alterar(c);

            //Console.WriteLine("Cliente alterado");

            var p = new Produto { Codigo = 1, Descricao = "Coca-cola", Preco = 5.43 };

            (new ProdutoDAO()).Inserir(p);

            Console.WriteLine("Produto inserido");

            //var prod = new Produto();

            //prod.Codigo = 123;
            //prod.Descricao = "Produto 123";
            //prod.Preco = 1.23;

            //(new ProdutoDAO()).Inserir(prod);



            Console.ReadKey();
        }
    }
}
