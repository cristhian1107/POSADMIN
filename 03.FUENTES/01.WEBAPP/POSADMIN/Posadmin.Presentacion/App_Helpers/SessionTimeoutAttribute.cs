using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Posadmin
{
  public class SessionTimeoutAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      HttpContext ctx = HttpContext.Current;
      if (HttpContext.Current.Session[Posadmin.Constantes.SessionUsuario] == null)
      {
        filterContext.Result = new RedirectResult("~/Account/Login");
        return;
      }
      base.OnActionExecuting(filterContext);
    }

  }
}