using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var pedido = await _pedidoRepository.GetAll();

                var pedidoDTO = pedido.Select(p => new PedidoDTO
                {
                    Id = p.Id,
                    IdCliente = p.IdCliente,
                    IdTipoPedido = p.IdTipoPedido,
                    IdDireccion = p.IdDireccion,
                    DireccionPersonalizada = p.DireccionPersonalizada,
                    Subtotal = p.Subtotal,
                    Impuesto = p.Impuesto,
                    Descuento = p.Descuento,
                    Total = p.Total,
                    Fecha = p.Fecha,
                    ClienteDescripcion = p.IdClienteNavigation?.Nombre,
                    TipoPedidoDescripcion = p.IdTipoPedidoNavigation?.Nombre,
                    DireccionDescripcion = p.IdDireccionNavigation?.Direccion
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = pedidoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                var pedido = await _pedidoRepository.GetById(id);

                if (pedido == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                var pedidoDTO = new PedidoDTO
                {
                    Id = pedido.Id,
                    IdCliente = pedido.IdCliente,
                    IdTipoPedido = pedido.IdTipoPedido,
                    IdDireccion = pedido.IdDireccion,
                    DireccionPersonalizada = pedido.DireccionPersonalizada,
                    Subtotal = pedido.Subtotal,
                    Impuesto = pedido.Impuesto,
                    Descuento = pedido.Descuento,
                    Total = pedido.Total,
                    Fecha = pedido.Fecha,
                    ClienteDescripcion = pedido.IdClienteNavigation?.Nombre,
                    TipoPedidoDescripcion = pedido.IdTipoPedidoNavigation?.Nombre,
                    DireccionDescripcion = pedido.IdDireccionNavigation?.Direccion
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = pedidoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreatePedidoDTO pedidoDTO)
        {
            try
            {
                var pedido = new Pedido
                {
                    IdCliente = pedidoDTO.IdCliente,
                    IdTipoPedido = pedidoDTO.IdTipoPedido,
                    IdDireccion = pedidoDTO.IdDireccion,
                    DireccionPersonalizada = pedidoDTO.DireccionPersonalizada,
                    Subtotal = pedidoDTO.Subtotal,
                    Impuesto = pedidoDTO.Impuesto,
                    Descuento = pedidoDTO.Descuento,
                    Total = pedidoDTO.Total,
                    Fecha = pedidoDTO.Fecha,
                };

                await _pedidoRepository.Create(pedido);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdatePedidoDTO pedidoDTO)
        {
            try
            {
                var pedido = new Pedido
                {
                    IdCliente = pedidoDTO.IdCliente,
                    IdTipoPedido = pedidoDTO.IdTipoPedido,
                    IdDireccion = pedidoDTO.IdDireccion,
                    DireccionPersonalizada = pedidoDTO.DireccionPersonalizada,
                    Subtotal = pedidoDTO.Subtotal,
                    Impuesto = pedidoDTO.Impuesto,
                    Descuento = pedidoDTO.Descuento,
                    Total = pedidoDTO.Total,
                    Fecha = pedidoDTO.Fecha,
                };

                var pedidoActualizado = await _pedidoRepository.Update(id, pedido);

                if (pedidoActualizado == null)
                    return NotFound(new { message = "Pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _pedidoRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
