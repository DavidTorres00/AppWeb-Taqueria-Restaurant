using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionTienda.Filters
{
    public class ValidarSessionAttribute : ActionFilterAttribute
    {
        //Método que se ejecuta cuando una vista se muestra
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Primero accedemos al contexto y después utilizamos nuestra sesion
            if (HttpContext.Current.Session["Cliente"] == null)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}