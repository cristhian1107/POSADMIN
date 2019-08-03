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
  public class ReservacionesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public ReservacionesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionReservaciones] == null)
        { Session[Constantes.SessionReservaciones] = new ReservacionesModel(); }
        if ((Session[Constantes.SessionReservaciones] as ReservacionesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionReservaciones] as ReservacionesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionReservaciones] as ReservacionesModel).InternoSucursal == 0)
        { (Session[Constantes.SessionReservaciones] as ReservacionesModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionReservaciones] as ReservacionesModel;
      }
      set { Session[Constantes.SessionReservaciones] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(ReservacionesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroEstado = ModelFrom.FiltroEstado;
        ViewModel.FiltroHuesped = ModelFrom.FiltroHuesped;
        ViewModel.FiltroNumero = ModelFrom.FiltroNumero;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ RESERVACIONES SEARCH GET : POST ]

    /// <summary>
    /// GET: Reservaciones Search
    /// </summary>
    /// <returns></returns>
    public ActionResult ReservacionesSearch()
    {
      try
      {
        ViewModel.Item = null;
        ViewModel.FiltroEstado = "A";
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosReservaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.FiltroEstado, ViewModel.FiltroHuesped, ViewModel.FiltroNumero);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Reservaciones Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult ReservacionesSearch(ReservacionesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosReservaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.FiltroEstado, ViewModel.FiltroHuesped, ViewModel.FiltroNumero);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Cancel Item
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ReservacionesCancel()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        Int64 l_internoSucursal = 0;
        Int64 l_interno = 0;
        if (!(Request.Form.AllKeys.Contains("CancelInternoEmpresa") && Int64.TryParse(Request.Form["CancelInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("CancelInternoSucursal") && Int64.TryParse(Request.Form["CancelInternoSucursal"].ToString(), out l_internoSucursal)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("CancelInterno") && Int64.TryParse(Request.Form["CancelInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Reservaciones l_item = new Reservaciones();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.SUCU_Interno = l_internoSucursal;
            l_item.RESE_Interno = l_interno;
            l_item.RESE_Estado = "X";
            l_item.AUDI_UsuarioCreacion = User.Identity.Name;
            l_item.AUDI_UsuarioModificacion = User.Identity.Name;
            l_item.Instance = InstanceEntity.Deleted;
            string l_mensaje = l_proxy.Client.SaveReservaciones(Connections.MainConnection, ref l_item);
            if (string.IsNullOrEmpty(l_mensaje))
            { return RedirectToAction("ReservacionesSearch"); }
            else { ModelState.AddModelError(string.Empty, l_mensaje); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("ReservacionesSearch", ViewModel);
    }

    #endregion

    #region [ RESERVACIONES REGISTER GET : POST ]

    /// <summary>
    /// GET: Reservaciones Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult ReservacionesRegister(Nullable<Int64> Empresa, Nullable<Int64> Sucursal, Nullable<Int64> Interno)
    {
      try
      {
        if (Empresa.HasValue && Sucursal.HasValue && Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaReservacion(Connections.MainConnection, Empresa.Value, Sucursal.Value, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Reservaciones.Nuevo();
          ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
          ViewModel.Item.SUCU_Interno = ViewModel.InternoSucursal;
          ViewModel.Item.RESE_Estado = "A";
          ViewModel.Item.RESE_FechaInicio = MyUtilities.GetDateTimeCountry("es-PE").AddDays(1);
          ViewModel.Item.RESE_FechaFin = ViewModel.Item.RESE_FechaInicio.AddDays(1);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Reservaciones Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ReservacionesRegister(ReservacionesModel Model)
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
            string l_mensaje = l_proxy.Client.SaveReservaciones(Connections.MainConnection, ref l_item);
            if (string.IsNullOrEmpty(l_mensaje))
            { return RedirectToAction("ReservacionesSearch"); }
            else { ModelState.AddModelError(string.Empty, l_mensaje); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.SUCU_InternoOK)
          { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
          if (!ViewModel.Item.RESE_InternoOK)
          { ModelState.AddModelError("RESE_Interno", ViewModel.Item.RESE_InternoMSGERROR); }
          if (!ViewModel.Item.RESE_EstadoOK)
          { ModelState.AddModelError("RESE_Estado", ViewModel.Item.RESE_EstadoMSGERROR); }
          if (!ViewModel.Item.HABI_InternoOK)
          { ModelState.AddModelError("HABI_Interno", ViewModel.Item.HABI_InternoMSGERROR); }
          if (!ViewModel.Item.RESE_FechaInicioOK)
          { ModelState.AddModelError("RESE_FechaInicio", ViewModel.Item.RESE_FechaInicioMSGERROR); }
          if (!ViewModel.Item.RESE_FechaFinOK)
          { ModelState.AddModelError("RESE_FechaFin", ViewModel.Item.RESE_FechaFinMSGERROR); }
          if (!ViewModel.Item.RESE_HuespedIdOK)
          { ModelState.AddModelError("RESE_HuespedId", ViewModel.Item.RESE_HuespedIdMSGERROR); }
          if (!ViewModel.Item.RESE_HuespedNombreCompletoOK)
          { ModelState.AddModelError("RESE_HuespedNombreCompleto", ViewModel.Item.RESE_HuespedNombreCompletoMSGERROR); }
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
    public ActionResult GetReservacionesSearchData()
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
        List<Reservaciones_Display> l_data = Reservaciones_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.RESE_NombreEstado_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.RESE_HabitacionDescripcion_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.RESE_HuespedNombreCompleto_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.RESE_Fechas_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.RESE_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.RESE_NombreEstado_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.RESE_NombreEstado_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.RESE_HabitacionDescripcion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.RESE_HabitacionDescripcion_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.RESE_HuespedNombreCompleto_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.RESE_HuespedNombreCompleto_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.RESE_Fechas_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.RESE_Fechas_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.RESE_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.RESE_Interno_Display).ToList();
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