using MeuFramework.Views;
using MeuFramework.ViewsModel;
using PedidosDominio.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoTrabalho.ViewModel
{
    public class ProdutoViewModel : BaseViewModel<Produto>
    {
        ProdutoDAO dao = new ProdutoDAO();

        public ProdutoViewModel(ITelaCadastro<Produto> telaCadastro) : base(telaCadastro)
        {
            AtualizarLista();
        }

        protected override void Deletar()
        {
            dao.Excluir(ObjetoSelecionado);
            ObjetoSelecionado = null;
            AtualizarLista();
        }

        protected override void Salvar()
        {
            if (ObjetoCadastro.Id.Equals(String.Empty))
            {
                dao.Inserir(ObjetoCadastro);
            }
            else
            {
                dao.Alterar(ObjetoCadastro);
            }

            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Lista = dao.RetornarTodos();
        }
    }
}
