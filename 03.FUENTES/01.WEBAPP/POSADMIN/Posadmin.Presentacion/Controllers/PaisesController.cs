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
  public class PaisesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public PaisesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionPaises] == null)
        { Session[Constantes.SessionPaises] = new PaisesModel(); }
        return Session[Constantes.SessionPaises] as PaisesModel;
      }
      set { Session[Constantes.SessionPaises] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(PaisesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroPaisNombre = ModelFrom.FiltroPaisNombre;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ PAISES SEARCH GET : POST ]

    /// <summary>
    /// GET: Paises Search
    /// </summary>
    /// <returns></returns>
    public ActionResult PaisesSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosPaises(Connections.MainConnection, ViewModel.FiltroPaisNombre);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Paises Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult PaisesSearch(PaisesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosPaises(Connections.MainConnection, ViewModel.FiltroPaisNombre);
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
    public ActionResult PaisesDelete()
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
            Paises l_item = new Paises();
            l_item.PAIS_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SavePaises(Connections.MainConnection, ref l_item))
            { return RedirectToAction("PaisesSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("PaisesSearch", ViewModel);
    }

    #endregion

    #region [ PAISES REGISTER GET : POST ]

    /// <summary>
    /// GET: Paises Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult PaisesRegister(Nullable<Int32> Interno)
    {
      try
      {
        if (Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaPais(Connections.MainConnection, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        { ViewModel.Item = Paises.Nuevo(); }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Paises Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult PaisesRegister(PaisesModel Model)
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
            if (l_proxy.Client.SavePaises(Connections.MainConnection, ref l_item))
            { return RedirectToAction("PaisesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.PAIS_InternoOK)
          { ModelState.AddModelError("PAIS_Interno", ViewModel.Item.PAIS_InternoMSGERROR); }
          if (!ViewModel.Item.PAIS_NombreOK)
          { ModelState.AddModelError("PAIS_Nombre", ViewModel.Item.PAIS_NombreMSGERROR); }
          if (!ViewModel.Item.PAIS_CodigoNumericoOK)
          { ModelState.AddModelError("PAIS_CodigoNumerico", ViewModel.Item.PAIS_CodigoNumericoMSGERROR); }
          if (!ViewModel.Item.PAIS_CodigoAlfa2OK)
          { ModelState.AddModelError("PAIS_CodigoAlfa2", ViewModel.Item.PAIS_CodigoAlfa2MSGERROR); }
          if (!ViewModel.Item.PAIS_CodigoAlfa3OK)
          { ModelState.AddModelError("PAIS_CodigoAlfa3", ViewModel.Item.PAIS_CodigoAlfa3MSGERROR); }
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
    public ActionResult GetPaisesSearchData()
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
        List<Paises_Display> l_data = Paises_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.PAIS_Nombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PAIS_CodigoAlfa2_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PAIS_CodigoAlfa3_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PAIS_Activo_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PAIS_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PAIS_Nombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PAIS_Nombre_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PAIS_CodigoAlfa2_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PAIS_CodigoAlfa2_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PAIS_CodigoAlfa3_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PAIS_CodigoAlfa3_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PAIS_Activo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PAIS_Activo_Display).ToList();
                break;
            }
            break;
          case "5":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PAIS_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PAIS_Interno_Display).ToList();
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