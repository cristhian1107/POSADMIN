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
  public class SucursalesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public SucursalesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionSucursales] == null)
        { Session[Constantes.SessionSucursales] = new SucursalesModel(); }
        if ((Session[Constantes.SessionSucursales] as SucursalesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionSucursales] as SucursalesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        return Session[Constantes.SessionSucursales] as SucursalesModel;
      }
      set { Session[Constantes.SessionSucursales] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(SucursalesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.NombreSucursal = ModelFrom.NombreSucursal;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ SUCURSALES SEARCH GET : POST ]

    /// <summary>
    /// GET: Sucursales Search
    /// </summary>
    /// <returns></returns>
    public ActionResult SucursalesSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosSucursales(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.NombreSucursal);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Sucursales Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult SucursalesSearch(SucursalesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosSucursales(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.NombreSucursal);
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
    public ActionResult SucursalesDelete()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        Int64 l_interno = 0;
        if (!(Request.Form.AllKeys.Contains("DeleteInternoEmpresa") && Int64.TryParse(Request.Form["DeleteInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("DeleteInterno") && Int64.TryParse(Request.Form["DeleteInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Sucursales l_item = new Sucursales();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.SUCU_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveSucursales(Connections.MainConnection, ref l_item))
            { return RedirectToAction("SucursalesSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("SucursalesSearch", ViewModel);
    }

    #endregion

    #region [ SUCURSALES REGISTER GET : POST ]

    /// <summary>
    /// GET: Sucursales Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult SucursalesRegister(Nullable<Int64> Empresa, Nullable<Int64> Interno)
    {
      try
      {
        if (Empresa.HasValue && Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaSucursal(Connections.MainConnection, Empresa.Value, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Sucursales.Nuevo();
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
    /// POST: Sucursales Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult SucursalesRegister(SucursalesModel Model)
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
            if (l_proxy.Client.SaveSucursales(Connections.MainConnection, ref l_item))
            { return RedirectToAction("SucursalesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.SUCU_InternoOK)
          { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
          if (!ViewModel.Item.SUCU_NombreOK)
          { ModelState.AddModelError("SUCU_Nombre", ViewModel.Item.SUCU_NombreMSGERROR); }
          if (!ViewModel.Item.SUCU_DireccionOK)
          { ModelState.AddModelError("SUCU_Direccion", ViewModel.Item.SUCU_DireccionMSGERROR); }
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
    public ActionResult GetSucursalesSearchData()
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
        List<Sucursales_Display> l_data = Sucursales_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.SUCU_Nombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.SUCU_Direccion_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.SUCU_Principal_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.SUCU_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.SUCU_Nombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.SUCU_Nombre_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.SUCU_Direccion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.SUCU_Direccion_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.SUCU_Principal_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.SUCU_Principal_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.SUCU_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.SUCU_Interno_Display).ToList();
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