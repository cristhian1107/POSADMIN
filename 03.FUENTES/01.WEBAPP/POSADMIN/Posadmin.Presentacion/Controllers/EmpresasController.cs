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
  public class EmpresasController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public EmpresasModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionEmpresas] == null)
        { Session[Constantes.SessionEmpresas] = new EmpresasModel(); }
        return Session[Constantes.SessionEmpresas] as EmpresasModel;
      }
      set { Session[Constantes.SessionEmpresas] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(EmpresasModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroId = ModelFrom.FiltroId;
        ViewModel.FiltroRazonSocial = ModelFrom.FiltroRazonSocial;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ EMPRESAS SEARCH GET : POST ]

    /// <summary>
    /// GET: Empresas Search
    /// </summary>
    /// <returns></returns>
    public ActionResult EmpresasSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosEmpresas(Connections.MainConnection, ViewModel.FiltroId, ViewModel.FiltroRazonSocial);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Empresas Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult EmpresasSearch(EmpresasModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosEmpresas(Connections.MainConnection, ViewModel.FiltroId, ViewModel.FiltroRazonSocial);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ EMPRESAS REGISTER GET : POST ]

    /// <summary>
    /// GET: Empresas Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult EmpresasRegister(Nullable<Int64> Interno)
    {
      try
      {
        if (Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaEmpresa(Connections.MainConnection, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        { ViewModel.Item = Empresas.Nuevo(); }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Empresas Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EmpresasRegister(EmpresasModel Model)
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
            if (l_proxy.Client.SaveEmpresas(Connections.MainConnection, ref l_item))
            { return RedirectToAction("EmpresasSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.PAIS_InternoOK)
          { ModelState.AddModelError("PAIS_Interno", ViewModel.Item.PAIS_InternoMSGERROR); }
          if (!ViewModel.Item.EMPR_IdOK)
          { ModelState.AddModelError("EMPR_Id", ViewModel.Item.EMPR_IdMSGERROR); }
          if (!ViewModel.Item.EMPR_RazonSocialOK)
          { ModelState.AddModelError("EMPR_RazonSocial", ViewModel.Item.EMPR_RazonSocialMSGERROR); }
          if (!ViewModel.Item.EMPR_DireccionOK)
          { ModelState.AddModelError("EMPR_Direccion", ViewModel.Item.EMPR_DireccionMSGERROR); }
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
    public ActionResult GetEmpresasSearchData()
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
        List<Empresas_Display> l_data = Empresas_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.EMPR_Id_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.EMPR_PaisNombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.EMPR_RazonSocial_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.EMPR_Direccion_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.EMPR_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.EMPR_Id_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.EMPR_Id_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.EMPR_PaisNombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.EMPR_PaisNombre_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.EMPR_RazonSocial_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.EMPR_RazonSocial_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.EMPR_Direccion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.EMPR_Direccion_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.EMPR_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.EMPR_Interno_Display).ToList();
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