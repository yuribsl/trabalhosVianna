using MeuFramework.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.DAO
{
    public class ProdutoDAO : BaseDAO<Produto>
    {
        protected override string Tabela => "produto";
        //AssinaturaAlteracao do tipo TimeStamp
        protected override string[] Campos => new string[] { "Id",
            "AssinaturaAlteracao", "Codigo", "Descricao", "Preco" };
    }
}
