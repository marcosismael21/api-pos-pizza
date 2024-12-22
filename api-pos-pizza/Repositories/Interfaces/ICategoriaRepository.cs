using api_pos_pizza.Models;

namespace api_pos_pizza.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categorium>> GetAll();
        Task<Categorium?> GetById(int id);
        Task<Categorium> Create(Categorium categoria);
        Task<Categorium?> Update(int id, Categorium categoria);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
