using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        //Formato Imagen
        public string Base64 { get; set; }
        //TipoImagen
        public string Extension { get; set; }
    }
}
