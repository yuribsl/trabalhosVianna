using MeuFramework.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeuFramework.ViewsModel
{
    public abstract class BaseViewModel<T> : INotifyPropertyChanged where T : new()
    {
        public BaseViewModel(ITelaCadastro<T> telaCadastro) => this.telaCadastro = telaCadastro;

        //public ObservableCollection<T> Lista { get; set; } = new ObservableCollection<T>();

        public ObservableCollection<T> Lista { get => lista ; set => Notificar(lista = value); }

        public T ObjetoSelecionado { get => objetoSelecionado; set => Notificar(objetoSelecionado = value); }

        public T ObjetoCadastro { get => objetoCadastro; set => Notificar(objetoCadastro = value); }

        public string NomeEntidade { get => nomeEntidade; set => Notificar(nomeEntidade = value); }

        public string NomeEntidadePlural { get => nomeEntidadePlural; set => Notificar(nomeEntidadePlural = value); }

        public bool HabilitarEdicao { get => habilitarEdicao; set => Notificar(habilitarEdicao = value); }

        public ICommand ExcluirCommand
        {
            get
            {
                if (excluirCommand == null)
                    excluirCommand = new RelayCommand(param => this.Deletar(), param => this.PodeDeletar());

                return excluirCommand;
            }
        }

        public ICommand SalvarCommand
        {
            get
            {
                if (salvarCommand == null)
                    salvarCommand = new RelayCommand(param => this.SalvarObjeto(), param => this.PodeSalvarObjeto());

                return salvarCommand;
            }
        }

        public ICommand AbrirConsultarCommand
        {
            get
            {
                if (abrirConsultarCommand == null)
                    abrirConsultarCommand = new RelayCommand(param => this.AbrirConsulta(), param => this.PodeConsultar());

                return abrirConsultarCommand;
            }
        }

        public ICommand AbrirInserirCommand
        {
            get
            {
                if (abrirInserirCommand == null)
                    abrirInserirCommand = new RelayCommand(param => this.AbrirInsercao(), param => this.PodeInserir());

                return abrirInserirCommand;
            }
        }

        public ICommand AbrirAlterarCommand
        {
            get
            {
                if (abrirAlterarCommand == null)
                    abrirAlterarCommand = new RelayCommand(param => this.AbrirAlteracao(), param => this.PodeAlterar());

                return abrirAlterarCommand;
            }
        }

        public ICommand FecharTelaCommand
        {
            get
            {
                if (fecharTelaCommand == null)
                    fecharTelaCommand = new RelayCommand(param => this.FecharTela(), param => this.PodeFecharTela());

                return fecharTelaCommand;
            }
        }

        protected virtual bool PodeDeletar() => ObjetoSelecionado != null;
        protected virtual bool PodeFecharTela() => true;
        protected virtual bool PodeSalvar() => ObjetoCadastro != null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract void Deletar();
        protected abstract void Salvar();

        protected void AbrirConsulta()
        {
            HabilitarEdicao = false;
            ObjetoCadastro = ObjetoSelecionado;
            telaCadastro.Abrir(this);
        }

        protected void AbrirInsercao()
        {
            HabilitarEdicao = true;
            ObjetoCadastro = new T();
            telaCadastro.Abrir(this);
        }

        protected void AbrirAlteracao()
        {
            HabilitarEdicao = true;
            ObjetoCadastro = ObjetoSelecionado;
            telaCadastro.Abrir(this);
        }

        protected virtual bool PodeInserir() => true;
        protected virtual bool PodeConsultar() => ObjetoSelecionado != null;
        protected virtual bool PodeAlterar() => ObjetoSelecionado != null;

        private bool PodeSalvarObjeto()
        {
            return HabilitarEdicao && PodeSalvar();
        }

        private void SalvarObjeto()
        {
            Salvar();
            HabilitarEdicao = true;
            ObjetoCadastro = default(T);
            telaCadastro.Fechar();
        }

        private void FecharTela()
        {
            telaCadastro.Fechar();
        }

        private void Notificar(object obj, [CallerMemberName] string nomePropriedade = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private ObservableCollection<T> lista;
        private RelayCommand salvarCommand;
        private RelayCommand excluirCommand;
        private RelayCommand abrirConsultarCommand;
        private RelayCommand abrirInserirCommand;
        private RelayCommand abrirAlterarCommand;
        private RelayCommand fecharTelaCommand;
        private ITelaCadastro<T> telaCadastro;
        private bool habilitarEdicao = true;
        private T objetoSelecionado = default(T);
        private T objetoCadastro = default(T);
        private string nomeEntidade = "";
        private string nomeEntidadePlural = "";
    }
}
