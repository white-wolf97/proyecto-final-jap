using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace CapaDatos.ModeloBaseDatos.Repos
{
    public class RepoProducto
    {

        public string Crearproducto(ProductoDTO producto)
        {
            Producto nuevoproducto = new Producto()
            {
                nombre = producto.nombre,
                idProducto = producto.idProducto,
                marca = producto.marca,
                precio = producto.precio,
                estaHabilitado = true
            };

            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                modeloDB.Producto.Add(nuevoproducto);
                modeloDB.SaveChanges();
            }

            return "Se agregó el producto " + producto.nombre + "a la lista de productos";
        }

        

        public string ActualizarProducto(ProductoDTO producto)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var productoActualizar = modeloDB.Producto.Where(p => p.idProducto == producto.idProducto).FirstOrDefault();
                modeloDB.Producto.Attach(productoActualizar);

                productoActualizar.nombre = producto.nombre;
                productoActualizar.marca = producto.marca;
                productoActualizar.precio = producto.precio;
                //no dejo actualizar el id, es la PK, si quiere actualizarlo lo borra y lo pone de nuevo

                modeloDB.SaveChanges();

            };
            return "Se actualizo el producto " + producto.nombre + "a la lista de productos";
        }

        public void HabilitarProducto(int id)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var productoHabilitar = modeloDB.Producto.Where(p => p.idProducto == id).FirstOrDefault();
                modeloDB.Producto.Attach(productoHabilitar);

                productoHabilitar.estaHabilitado = true;

                modeloDB.SaveChanges();

            };
        }

        public void DeshabilitarProducto(int id)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var productoDeshabilitar = modeloDB.Producto.Where(p => p.idProducto == id).FirstOrDefault();
                modeloDB.Producto.Attach(productoDeshabilitar);

                productoDeshabilitar.estaHabilitado = false;

                modeloDB.SaveChanges();

            };
        }

       
        public ProductoDTO BuscarProducto(int id)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                Producto productoDB = modeloDB.Producto.Where(p => p.idProducto == id).FirstOrDefault();

                //mapeo
                ProductoDTO ProductoRetorno = new ProductoDTO()
                {
                    nombre = productoDB.nombre,
                    idProducto = productoDB.idProducto,
                    marca = productoDB.marca,
                    precio = productoDB.precio
                };
                return ProductoRetorno;
            };
        }

        public List<ProductoDTO> ListarProductosHabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from p in ModeloBD.Producto
                            where p.estaHabilitado == true
                            select new ProductoDTO()
                            {
                                nombre = p.nombre,
                                marca = p.marca,
                                precio = p.precio,
                                idProducto = p.idProducto,
                                estaHabilitado = true
                            };
                return lista.ToList();
            }
        }


        public List<ProductoDTO> ListaDeProductosDeshabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from p in ModeloBD.Producto
                            where p.estaHabilitado == false
                            select new ProductoDTO()
                            {
                                nombre = p.nombre,
                                marca = p.marca,
                                precio = p.precio,
                                idProducto = p.idProducto,
                                estaHabilitado = false
                            };
                return lista.ToList();
            }
        }
    }
}
