using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repos
{
    public class RepoDetalle
    {

        public void CrearDetalle(DetalleDTO detalleDto)
        {
            Detalle detalleDB = new Detalle
            {
                idProducto = detalleDto.idProducto,
                idFactura = detalleDto.idFactura,
                cantidad = detalleDto.cantidad
            };

            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                modeloDB.Detalle.Add(detalleDB);
                modeloDB.SaveChanges();
            }
        }

        public List<DetalleDTO> ListaDetallesDeFactura(int idFactura)
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from d in ModeloBD.Detalle
                            where (d.idFactura == idFactura)
                            select new DetalleDTO()
                            {
                                idProducto = d.idProducto,
                                cantidad = d.cantidad
                            };
                return lista.ToList();
            }
        }
    }
}
