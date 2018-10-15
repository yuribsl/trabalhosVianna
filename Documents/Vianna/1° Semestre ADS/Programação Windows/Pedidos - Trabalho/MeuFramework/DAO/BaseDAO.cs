using MeuFramework.DAO.DbDapper;
using MeuFramework.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.DAO
{
    public abstract class BaseDAO<T> where T : Modelo
    {
        protected static IDbDapper baseDados = (new DbDapperFactory()).Get();

        protected abstract string Tabela { get; }
        protected abstract string[] Campos { get; }

        public virtual void Inserir(T obj)
        {
            if (obj.Id == null || obj.Id.Length == 0)
                obj.Id = Guid.NewGuid().ToString();
            
            string sql = $"insert into {Tabela} ({RetornarCamposInsert()}) values ({RetornarParametrosInsert()})";

            baseDados.Execute(baseDados.GetCommand(sql, obj));
        }

        public virtual void Alterar(T obj)
        {
            string sql = $"update {Tabela} set {RetornarCamposEParametrosUpdate()} where id=@Id and assinaturaAlteracao = @AssinaturaAlteracao";

            if (baseDados.Execute(baseDados.GetCommand(sql, obj)) == 0)
                throw new Exception("O registro que você está tentando alterar já foi alterado por outro usuário");
        }

        public virtual void Excluir(T obj)
        {
            string sql = $"delete from {Tabela} where id=@Id";

            baseDados.Execute(baseDados.GetCommand(sql, obj));
        }

        public virtual T ConsultarPorId(string id)
        {
            string query = $"select * from {Tabela} where id=@Id";

            return baseDados.QuerySingle<T>(baseDados.GetCommand(query, new { Id = id }));
        }

        public virtual ObservableCollection<T> RetornarTodos()
        {
            string query = $"select * from {Tabela}";

            return baseDados.Query<T>(baseDados.GetCommand(query, null));
        }

        public virtual ObservableCollection<T> Consultar(string query, object objeto)
        {
            return baseDados.Query<T>(baseDados.GetCommand(query, objeto));
        }

        protected object RetornarCamposEParametrosUpdate()
        {
            return RetornarCamposEParametrosUpdate(Campos);
        }

        protected object RetornarCamposEParametrosUpdate(string[] campos)
        {
            StringBuilder sb = new StringBuilder();

            if (campos.Length > 0)
            {
                sb.Append(campos[0] + "=@" + campos[0]);

                for (int i = 1; i < campos.Length; i++)
                    sb.Append("," + campos[i] + "=@" + campos[i]);
            }

            return sb.ToString();
        }

        protected object RetornarParametrosInsert()
        {
            return RetornarParametrosInsert(Campos);
        }

        protected object RetornarParametrosInsert(string[] campos)
        {
            StringBuilder sb = new StringBuilder();

            if (campos.Length > 0)
            {
                sb.Append("@" + campos[0]);

                for (int i = 1; i < campos.Length; i++)
                    if (!campos[i].Equals("AssinaturaAlteracao"))
                        sb.Append(",@" + campos[i]);
            }

            return sb.ToString();
        }

        protected object RetornarCamposInsert()
        {
            return RetornarCamposInsert(Campos);
        }

        protected object RetornarCamposInsert(string[] campos)
        {
            StringBuilder sb = new StringBuilder();

            if (campos.Length > 0)
            {
                sb.Append(campos[0]);

                for (int i = 1; i < campos.Length; i++)
                    if (!campos[i].Equals("AssinaturaAlteracao"))
                        sb.Append("," + campos[i]);
            }

            return sb.ToString();
        }
    }
}
