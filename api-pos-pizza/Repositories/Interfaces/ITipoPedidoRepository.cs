using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface ITipoPedidoRepository
    {
        Task<IEnumerable<TipoPedido>> GetAll();
        Task<TipoPedido?> GetById(int id);
        Task<TipoPedido> Create(TipoPedido tipoPedido);
        Task<TipoPedido?> Update(int id, TipoPedido tipoPedido);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
