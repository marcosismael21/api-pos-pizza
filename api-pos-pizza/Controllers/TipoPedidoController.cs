using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPedidoController : ControllerBase
    {
        private readonly ITipoPedidoRepository _tipoPedidoRepository;

        public TipoPedidoController(ITipoPedidoRepository tipoPedidoRepository)
        {
            _tipoPedidoRepository = tipoPedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var tipoPedido = await _tipoPedidoRepository.GetAll();

                var tipoPedidoDTO = tipoPedido.Select(tp => new TipoPedidoDTO
                {
                    Id = tp.Id,
                    Nombre = tp.Nombre,
                    Estado = tp.Estado
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = tipoPedidoDTO
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
                var tipoPedido = await _tipoPedidoRepository.GetById(id);

                if (tipoPedido == null)
                    return NotFound(new { message = "Tipo de Pedido no encontrado" });

                var tipoPedidoDTO = new TipoPedidoDTO
                {
                    Id = tipoPedido.Id,
                    Nombre = tipoPedido.Nombre,
                    Estado = tipoPedido.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = tipoPedidoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateTipoPedidoDTO tipoPedidoDTO)
        {
            try
            {
                var tipoPedido = new TipoPedido
                {
                    Nombre = tipoPedidoDTO.Nombre,
                    Estado = tipoPedidoDTO.Estado
                };

                await _tipoPedidoRepository.Create(tipoPedido);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = tipoPedido
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateTipoPedidoDTO tipoPedidoDTO)
        {
            try
            {
                var tipoPedido = new TipoPedido
                {
                    Nombre = tipoPedidoDTO.Nombre,
                    Estado = tipoPedidoDTO.Estado
                };

                var tipoPedidoActualizada = await _tipoPedidoRepository.Update(id, tipoPedido);

                if (tipoPedidoActualizada == null)
                    return NotFound(new { message = "Tipo de Pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = tipoPedido
                });
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
                var resultado = await _tipoPedidoRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Tipo de Pedido no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
