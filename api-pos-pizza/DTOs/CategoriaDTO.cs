namespace api_pos_pizza.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }

    }

    public class CreateCategoriaDTO
    {
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateCategoriaDTO
    {
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }
    }
}
