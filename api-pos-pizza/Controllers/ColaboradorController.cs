using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var colaborador = await _colaboradorRepository.GetAll();

                var colaboradorDTO = colaborador.Select(c => new ColaboradorDTO
                {
                    Id = c.Id,
                    Idrol = c.Idrol,
                    Nombres = c.Nombres,
                    Dni = c.Dni,
                    Correo = c.Correo,
                    Telefono = c.Telefono,
                    Usuario = c.Usuario,
                    Clave = c.Clave,
                    Estado = c.Estado,
                    RolDescripcion = c.IdrolNavigation?.Nombre
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = colaboradorDTO
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
                var colaborador = await _colaboradorRepository.GetById(id);

                if (colaborador == null)
                    return NotFound(new { message = "Colaborador no encontrado" });

                var colaboradorDTO = new ColaboradorDTO
                {
                    Id = colaborador.Id,
                    Idrol = colaborador.Idrol,
                    Nombres = colaborador.Nombres,
                    Dni = colaborador.Dni,
                    Correo = colaborador.Correo,
                    Telefono = colaborador.Telefono,
                    Usuario = colaborador.Usuario,
                    Clave = colaborador.Clave,
                    Estado = colaborador.Estado,
                    RolDescripcion = colaborador.IdrolNavigation?.Nombre
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = colaboradorDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateColaboradorDTO colaboradorDTO)
        {
            try
            {
                var existeUsuario = await _colaboradorRepository.ExisteUsuario(colaboradorDTO.Usuario);

                if (existeUsuario)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new { message = "Ya existe un colaborador con este nombre de usuario" });
                }

                var colaborador = new Colaborador
                {
                    Idrol = colaboradorDTO.Idrol,
                    Nombres = colaboradorDTO.Nombres,
                    Dni = colaboradorDTO.Dni,
                    Correo = colaboradorDTO.Correo,
                    Telefono = colaboradorDTO.Telefono,
                    Usuario = colaboradorDTO.Usuario,
                    Clave = colaboradorDTO.Clave,
                    Estado = colaboradorDTO.Estado,
                };

                await _colaboradorRepository.Create(colaborador);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateColaboradorDTO colaboradorDTO)
        {
            try
            {
                var colaborador = new Colaborador
                {
                    Idrol = colaboradorDTO.Idrol,
                    Nombres = colaboradorDTO.Nombres,
                    Dni = colaboradorDTO.Dni,
                    Correo = colaboradorDTO.Correo,
                    Telefono = colaboradorDTO.Telefono,
                    Usuario = colaboradorDTO.Usuario,
                    Clave = colaboradorDTO.Clave,
                    Estado = colaboradorDTO.Estado,
                };

                var colanoradorActualizado = await _colaboradorRepository.Update(id, colaborador);

                if (colanoradorActualizado == null)
                    return NotFound(new { message = "Colaborador no encontrado" });

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
                var resultado = await _colaboradorRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Colaborador no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
