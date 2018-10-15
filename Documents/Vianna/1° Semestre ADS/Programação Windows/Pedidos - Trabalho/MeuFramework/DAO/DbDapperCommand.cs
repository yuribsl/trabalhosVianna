using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.DAO
{
    public class DbDapperCommand
    {
        public string Sql { get; set; }

        public object Objeto { get; set; }

        public bool ControlarAlteracaoConcomitante { get; set; }
    }
}
