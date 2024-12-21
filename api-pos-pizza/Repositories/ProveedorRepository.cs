using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly DBAPIContext _context;

        public ProveedorRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> GetAll()
        {
            return await _context.Proveedors.ToListAsync();
        }

        public async Task<Proveedor?> GetById(int id)
        {
            return await _context.Proveedors.FindAsync(id);
        }

        public async Task<Proveedor> Create(Proveedor proveedor)
        {
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<Proveedor?> Update(int id, Proveedor proveedor)
        {
            var existingProveedor = await _context.Proveedors.FindAsync(id);

            if (existingProveedor == null)
                return null;



            existingProveedor.NombreComercio = proveedor.NombreComercio ?? existingProveedor.NombreComercio;
            existingProveedor.NombreProveedor = proveedor.NombreComercio ?? existingProveedor.NombreComercio;
            existingProveedor.Rtn = proveedor.Rtn ?? existingProveedor.Rtn;
            existingProveedor.Telefono = proveedor.Telefono ?? existingProveedor.Telefono;
            existingProveedor.Correo = proveedor.Correo ?? existingProveedor.Correo;
            existingProveedor.Estado = proveedor.Estado ?? existingProveedor.Estado;

            await _context.SaveChangesAsync();
            return existingProveedor;
        }

        public async Task<bool> Delete(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
                return false;

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Proveedors.AnyAsync(p => p.Id == id);
        }
    }
}
