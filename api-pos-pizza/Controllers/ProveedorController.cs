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
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorController(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        [Authorize(Roles = "Administrador,Cajero")]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var proveedor = await _proveedorRepository.GetAll();

                var proveedorDTO = proveedor.Select(p => new ProveedorDTO
                {
                    Id = p.Id,
                    NombreComercio = p.NombreComercio,
                    NombreProveedor = p.NombreProveedor,
                    Rtn = p.Rtn,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    Estado = p.Estado
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = proveedorDTO
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
                var proveedor = await _proveedorRepository.GetById(id);

                if (proveedor == null)
                    return NotFound(new { message = "Proveedor no encontrado" });

                var proveedorDTO = new ProveedorDTO
                {
                    Id = proveedor.Id,
                    NombreComercio = proveedor.NombreComercio,
                    NombreProveedor = proveedor.NombreProveedor,
                    Rtn = proveedor.Rtn,
                    Telefono = proveedor.Telefono,
                    Correo = proveedor.Correo,
                    Estado = proveedor.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = proveedorDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateProveedorDTO proveedorDTO)
        {
            try
            {
                var proveedor = new Proveedor
                {
                    NombreComercio = proveedorDTO.NombreComercio,
                    NombreProveedor = proveedorDTO.NombreProveedor,
                    Rtn = proveedorDTO.Rtn,
                    Telefono = proveedorDTO.Telefono,
                    Correo = proveedorDTO.Correo,
                    Estado = proveedorDTO.Estado
                };

                await _proveedorRepository.Create(proveedor);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = proveedor
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateProveedorDTO proveedorDTO)
        {
            try
            {
                var proveedor = new Proveedor
                {
                    NombreComercio = proveedorDTO.NombreComercio,
                    NombreProveedor = proveedorDTO.NombreProveedor,
                    Rtn = proveedorDTO.Rtn,
                    Telefono = proveedorDTO.Telefono,
                    Correo = proveedorDTO.Correo,
                    Estado = proveedorDTO.Estado
                };

                var proveedorActualizado = await _proveedorRepository.Update(id, proveedor);

                if (proveedorActualizado == null)
                    return NotFound(new { message = "Proveedor no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    responde = proveedor
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _proveedorRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Proveedor no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
