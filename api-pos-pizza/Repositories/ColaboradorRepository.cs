using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using api_pos_pizza.Services;
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
            var usuarioExistente = await _context.Colaboradors
                .AnyAsync(c => c.Usuario == colaborador.Usuario);

            if (usuarioExistente)
            {
                throw new Exception("Ya existe un colaborador con este nombre de usuario");
            }

            colaborador.Clave = PasswordService.HashPassword(colaborador.Clave);
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<Colaborador?> Update(int id, Colaborador colaborador)
        {
            var existingColaborador = await _context.Colaboradors.FindAsync(id);
            if (existingColaborador == null)
                return null;

            existingColaborador.Idrol = colaborador.Idrol;
            existingColaborador.Nombres = colaborador.Nombres ?? existingColaborador.Nombres;
            existingColaborador.Dni = colaborador.Dni ?? existingColaborador.Dni;
            existingColaborador.Correo = colaborador.Correo ?? existingColaborador.Correo;
            existingColaborador.Telefono = colaborador.Telefono ?? existingColaborador.Telefono;
            existingColaborador.Usuario = colaborador.Usuario ?? existingColaborador.Usuario;

            if (!string.IsNullOrEmpty(colaborador.Clave))
            {
                existingColaborador.Clave = PasswordService.HashPassword(colaborador.Clave);
            }

            existingColaborador.Estado = colaborador.Estado ?? existingColaborador.Estado;
            await _context.SaveChangesAsync();
            return existingColaborador;
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

        public async Task<bool> ExisteUsuario(string usuario)
        {
            return await _context.Colaboradors
                .AnyAsync(c => c.Usuario == usuario);
        }
    }
}
