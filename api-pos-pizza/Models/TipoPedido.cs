using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class TipoPedido
    {
        public TipoPedido()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
