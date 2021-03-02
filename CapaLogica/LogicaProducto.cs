using CapaDatos.ModeloBaseDatos.Repos;
using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaProducto
    {
        RepoProducto repoProducto = new RepoProducto();

        public ProductoDTO BuscarProducto(int id)
        {
            return repoProducto.BuscarProducto(id);
        }

        public string CrearProducto(ProductoDTO producto)
        {
            return repoProducto.Crearproducto(producto);
        }

        public string ActualizarProducto(ProductoDTO producto)
        {
            return repoProducto.ActualizarProducto(producto);
        }

        public void HabilitarProducto(int id)
        {
            repoProducto.HabilitarProducto(id);
        }

        public void DeshabilitarProducto(int id)
        {
            repoProducto.DeshabilitarProducto(id);
        }

        
        public List<ProductoDTO> ListarProductosHabilitados()
        {
            return repoProducto.ListarProductosHabilitados();
        }

        public List<ProductoDTO> ListarProductosDeshabilitados()
        {
            return repoProducto.ListaDeProductosDeshabilitados();
        }
    }
}
