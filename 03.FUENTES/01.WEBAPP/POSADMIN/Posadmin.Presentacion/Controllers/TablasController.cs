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
  public class TablasController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public TablasModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionTablas] == null)
        { Session[Constantes.SessionTablas] = new TablasModel(); }
        if ((Session[Constantes.SessionTablas] as TablasModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionTablas] as TablasModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        return Session[Constantes.SessionTablas] as TablasModel;
      }
      set { Session[Constantes.SessionTablas] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(TablasModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.CodigoTabla = ModelFrom.CodigoTabla;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ TABLAS SEARCH GET : POST ]

    /// <summary>
    /// GET: Tablas Search
    /// </summary>
    /// <returns></returns>
    public ActionResult TablasSearch()
    {
      try
      {
        ViewModel.Item = null;
        ViewModel.CodigoTabla = Constantes.TablaTAB;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_CodigoTabla = null;
          if (ViewModel.CodigoTabla != "0")
          { l_CodigoTabla = ViewModel.CodigoTabla; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosTablas(Connections.MainConnection, ViewModel.InternoEmpresa, l_CodigoTabla, null);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Tablas Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult TablasSearch(TablasModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_CodigoTabla = null;
          if (ViewModel.CodigoTabla != "0")
          { l_CodigoTabla = ViewModel.CodigoTabla; }
          ViewModel.Items = l_proxy.Client.ConsultaTodosTablas(Connections.MainConnection, ViewModel.InternoEmpresa, l_CodigoTabla, null);
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
    public ActionResult TablasDelete()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        String l_tabla = null;
        String l_internoTabla = null;
        if (!(Request.Form.AllKeys.Contains("DeleteInternoEmpresa") && Int64.TryParse(Request.Form["DeleteInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (Request.Form.AllKeys.Contains("DeleteTabla"))
        { l_tabla = Request.Form["DeleteTabla"].ToString(); }
        else
        { l_isCorrect = false; }
        if (Request.Form.AllKeys.Contains("DeleteInternoTabla"))
        { l_internoTabla = Request.Form["DeleteInternoTabla"].ToString(); }
        else
        { l_isCorrect = false; }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Tablas l_item = new Tablas();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.TABL_Tabla = l_tabla;
            l_item.TABL_Interno = l_internoTabla;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveTablas(Connections.MainConnection, ref l_item))
            { return RedirectToAction("TablasSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("TablasSearch", ViewModel);
    }

    #endregion

    #region [ TABLAS REGISTER GET : POST ]

    /// <summary>
    /// GET: Tablas Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult TablasRegister(Nullable<Int64> Empresa, String Tabla, String Interno)
    {
      try
      {
        if (Empresa.HasValue && !string.IsNullOrEmpty(Tabla) && !string.IsNullOrEmpty(Interno))
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaTabla(Connections.MainConnection, Empresa.Value, Tabla, Interno);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Tablas.Nuevo();
          ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Tablas Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult TablasRegister(TablasModel Model)
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
            if (l_proxy.Client.SaveTablas(Connections.MainConnection, ref l_item))
            { return RedirectToAction("TablasSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.TABL_TablaOK)
          { ModelState.AddModelError("TABL_Tabla", ViewModel.Item.TABL_TablaMSGERROR); }
          if (!ViewModel.Item.TABL_InternoOK)
          { ModelState.AddModelError("TABL_Interno", ViewModel.Item.TABL_InternoMSGERROR); }
          if (!ViewModel.Item.TABL_NombreOK)
          { ModelState.AddModelError("TABL_Nombre", ViewModel.Item.TABL_NombreMSGERROR); }
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
    public ActionResult GetTablasSearchData()
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
        List<Tablas_Display> l_data = Tablas_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.TABL_Tabla_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TABL_Interno_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TABL_Nombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.TABL_Activo_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_Tabla_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_Tabla_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_Interno_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_Nombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_Nombre_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_Activo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_Activo_Display).ToList();
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