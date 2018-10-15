using MeuFramework.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWpf.Models;

namespace TesteWpf.DAO
{
    public class AtletaDAO : BaseDAO<Atleta>
    {
        protected override string Tabela => "atleta";

        protected override string[] Campos => new string[] {
            "id",
            "assinaturaalteracao",
            "nome",
            "altura"
        };
    }
}
