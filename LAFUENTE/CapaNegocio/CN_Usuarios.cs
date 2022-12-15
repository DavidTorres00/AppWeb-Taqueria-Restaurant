using CapaEntidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios oCapaDatos = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return oCapaDatos.Listar();
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje = "El correo del usuario no puede ser vacio";
            }

            //Correo,Clave
            if (string.IsNullOrEmpty(mensaje))
            {
                string clave = CNRecursos.GenerarClave();
                string asunto = "Creación de Cuenta";
                string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);


                bool respuesta = CNRecursos.EnviarCorreo(obj.Correo, asunto, mensajeCorreo);

                if (respuesta)
                {
                    obj.Clave = CNRecursos.ConvertirSha256(clave);
                    return oCapaDatos.Registrar(obj, out mensaje);
                }
                else
                {
                    mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje = "El correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return oCapaDatos.Editar(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return oCapaDatos.Eliminar(id, out mensaje);
        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return oCapaDatos.CambiarClave(idusuario, nuevaclave, out Mensaje);
        }


        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = CNRecursos.GenerarClave();
            bool resultado = oCapaDatos.ReestablecerClave(idusuario, CNRecursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);

                bool respuesta = CNRecursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }
    }
}
