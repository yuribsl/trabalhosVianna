using MeuFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.Models
{
    public class Produto : Modelo
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
