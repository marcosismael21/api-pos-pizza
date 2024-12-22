namespace api_pos_pizza.DTOs
{

    public class TipoPedidoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }

    }

    public class CreateTipoPedidoDTO
    {
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateTipoPedidoDTO
    {
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }
    }
}
