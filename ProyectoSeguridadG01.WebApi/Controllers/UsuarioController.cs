using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//******************************
using SysSeguridad.EN;
using SysSeguridad.BL;
using System.Text.Json;

namespace ProyectoSeguridadG01.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBL = new UsuarioBL();

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            Usuario usuario = new Usuario();
            return await usuarioBL.BuscarIncluirRolesAsync(usuario);
        }

        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            return await usuarioBL.ObtenerPorIdAsync(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(
                    strUsuario, option);
                await usuarioBL.CrearAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(
                strUsuario, option);
            if (usuario.Id == id)
            {
                await usuarioBL.ModificarAsync(usuario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Id = id;
                await usuarioBL.EliminarAsync(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Usuario>> Buscar([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(
                strUsuario,option
                );
            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(
                usuario);
            //usuarios.ForEach(s => s.Rol.Usuario = null);
            return usuarios;
        }

        [HttpPost("Login")]
        public async Task<Usuario> Login([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(
                strUsuario, option
                );
            Usuario usuario_auth = await usuarioBL.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.Id > 0
                && usuario_auth.Login == usuario.Login)
            {
                return usuario;
            }
            else
            {
                return new Usuario();
            }
        }

        [HttpPost("CambiarPassword")]
        public async Task<ActionResult> CambiarPassword(
            [FromBody] object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(
                    strUsuario, option
                    );
                await usuarioBL.cambiarPasswordAsync(usuario,
                    usuario.ConfirmPassword_Aux);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }
    }
}
