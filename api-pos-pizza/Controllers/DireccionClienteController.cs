using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionClienteController : ControllerBase
    {
        private readonly IDireccionClienteRepository _direccionClienteRepository;

        public DireccionClienteController(IDireccionClienteRepository direccionClienteRepository)
        {
            _direccionClienteRepository = direccionClienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var direccionCliente = await _direccionClienteRepository.GetAll();

                var direccionClienteDTO = direccionCliente.Select(dc => new DireccionClienteDTO
                {
                    Id = dc.Id,
                    IdCliente = dc.IdCliente,
                    Alias = dc.Alias,
                    Direccion = dc.Direccion,
                    ClienteDescripcion = dc.IdClienteNavigation?.Nombre,
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = direccionClienteDTO
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
                var direccionCliente = await _direccionClienteRepository.GetById(id);

                if (direccionCliente == null)
                    return NotFound(new { message = "Dirección no encontrada" });

                var direccionClienteDTO = new DireccionClienteDTO
                {
                    Id = direccionCliente.Id,
                    IdCliente = direccionCliente.IdCliente,
                    Alias = direccionCliente.Alias,
                    Direccion = direccionCliente.Direccion,
                    ClienteDescripcion = direccionCliente.IdClienteNavigation?.Nombre,
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = direccionClienteDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateDireccionClienteDTO direccionClienteDTO)
        {
            try
            {
                var direccionCliente = new DireccionCliente
                {
                    IdCliente = direccionClienteDTO.IdCliente,
                    Alias = direccionClienteDTO.Alias,
                    Direccion = direccionClienteDTO.Direccion,
                };

                await _direccionClienteRepository.Create(direccionCliente);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateDireccionClienteDTO direccionClienteDTO)
        {
            try
            {
                var direccionCliente = new DireccionCliente
                {
                    IdCliente = direccionClienteDTO.IdCliente,
                    Alias = direccionClienteDTO.Alias,
                    Direccion = direccionClienteDTO.Direccion,
                };

                var direccionClienteActualizado = await _direccionClienteRepository.Update(id, direccionCliente);

                if (direccionClienteActualizado == null)
                    return NotFound(new { message = "Dirección no encontrada" });

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
                var resultado = await _direccionClienteRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Dirección no encontrada" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
