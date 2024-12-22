namespace api_pos_pizza.DTOs
{
    public class DetallePedidoDTO
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? PedidoDescripcion { get; set; }
        public string? ProductoDescripcion { get; set; }
    }

    public class CreateDetallePedidoDTO
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
    }

    public class UpdateDetallePedidoDTO
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
