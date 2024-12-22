using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DBAPIContext _context;

        public CategoriaRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categorium>> GetAll()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categorium?> GetById(int id)
        {
            return await _context.Categoria.FindAsync(id);
        }

        public async Task<Categorium> Create(Categorium categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categorium?> Update(int id, Categorium categoria)
        {
            var existingCategoria = await _context.Categoria.FindAsync(id);

            if (existingCategoria == null)
                return null;

            existingCategoria.Descripcion = categoria.Descripcion ?? existingCategoria.Descripcion;
            existingCategoria.Estado = categoria.Estado ?? existingCategoria.Estado;
            
            await _context.SaveChangesAsync();
            return existingCategoria;
        }

        public async Task<bool> Delete(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if(categoria == null) 
                return false;

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Categoria.AnyAsync(c => c.Id == id);
        }
    }
}
