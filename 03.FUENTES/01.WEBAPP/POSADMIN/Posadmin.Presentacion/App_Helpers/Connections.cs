using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Posadmin
{
  public static class Connections
  {
    public static string m_mainconnection;
    public static string MainConnection
    {
      get
      {
        var StringConnectionPath = HttpContext.Current.Server.MapPath("~/Web.config");
        var StringConnectionMap = new ExeConfigurationFileMap() { ExeConfigFilename = StringConnectionPath };
        var config = ConfigurationManager.OpenMappedExeConfiguration(StringConnectionMap, ConfigurationUserLevel.None);
        if ((config.ConnectionStrings.ConnectionStrings["DefaultConnection"] != null))
        { m_mainconnection = config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString; }
        else { m_mainconnection = string.Empty; }
        return m_mainconnection;
      }
    }
  }
}