using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Colaborador
    {
        public int Id { get; set; }
        public int Idrol { get; set; }
        public string Nombres { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Usuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual Rol IdrolNavigation { get; set; } = null!;
    }
}
