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
  public class IndicadoresAnualesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public IndicadoresAnualesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionIndicadoresAnuales] == null)
        { Session[Constantes.SessionIndicadoresAnuales] = new IndicadoresAnualesModel(); }
        if ((Session[Constantes.SessionIndicadoresAnuales] as IndicadoresAnualesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionIndicadoresAnuales] as IndicadoresAnualesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionIndicadoresAnuales] as IndicadoresAnualesModel).InternoSucursal == 0)
        { (Session[Constantes.SessionIndicadoresAnuales] as IndicadoresAnualesModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionIndicadoresAnuales] as IndicadoresAnualesModel;
      }
      set { Session[Constantes.SessionIndicadoresAnuales] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(IndicadoresAnualesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroAnio = ModelFrom.FiltroAnio;
        ViewModel.FiltroSucursal = ModelFrom.FiltroSucursal;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
    }

    #endregion

    #region [ INDICADORES SEARCH GET : POST ]

    /// <summary>
    /// GET: IndicadoresAnuales Search
    /// </summary>
    /// <returns></returns>
    public ActionResult IndicadoresAnualesSearch()
    {
      try
      {
        ViewModel.FiltroSucursal = ViewModel.InternoSucursal;
        ViewModel.FiltroAnio = MyUtilities.GetDateTimeCountry("es - PE").Year;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          { ViewModel.DSData = l_proxy.Client.ReporteIndicadorAnual(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroAnio); }
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
    /// POST: IndicadoresAnuales Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult IndicadoresAnualesSearch(IndicadoresAnualesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          { ViewModel.DSData = l_proxy.Client.ReporteIndicadorAnual(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroAnio); }
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

    public String GetIndicadorVentasMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 0 && ViewModel.DSData.Tables[0] != null && ViewModel.DSData.Tables[0].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[0].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Meses\": \"" + _row[1].ToString() + "\", \"Montos\": " + _row[2].ToString() + ", \"Pagos\": " + _row[3].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorInformeMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 1 && ViewModel.DSData.Tables[1] != null && ViewModel.DSData.Tables[1].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Registros\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[1].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Meses\": \"" + _row[0].ToString() + "\", \"Activos\": " + _row[1].ToString() + ", \"Anulados\": " + _row[2].ToString() + ", \"Cancelados\": " + _row[3].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorVisitasMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 2 && ViewModel.DSData.Tables[2] != null && ViewModel.DSData.Tables[2].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[2].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Meses\": \"" + _row[0].ToString() + "\", \"Cantidad\": " + _row[1].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorMontosMesJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 2 && ViewModel.DSData.Tables[2] != null && ViewModel.DSData.Tables[2].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[2].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Meses\": \"" + _row[0].ToString() + "\", \"Monto\": " + _row[2].ToString() + " }";
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
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 3 && ViewModel.DSData.Tables[3] != null && ViewModel.DSData.Tables[3].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Reservas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[3].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Meses\": \"" + _row[0].ToString() + "\", \"Inconclusos\": " + _row[1].ToString() + ", \"Anulados\": " + _row[2].ToString() + ", \"Concluidos\": " + _row[3].ToString() + " }";
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
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 4 && ViewModel.DSData.Tables[4] != null && ViewModel.DSData.Tables[4].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Visitas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[4].Rows)
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

    #endregion
  }
}