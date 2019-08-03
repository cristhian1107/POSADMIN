using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareFactory.Infrastructure.Utilities
{
  public class MyUtilities
  {

    /// <summary>
    /// Convert Exception a String Message Error
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string ExceptionToString(Exception ex)
    {
      string l_tecnichalMessage = string.Empty;
      if (ex != null)
      {
        l_tecnichalMessage += "Descripción: " + ex.Message + Environment.NewLine;
        l_tecnichalMessage += "Origen: " + ex.Source + Environment.NewLine;
        l_tecnichalMessage += "Método Origen: " + ex.TargetSite.ToString() + Environment.NewLine;
        l_tecnichalMessage += "Seguimiento del Error: " + ex.StackTrace + Environment.NewLine;
        if (ex.InnerException != null)
        {
          l_tecnichalMessage = l_tecnichalMessage + ex.InnerException.Message;
        }
      }
      else
      {
        l_tecnichalMessage = "No hay detalles del error.";
      }
      return l_tecnichalMessage;
    }

    /// <summary>
    /// Get DateTime - Country
    /// </summary>
    /// <param name="Country"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeCountry(String Country)
    {
      try
      {
        DateTime l_DateTimeCountry = DateTime.UtcNow;
        switch (Country)
        {
          //UTC -5 (Peru)
          case "es-PE":
            l_DateTimeCountry = DateTime.UtcNow.AddHours(-5);
            break;
          default:
            l_DateTimeCountry = DateTime.UtcNow;
            break;
        }
        return l_DateTimeCountry;
      }
      catch (Exception ex)
      { throw ex; }
    }

  }
}
