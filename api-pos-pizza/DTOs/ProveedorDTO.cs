namespace api_pos_pizza.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string? NombreComercio { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Rtn { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }

    public class CreateProveedorDTO
    {
        public string? NombreComercio { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Rtn { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateProveedorDTO
    {
        public string? NombreComercio { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Rtn { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }
}
