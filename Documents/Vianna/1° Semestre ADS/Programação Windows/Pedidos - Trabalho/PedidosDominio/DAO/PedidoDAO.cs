using MeuFramework.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.DAO
{
    public class PedidoDAO : BaseDAO<Pedido>
    {
        protected override string Tabela => "pedido";

        private string TabelaItem => "pedidoitem";

        protected override string[] Campos => new string[] { "Id", "AssinaturaAlteracao",
            "Numero", "Data", "IdCliente" };

        protected string[] CamposItem => new string[] { "Id", "IdPedido", "IdProduto",
            "Quantidade" };

        public override void Inserir(Pedido obj)
        {
            if (obj.Id == null || obj.Id.Length == 0)
                obj.Id = Guid.NewGuid().ToString();

            string sql = $"insert into {Tabela} ({RetornarCamposInsert()}) values" +
                $" ({RetornarParametrosInsert()})";

            var comandos = new List<DbDapperCommand>();

            comandos.Add(baseDados.GetCommand(sql, obj));

            foreach (var item in obj.Itens)
            {
                if (item.Id == null || item.Id.Length == 0)
                    item.Id = Guid.NewGuid().ToString();

                item.Pedido = obj;
                comandos.Add(RetornarComandoInserirItem(item));
            }

            baseDados.Execute(comandos);
        }

        public override void Alterar(Pedido obj)
        {
            string sql = $"update {Tabela} set {RetornarCamposEParametrosUpdate()} where id=@Id and assinaturaAlteracao = @AssinaturaAlteracao";

            var comandos = new List<DbDapperCommand>();

            var cmd = baseDados.GetCommand(sql, obj);
            cmd.ControlarAlteracaoConcomitante = true;

            comandos.Add(cmd);

            comandos.Add(RetornarComandoExcluirItens(obj));

            foreach (var item in obj.Itens)
            {
                item.Pedido = obj;
                comandos.Add(RetornarComandoInserirItem(item));
            }

            baseDados.Execute(comandos);
        }

        public override void Excluir(Pedido obj)
        {
            string sql = $"delete from {Tabela} where id = @Id and assinaturaAlteracao = @AssinaturaAlteracao";

            var comandos = new List<DbDapperCommand>();

            var cmd = baseDados.GetCommand(sql, obj);
            cmd.ControlarAlteracaoConcomitante = true;

            comandos.Add(RetornarComandoExcluirItens(obj));

            comandos.Add(cmd);

            baseDados.Execute(comandos);
        }

        private DbDapperCommand RetornarComandoExcluirItens(Pedido obj)
        {
            var cmd = new DbDapperCommand();

            string sql = $"delete from {TabelaItem} where idpedido = @IdPedido";

            cmd.Sql = sql;
            cmd.Objeto = obj;

            return cmd;
        }

        private DbDapperCommand RetornarComandoInserirItem(PedidoItem obj)
        {
            var cmd = new DbDapperCommand();

            string sql = $"insert into {TabelaItem} ({RetornarCamposInsert(CamposItem)}) values ({RetornarParametrosInsert(CamposItem)})";

            cmd.Sql = sql;
            cmd.Objeto = obj;

            return cmd;
        }
    }
}
