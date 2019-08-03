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
  public class TurnosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public TurnosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionTurnos] == null)
        { Session[Constantes.SessionTurnos] = new TurnosModel(); }
        if ((Session[Constantes.SessionTurnos] as TurnosModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionTurnos] as TurnosModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionTurnos] as TurnosModel).InternoSucursal == 0)
        { (Session[Constantes.SessionTurnos] as TurnosModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionTurnos] as TurnosModel;
      }
      set { Session[Constantes.SessionTurnos] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(TurnosModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroSucursal = ModelFrom.FiltroSucursal;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ TURNOS SEARCH GET : POST ]

    /// <summary>
    /// GET: Turnos Search
    /// </summary>
    /// <returns></returns>
    public ActionResult TurnosSearch()
    {
      try
      {
        ViewModel.Item = null;
        ViewModel.FiltroSucursal = ViewModel.InternoSucursal;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          Nullable<Int64> l_sucursal = null;
          if (ViewModel.FiltroSucursal != 0) { l_sucursal = ViewModel.FiltroSucursal; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosTurnos(Connections.MainConnection, ViewModel.InternoEmpresa, l_sucursal);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Turnos Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult TurnosSearch(TurnosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          Nullable<Int64> l_sucursal = null;
          if (ViewModel.FiltroSucursal != 0) { l_sucursal = ViewModel.FiltroSucursal; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosTurnos(Connections.MainConnection, ViewModel.InternoEmpresa, l_sucursal);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Elima Item
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult TurnosDelete()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        Int64 l_internoSucursal = 0;
        Int64 l_interno = 0;
        if (!(Request.Form.AllKeys.Contains("DeleteInternoEmpresa") && Int64.TryParse(Request.Form["DeleteInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("DeleteInternoSucursal") && Int64.TryParse(Request.Form["DeleteInternoSucursal"].ToString(), out l_internoSucursal)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("DeleteInterno") && Int64.TryParse(Request.Form["DeleteInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Turnos l_item = new Turnos();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.SUCU_Interno = l_internoSucursal;
            l_item.TURN_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveTurnos(Connections.MainConnection, ref l_item))
            { return RedirectToAction("TurnosSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("TurnosSearch", ViewModel);
    }

    #endregion

    #region [ TURNOS REGISTER GET : POST ]

    /// <summary>
    /// GET: Turnos Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult TurnosRegister(Nullable<Int64> Empresa, Nullable<Int64> Sucursal, Nullable<Int64> Interno)
    {
      try
      {
        if (Empresa.HasValue && Sucursal.HasValue && Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaTurno(Connections.MainConnection, Empresa.Value, Sucursal.Value, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Turnos.Nuevo();
          ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
          ViewModel.Item.SUCU_Interno = ViewModel.InternoSucursal;
          ViewModel.Item.TURN_Activo = true;
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Turnos Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult TurnosRegister(TurnosModel Model)
    {
      try
      {
        SetValuesToModel(Model);
        if (ViewModel.Item.Validar())
        {
          var l_item = ViewModel.Item;
          using (ProxyManager l_proxy = new ProxyManager())
          {
            l_item.AUDI_UsuarioCreacion = User.Identity.Name;
            l_item.AUDI_UsuarioModificacion = User.Identity.Name;
            if (l_proxy.Client.SaveTurnos(Connections.MainConnection, ref l_item))
            { return RedirectToAction("TurnosSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.SUCU_InternoOK)
          { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
          if (!ViewModel.Item.TURN_InternoOK)
          { ModelState.AddModelError("TURN_Interno", ViewModel.Item.TURN_InternoMSGERROR); }
          if (!ViewModel.Item.TURN_NominacionOK)
          { ModelState.AddModelError("TURN_Nominacion", ViewModel.Item.TURN_NominacionMSGERROR); }
          if (!ViewModel.Item.TURN_HoraInicioOK)
          { ModelState.AddModelError("TURN_HoraInicio", ViewModel.Item.TURN_HoraInicioMSGERROR); }
          if (!ViewModel.Item.TURN_HoraFinOK)
          { ModelState.AddModelError("TURN_HoraFin", ViewModel.Item.TURN_HoraFinMSGERROR); }
          if (!ViewModel.Item.TURN_ColorOK)
          { ModelState.AddModelError("TURN_Color", ViewModel.Item.TURN_ColorMSGERROR); }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ METODOS ]

    /// <summary>
    /// Json que sirve para llevar la tabla de resultado de Busqueda
    /// </summary>
    /// <returns></returns>
    public ActionResult GetTurnosSearchData()
    {
      JsonResult l_result = new JsonResult();
      try
      {
        // Initialization.
        string l_search = Request.Form.GetValues("search[value]")[0];
        string l_draw = Request.Form.GetValues("draw")[0];
        string l_order = Request.Form.GetValues("order[0][column]")[0];
        string l_orderDir = Request.Form.GetValues("order[0][dir]")[0];
        int l_startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        int l_pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

        // Loading.
        List<Turnos_Display> l_data = Turnos_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.TURN_Sucursal_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TURN_Nominacion_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TURN_HoraInicio_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TURN_HoraFin_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TURN_Activo_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TURN_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_Sucursal_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_Sucursal_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_Nominacion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_Nominacion_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_HoraInicio_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_HoraInicio_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_HoraFin_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_HoraFin_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_Activo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_Activo_Display).ToList();
                break;
            }
            break;
          case "6":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TURN_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TURN_Interno_Display).ToList();
                break;
            }
            break;
        }

        // Filter record count.
        int l_recFilter = l_data.Count;

        // Apply pagination.
        l_data = l_data.Skip(l_startRec).Take(l_pageSize).ToList();

        // Loading drop down lists.
        l_result = this.Json(new { draw = Convert.ToInt32(l_draw), recordsTotal = l_totalRecords, recordsFiltered = l_recFilter, data = l_data }, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        // Info
        Console.Write(ex);
      }

      // Return info.
      return l_result;
    }

    #endregion    
  }
}