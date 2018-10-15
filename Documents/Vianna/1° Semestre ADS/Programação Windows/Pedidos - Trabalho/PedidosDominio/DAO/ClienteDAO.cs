using MeuFramework.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.DAO
{
    public class ClienteDAO : BaseDAO<Cliente>
    {
        protected override string Tabela => "cliente";

        protected override string[] Campos => new string[] { "Id",
            "AssinaturaAlteracao", "Nome", "Email" };
    }
}
