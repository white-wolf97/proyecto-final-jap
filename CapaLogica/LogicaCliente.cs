using CapaDatos.ModeloBaseDatos.Repos;
using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaCliente
    {
        RepoCliente repoCliente = new RepoCliente();

        //ojo, asume que está el cliente con la cédula dada
        public ClienteDTO BuscarCliente(int ci)
        {
            return repoCliente.BuscarCliente(ci);
        }

        public bool EsMayorDeEdad(DateTime fechaCliente)
        {
            DateTime fechaActual = DateTime.Now;
            double edad = fechaActual.Subtract(fechaCliente).TotalDays;
            if (edad >= 6575)  //número verificado experimentalmente
            {
                return true;
            }
            else
            {
                //Menores de edad no permitidos!
                return false;
            }
        }

        public string CrearCliente(ClienteDTO cliente)
        {
            return repoCliente.CrearCliente(cliente);
        }

        public string ActualizarCliente(ClienteDTO cliente)
        {
            return repoCliente.ActualizarCliente(cliente);
        }

        public void HabilitarCliente(int ci)
        {
            repoCliente.HabilitarCliente(ci);
        }

        public void DeshabilitarCliente(int ci)
        {
             repoCliente.DeshabilitarCliente(ci);
        }

        public List<ClienteDTO> ListarClientesHabilitados()
        {
            return repoCliente.ListarClientesHabilitados();
        }

        public List<ClienteDTO> ListarClientesDeshabilitados()
        {
            return repoCliente.ListaDeClientesDeshabilitados();
        }

        public bool EsCedulaValida(int cedula)
        {
            //recibo un int y la convierto a string porque es útil para manipularla aquí
            string ci = cedula.ToString();
            int[] NUMERO_MAGICO = new int[] { 2, 9, 8, 7, 6, 3, 4 };
            try
            {
                int suma = 0;
                for (int i = 0; i < 7; i++)
                {
                    suma = suma + (Convert.ToInt32(ci[i].ToString()) * NUMERO_MAGICO[i]);
                }
                int resto = suma % 10;
                int digito = 10 - resto;
                if (digito == Convert.ToInt32(ci[7].ToString()))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}
