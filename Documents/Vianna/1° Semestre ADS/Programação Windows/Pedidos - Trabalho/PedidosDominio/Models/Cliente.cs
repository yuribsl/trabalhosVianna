using MeuFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.Models
{
    public class Cliente : Modelo
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
