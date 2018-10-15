using MeuFramework.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeuFramework.Views
{
    public class TelaCadastroApresentacao<T, K> : ITelaCadastro<T>
        where T : new()
        where K : Window, new()
    {
        private K form = null;

        public void Abrir(BaseViewModel<T> viewModel)
        {
            form = new K();
            form.DataContext = viewModel;
            form.ShowDialog();
        }

        public void Fechar()
        {
            form.Close();
        }
    }
}
