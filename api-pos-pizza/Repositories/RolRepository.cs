using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DBAPIContext _context;

        public RolRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAll()
        {
            return await _context.Rols.ToListAsync();
        }

        public async Task<Rol?> GetById(int id)
        {
            return await _context.Rols.FindAsync(id);
        }

        public async Task<Rol> Create(Rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol?> Update(int id, Rol rol)
        {
            var existingRol = await _context.Rols.FindAsync(id);

            if (existingRol == null)
                return null;

            existingRol.Nombre = rol.Nombre ?? existingRol.Nombre;
            existingRol.Estado = rol.Estado ?? existingRol.Estado;

            await _context.SaveChangesAsync();
            return existingRol;
        }

        public async Task<bool> Delete(int id)
        {
            var rol = await _context.Rols.FindAsync(id);

            if (rol == null)
                return false;

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Rols.AnyAsync(r => r.Id == id);
        }
    }
}
