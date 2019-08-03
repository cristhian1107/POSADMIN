using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Posadmin
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration Config)
    {
      Config.MapHttpAttributeRoutes();

      Config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}