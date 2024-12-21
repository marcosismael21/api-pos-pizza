using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Precio { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
    }
}
