using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.DAO
{
    public interface IDbDapper
    {
        int Execute(DbDapperCommand comando);
        int Execute(IEnumerable<DbDapperCommand> comandos);
        DbDapperCommand GetCommand(string sql, object objeto);
        T QuerySingle<T>(DbDapperCommand comando);
        ObservableCollection<T> Query<T>(DbDapperCommand comando);
    }
}
