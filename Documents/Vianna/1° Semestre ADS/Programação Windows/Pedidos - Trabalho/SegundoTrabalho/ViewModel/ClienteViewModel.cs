using MeuFramework.Views;
using MeuFramework.ViewsModel;
using PedidosDominio.DAO;
using PedidosDominio.Models;
using SegundoTrabalho.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SegundoTrabalho.ViewModel
{
    public class ClienteViewModel : BaseViewModel<Cliente>
    {
        private ClienteDAO dao = new ClienteDAO();

        public ClienteViewModel(ITelaCadastro<Cliente> telaCadastro) : base(telaCadastro)
        {
            AtualizarLista();
        }

        protected override void Deletar()
        {
            dao.Excluir(this.ObjetoSelecionado);
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
                dao.Alterar(this.ObjetoCadastro);
            }
         
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Lista = dao.RetornarTodos();            
        }

        //Abre Lista de Produtos
        private RelayCommand abrirListaProdutosCommand;
        public ICommand AbrirListaProdutosCommand
        {
            get
            {
                if (abrirListaProdutosCommand == null)
                    abrirListaProdutosCommand = new RelayCommand(param => AbrirListaProdutos());


                return abrirListaProdutosCommand;
            }
        }
        
        private void AbrirListaProdutos()
        {
            frmProdutosLista fpl = new frmProdutosLista();
            fpl.ShowDialog();
        }

        private RelayCommand abrirListaPedidosCommand;
        public ICommand AbrirListaPedidosCommand
        {
            get
            {
                if (abrirListaPedidosCommand == null)
                    abrirListaPedidosCommand = new RelayCommand(param => AbrirListaDePedidos());

                return abrirListaPedidosCommand;
            }
        }

        private void AbrirListaDePedidos()
        {
            frmPedidosLista fpl = new frmPedidosLista();
            fpl.ShowDialog();
        }
    }
}
