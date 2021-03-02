using CapaDatos.ModeloBaseDatos;
using EntidadesCompartidas;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CapaDatos.Repos
{
    public class RepoUsuario
    {
        public string CrearUsuario(UsuarioDTO usuario)
        {
            Usuario nuevoUsuario = new Usuario()
            {
                nombreUser = usuario.nombreUser,
                pass = usuario.password,
                esAdmin = usuario.esAdmin,
                estaHabilitado = usuario.estaHabilitado
            };

            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                modeloDB.Usuario.Add(nuevoUsuario);
                modeloDB.SaveChanges();
            }

            return "Se agregó el usuario " + usuario.nombreUser + "a la lista de usuarios";
        }

        public string CambiarPass(UsuarioDTO usuario)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var usuarioActualizar = modeloDB.Usuario.Where(u => u.nombreUser == usuario.nombreUser).FirstOrDefault();
                modeloDB.Usuario.Attach(usuarioActualizar);

                usuarioActualizar.pass = usuario.password;
                modeloDB.SaveChanges();
            };
            return "Se actualizo el usuario " + usuario.nombreUser ;
        }

        //este método lo llamo una vez que el usuario se loguea para actualizar el campo esAdmin de Session y
        //así mostrar o no ciertas acciones, como el poder dar de alta a otros users, darlos de baja, etc.
        public bool EsAdmin(string username)
        {
            var lista = ListarUsuariosHabilitados();
            foreach (UsuarioDTO u in lista)
            {
                if (u.nombreUser.ToLower() == username.ToLower())
                {
                    return u.esAdmin;
                }
            }
            return false;
        }

        public bool LoginCorrecto(UsuarioDTO usuario)
        {
            var lista = ListarUsuariosHabilitados();
            foreach (UsuarioDTO u in lista)
            {
                if (u.nombreUser.ToLower() == usuario.nombreUser.ToLower() && u.password == usuario.password)
                {
                    return true;
                }
            }
            return false;
        }

        
        public string HabilitarUsuario(string username)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var usuarioHab = modeloDB.Usuario.Where(u => u.nombreUser == username).FirstOrDefault();
                modeloDB.Usuario.Attach(usuarioHab);

                usuarioHab.estaHabilitado = true;
                modeloDB.SaveChanges();

            };
            return "Se Habilitó el usuario " + username + "a la lista de usuarios";
        }

        public string DeshabilitarUsuario(string username)
        {
            using (BaseDatosEF modeloDB = new BaseDatosEF())
            {
                var usuarioDeshab = modeloDB.Usuario.Where(u => u.nombreUser == username).FirstOrDefault();
                modeloDB.Usuario.Attach(usuarioDeshab);

                usuarioDeshab.estaHabilitado = false;
                modeloDB.SaveChanges();

            };
            return "Se deshabilitó el usuario " + username + "a la lista de usuarios";
        }

        public List<UsuarioDTO> ListarUsuariosHabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from u in ModeloBD.Usuario
                            where (u.estaHabilitado == true )
                            select new UsuarioDTO()
                            {
                                nombreUser = u.nombreUser,
                                password = u.pass,
                                esAdmin = u.esAdmin, 
                                estaHabilitado = true
                            };
                return lista.ToList();
            }
        }

        public List<UsuarioDTO> ListaDeUsuariosDeshabilitados()
        {
            using (BaseDatosEF ModeloBD = new BaseDatosEF())
            {
                var lista = from u in ModeloBD.Usuario
                            where u.estaHabilitado == false
                            select new UsuarioDTO()
                            {
                                nombreUser = u.nombreUser,
                                password = u.pass,
                                esAdmin = u.esAdmin,
                                estaHabilitado = false
                            };
                return lista.ToList();
            }
        }

    }
}
