namespace api_pos_pizza.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Precio { get; set; }
        public string? CategoriaDescripcion { get; set; }
        public string? NombreProveedor { get; set; }
    }

    public class CreateProductoDTO
    {
        public string? Nombre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Precio { get; set; }
    }

    public class UpdateProductoDTO
    {
        public string? Nombre { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Precio { get; set; }
    }
}
