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
    public class ProductoController : Controller
    {
        LogicaProducto logProd = new LogicaProducto();

        //Los Action están en orden CRUD

        /****************************************Crear***************************************************/

        public ActionResult CrearProducto()
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
        public ActionResult GuardarProducto(ProductoModel productoMod)
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    ProductoDTO productoDto = new ProductoDTO()
                    {
                        nombre = productoMod.nombre,
                        idProducto = productoMod.idProducto,
                        precio = productoMod.precio,
                        marca = productoMod.marca
                    };
                    try
                    {
                        logProd.CrearProducto(productoDto);
                        return RedirectToAction("ListarProductosHabilitados");

                    }
                    catch (Exception)
                    {
                        ViewBag.message = "Ya existe un producto con ese id";
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

        /****************************************Detalles***************************************************/
        public ActionResult Detalles(int id)
        {
            try
            {
                ProductoDTO prodDTO = logProd.BuscarProducto(id);
                ProductoModel productMod = new ProductoModel()
                {
                    nombre = prodDTO.nombre,
                    marca = prodDTO.marca,
                    precio = prodDTO.precio,
                    idProducto = prodDTO.idProducto
                };
                return View(productMod);
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

                ProductoDTO prodDTO = logProd.BuscarProducto(id);
                ProductoModel productoMod = new ProductoModel()
                {
                    nombre = prodDTO.nombre,
                    marca = prodDTO.marca,
                    precio = prodDTO.precio,
                    idProducto = prodDTO.idProducto
                };

                return View(productoMod);
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        [HttpPost]
        public ActionResult ActualizarProducto(ProductoModel productoMod)
        {
            try
            {

                ProductoDTO productoDto = new ProductoDTO()
                {
                    nombre = productoMod.nombre,
                    marca = productoMod.marca,
                    precio = productoMod.precio,
                    idProducto = productoMod.idProducto
                    //uso la cédula para buscarlo en la DB, pero nunca dejo que la editen.
                };

                logProd.ActualizarProducto(productoDto);

                return RedirectToAction("ListarProductosHabilitados");
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

                ProductoDTO prodDTO = logProd.BuscarProducto(id);
                ProductoModel productoMod = new ProductoModel()
                {
                    nombre = prodDTO.nombre,
                    marca = prodDTO.marca,
                    precio = prodDTO.precio,
                    idProducto = prodDTO.idProducto
                };
                return View(productoMod);
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }
        [HttpPost]
        public ActionResult DeshabilitarProducto(int idProducto)
        {

            try
            {

                logProd.DeshabilitarProducto(idProducto);

                return RedirectToAction("ListarProductosHabilitados");
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

                var listaHabilitados = logProd.ListarProductosHabilitados();
                foreach (ProductoDTO p in listaHabilitados)
                {
                    logProd.DeshabilitarProducto(p.idProducto);
                }
                return RedirectToAction("ListarProductosHabilitados");
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

                logProd.HabilitarProducto(id);
                return RedirectToAction("ListarProductosDeshabilitados");
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

                var listaDeshabilitados = logProd.ListarProductosDeshabilitados();
                foreach (ProductoDTO p in listaDeshabilitados)
                {
                    logProd.HabilitarProducto(p.idProducto);
                }
                return RedirectToAction("ListarProductosDeshabilitados");
            }
            catch (Exception)
            {
                ViewBag.message = "Ocurrió un error en el servidor";
                return View("~/Views/Shared/ERROR.cshtml");
            }

        }

        /*************************************Listar************************************************/
        public ActionResult ListarProductosHabilitados()
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    var lista = logProd.ListarProductosHabilitados();

                    List<ProductoModel> listaProductos = new List<ProductoModel>();

                    foreach (var i in lista)
                    {
                        ProductoModel nuevoProducto = new ProductoModel();

                        nuevoProducto.nombre = i.nombre;
                        nuevoProducto.marca = i.marca;
                        nuevoProducto.precio = i.precio;
                        nuevoProducto.idProducto = i.idProducto;
                        listaProductos.Add(nuevoProducto);
                    }

                    return View(listaProductos);
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
        public ActionResult ListarProductosDeshabilitados()
        {
            try
            {
                if ((bool)Session["logueado"])
                {

                    var lista = logProd.ListarProductosDeshabilitados();

                    List<ProductoModel> listaProductos = new List<ProductoModel>();

                    foreach (var i in lista)
                    {
                        ProductoModel nuevoProducto = new ProductoModel();

                        nuevoProducto.nombre = i.nombre;
                        nuevoProducto.marca = i.marca;
                        nuevoProducto.precio = i.precio;
                        nuevoProducto.idProducto = i.idProducto;
                        listaProductos.Add(nuevoProducto);
                    }
                    return View(listaProductos);
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
