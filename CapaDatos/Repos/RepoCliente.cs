using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.ModeloBaseDatos.Repos
{
    public class RepoCliente
    {
        public string CrearCliente(ClienteDTO cliente)
        {
            Cliente nuevoCliente = new Cliente()
            {
                nombre = cliente.nombre,
                fachaNacimiento = cliente.fechaNacimiento,
                domicilio = cliente.domicilio,
                cioRUT = cliente.ci, 
                estaHabilitado = true
            };

            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                modeloDB.Cliente.Add(nuevoCliente);
                modeloDB.SaveChanges();
            }

            return "Se agregó el cliente " + cliente.nombre + "a la lista de clientes";
        }

        public string ActualizarCliente(ClienteDTO cliente)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var clienteActualizar = modeloDB.Cliente.Where(c => c.cioRUT == cliente.ci).FirstOrDefault();
                
                modeloDB.Cliente.Attach(clienteActualizar);

                clienteActualizar.nombre = cliente.nombre;
                clienteActualizar.fachaNacimiento = cliente.fechaNacimiento;
                clienteActualizar.domicilio = cliente.domicilio;
                modeloDB.SaveChanges();

            };
            return "Se actualizo el cliente " + cliente.nombre + "a la lista de clientes";
        }


        public void HabilitarCliente(int ci)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var clienteHabilitar = modeloDB.Cliente.Where(c => c.cioRUT == ci).FirstOrDefault();
                modeloDB.Cliente.Attach(clienteHabilitar);
                clienteHabilitar.estaHabilitado = true;

                modeloDB.SaveChanges();

            };
        }

        public void DeshabilitarCliente(int ci)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var clienteDeshabilitar = modeloDB.Cliente.Where(c => c.cioRUT == ci).FirstOrDefault();
                modeloDB.Cliente.Attach(clienteDeshabilitar);
                clienteDeshabilitar.estaHabilitado = false;
                
                modeloDB.SaveChanges();

            };
        }

        public ClienteDTO BuscarCliente(int ci)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                Cliente clientDB = modeloDB.Cliente.Where(c => c.cioRUT == ci).FirstOrDefault();

                //mapeo
                ClienteDTO ClienteRetorno = new ClienteDTO()
                {
                    nombre = clientDB.nombre,
                    ci = clientDB.cioRUT,
                    domicilio = clientDB.domicilio,
                    fechaNacimiento = clientDB.fachaNacimiento,
                    estaHabilitado = clientDB.estaHabilitado
                };
                return ClienteRetorno;
            };
        }

        public List<ClienteDTO> ListarClientesHabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from c in ModeloBD.Cliente where c.estaHabilitado == true
                            select new ClienteDTO()
                            {
                                ci = c.cioRUT,
                                nombre = c.nombre,
                                domicilio = c.domicilio,
                                fechaNacimiento = c.fachaNacimiento,
                                estaHabilitado = true
                            };
                return lista.ToList();
            }
        }

        public List<ClienteDTO> ListaDeClientesDeshabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from c in ModeloBD.Cliente
                            where c.estaHabilitado == false
                            select new ClienteDTO()
                            {
                                ci = c.cioRUT,
                                nombre = c.nombre,
                                domicilio = c.domicilio,
                                fechaNacimiento = c.fachaNacimiento,
                                estaHabilitado = false
                            };
                return lista.ToList();
            }
        }


    }
}
