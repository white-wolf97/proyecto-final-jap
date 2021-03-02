using CapaLogica;
using EntidadesCompartidas;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult FormularioIngreso()
        {
            Session.Add("logueado", false);
            return View();
        }
    }
}