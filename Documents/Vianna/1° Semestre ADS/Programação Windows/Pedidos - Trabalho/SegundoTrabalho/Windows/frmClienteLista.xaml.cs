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
    /// Interaction logic for frmClienteLista.xaml
    /// </summary>
    public partial class frmClienteLista : Window
    {
        public frmClienteLista()
        {
            InitializeComponent();
            DataContext = new ClienteViewModel(new TelaCadastroApresentacao<Cliente, frmClienteCadastra>()) { NomeEntidade = "Cliente", NomeEntidadePlural = "Clientes" };
        }
    }
}
