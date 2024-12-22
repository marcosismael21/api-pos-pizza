using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAll();
        Task<Rol?> GetById(int id);
        Task<Rol> Create(Rol rol);
        Task<Rol?> Update(int id, Rol rol);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
