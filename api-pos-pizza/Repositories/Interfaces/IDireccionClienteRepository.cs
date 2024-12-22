using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IDireccionClienteRepository
    {
        Task<IEnumerable<DireccionCliente>> GetAll();
        Task<DireccionCliente?> GetById(int id);
        Task<DireccionCliente> Create(DireccionCliente direccionCliente);
        Task<DireccionCliente?> Update(int id, DireccionCliente direccionCliente);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
