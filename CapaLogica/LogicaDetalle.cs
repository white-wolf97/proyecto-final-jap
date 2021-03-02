using CapaDatos.Repos;
using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaDetalle
    {
        RepoDetalle detalleRepo = new RepoDetalle();

        public void CrearDetalle(DetalleDTO detalleDto)
        {
            detalleRepo.CrearDetalle(detalleDto);
        }

        public List<DetalleDTO> ListaDetallesDeFactura(int idFactura) {
            return detalleRepo.ListaDetallesDeFactura(idFactura);
        }

    
    }
}
