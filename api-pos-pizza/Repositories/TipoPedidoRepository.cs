using api_pos_pizza.Data;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Repositories
{
    public class TipoPedidoRepository: ITipoPedidoRepository
    {
        private readonly DBAPIContext _context;

        public TipoPedidoRepository(DBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoPedido>> GetAll()
        {
            return await _context.TipoPedidos.ToListAsync();
        }

        public async Task<TipoPedido?> GetById(int id)
        {
            return await _context.TipoPedidos.FindAsync(id);
        }

        public async Task<TipoPedido> Create(TipoPedido tipoPedido)
        {
            _context.TipoPedidos.Add(tipoPedido);
            await _context.SaveChangesAsync();
            return tipoPedido;
        }

        public async Task<TipoPedido?> Update(int id, TipoPedido tipoPedido)
        {
            var existingTipoPedido = await _context.TipoPedidos.FindAsync(id);

            if (existingTipoPedido == null)
                return null;

            existingTipoPedido.Nombre = tipoPedido.Nombre ?? existingTipoPedido.Nombre;
            existingTipoPedido.Estado = tipoPedido.Estado ?? existingTipoPedido.Estado;

            await _context.SaveChangesAsync();
            return existingTipoPedido;
        }

        public async Task<bool> Delete(int id)
        {
            var tipoPedido = await _context.TipoPedidos.FindAsync(id);

            if (tipoPedido == null)
                return false;

            _context.TipoPedidos.Remove(tipoPedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Categoria.AnyAsync(c => c.Id == id);
        }
    }
}
