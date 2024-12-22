using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;

        public RolController(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var rol = await _rolRepository.GetAll();

                var rolDTO = rol.Select(r => new RolDTO
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Estado = r.Estado
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = rolDTO
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
                var rol = await _rolRepository.GetById(id);

                if (rol == null)
                    return NotFound(new { message = "Rol no encontrado" });

                var rolDTO = new RolDTO
                {
                    Id = rol.Id,
                    Nombre = rol.Nombre,
                    Estado = rol.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = rolDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateRolDTO rolDTO)
        {
            try
            {
                var rol = new Rol
                {
                    Nombre = rolDTO.Nombre,
                    Estado = rolDTO.Estado
                };

                await _rolRepository.Create(rol);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = rol
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateRolDTO rolDTO)
        {
            try
            {
                var rol = new Rol
                {
                    Nombre = rolDTO.Nombre,
                    Estado = rolDTO.Estado
                };

                var rolActualizado = await _rolRepository.Update(id, rol);

                if (rolActualizado == null)
                    return NotFound(new { message = "Rol no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = rol
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
                var resultado = await _rolRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Rol no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
