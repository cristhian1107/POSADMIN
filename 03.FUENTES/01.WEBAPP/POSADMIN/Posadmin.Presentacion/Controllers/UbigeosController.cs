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
  public class UbigeosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public UbigeosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionUbigeos] == null)
        { Session[Constantes.SessionUbigeos] = new UbigeosModel(); }
        return Session[Constantes.SessionUbigeos] as UbigeosModel;
      }
      set { Session[Constantes.SessionUbigeos] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(UbigeosModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroUbigeoNombre = ModelFrom.FiltroUbigeoNombre;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ UBIGEOS SEARCH GET : POST ]

    /// <summary>
    /// GET: Ubigeos Search
    /// </summary>
    /// <returns></returns>
    public ActionResult UbigeosSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosUbigeos(Connections.MainConnection, ViewModel.FiltroUbigeoNombre);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Ubigeos Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult UbigeosSearch(UbigeosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosUbigeos(Connections.MainConnection, ViewModel.FiltroUbigeoNombre);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UbigeosDelete()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int32 l_interno = 0;
        l_isCorrect = (Request.Form.AllKeys.Contains("DeleteInterno") && Int32.TryParse(Request.Form["DeleteInterno"].ToString(), out l_interno));
        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Ubigeos l_item = new Ubigeos();
            l_item.UBIG_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveUbigeos(Connections.MainConnection, ref l_item))
            { return RedirectToAction("UbigeosSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("UbigeosSearch", ViewModel);
    }

    #endregion

    #region [ UBIGEOS REGISTER GET : POST ]

    /// <summary>
    /// GET: Ubigeos Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult UbigeosRegister(Nullable<Int32> Interno)
    {
      try
      {
        if (Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaUbigeo(Connections.MainConnection, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        { ViewModel.Item = Ubigeos.Nuevo(); }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Ubigeos Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UbigeosRegister(UbigeosModel Model)
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
            if (l_proxy.Client.SaveUbigeos(Connections.MainConnection, ref l_item))
            { return RedirectToAction("UbigeosSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.UBIG_InternoOK)
          { ModelState.AddModelError("UBIG_Interno", ViewModel.Item.UBIG_InternoMSGERROR); }
          if (!ViewModel.Item.PAIS_InternoOK)
          { ModelState.AddModelError("PAIS_Interno", ViewModel.Item.PAIS_InternoMSGERROR); }
          if (!ViewModel.Item.UBIG_NombreOK)
          { ModelState.AddModelError("UBIG_Nombre", ViewModel.Item.UBIG_NombreMSGERROR); }
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
    public ActionResult GetUbigeosSearchData()
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
        List<Ubigeos_Display> l_data = Ubigeos_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.UBIG_NombrePais_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.UBIG_Nombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.UBIG_NombrePadre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.UBIG_Activo_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.UBIG_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.UBIG_NombrePais_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.UBIG_NombrePais_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.UBIG_Nombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.UBIG_Nombre_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.UBIG_NombrePadre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.UBIG_NombrePadre_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.UBIG_Activo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.UBIG_Activo_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.UBIG_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.UBIG_Interno_Display).ToList();
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