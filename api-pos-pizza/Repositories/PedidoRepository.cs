using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DBAPIContext _context;

        public PedidoRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdDireccionNavigation)
                .Include(p => p.IdTipoPedidoNavigation)
                .ToListAsync();
        }

        public async Task<Pedido?> GetById(int id)
        {
            return await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdDireccionNavigation)
                .Include(p => p.IdTipoPedidoNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> Create(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido?> Update(int id, Pedido pedido)
        {
            var existingPedido = await _context.Pedidos.FindAsync(id);

            if (existingPedido == null)
                return null;

            existingPedido.IdCliente = pedido.IdCliente;
            existingPedido.IdTipoPedido = pedido.IdTipoPedido;
            existingPedido.IdDireccion = pedido.IdDireccion;
            existingPedido.DireccionPersonalizada = pedido.DireccionPersonalizada ?? existingPedido.DireccionPersonalizada;
            existingPedido.Subtotal = pedido.Subtotal ?? existingPedido.Subtotal;
            existingPedido.Impuesto = pedido.Impuesto ?? existingPedido.Impuesto;
            existingPedido.Descuento = pedido.Descuento ?? existingPedido.Descuento;
            existingPedido.Total = pedido.Total ?? existingPedido.Total;
            existingPedido.Subtotal = pedido.Subtotal ?? existingPedido.Subtotal;
            existingPedido.Fecha = pedido.Fecha ?? existingPedido.Fecha;

            await _context.SaveChangesAsync();
            return existingPedido;
        }

        public async Task<bool> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Pedidos.AnyAsync(p => p.Id == id);
        }
    }
}
