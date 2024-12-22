using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAll();
        Task<Pedido?> GetById(int id);
        Task<Pedido> Create(Pedido pedido);
        Task<Pedido?> Update(int id, Pedido pedido);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
