using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly DBAPIContext _context;

        public ColaboradorRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Colaborador>> GetAll()
        {
            return await _context.Colaboradors
                .Include(c => c.IdrolNavigation)
                .ToListAsync();
        }

        public async Task<Colaborador?> GetById(int id)
        {
            return await _context.Colaboradors
                .Include(c => c.IdrolNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Colaborador> Create(Colaborador colaborador)
        {
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<Colaborador?> Update(int id, Colaborador colaborador)
        {
            var existingColanorador = await _context.Colaboradors.FindAsync(id);

            if (existingColanorador == null)
                return null;

            existingColanorador.Idrol = colaborador.Idrol;
            existingColanorador.Nombres = colaborador.Nombres ?? existingColanorador.Nombres;
            existingColanorador.Dni = colaborador.Dni ?? existingColanorador.Dni;
            existingColanorador.Correo = colaborador.Correo ?? existingColanorador.Correo;
            existingColanorador.Telefono = colaborador.Telefono ?? existingColanorador.Telefono;
            existingColanorador.Usuario = colaborador.Usuario ?? existingColanorador.Usuario;
            existingColanorador.Clave = colaborador.Clave ?? existingColanorador.Clave;
            existingColanorador.Estado = colaborador.Estado ?? existingColanorador.Estado;

            await _context.SaveChangesAsync();
            return existingColanorador;
        }

        public async Task<bool> Delete(int id)
        {
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
                return false;

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Colaboradors.AnyAsync(c => c.Id == id);
        }
    }
}
