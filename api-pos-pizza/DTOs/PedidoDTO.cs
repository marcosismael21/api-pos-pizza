namespace api_pos_pizza.DTOs
{
    public class PedidoDTO
    {

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoPedido { get; set; }
        public int? IdDireccion { get; set; }
        public string? DireccionPersonalizada { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Fecha { get; set; }
        public string? ClienteDescripcion { get; set; }
        public string? TipoPedidoDescripcion { get; set; }
        public string? DireccionDescripcion { get; set; }
    }

    public class CreatePedidoDTO
    {
        public int IdCliente { get; set; }
        public int IdTipoPedido { get; set; }
        public int? IdDireccion { get; set; }
        public string? DireccionPersonalizada { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class UpdatePedidoDTO
    {
        public int IdCliente { get; set; }
        public int IdTipoPedido { get; set; }
        public int? IdDireccion { get; set; }
        public string? DireccionPersonalizada { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
