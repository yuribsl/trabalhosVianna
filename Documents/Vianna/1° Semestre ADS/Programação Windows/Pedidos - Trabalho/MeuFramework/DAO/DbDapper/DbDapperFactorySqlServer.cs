using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MeuFramework.DAO.DbDapper
{
    public class DbDapperFactorySqlServer : IDbDapper
    {
        private string connectionString = "";

        public DbDapperFactorySqlServer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Execute(IEnumerable<DbDapperCommand> comandos)
        {
            using (var conexao = new SqlConnection(connectionString))
            {
                int linhasAlteradas = 0;

                conexao.Open();

                IDbTransaction transacao = conexao.BeginTransaction();

                try
                {
                    foreach (var comando in comandos)
                        linhasAlteradas += conexao.Execute(comando.Sql, comando.Objeto, transacao);

                    transacao.Commit();

                    return linhasAlteradas;
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw ex;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public int Execute(DbDapperCommand comando)
        {
            using (var conexao = new SqlConnection(connectionString))
            {
                return conexao.Execute(comando.Sql, comando.Objeto);
            }
        }

        public DbDapperCommand GetCommand(string sql, object objeto)
        {
            return new DbDapperCommand() { Objeto = objeto, Sql = sql };
        }

        public ObservableCollection<T> Query<T>(DbDapperCommand comando)
        {
            using (var conexao = new SqlConnection(connectionString))
            {
                return new ObservableCollection<T>(conexao.Query<T>(comando.Sql, comando.Objeto));
            }
        }

        public T QuerySingle<T>(DbDapperCommand comando)
        {
            using (var conexao = new SqlConnection(connectionString))
            {
                return conexao.Query<T>(comando.Sql, comando.Objeto).FirstOrDefault();
            }
        }
    }
}
