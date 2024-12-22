using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class DireccionClienteRepository : IDireccionClienteRepository
    {
        private readonly DBAPIContext _context;

        public DireccionClienteRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DireccionCliente>> GetAll()
        {
            return await _context.DireccionClientes
                .Include(dc => dc.IdClienteNavigation)
                .ToListAsync();
        }

        public async Task<DireccionCliente?> GetById(int id)
        {
            return await _context.DireccionClientes
                .Include(d => d.IdClienteNavigation)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DireccionCliente> Create(DireccionCliente direccionCliente)
        {
            _context.DireccionClientes.Add(direccionCliente);
            await _context.SaveChangesAsync();
            return direccionCliente;
        }

        public async Task<DireccionCliente?> Update(int id, DireccionCliente direccionCliente)
        {
            var existingDireccionCliente = await _context.DireccionClientes.FindAsync(id);

            if (existingDireccionCliente == null)
                return null;

            existingDireccionCliente.IdCliente = direccionCliente.IdCliente;
            existingDireccionCliente.Alias = direccionCliente.Alias ?? existingDireccionCliente.Alias;
            existingDireccionCliente.Direccion = direccionCliente.Direccion ?? existingDireccionCliente.Direccion;

            await _context.SaveChangesAsync();
            return existingDireccionCliente;
        }

        public async Task<bool> Delete(int id)
        {
            var direccionCliente = await _context.DireccionClientes.FindAsync(id);

            if (direccionCliente == null)
                return false;

            _context.DireccionClientes.Remove(direccionCliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.DireccionClientes.AnyAsync(c => c.Id == id);
        }
    }
}
