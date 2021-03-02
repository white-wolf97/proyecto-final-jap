using CapaDatos.Repos;
using CapaLogica;
using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaUsuario
    {
        RepoUsuario repoUsuario = new RepoUsuario();

        public string CrearUsuario(UsuarioDTO usuario)
        {
            return repoUsuario.CrearUsuario(usuario);
        }

        public string CambiarPass(UsuarioDTO usuario)
        {
            return repoUsuario.CambiarPass(usuario);
        }

        public bool EsAdmin(string username)
        {
            return repoUsuario.EsAdmin(username);
        }

        public string DeshabilitarUsuario(string username)
        {
            return repoUsuario.DeshabilitarUsuario(username);
        }

        public string HabilitarUsuario(string username)
        {
            return repoUsuario.HabilitarUsuario(username);
        }

        public bool LoginCorrecto(UsuarioDTO usuario)
        {
            return repoUsuario.LoginCorrecto(usuario);
        }

        public List<UsuarioDTO> ListarUsuariosHabilitados() {
            return repoUsuario.ListarUsuariosHabilitados();
        }
       
        public List<UsuarioDTO> ListarUsuariosDeshabilitados()
        {
            return repoUsuario.ListaDeUsuariosDeshabilitados();
        }
    }
}
