using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posadmin.Models;
using Posadmin.BusinessEntities;
using SoftwareFactory.Infrastructure.Utilities;
using SoftwareFactory.Infrastructure.BusinessEntity;

namespace Posadmin.Controllers
{
  [Authorize]
  [SessionTimeoutAttribute]
  public class IndicadoresMensualesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public IndicadoresMensualesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionIndicadoresMensuales] == null)
        { Session[Constantes.SessionIndicadoresMensuales] = new IndicadoresMensualesModel(); }
        if ((Session[Constantes.SessionIndicadoresMensuales] as IndicadoresMensualesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionIndicadoresMensuales] as IndicadoresMensualesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionIndicadoresMensuales] as IndicadoresMensualesModel).InternoSucursal == 0)
        { (Session[Constantes.SessionIndicadoresMensuales] as IndicadoresMensualesModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionIndicadoresMensuales] as IndicadoresMensualesModel;
      }
      set { Session[Constantes.SessionIndicadoresMensuales] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(IndicadoresMensualesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroMes = ModelFrom.FiltroMes;
        ViewModel.FiltroAnio = ModelFrom.FiltroAnio;
        ViewModel.FiltroSucursal = ModelFrom.FiltroSucursal;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
    }

    #endregion

    #region [ INDICADORES SEARCH GET : POST ]

    /// <summary>
    /// GET: IndicadoresMensuales Search
    /// </summary>
    /// <returns></returns>
    public ActionResult IndicadoresMensualesSearch()
    {
      try
      {
        ViewModel.FiltroSucursal = ViewModel.InternoSucursal;
        ViewModel.FiltroAnio = MyUtilities.GetDateTimeCountry("es - PE").Year;
        ViewModel.FiltroMes = MyUtilities.GetDateTimeCountry("es - PE").Month - 1;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          { ViewModel.DSData = l_proxy.Client.ReporteIndicadorMensual(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroAnio, ViewModel.FiltroMes); }
          else
          {
            ViewModel.DSData = null;
            ModelState.AddModelError("FiltroSucursal", "Debe seleccionar un sucursal");
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: IndicadoresMensuales Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult IndicadoresMensualesSearch(IndicadoresMensualesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          { ViewModel.DSData = l_proxy.Client.ReporteIndicadorMensual(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroAnio, ViewModel.FiltroMes); }
          else
          {
            ViewModel.DSData = null;
            ModelState.AddModelError("FiltroSucursal", "Debe seleccionar un sucursal");
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ GET - CHART ]

    public String GetIndicadorRegistrosMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 0 && ViewModel.DSData.Tables[0] != null && ViewModel.DSData.Tables[0].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Registros\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[0].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Estados\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorTopRecaudacionDiaJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 1 && ViewModel.DSData.Tables[1] != null && ViewModel.DSData.Tables[1].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Recaudacion\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[1].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dia\": \"" + _row[0].ToString() + "\", \"Monto\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorReservasMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 2 && ViewModel.DSData.Tables[2] != null && ViewModel.DSData.Tables[2].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Reservas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[2].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Estados\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorHabitacionesUsadasJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 3 && ViewModel.DSData.Tables[3] != null && ViewModel.DSData.Tables[3].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[3].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Habitaciones\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorHabitacionesIngresosJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 4 && ViewModel.DSData.Tables[4] != null && ViewModel.DSData.Tables[4].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[4].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Habitaciones\": \"" + _row[0].ToString() + "\", \"Monto\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorVisitasTurnoJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 5 && ViewModel.DSData.Tables[5] != null && ViewModel.DSData.Tables[5].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[5].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Turnos\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorMontosTurnoJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 6 && ViewModel.DSData.Tables[6] != null && ViewModel.DSData.Tables[6].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[6].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Turnos\": \"" + _row[0].ToString() + "\", \"Monto\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorTopUsuariosRegistrosJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 7 && ViewModel.DSData.Tables[7] != null && ViewModel.DSData.Tables[7].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Registros\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[7].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Usuario\": \"" + _row[0].ToString() + "\", \"Total\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorTopUsuariosVentasJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 8 && ViewModel.DSData.Tables[8] != null && ViewModel.DSData.Tables[8].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[8].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Usuario\": \"" + _row[0].ToString() + "\", \"Total\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorTopHuespedJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 9 && ViewModel.DSData.Tables[9] != null && ViewModel.DSData.Tables[9].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[9].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Huesped\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorVisitasDiaJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 10 && ViewModel.DSData.Tables[10] != null && ViewModel.DSData.Tables[10].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[10].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dias\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorMontosDiaJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 10 && ViewModel.DSData.Tables[10] != null && ViewModel.DSData.Tables[10].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[10].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dias\": \"" + _row[0].ToString() + "\", \"Monto\": " + _row[2].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }

    #endregion
  }
}