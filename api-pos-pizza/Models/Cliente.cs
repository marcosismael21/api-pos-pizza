using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            DireccionClientes = new HashSet<DireccionCliente>();
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rtn { get; set; }
        public string? Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<DireccionCliente> DireccionClientes { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
