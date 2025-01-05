using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IDetallePedidoRepository
    {
        Task<IEnumerable<DetallePedido>> GetAll();
        Task<DetallePedido?> GetById(int id);
        Task<DetallePedido> Create(DetallePedido detallePedido);
        Task<DetallePedido?> Update(int id, DetallePedido detallePedido);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
        Task<List<DetallePedido>> CreateMultiple(List<DetallePedido> detalles);
        Task<bool> FinalizarDetallesPedido(int idPedido);
    }
}
