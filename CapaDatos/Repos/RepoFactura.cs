using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ModeloBaseDatos.Repos
{
    public class RepoFactura
    {
        public void CrearFactura(FacturaDTO facturaDto)
        {
            Factura facturaDB = new Factura
            {
                estaHabilitada = true,
                cioRUT = facturaDto.ci,
                idFactura = facturaDto.idFactura,
                fecha = DateTime.Now,
                montoTotal = facturaDto.montoTotal
            };

            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                modeloDB.Factura.Add(facturaDB);
                modeloDB.SaveChanges();
            }
        }

        public void HabilitarFactura(int idFactura)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var facturaHabilitar = modeloDB.Factura.Where(f => f.idFactura == idFactura).FirstOrDefault();
                modeloDB.Factura.Attach(facturaHabilitar);
                facturaHabilitar.estaHabilitada = true;

                modeloDB.SaveChanges();

            };
        }

        public void DeshabilitarFactura(int idFactura)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var facturaDeshabilitar = modeloDB.Factura.Where(f => f.idFactura == idFactura).FirstOrDefault();
                modeloDB.Factura.Attach(facturaDeshabilitar);
                facturaDeshabilitar.estaHabilitada = false;

                modeloDB.SaveChanges();

            };
        }

        public FacturaDTO BuscarFacturaPoriD(int idFactura)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                Factura facturaDB = modeloDB.Factura.Where(f => f.idFactura == idFactura).FirstOrDefault();

                //mapeo
                FacturaDTO FacturaRetorno = new FacturaDTO()
                {
                    ci = facturaDB.cioRUT,
                    fecha = facturaDB.fecha,
                    idFactura = facturaDB.idFactura,
                    montoTotal = facturaDB.montoTotal,
                    estaHabilitada = facturaDB.estaHabilitada
                };
                return FacturaRetorno;
            };
        }

        public List<FacturaDTO> ListarFacturasHabilitadas()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from f in ModeloBD.Factura
                            where f.estaHabilitada == true
                            select new FacturaDTO()
                            {
                                ci = f.cioRUT,
                                fecha = f.fecha,
                                idFactura = f.idFactura,
                                montoTotal = f.montoTotal
                            };
                return lista.ToList();
            }
        }

        public List<FacturaDTO> ListarFacturasDeshabilitadas()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from f in ModeloBD.Factura
                            where f.estaHabilitada == false
                            select new FacturaDTO()
                            {
                                ci = f.cioRUT,
                                fecha = f.fecha,
                                idFactura = f.idFactura,
                                montoTotal = f.montoTotal
                            };
                return lista.ToList();
            }
        }
    }
}
