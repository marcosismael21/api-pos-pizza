using api_pos_pizza.Data;
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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var productos = await _productoRepository.GetAll();

                var productosDTO = productos.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CodigoBarra = p.CodigoBarra,
                    Descripcion = p.Descripcion,
                    IdCategoria = p.IdCategoria,
                    IdProveedor = p.IdProveedor,
                    Precio = p.Precio,
                    CategoriaDescripcion = p.IdCategoriaNavigation?.Descripcion,
                    NombreProveedor = p.IdProveedorNavigation?.NombreProveedor
                });

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = productosDTO
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
                var producto = await _productoRepository.GetById(id);

                if (producto == null)
                    return NotFound(new { message = "Producto no encontrado" });

                var productoDTO = new ProductoDTO
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    CodigoBarra = producto.CodigoBarra,
                    Descripcion = producto.Descripcion,
                    IdCategoria = producto.IdCategoria,
                    IdProveedor = producto.IdProveedor,
                    Precio = producto.Precio,
                    CategoriaDescripcion = producto.IdCategoriaNavigation?.Descripcion,
                    NombreProveedor = producto.IdProveedorNavigation?.NombreProveedor
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "ok",
                    response = productoDTO
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CreateProductoDTO productoDTO)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = productoDTO.Nombre,
                    CodigoBarra = productoDTO.CodigoBarra,
                    Descripcion = productoDTO.Descripcion,
                    IdCategoria = productoDTO.IdCategoria,
                    IdProveedor = productoDTO.IdProveedor,
                    Precio = productoDTO.Precio
                };

                await _productoRepository.Create(producto);
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] UpdateProductoDTO productoDTO)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = productoDTO.Nombre,
                    CodigoBarra = productoDTO.CodigoBarra,
                    Descripcion = productoDTO.Descripcion,
                    IdCategoria = productoDTO.IdCategoria,
                    IdProveedor = productoDTO.IdProveedor,
                    Precio = productoDTO.Precio
                };

                var productoActualizado = await _productoRepository.Update(id, producto);

                if (productoActualizado == null)
                    return NotFound(new { message = "Producto no encontrado" });

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
                var resultado = await _productoRepository.Delete(id);

                if (!resultado)
                    return NotFound(new { message = "Producto no encontrado" });

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
