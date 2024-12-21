using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DBAPIContext _context;

        public ProductoRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            return await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .ToListAsync();
        }

        public async Task<Producto?> GetById(int id)
        {
            return await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> Create(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto?> Update(int id, Producto producto)
        {
            var existingProducto = await _context.Productos.FindAsync(id);

            if (existingProducto == null)
                return null;

            existingProducto.Nombre = producto.Nombre ?? existingProducto.Nombre;
            existingProducto.CodigoBarra = producto.CodigoBarra ?? existingProducto.CodigoBarra;
            existingProducto.Descripcion = producto.Descripcion ?? existingProducto.Descripcion;
            existingProducto.IdCategoria = producto.IdCategoria;
            existingProducto.IdProveedor = producto.IdProveedor;
            existingProducto.Precio = producto.Precio ?? existingProducto.Precio;

            await _context.SaveChangesAsync();
            return existingProducto;
        }

        public async Task<bool> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Productos.AnyAsync(p => p.Id == id);
        }
    }
}