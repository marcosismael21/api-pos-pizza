using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class DireccionCliente
    {
        public DireccionCliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string? Alias { get; set; }
        public string? Direccion { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
