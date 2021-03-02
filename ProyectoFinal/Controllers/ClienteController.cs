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
    public class ClienteController : Controller
    {
        LogicaCliente logClient = new LogicaCliente();
        //Los Action están en orden CRUD

        /****************************************Crear***************************************************/

        public ActionResult CrearCliente()
        {
            if ((bool)Session["logueado"])
            {
                return View();
            }
            else
            {
                ViewBag.message = "No estás logueado en el sistema!";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }

        [HttpPost]
        public ActionResult GuardarCliente(ClienteModel clienteMod)
        {

            try
            {
                if ((bool)Session["logueado"])
                {

                    if (ModelState.IsValid)
                    {
                        if (logClient.EsCedulaValida(clienteMod.ci))
                        {
                            if (logClient.EsMayorDeEdad(clienteMod.fechaNacimiento))
                            {
                                ClienteDTO clienteDto = new ClienteDTO()
                                {
                                    nombre = clienteMod.nombre,
                                    ci = clienteMod.ci,
                                    domicilio = clienteMod.domicilio,
                                    fechaNacimiento = clienteMod.fechaNacimiento
                                };
                                try
                                {
                                    logClient.CrearCliente(clienteDto);
                                    return RedirectToAction("ListarClientesHabilitados");
                                }
                                catch (Exception)
                                {
                                    ViewBag.message = "Ya existe un cliente con esa cédula";
                                    return View("~/Views/Shared/ERROR.cshtml");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("fechaNacimiento", "Debe ser mayor de edad");
                                return View("CrearCliente", clienteMod);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ci", "La cédula ingresada no es válida");
                            return View("CrearCliente", clienteMod);
                        }
                    }
                    else
                    {
                        return View("CrearCliente", clienteMod);
                    }
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }


        /****************************************Detalles***************************************************/
        public ActionResult Detalles(int id)
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    ClienteDTO clientDTO = logClient.BuscarCliente(id);
                    ClienteModel clienteMod = new ClienteModel()
                    {
                        nombre = clientDTO.nombre,
                        domicilio = clientDTO.domicilio,
                        fechaNacimiento = clientDTO.fechaNacimiento,
                        ci = clientDTO.ci
                    };
                    return View(clienteMod);
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }


        /***********************************Editar/Actualizar****************************************************/

        public ActionResult Editar(int id)
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    ClienteDTO clientDTO = logClient.BuscarCliente(id);
                    ClienteModel clienteMod = new ClienteModel()
                    {
                        nombre = clientDTO.nombre,
                        domicilio = clientDTO.domicilio,
                        fechaNacimiento = clientDTO.fechaNacimiento,
                        ci = clientDTO.ci
                    };

                    return View(clienteMod);
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        [HttpPost]
        public ActionResult Actualizar(ClienteModel clienteMod)
        {
            try
            {
                if ((bool)Session["logueado"])
                {
                    ClienteDTO clienteDto = new ClienteDTO()
                    {
                        nombre = clienteMod.nombre,
                        domicilio = clienteMod.domicilio,
                        fechaNacimiento = clienteMod.fechaNacimiento,
                        ci = clienteMod.ci //uso la cédula para buscarlo en la DB, pero nunca dejo que la editen.

                    };

                    logClient.ActualizarCliente(clienteDto);

                    return RedirectToAction("ListarClientesHabilitados");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        /*****************************Deshabilitar************************************************************/

        public ActionResult Deshabilitar(int id)
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    ClienteDTO clientDTO = logClient.BuscarCliente(id);

                    ClienteModel clienteMod = new ClienteModel()
                    {

                        nombre = clientDTO.nombre,
                        domicilio = clientDTO.domicilio,
                        fechaNacimiento = clientDTO.fechaNacimiento,
                        ci = clientDTO.ci
                    };
                    return View(clienteMod);

                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        public ActionResult DeshabilitarCliente(int ci)
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    logClient.DeshabilitarCliente(ci);

                    return RedirectToAction("ListarClientesHabilitados");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        public ActionResult DeshabilitarTodos()
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    var listaHabilitados = logClient.ListarClientesHabilitados();
                    foreach (ClienteDTO c in listaHabilitados)
                    {
                        logClient.DeshabilitarCliente(c.ci);
                    }
                    return RedirectToAction("ListarClientesHabilitados");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }
        /****************************************Habilitar***************************************************/
        public ActionResult Habilitar(int id)
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    logClient.HabilitarCliente(id);
                    return RedirectToAction("ListarClientesDeshabilitados");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult HabilitarTodos()
        {

            try
            {
                if ((bool)Session["logueado"])
                {

                    var listaDeshabilitados = logClient.ListarClientesDeshabilitados();
                    foreach (ClienteDTO c in listaDeshabilitados)
                    {
                        logClient.HabilitarCliente(c.ci);
                    }
                    return RedirectToAction("ListarClientesDeshabilitados");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        /*************************************Listar************************************************/
        public ActionResult ListarClientesHabilitados()
        {
            try
            {
                if ((bool)Session["logueado"])
                {
                    var lista = logClient.ListarClientesHabilitados();

                    List<ClienteModel> listaClientes = new List<ClienteModel>();

                    foreach (var i in lista)
                    {
                        ClienteModel nuevoCliente = new ClienteModel();

                        nuevoCliente.nombre = i.nombre;
                        nuevoCliente.ci = i.ci;
                        listaClientes.Add(nuevoCliente);
                    }
                    return View(listaClientes);
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        public ActionResult ListarClientesDeshabilitados()
        {

            try
            {
                if ((bool)Session["logueado"])
                {
                    var lista = logClient.ListarClientesDeshabilitados();

                    List<ClienteModel> listaClientes = new List<ClienteModel>();

                    foreach (var i in lista)
                    {
                        ClienteModel nuevoCliente = new ClienteModel();

                        nuevoCliente.nombre = i.nombre;
                        nuevoCliente.ci = i.ci;
                        listaClientes.Add(nuevoCliente);
                    }
                    return View(listaClientes);

                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return View("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }
        }
    }
}