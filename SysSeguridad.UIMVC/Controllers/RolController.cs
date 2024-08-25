using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//**********************************
using SysSeguridad.BL;
using SysSeguridad.EN;

namespace SysSeguridad.UIMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class RolController : Controller
    {
        RolBL rolBl = new RolBL();
        // GET: RolController
        public async Task<IActionResult> Index(Rol pRol = null)
        {
            if(pRol == null)
                pRol = new Rol();
            if (pRol.Top_Aux == 0)
                pRol.Top_Aux = 10;
            else
                if (pRol.Top_Aux == -1)
                    pRol.Top_Aux = 0;

            var roles = await rolBl.BuscarAsync(pRol);
            ViewBag.Top = pRol.Top_Aux;
            return View(roles);
        }

        // GET: RolController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Rol _rol = new Rol();
            _rol.Id = id;
            var rol = await rolBl.ObtenerPorIdAsync(_rol);
            return View(rol);
        }

        // GET: RolController/Create
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol pRol)
        {
            try
            {
                int result = await rolBl.CrearAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Rol pRol)
        {
            var rol = await rolBl.ObtenerPorIdAsync(pRol);
            ViewBag.Error = "";
            return View(rol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol pRol)
        {
            try
            {
                int result = await rolBl.ModificarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Rol pRol)
        {
            var rol = await rolBl.ObtenerPorIdAsync(pRol);
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try
            {
                var result = await rolBl.EliminarAsync(pRol);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }
    }
}
