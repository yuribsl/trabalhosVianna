using MeuFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDominio.Models
{
    public class PedidoItem : Modelo
    {
        public string IdProduto { get => Produto == null ? (Guid.NewGuid()).ToString() : Produto.Id; }
        public Produto Produto { get; set; }     

        public Pedido Pedido { get; set; }
        public string IdPedido { get => Pedido == null ? idPedido : Pedido.Id; set => IdPedido = value; }

        public double Quantidade { get; set; }

        public double ValorTotal
        {
            get { return Produto == null ? 0 : Quantidade * Produto.Preco; }
        }

        private string idPedido = Guid.NewGuid().ToString();
    }
}
