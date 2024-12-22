using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoPedido { get; set; }
        public int? IdDireccion { get; set; }
        public string? DireccionPersonalizada { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual DireccionCliente? IdDireccionNavigation { get; set; }
        public virtual TipoPedido IdTipoPedidoNavigation { get; set; } = null!;
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
