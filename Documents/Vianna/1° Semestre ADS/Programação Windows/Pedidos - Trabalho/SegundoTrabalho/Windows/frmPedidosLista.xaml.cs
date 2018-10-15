using MeuFramework.Views;
using PedidosDominio.Models;
using SegundoTrabalho.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SegundoTrabalho.Windows
{
    /// <summary>
    /// Interaction logic for frmPedidosLista.xaml
    /// </summary>
    public partial class frmPedidosLista : Window
    {
        public frmPedidosLista()
        {
            InitializeComponent();
            DataContext = new PedidoViewModel(new TelaCadastroApresentacao<Pedido, frmPedidosCadastra>()) { NomeEntidade = "Pedido", NomeEntidadePlural = "Pedidos" };
        }
    }
}
