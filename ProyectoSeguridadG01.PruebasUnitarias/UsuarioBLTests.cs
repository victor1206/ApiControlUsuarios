using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysSeguridad.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysSeguridad.EN;

namespace SysSeguridad.BL.Tests
{
    [TestClass()]
    public class UsuarioBLTests
    {
        private Usuario usuarioInicial = new Usuario { Id = 15, 
            IdRol = 3, Login = "Roberto", Password = "123456"};
        private UsuarioBL usuarioBL = new UsuarioBL();
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "Julito";
            usuario.Apellido = "Iglesias";
            usuario.Login = "Iglesias";
            usuario.Password = "123456";
            usuario.Estatus = (byte)Estatus_Usuario.ACTIVO;
            int result = await usuarioBL.CrearAsync(usuario);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "Julio";
            usuario.Apellido = "Iglesia";
            usuario.Login = "Julio";
            usuario.Estatus = (byte)Estatus_Usuario.INACTIVO;
            var result = await usuarioBL.ModificarAsync(usuario);
            Assert.IsTrue(result == 1);
        }

        [TestMethod()]
        public async Task T3ObtenerTodosAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T4ObtenerPorIdAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T6BuscarIncluirRolesAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T7LoginAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T8cambiarPasswordAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task T9EliminarAsyncTest()
        {
            Assert.Fail();
        }
    }
}