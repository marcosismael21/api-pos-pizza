using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var cliente = await _clienteRepository.GetAll();

                var clienteDTO = cliente.Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Rtn = c.Rtn,
                    Dni = c.Dni,
                    Telefono = c.Telefono,
                    Correo = c.Correo,
                    Estado = c.Estado
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = clienteDTO
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
                var cliente = await _clienteRepository.GetById(id);

                if (cliente == null)
                    return NotFound(new { message = "Cliente no encontrado" });

                var clienteDTO = new ClienteDTO
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Rtn = cliente.Rtn,
                    Dni = cliente.Dni,
                    Telefono = cliente.Telefono,
                    Correo = cliente.Correo,
                    Estado = cliente.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = clienteDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateClienteDTO clienteDTO)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = clienteDTO.Nombre,
                    Rtn = clienteDTO.Rtn,
                    Dni = clienteDTO.Dni,
                    Telefono = clienteDTO.Telefono,
                    Correo = clienteDTO.Correo,
                    Estado = clienteDTO.Estado
                };

                await _clienteRepository.Create(cliente);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = cliente
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateClienteDTO clienteDTO)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = clienteDTO.Nombre,
                    Rtn = clienteDTO.Rtn,
                    Dni = clienteDTO.Dni,
                    Telefono = clienteDTO.Telefono,
                    Correo = clienteDTO.Correo,
                    Estado = clienteDTO.Estado
                };

                var clienteActualizado = await _clienteRepository.Update(id, cliente);

                if (clienteActualizado == null)
                    return NotFound(new { message = "Cliente no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = cliente
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
                var resultado = await _clienteRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Cliente no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
