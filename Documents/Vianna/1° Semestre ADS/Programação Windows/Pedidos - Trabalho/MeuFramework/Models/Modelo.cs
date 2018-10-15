using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeuFramework.Models
{
    public class Modelo : INotifyPropertyChanged
    {
        private string id = "";
        public string Id { get => id; set => Notificar(id = value); }
        public object AssinaturaAlteracao { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notificar(object obj, [CallerMemberName] string nomePropriedade = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

   

    
    }
}
