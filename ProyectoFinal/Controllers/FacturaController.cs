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
    public class FacturaController : Controller
    {
        LogicaFactura logFactura = new LogicaFactura();
        LogicaDetalle logDetalle = new LogicaDetalle();
        LogicaProducto logProducto = new LogicaProducto();
        LogicaCliente logCliente = new LogicaCliente();

        public ActionResult Crear()
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    var listaClientes = logCliente.ListarClientesHabilitados();
                    var listaProductos = logProducto.ListarProductosHabilitados();
                    if (listaClientes.Count == 0)
                    {
                        ViewBag.message = "No hay clientes en el sistema";
                        return View("~/Views/Shared/ERROR.cshtml");
                    }
                    else if (listaProductos.Count == 0)
                    {
                        ViewBag.message = "No hay productos en el sistema";
                        return View("~/Views/Shared/ERROR.cshtml");
                    }
                    else
                    {
                        FacturaModel facturaModelSession = new FacturaModel();
                        Session.Add("facturaModelSession", facturaModelSession);
                        facturaModelSession.Clientes = AgregarClientes(listaClientes);
                        facturaModelSession.Productos = AgregarProductos(listaProductos);
                        return View(facturaModelSession);
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

        private List<SelectListItem> AgregarClientes(List<ClienteDTO> listClient)
        {
            List<SelectListItem> retorno = new List<SelectListItem>();
            foreach (ClienteDTO cDTO in listClient)
            {
                retorno.Add(new SelectListItem
                {
                    Text = (cDTO.ci + "-" + cDTO.nombre),
                    Value = cDTO.ci.ToString(),
                    Selected = false
                });
            }
            return retorno;
        }

        private List<SelectListItem> AgregarProductos(List<ProductoDTO> listProd)
        {
            List<SelectListItem> retorno = new List<SelectListItem>();
            foreach (ProductoDTO pDTO in listProd)
            {
                retorno.Add(new SelectListItem
                {
                    Text = (pDTO.idProducto + "-" + pDTO.nombre + "-" + pDTO.marca),
                    Value = pDTO.idProducto.ToString(),
                    Selected = false
                });
            }

            return retorno;
        }

        [HttpPost]
        public ActionResult Crear(FacturaModel facturaMod)
        {
            try
            {


                ProductoDTO productoDto = logProducto.BuscarProducto(facturaMod.idProducto);
                FacturaModel sessionFacturaModel = (FacturaModel)Session["facturaModelSession"];

                DetalleModel detalleMod = new DetalleModel()
                {
                    nombreProducto = productoDto.nombre,
                    marca = productoDto.marca,
                    precio = productoDto.precio,
                    precioTotal = productoDto.precio * facturaMod.cantidad,
                    cantidad = facturaMod.cantidad,
                    idFactura = facturaMod.idFactura,
                    idProducto = facturaMod.idProducto
                };

                //vuelvo a cargar estos datos para tenerlos disponibles en la vista "madre"
                sessionFacturaModel.ci = facturaMod.ci;
                sessionFacturaModel.idFactura = facturaMod.idFactura;

                sessionFacturaModel.Detalles.Add(detalleMod);
                sessionFacturaModel.montoTotal = CalcularMontoTotal(sessionFacturaModel.Detalles);

                Session.Add("facturaModelSession", sessionFacturaModel);
                return View(sessionFacturaModel);
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        public ActionResult Guardar()
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    FacturaModel facturaMod = (FacturaModel)Session["facturaModelSession"];

                    FacturaDTO facturaDTO = new FacturaDTO()
                    {
                        ci = facturaMod.ci,
                        idFactura = facturaMod.idFactura,
                        montoTotal = facturaMod.montoTotal
                    };

                    try
                    {
                        logFactura.CrearFactura(facturaDTO);
                        foreach (DetalleModel detalleMod in facturaMod.Detalles)
                        {
                            DetalleDTO detalleDTO = new DetalleDTO()
                            {
                                idFactura = detalleMod.idFactura,
                                idProducto = detalleMod.idProducto,
                                cantidad = detalleMod.cantidad
                            };
                            logDetalle.CrearDetalle(detalleDTO);
                        }
                        return View("FacturaCreada"); ;
                    }
                    catch (Exception)
                    {
                        ViewBag.message = "Ya existe una factura con ese ID en el sistema";
                        return View("~/Views/Shared/ERROR.cshtml");
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

        public int CalcularMontoTotal(List<DetalleModel> detalleModelList)
        {
            int total = 0;
            foreach (DetalleModel detalleMod in detalleModelList)
            {
                total = total + detalleMod.precioTotal;
            }

            return total;
        }

        public ActionResult Detalles(int idFactura)
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    FacturaDTO facturaDto = logFactura.BuscarFacturaPorID(idFactura);
                    FacturaModel facturaMod = new FacturaModel()
                    {
                        idFactura = facturaDto.idFactura,
                        ci = facturaDto.ci,
                        montoTotal = facturaDto.montoTotal,
                        fecha = facturaDto.fecha
                    };
                    var listaDetallesDTO = logDetalle.ListaDetallesDeFactura(idFactura); ;
                    foreach (DetalleDTO d in listaDetallesDTO)
                    {
                        ProductoDTO productoDto = logProducto.BuscarProducto(d.idProducto);
                        DetalleModel detalleMod = new DetalleModel()
                        {
                            nombreProducto = productoDto.nombre,
                            marca = productoDto.marca,
                            precio = productoDto.precio,
                            precioTotal = productoDto.precio * d.cantidad,
                            cantidad = d.cantidad,
                            idProducto = d.idProducto
                        };
                        facturaMod.Detalles.Add(detalleMod);
                    }
                    return View(facturaMod);
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

        public ActionResult Deshabilitar(int idFactura)
        {
            try
            {
                if ((bool)Session["logueado"])
                {
                    logFactura.DeshabilitarFactura(idFactura);
                    return RedirectToAction("ListarFacturasHabilitadas");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return RedirectToAction("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult Habilitar(int idFactura)
        {
            try
            {
                if ((bool)Session["logueado"])
                {
                    logFactura.HabilitarFactura(idFactura);
                    return RedirectToAction("ListarFacturasDeshabilitadas");
                }
                else
                {
                    ViewBag.message = "No estás logueado en el sistema!";
                    return RedirectToAction("~/Views/Shared/ERROR.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }


        public ActionResult ListarFacturasHabilitadas()
        {
            if ((bool)Session["logueado"])
            {
                try
                {
                    var lista = logFactura.ListarFacturasHabilitadas();

                    List<FacturaModel> listaFacturas = new List<FacturaModel>();

                    foreach (var i in lista)
                    {
                        FacturaModel nuevaFactura = new FacturaModel();

                        nuevaFactura.idFactura = i.idFactura;
                        nuevaFactura.montoTotal = i.montoTotal;
                        nuevaFactura.ci = i.ci;
                        listaFacturas.Add(nuevaFactura);
                    }
                    return View(listaFacturas);
                }
                catch (Exception)
                {
                    ViewBag.message = "Ocurrió un error en el servidor";
                    return RedirectToAction("~/Views/Shared/ERROR.cshtml");
                }
            }
            else
            {
                ViewBag.message = "No estás logueado en el sistema!";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }

        public ActionResult ListarFacturasDeshabilitadas()
        {
            if ((bool)Session["logueado"])
            {
                try
                {
                    var lista = logFactura.ListarFacturasDeshabilitadas();

                    List<FacturaModel> listaFacturas = new List<FacturaModel>();

                    foreach (var i in lista)
                    {
                        FacturaModel nuevaFactura = new FacturaModel();

                        nuevaFactura.idFactura = i.idFactura;
                        nuevaFactura.montoTotal = i.montoTotal;
                        nuevaFactura.ci = i.ci;
                        listaFacturas.Add(nuevaFactura);
                    }
                    return View(listaFacturas);
                }
                catch (Exception)
                {
                    ViewBag.message = "Ocurrió un error en el servidor";
                    return RedirectToAction("~/Views/Shared/ERROR.cshtml");
                }
            }
            else
            {
                ViewBag.message = "No estás logueado en el sistema!";
                return RedirectToAction("~/Views/Shared/ERROR.cshtml");
            }
        }
    }
}
