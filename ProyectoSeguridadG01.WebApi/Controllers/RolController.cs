﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//**************
using SysSeguridad.BL;
using SysSeguridad.EN;
using System.Globalization;
using System.Text.Json;
using ProyectoSeguridadG01.WebApi.Models;

namespace ProyectoSeguridadG01.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private RolBL rolBL = new RolBL();

        [HttpGet]
        public async Task<IEnumerable<Rol>> Get()
        {
            return await rolBL.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        public async Task<Rol> Get(int id)
        {
            Rol rol = new Rol();
            rol.Id = id;
            return await rolBL.ObtenerPorIdAsync(rol);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Rol rol)
        {
            try
            {
                await rolBL.CrearAsync(rol);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Rol rol)
        {
            if (rol.Id == id)
            {
                await rolBL.ModificarAsync(rol);
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
                Rol rol = new Rol();
                rol.Id = id;
                await rolBL.EliminarAsync(rol);
                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Rol>> Buscar([FromBody] object pRol)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string strRol = JsonSerializer.Serialize(pRol);
            Rol rol = JsonSerializer.Deserialize<Rol>(
                strRol, option);
            return await rolBL.BuscarAsync(rol);
        }
    }
}
