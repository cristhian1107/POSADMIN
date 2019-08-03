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
  public class HabitacionesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public HabitacionesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionHabitaciones] == null)
        { Session[Constantes.SessionHabitaciones] = new HabitacionesModel(); }
        if ((Session[Constantes.SessionHabitaciones] as HabitacionesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionHabitaciones] as HabitacionesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionHabitaciones] as HabitacionesModel).InternoSucursal == 0)
        { (Session[Constantes.SessionHabitaciones] as HabitacionesModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionHabitaciones] as HabitacionesModel;
      }
      set { Session[Constantes.SessionHabitaciones] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(HabitacionesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.CodigoTablaPIS = ModelFrom.CodigoTablaPIS;
        ViewModel.CodigoTablaTHA = ModelFrom.CodigoTablaTHA;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
      ViewModel.Item = ModelFrom.Item;
      ViewModel.Item.TABL_TablaPIS = Constantes.TablaPIS;
      ViewModel.Item.TABL_TablaTHA = Constantes.TablaTHA;
    }

    #endregion

    #region [ HABITACIONES SEARCH GET : POST ]

    /// <summary>
    /// GET: Habitaciones Search
    /// </summary>
    /// <returns></returns>
    public ActionResult HabitacionesSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_pis = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaPIS) && ViewModel.CodigoTablaPIS != "0") { l_pis = ViewModel.CodigoTablaPIS; }
          string l_tha = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaTHA) && ViewModel.CodigoTablaTHA != "0") { l_tha = ViewModel.CodigoTablaTHA; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosHabitaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, Constantes.TablaPIS, l_pis, Constantes.TablaTHA, l_tha);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Habitaciones Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult HabitacionesSearch(HabitacionesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_pis = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaPIS) && ViewModel.CodigoTablaPIS != "0") { l_pis = ViewModel.CodigoTablaPIS; }
          string l_tha = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaTHA) && ViewModel.CodigoTablaTHA != "0") { l_tha = ViewModel.CodigoTablaTHA; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosHabitaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, Constantes.TablaPIS, l_pis, Constantes.TablaTHA, l_tha);
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
    public ActionResult HabitacionesDelete()
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
            Habitaciones l_item = new Habitaciones();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.SUCU_Interno = l_internoSucursal;
            l_item.HABI_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveHabitaciones(Connections.MainConnection, ref l_item))
            { return RedirectToAction("HabitacionesSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("HabitacionesSearch", ViewModel);
    }

    #endregion

    #region [ HABITACIONES REGISTER GET : POST ]

    /// <summary>
    /// GET: Habitaciones Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult HabitacionesRegister(Nullable<Int64> Empresa, Nullable<Int64> Sucursal, Nullable<Int64> Interno)
    {
      try
      {
        if (Empresa.HasValue && Sucursal.HasValue && Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaHabitacion(Connections.MainConnection, Empresa.Value, Sucursal.Value, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Habitaciones.Nuevo();
          ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
          ViewModel.Item.SUCU_Interno = ViewModel.InternoSucursal;
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Habitaciones Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult HabitacionesRegister(HabitacionesModel Model)
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
            if (l_proxy.Client.SaveHabitaciones(Connections.MainConnection, ref l_item))
            { return RedirectToAction("HabitacionesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.SUCU_InternoOK)
          { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
          if (!ViewModel.Item.HABI_InternoOK)
          { ModelState.AddModelError("HABI_Interno", ViewModel.Item.HABI_InternoMSGERROR); }
          if (!ViewModel.Item.TABL_TablaPISOK)
          { ModelState.AddModelError("TABL_TablaPIS", ViewModel.Item.TABL_TablaPISMSGERROR); }
          if (!ViewModel.Item.TABL_InternoPISOK)
          { ModelState.AddModelError("TABL_InternoPIS", ViewModel.Item.TABL_InternoPISMSGERROR); }
          if (!ViewModel.Item.TABL_TablaTHAOK)
          { ModelState.AddModelError("TABL_TablaTHA", ViewModel.Item.TABL_TablaTHAMSGERROR); }
          if (!ViewModel.Item.TABL_InternoTHAOK)
          { ModelState.AddModelError("TABL_InternoTHA", ViewModel.Item.TABL_InternoTHAMSGERROR); }
          if (!ViewModel.Item.HABI_NumeroOK)
          { ModelState.AddModelError("HABI_Numero", ViewModel.Item.HABI_NumeroMSGERROR); }
          if (!ViewModel.Item.HABI_PrecioDiaOK)
          { ModelState.AddModelError("HABI_PrecioDia", ViewModel.Item.HABI_PrecioDiaMSGERROR); }
          if (!ViewModel.Item.HABI_PrecioHoraOK)
          { ModelState.AddModelError("HABI_PrecioHora", ViewModel.Item.HABI_PrecioHoraMSGERROR); }
          if (!ViewModel.Item.HABI_PrecioMinimoOK)
          { ModelState.AddModelError("HABI_PrecioMinimo", ViewModel.Item.HABI_PrecioMinimoMSGERROR); }
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
    public ActionResult GetHabitacionesSearchData()
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
        List<Habitaciones_Display> l_data = Habitaciones_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.TABL_NombrePIS_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TABL_NombreTHA_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.HABI_Numero_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.HABI_Activo_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.HABI_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_NombrePIS_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_NombrePIS_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_NombreTHA_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_NombreTHA_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.HABI_Numero_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.HABI_Numero_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.HABI_Activo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.HABI_Activo_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.HABI_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.HABI_Interno_Display).ToList();
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