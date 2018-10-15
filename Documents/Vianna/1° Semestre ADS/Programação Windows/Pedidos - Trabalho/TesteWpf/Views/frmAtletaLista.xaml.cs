using MeuFramework.Views;
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
using TesteWpf.Models;
using TesteWpf.ViewsModel;

namespace TesteWpf.Views
{
    /// <summary>
    /// Interaction logic for frmAtletaLista.xaml
    /// </summary>
    public partial class frmAtletaLista : Window
    {
        public frmAtletaLista()
        {
            InitializeComponent();
            DataContext = new AtletaViewModel(new TelaCadastroApresentacao<Atleta, frmAtleta>()) { NomeEntidade = "Atleta", NomeEntidadePlural = "Atletas" };
        }
    }
}
