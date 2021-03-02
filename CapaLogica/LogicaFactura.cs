using CapaDatos.ModeloBaseDatos.Repos;
using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaFactura
    {
        RepoFactura facturaRepo = new RepoFactura();

        public void CrearFactura(FacturaDTO facturaDto)
        {
            facturaRepo.CrearFactura(facturaDto);
        }

        public List<FacturaDTO> ListarFacturasHabilitadas()
        {
            return facturaRepo.ListarFacturasHabilitadas();
        }

        public List<FacturaDTO> ListarFacturasDeshabilitadas()
        {
            return facturaRepo.ListarFacturasDeshabilitadas();
        }

        public void HabilitarFactura(int idFactura)
        {
            facturaRepo.HabilitarFactura(idFactura);
        }

        public void DeshabilitarFactura(int idFactura)
        {
            facturaRepo.DeshabilitarFactura(idFactura);
        }

        public FacturaDTO BuscarFacturaPorID(int idFactura)
        {
            return facturaRepo.BuscarFacturaPoriD(idFactura);
        }


    }
}
