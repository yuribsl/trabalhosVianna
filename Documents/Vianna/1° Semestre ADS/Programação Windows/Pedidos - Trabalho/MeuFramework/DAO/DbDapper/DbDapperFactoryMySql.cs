using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace MeuFramework.DAO.DbDapper
{
    public class DbDapperFactoryMySql : IDbDapper
    {
        private string connectionString = "";

        public DbDapperFactoryMySql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Execute(IEnumerable<DbDapperCommand> comandos)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                int linhasAlteradas = 0;

                conexao.Open();

                IDbTransaction transacao = conexao.BeginTransaction();

                try
                {
                    int qtdeLinhas;

                    foreach (var comando in comandos)
                    {
                        qtdeLinhas = conexao.Execute(comando.Sql, comando.Objeto, transacao);
                        linhasAlteradas += qtdeLinhas;

                        if (comando.ControlarAlteracaoConcomitante && qtdeLinhas == 0)
                            throw new Exception("O registro que você está tentando alterar" +
                                " já foi alterado por outro usuário");
                    }

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
            using (var conexao = new MySqlConnection(connectionString))
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
            using (var conexao = new MySqlConnection(connectionString))
            {
                return new ObservableCollection<T>(conexao.Query<T>(comando.Sql, comando.Objeto));
            }
        }

        public T QuerySingle<T>(DbDapperCommand comando)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                return conexao.Query<T>(comando.Sql, comando.Objeto).FirstOrDefault();
            }
        }
    }
}
