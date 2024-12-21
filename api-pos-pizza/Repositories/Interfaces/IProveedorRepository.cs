using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IProveedorRepository
    {

        Task<IEnumerable<Proveedor>> GetAll();
        Task<Proveedor?> GetById(int id);
        Task<Proveedor> Create(Proveedor proveedor);
        Task<Proveedor?> Update(int id, Proveedor proveedor);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);

    }
}
