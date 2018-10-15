using MeuFramework.Views;
using MeuFramework.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteWpf.DAO;
using TesteWpf.Models;

namespace TesteWpf.ViewsModel
{
    public class AtletaViewModel : BaseViewModel<Atleta>
    {
        private AtletaDAO dao = new AtletaDAO();

        public AtletaViewModel(ITelaCadastro<Atleta> telaCadastro) : base(telaCadastro)
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
                try
                {
                    ObjetoCadastro.Id = (Guid.NewGuid().ToString());
                    dao.Inserir(this.ObjetoCadastro);
                }
                catch (Exception ex)
                {
                    ObjetoCadastro.Id = String.Empty;
                    throw ex;
                }
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
    }
}
