using MeuFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWpf.Models
{
    public class Atleta : Modelo
    {
        public string Nome { get => nome; set => Notificar(nome = value); }

        public double Altura { get => altura; set => Notificar(altura = value); }

        private string nome;
        private double altura;
    }
}
