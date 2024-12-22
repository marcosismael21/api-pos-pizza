using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_pos_pizza.Services;
using api_pos_pizza.DTOs;
using api_pos_pizza.Data;
using api_pos_pizza.Models;

namespace api_pos_pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DBAPIContext _context;

        public AuthController(IConfiguration config, DBAPIContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var colaborador = await _context.Colaboradors
                    .Include(c => c.IdrolNavigation)
                    .FirstOrDefaultAsync(x =>
                        x.Usuario == login.Usuario &&
                        x.Estado == true);

                if (colaborador == null)
                    return Unauthorized(new { message = "Usuario no encontrado o inactivo" });

                if (!PasswordService.VerifyPassword(login.Clave, colaborador.Clave))
                    return Unauthorized(new { message = "Contraseña incorrecta" });

                var token = GenerateToken(colaborador);

                return Ok(new
                {
                    message = "ok",
                    token = token,
                    usuario = new
                    {
                        id = colaborador.Id,
                        nombre = colaborador.Nombres,
                        rol = colaborador.IdrolNavigation.Nombre
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private string GenerateToken(Colaborador colaborador)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, colaborador.Id.ToString()),
                new Claim(ClaimTypes.Name, colaborador.Usuario),
                new Claim(ClaimTypes.Role, colaborador.IdrolNavigation.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}