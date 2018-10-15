using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.DAO.DbDapper
{
    public class DbDapperFactory
    {
        public IDbDapper Get()
        {
            return new DbDapperFactoryMySql(@"Server=localhost;Database=pedidos;Uid=root;Pwd=781443aa;");
            //return new DbDapperFactorySqlServer(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Books;Data Source=P07\SQLEXPRESS14");
        }
    }
}
