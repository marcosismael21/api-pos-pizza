namespace api_pos_pizza.DTOs
{
    public class DireccionClienteDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string? Alias { get; set; }
        public string? Direccion { get; set; }
        public string? ClienteDescripcion { get; set; }

    }

    public class CreateDireccionClienteDTO
    {
        public int IdCliente { get; set; }
        public string? Alias { get; set; }
        public string? Direccion { get; set; }
    }

    public class UpdateDireccionClienteDTO
    {
        public int IdCliente { get; set; }
        public string? Alias { get; set; }
        public string? Direccion { get; set; }
    }
}
