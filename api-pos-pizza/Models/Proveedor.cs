using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string? NombreComercio { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Rtn { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
