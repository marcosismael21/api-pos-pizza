namespace api_pos_pizza.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rtn { get; set; }
        public string? Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }

    public class CreateClienteDTO
    {
        public string? Nombre { get; set; }
        public string? Rtn { get; set; }
        public string? Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateClienteDTO
    {
        public string? Nombre { get; set; }
        public string? Rtn { get; set; }
        public string? Dni { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool? Estado { get; set; }
    }
}
