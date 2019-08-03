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
  public class IndicadoresSemanalesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public IndicadoresSemanalesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionIndicadoresSemanales] == null)
        { Session[Constantes.SessionIndicadoresSemanales] = new IndicadoresSemanalesModel(); }
        if ((Session[Constantes.SessionIndicadoresSemanales] as IndicadoresSemanalesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionIndicadoresSemanales] as IndicadoresSemanalesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionIndicadoresSemanales] as IndicadoresSemanalesModel).InternoSucursal == 0)
        { (Session[Constantes.SessionIndicadoresSemanales] as IndicadoresSemanalesModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionIndicadoresSemanales] as IndicadoresSemanalesModel;
      }
      set { Session[Constantes.SessionIndicadoresSemanales] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(IndicadoresSemanalesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroFecDesde = ModelFrom.FiltroFecDesde;
        ViewModel.FiltroFecHasta = ModelFrom.FiltroFecHasta;
        ViewModel.FiltroSucursal = ModelFrom.FiltroSucursal;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
    }

    #endregion

    #region [ INDICADORES SEARCH GET : POST ]

    /// <summary>
    /// GET: IndicadoresSemanales Search
    /// </summary>
    /// <returns></returns>
    public ActionResult IndicadoresSemanalesSearch()
    {
      try
      {
        ViewModel.FiltroSucursal = ViewModel.InternoSucursal;
        ViewModel.FiltroFecHasta = MyUtilities.GetDateTimeCountry("es - PE").AddDays(-1);
        ViewModel.FiltroFecDesde = ViewModel.FiltroFecHasta.AddDays(-7);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          {
            if ((ViewModel.FiltroFecHasta - ViewModel.FiltroFecDesde).Days <= 7)
            { ViewModel.DSData = l_proxy.Client.ReporteIndicadorSemanal(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroFecDesde, ViewModel.FiltroFecHasta); }
            else
            {
              ViewModel.DSData = null;
              ModelState.AddModelError(string.Empty, "El rango de fechas no debe superar los 7 dias");
            }
          }
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
    /// POST: IndicadoresSemanales Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult IndicadoresSemanalesSearch(IndicadoresSemanalesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          if (ViewModel.FiltroSucursal != 0)
          {
            if ((ViewModel.FiltroFecHasta - ViewModel.FiltroFecDesde).Days <= 7)
            { ViewModel.DSData = l_proxy.Client.ReporteIndicadorSemanal(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroSucursal, ViewModel.FiltroFecDesde, ViewModel.FiltroFecHasta); }
            else
            {
              ViewModel.DSData = null;
              ModelState.AddModelError(string.Empty, "El rango de fechas no debe superar los 7 dias");
            }
          }
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

    public String GetIndicadorVentasDiaJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 0 && ViewModel.DSData.Tables[0] != null && ViewModel.DSData.Tables[0].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Ventas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[0].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dias\": \"" + _row[0].ToString() + "\", \"Ideal\": " + _row[1].ToString() + ", \"Esperada\": " + _row[2].ToString() + ", \"Obtenida\": " + _row[3].ToString() + " }";
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
    public String GetIndicadorInformeRegistrosJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 2 && ViewModel.DSData.Tables[2] != null && ViewModel.DSData.Tables[2].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Registros\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[2].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dias\": \"" + _row[0].ToString() + "\", \"Activos\": " + _row[1].ToString() + ", \"Anulados\": " + _row[2].ToString() + ", \"Cancelados\": " + _row[3].ToString() + " }";
          _inicio = false;
        }
        _data += "] }";

        return _data;
      }
      else
      { return ""; }
    }
    public String GetIndicadorInformeReservasJSON()
    {
      if (ViewModel.DSData != null && ViewModel.DSData.Tables.Count > 3 && ViewModel.DSData.Tables[3] != null && ViewModel.DSData.Tables[3].Rows.Count > 0)
      {
        String _data = "";
        _data += "{ \"Reservas\" : [";
        Boolean _inicio = true;
        foreach (System.Data.DataRow _row in ViewModel.DSData.Tables[3].Rows)
        {
          if (!_inicio) { _data += ","; }
          _data += "{ \"Dias\": \"" + _row[0].ToString() + "\", \"Inconclusos\": " + _row[1].ToString() + ", \"Anulados\": " + _row[2].ToString() + ", \"Concluidos\": " + _row[3].ToString() + " }";
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