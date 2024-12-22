namespace api_pos_pizza.DTOs
{
    public class RolDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

    }

    public class CreateRolDTO
    {
        public string Nombre { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateRolDTO
    {
        public string Nombre { get; set; }
        public bool? Estado { get; set; }
    }
}
