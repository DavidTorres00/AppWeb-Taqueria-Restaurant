using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        private CD_Carrito oCapaDato = new CD_Carrito();


        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            return oCapaDato.ExisteCarrito(idcliente,idproducto);
        }

        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje)
        {
            return oCapaDato.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }

        public int CantidadEnCarrito(int idcliente)
        {
            return oCapaDato.CantidadEnCarrito(idcliente);
        }

        public List<Carrito> ListarProducto(int idcliente)
        {
            return oCapaDato.ListarProducto(idcliente);
        }

        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            return oCapaDato.EliminarCarrito(idcliente, idproducto);
        }
    }
}
