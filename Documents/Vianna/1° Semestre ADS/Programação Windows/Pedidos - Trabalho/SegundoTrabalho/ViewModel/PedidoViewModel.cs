using MeuFramework.Views;
using MeuFramework.ViewsModel;
using PedidosDominio.DAO;
using PedidosDominio.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SegundoTrabalho.ViewModel
{
    public class PedidoViewModel : BaseViewModel<Pedido>
    {
        ClienteDAO clienteDao = new ClienteDAO();
        PedidoDAO pedidoDao = new PedidoDAO();
        ProdutoDAO produtoDao = new ProdutoDAO();

        public ObservableCollection<Cliente> Clientes { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<PedidoItem> PedidoItens { get; set; } = new ObservableCollection<PedidoItem>();

        public PedidoItem PedidoACadastrar { get; set; } = new PedidoItem();

        public PedidoViewModel(ITelaCadastro<Pedido> telaCadastro) : base(telaCadastro)
        {
            AtualizarComboBoxClientes();
            AtualizarComboBoxProdutos();
            AtualizarListaDePedidos();
        }
                
        protected override void Deletar()
        {
            pedidoDao.Excluir(ObjetoSelecionado);
            AtualizarListaDePedidos();
        }

        private RelayCommand adicionarProdutoNaLista;
        public ICommand AdicionarProdutoNaListaCommand
        {
            get
            {
                if (adicionarProdutoNaLista == null)                
                    adicionarProdutoNaLista = new RelayCommand(param => AddProdutoNaLista());

                return adicionarProdutoNaLista;                
            }
        }

        private void AddProdutoNaLista()
        {
            var a = new PedidoItem
            {
                Pedido = PedidoACadastrar.Pedido,
                Produto = PedidoACadastrar.Produto,
                Quantidade = PedidoACadastrar.Quantidade
            };

            PedidoItens.Add(a);
           
        }

        protected override void Salvar()
        {
            if (ObjetoCadastro.Id.Equals(String.Empty))
            {
                ObjetoCadastro.Itens = PedidoItens;
                pedidoDao.Inserir(ObjetoCadastro);
            }
            else
            {
                pedidoDao.Alterar(ObjetoCadastro);
            }
        }

        private void AtualizarComboBoxClientes()
        {
            Clientes = clienteDao.RetornarTodos();
        }

        private void AtualizarComboBoxProdutos()
        {
            Produtos = produtoDao.RetornarTodos();
        }

        private void AtualizarListaDePedidos()
        {
            Lista = pedidoDao.RetornarTodos();
        }
    }
}
