namespace api_pos_pizza.DTOs
{
    public class CreateMultipleDetallePedidoDTO
    {
        public int IdPedido { get; set; }
        public List<DetalleProductoDTO> Productos { get; set; }
    }

    public class DetalleProductoDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
