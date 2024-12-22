using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DBAPIContext _context;

        public ClienteRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> Update(int id, Cliente cliente)
        {
            var existingCliente = await _context.Clientes.FindAsync(id);

            if (existingCliente == null)
                return null;

            existingCliente.Nombre = cliente.Nombre ?? existingCliente.Nombre;
            existingCliente.Rtn = cliente.Rtn ?? existingCliente.Rtn;
            existingCliente.Dni = cliente.Dni ?? existingCliente.Dni;
            existingCliente.Telefono = cliente.Telefono ?? existingCliente.Telefono;
            existingCliente.Correo = cliente.Correo ?? existingCliente.Correo;
            existingCliente.Estado = cliente.Estado ?? existingCliente.Estado;

            await _context.SaveChangesAsync();
            return existingCliente;
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }
    }
}
