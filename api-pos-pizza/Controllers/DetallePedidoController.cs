using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoRepository _detallePedidoRepository;

        public DetallePedidoController(IDetallePedidoRepository detallePedidoRepository)
        {
            _detallePedidoRepository = detallePedidoRepository;
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var detallePedidos = await _detallePedidoRepository.GetAll();

                var detallePedidoDTO = detallePedidos.Select(pd => new DetallePedidoDTO
                {
                    Id = pd.Id,
                    IdPedido = pd.IdPedido,
                    IdProducto = pd.IdProducto,
                    Cantidad = pd.Cantidad,
                    PrecioUnitario = pd.PrecioUnitario,
                    Subtotal = pd.Subtotal,
                    PedidoDescripcion = pd.IdPedidoNavigation?.Total,
                    ProductoDescripcion = pd.IdProductoNavigation?.Nombre
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = detallePedidoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                var detallePedido = await _detallePedidoRepository.GetById(id);

                if (detallePedido == null)
                    return NotFound(new { message = "Detalle del pedido no encontrado" });

                var detalleProductoDTO = new DetallePedidoDTO
                {
                    Id = detallePedido.Id,
                    IdPedido = detallePedido.IdPedido,
                    IdProducto = detallePedido.IdProducto,
                    Cantidad = detallePedido.Cantidad,
                    PrecioUnitario = detallePedido.PrecioUnitario,
                    Subtotal = detallePedido.Subtotal,
                    PedidoDescripcion = detallePedido.IdPedidoNavigation?.Total,
                    ProductoDescripcion = detallePedido.IdProductoNavigation?.Nombre
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = detalleProductoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateDetallePedidoDTO detallePedidoDTO)
        {
            try
            {
                var detallePedido = new DetallePedido
                {
                    IdPedido = detallePedidoDTO.IdPedido,
                    IdProducto = detallePedidoDTO.IdProducto,
                    Cantidad = detallePedidoDTO.Cantidad,
                    PrecioUnitario = detallePedidoDTO.PrecioUnitario,
                    Subtotal = detallePedidoDTO.Subtotal
                };

                await _detallePedidoRepository.Create(detallePedido);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateDetallePedidoDTO detallePedidoDTO)
        {
            try
            {
                var detallePedido = new DetallePedido
                {
                    IdPedido = detallePedidoDTO.IdPedido,
                    IdProducto = detallePedidoDTO.IdProducto,
                    Cantidad = detallePedidoDTO.Cantidad,
                    PrecioUnitario = detallePedidoDTO.PrecioUnitario,
                    Subtotal = detallePedidoDTO.Subtotal
                };

                var detallePedidoActualizado = await _detallePedidoRepository.Update(id, detallePedido);

                if (detallePedidoActualizado == null)
                    return NotFound(new { message = "Detalle del pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _detallePedidoRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Detalle del pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
