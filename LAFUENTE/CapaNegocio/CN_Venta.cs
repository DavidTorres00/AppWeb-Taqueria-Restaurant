using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta oCapaDatos = new CD_Venta(); 

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string mensaje)
        {
            return oCapaDatos.Registrar(obj, DetalleVenta, out mensaje);
        }

        public List<DetalleVenta> ListarCompras(int idcliente)
        {
            return oCapaDatos.ListarCompras(idcliente);
        }
    }
}
