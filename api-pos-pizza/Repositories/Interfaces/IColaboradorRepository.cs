using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<IEnumerable<Colaborador>> GetAll();
        Task<Colaborador?> GetById(int id);
        Task<Colaborador> Create(Colaborador colaborador);
        Task<Colaborador?> Update(int id, Colaborador colaborador);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
        Task<bool> ExisteUsuario(string usuario);
    }
}
