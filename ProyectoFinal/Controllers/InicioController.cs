using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Home()
        {
            try
            {
                bool logueado = (bool)Session["logueado"];
                if (logueado)
                {
                    return View();
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return RedirectToAction("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Error en el servidor!";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }

        // GET: Inicio/LogOut
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Add("logueado", false);

            return RedirectToAction("../Login/FormularioIngreso");
        }

        public ActionResult Registrarse()
        {
            return View();
        }
    }
}
