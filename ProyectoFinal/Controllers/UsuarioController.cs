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
    public class UsuarioController : Controller
    {
        LogicaUsuario logUsuario = new LogicaUsuario();

        public ActionResult VerificarIngreso(FormCollection form)
        {
            try
            {
                var nombreUsuario = form["uname"];
                var pass = form["psw"];
                UsuarioDTO usuario = new UsuarioDTO()
                {
                    nombreUser = nombreUsuario,
                    password = pass
                };
                if (logUsuario.LoginCorrecto(usuario))
                {
                    Session.Add("username", nombreUsuario);
                    Session.Add("logueado", true);
                    Session.Add("esAdmin", logUsuario.EsAdmin(nombreUsuario));
                    return RedirectToAction("../Inicio/Home");

                }
                else
                {
                    ViewBag.message = "Datos incorrectos";
                    return View("~/Views/Login/FormularioIngreso.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(ConfirmarPassUsuarioModel usuarioModRegistrar)
        {
            Session.Add("username", usuarioModRegistrar.nombreUser);
            Session.Add("esAdmin", false);
            try
            {
                if (ModelState.IsValid)//en caso de que tenga el javascript desabilitado
                {
                    UsuarioDTO usuarioDto = new UsuarioDTO()
                    {
                        nombreUser = usuarioModRegistrar.nombreUser,
                        esAdmin = false, 
                        estaHabilitado = true,
                        password = usuarioModRegistrar.pass
                    };
                    try
                    {
                        logUsuario.CrearUsuario(usuarioDto);
                        Session.Add("logueado", true);
                        return View("~/Views/Shared/UsuarioCreado.cshtml");
                    }
                    catch (Exception)
                    {
                        ViewBag.message = "El nombre de usuario elegido ya se encuentra en el sistema";
                        return View("~/Views/Shared/ERROR.cshtml");
                    }
                }
                else
                {
                    //en caso de que tenga el javascript desabilitado tiro los errores
                    return View(usuarioModRegistrar);
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult CambiarPass()
        {
            return View();
        }

        [HttpPost]//lo unico que dejo actualizar es el pass
        public ActionResult CambiarPass(CambiarPassUsuarioModel cambiarPassUsuarioMod)
        {
            try
            {
                if (ModelState.IsValid)//en caso de que tenga el javascript desabilitado
                {
                    UsuarioDTO usuarioDto = new UsuarioDTO()
                    {
                        nombreUser = (string)Session["username"],
                        esAdmin = (bool)Session["esAdmin"],
                        estaHabilitado = true,
                        password = cambiarPassUsuarioMod.pass
                    };
                    logUsuario.CambiarPass(usuarioDto);
                    return View("~/Views/Shared/PassCambiada.cshtml");
                }
                else
                {
                    //en caso de que tenga el javascript desabilitado tiro los errores
                    return View(cambiarPassUsuarioMod);
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult ListarUsuariosDeshabilitados()
        {
            try
            {
                var lista = logUsuario.ListarUsuariosDeshabilitados();
                List<UsuarioModel> listaModel = new List<UsuarioModel>();
                foreach (UsuarioDTO usuarioDto in lista)
                {
                    UsuarioModel usuarioModel = new UsuarioModel()
                    {
                        nombreUser = usuarioDto.nombreUser,
                    };
                    listaModel.Add(usuarioModel);
                }

                return View(listaModel);
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }


        public ActionResult ListarUsuariosHabilitados()
        {
            try
            {
                var lista = logUsuario.ListarUsuariosHabilitados();
                List<UsuarioModel> listaModel = new List<UsuarioModel>();
                foreach (UsuarioDTO usuarioDto in lista)
                {
                    UsuarioModel usuarioModel = new UsuarioModel()
                    {
                        nombreUser = usuarioDto.nombreUser,
                    };
                    listaModel.Add(usuarioModel);
                }

                return View(listaModel);
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }


        public ActionResult Deshabilitar(string id)
        {
            try
            {
                logUsuario.DeshabilitarUsuario(id);
                return RedirectToAction("../Usuario/ListarUsuariosHabilitados");
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult Habilitar(string id)
        {
            try
            {
                logUsuario.HabilitarUsuario(id);
                return RedirectToAction("../Usuario/ListarUsuariosDeshabilitados");
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }
    }
}
