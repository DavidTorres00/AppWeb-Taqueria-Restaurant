using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria oCapaDato = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return oCapaDato.Listar();
        }


        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la categoría no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }


        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la categoría no puede ser vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return oCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return oCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
