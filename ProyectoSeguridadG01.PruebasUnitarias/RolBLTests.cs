using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysSeguridad.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysSeguridad.EN;
using System.Runtime.CompilerServices;

namespace SysSeguridad.BL.Tests
{
    [TestClass()]
    public class RolBLTests
    {
        private static Rol rolInicial = new Rol { Id = 15 };//Rol existente en la DB
        private RolBL rolBL = new RolBL();
        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var rol = new Rol();
            rol.Nombre = "Admin2";
            int result = await rolBL.CrearAsync(rol);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            rol.Nombre = "Admin3";
            int result = await rolBL.ModificarAsync(rol);
            Assert.AreNotEqual(0, result);
        }


        [TestMethod()]
        public async Task T3ObtenerTodosAsyncTest()
        {
            var result = await rolBL.ObtenerTodosAsync();
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod()]
        public async Task T4ObtenerPorIdAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            var resultRol = await rolBL.ObtenerPorIdAsync(rol);
            Assert.AreEqual(rol.Id, resultRol.Id);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var rol = new Rol();
            rol.Nombre = "a";
            rol.Top_Aux = 10;
            var resultRoles = await rolBL.BuscarAsync(rol);
            Assert.AreNotEqual(0, resultRoles.Count);
        }

        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            int result = await rolBL.EliminarAsync(rol);
            Assert.AreNotEqual(0, result);
        }
    }
}