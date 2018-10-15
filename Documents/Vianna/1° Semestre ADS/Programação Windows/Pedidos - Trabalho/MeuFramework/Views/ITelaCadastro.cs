using MeuFramework.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.Views
{
    public interface ITelaCadastro<T> where T : new()
    {
        void Abrir(BaseViewModel<T> viewModel);
        void Fechar();
    }
}
