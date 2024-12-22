using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Colaborador
    {
        public int Id { get; set; }
        public int Idrol { get; set; }
        public string? Nombres { get; set; }
        public string? Dni { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public bool? Estado { get; set; }

        public virtual Rol IdrolNavigation { get; set; } = null!;
    }
}
