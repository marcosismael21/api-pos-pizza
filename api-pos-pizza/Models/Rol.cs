using System;
using System.Collections.Generic;

namespace api_pos_pizza.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Colaboradors = new HashSet<Colaborador>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Colaborador> Colaboradors { get; set; }
    }
}
