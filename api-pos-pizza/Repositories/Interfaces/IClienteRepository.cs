using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente?> GetById(int id);
        Task<Cliente> Create(Cliente cliente);
        Task<Cliente?> Update(int id, Cliente cliente);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
