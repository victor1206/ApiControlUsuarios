using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//********************************
using Microsoft.EntityFrameworkCore;
using SysSeguridad.EN;
using System.Security.Cryptography;

namespace SysSeguridad.DAL
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            { 
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pUsuario.Password));
                var strencriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strencriptar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strencriptar;
            }
                
        }

        private static async Task<bool> ExisteLogin(Usuario pUsuario, DBContexto pDbContexto)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContexto.Usuario.FirstOrDefaultAsync(
                s => s.Login == pUsuario.Login && s.Id != pUsuario.Id);
            if(loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && 
               loginUsuarioExiste.Login == pUsuario.Login)
                result = true;
            return result;
        }


        #region CRUD

        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                if (existeLogin == false)
                {
                    pUsuario.FechaRegistro = DateTime.Now;
                    EncriptarMD5(pUsuario);
                    dbContexto.Add(pUsuario);
                    result = await dbContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dBContexto = new DBContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, dBContexto);
                if (existeLogin == false)
                {
                    var usuario = await dBContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                    usuario.IdRol = pUsuario.IdRol;
                    usuario.Nombre = pUsuario.Nombre;
                    usuario.Apellido = pUsuario.Apellido;
                    usuario.Login = pUsuario.Login;
                    usuario.Estatus = pUsuario.Estatus;
                    dBContexto.Update(usuario);
                    result = await dBContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya Existe");
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                dbContexto.Usuario.Remove(usuario);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        { 
            var usuario = new Usuario();
            using (var dbContexto = new DBContexto())
            {
                usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
            }
            return usuario;
        }

        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuarios = new List<Usuario>();
            using (var dbContexto = new DBContexto())
            {
                usuarios = await dbContexto.Usuario.ToListAsync();
            }
            return usuarios;
        }

        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
            if (pUsuario.IdRol > 0)
                pQuery = pQuery.Where(s => s.IdRol == pUsuario.IdRol);
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre));
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido));
            if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                pQuery = pQuery.Where(s => s.Login.Equals(pUsuario.Login));
            if (pUsuario.Estatus > 0)
                pQuery = pQuery.Where(s => s.Estatus == pUsuario.Estatus);
            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month,
                                                     pUsuario.FechaRegistro.Day,0,0,0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        #endregion

        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        public static async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            Usuario usuario = new Usuario();
            using (var dbContexto = new DBContexto())
            {
                EncriptarMD5(pUsuario);
                usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login &&
                s.Password == pUsuario.Password && s.Estatus == (byte)Estatus_Usuario.ACTIVO);
            }
            return usuario;
        }

        public static async Task<int> cambiarPasswordAsync(Usuario pUsuario, string pPasswordAnterior)
        {
            int result = 0;
            var usuarioAnt = new Usuario { Password = pPasswordAnterior };
            EncriptarMD5(usuarioAnt);
            using (var dbContexto = new DBContexto())
            {
                var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                if (usuarioAnt.Password == usuario.Password.Trim())
                {
                    EncriptarMD5(pUsuario);
                    usuario.Password = pUsuario.Password;
                    dbContexto.Update(usuario);
                    result = await dbContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("El password actual es incorrecto");
            }
            return result;
        }
    }
}
