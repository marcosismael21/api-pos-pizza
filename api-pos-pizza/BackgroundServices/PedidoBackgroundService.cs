using api_pos_pizza.Data;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.BackgroundServices
{
    public class PedidoBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PedidoBackgroundService> _logger;

        public PedidoBackgroundService(IServiceScopeFactory scopeFactory, ILogger<PedidoBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<DBAPIContext>();

                    var pedidosConDetallesActivos = await dbContext.Pedidos
                        .Include(p => p.DetallePedidos.Where(d => d.Estado == true))
                        .Where(p => p.DetallePedidos.Any(d => d.Estado == true))
                        .ToListAsync();

                    foreach (var pedido in pedidosConDetallesActivos)
                    {
                        var subtotal = pedido.DetallePedidos.Sum(d => d.Subtotal ?? 0);
                        pedido.Subtotal = subtotal;
                        pedido.Impuesto = subtotal * 0.15m;
                        pedido.Total = subtotal + pedido.Impuesto - (pedido.Descuento ?? 0);
                    }

                    await dbContext.SaveChangesAsync();
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en el procesamiento de pedidos");
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }
    }
}
