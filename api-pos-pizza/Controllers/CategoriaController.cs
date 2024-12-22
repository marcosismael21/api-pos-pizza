using api_pos_pizza.DTOs;
using api_pos_pizza.Models;
using api_pos_pizza.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var categoria = await _categoriaRepository.GetAll();

                var categoriaDTO = categoria.Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Descripcion = c.Descripcion,
                    Estado = c.Estado
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = categoriaDTO
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
                var categoria = await _categoriaRepository.GetById(id);

                if (categoria == null)
                    return NotFound(new { message = "Categoria no encontrada" });

                var categoriaDTO = new CategoriaDTO
                {
                    Id = categoria.Id,
                    Descripcion = categoria.Descripcion,
                    Estado = categoria.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = categoriaDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateCategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = new Categorium
                {
                    Descripcion = categoriaDTO.Descripcion,
                    Estado = categoriaDTO.Estado
                };

                await _categoriaRepository.Create(categoria);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = categoria
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateCategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = new Categorium
                {
                    Descripcion = categoriaDTO.Descripcion,
                    Estado = categoriaDTO.Estado
                };

                var categoriaActualizada = await _categoriaRepository.Update(id, categoria);

                if (categoriaActualizada == null)
                    return NotFound(new { message = "Categoria no encontrada" });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = categoria
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
                var resultado = await _categoriaRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Categoria no encontrada" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
