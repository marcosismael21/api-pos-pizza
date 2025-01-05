using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly DBAPIContext _context;

        public DetallePedidoRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetallePedido>> GetAll()
        {
            return await _context.DetallePedidos
                .Include(dp => dp.IdPedidoNavigation)
                .Include(dp => dp.IdProductoNavigation)
                .ToListAsync();
        }

        public async Task<DetallePedido?> GetById(int id)
        {
            return await _context.DetallePedidos
                .Include(dp => dp.IdPedidoNavigation)
                .Include(dp => dp.IdProductoNavigation)
                .FirstOrDefaultAsync(dp => dp.Id == id);
        }

        public async Task<DetallePedido> Create(DetallePedido detallePedido)
        {
            _context.DetallePedidos.Add(detallePedido);
            await _context.SaveChangesAsync();
            return detallePedido;
        }

        public async Task<DetallePedido?> Update(int id, DetallePedido detallePedido)
        {
            var existingDetalleProducto = await _context.DetallePedidos.FindAsync(id);

            if (existingDetalleProducto == null)
                return null;

            existingDetalleProducto.IdPedido = detallePedido.IdPedido;
            existingDetalleProducto.IdProducto = detallePedido.IdProducto;
            existingDetalleProducto.Cantidad = detallePedido.Cantidad;
            existingDetalleProducto.PrecioUnitario = detallePedido.PrecioUnitario ?? existingDetalleProducto.PrecioUnitario;
            existingDetalleProducto.Subtotal = detallePedido.Subtotal ?? existingDetalleProducto.Subtotal;
            existingDetalleProducto.Estado = detallePedido.Estado ?? existingDetalleProducto.Estado;

            await _context.SaveChangesAsync();
            return existingDetalleProducto;
        }

        public async Task<bool> Delete(int id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
                return false;

            _context.DetallePedidos.Remove(detallePedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.DetallePedidos.AnyAsync(dp => dp.Id == id);
        }

        public async Task<List<DetallePedido>> CreateMultiple(List<DetallePedido> detalles)
        {
            foreach (var detalle in detalles)
            {
                detalle.Estado = true;
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
            }

            _context.DetallePedidos.AddRange(detalles);
            await _context.SaveChangesAsync();
            return detalles;
        }

        public async Task<bool> FinalizarDetallesPedido(int idPedido)
        {
            var detalles = await _context.DetallePedidos
                .Where(d => d.IdPedido == idPedido && d.Estado == true)
                .ToListAsync();

            foreach (var detalle in detalles)
            {
                detalle.Estado = false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
