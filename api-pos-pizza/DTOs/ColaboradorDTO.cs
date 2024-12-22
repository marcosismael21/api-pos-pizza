namespace api_pos_pizza.DTOs
{
    public class ColaboradorDTO
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
        public string? RolDescripcion { get; set; }
    }

    public class CreateColaboradorDTO
    {
        public int Idrol { get; set; }
        public string? Nombres { get; set; }
        public string? Dni { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateColaboradorDTO
    {
        public int Idrol { get; set; }
        public string? Nombres { get; set; }
        public string? Dni { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public bool? Estado { get; set; }
    }
}
