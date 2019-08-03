using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posadmin.Models;
using Posadmin.BusinessEntities;
using SoftwareFactory.Infrastructure.Utilities;
using SoftwareFactory.Infrastructure.BusinessEntity;
using System.Collections.ObjectModel;

namespace Posadmin.Controllers
{
  [Authorize]
  [SessionTimeoutAttribute]
  public class RolesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public RolesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionRoles] == null)
        { Session[Constantes.SessionRoles] = new RolesModel(); }
        return Session[Constantes.SessionRoles] as RolesModel;
      }
      set { Session[Constantes.SessionRoles] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(RolesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroRolNombre = ModelFrom.FiltroRolNombre;
      }
      var l_opciones = ViewModel.Item.ROLE_Opciones;
      ViewModel.Item = ModelFrom.Item;
      ViewModel.Item.ROLE_Opciones = l_opciones;
    }

    #endregion

    #region [ ROLES SEARCH GET : POST ]

    /// <summary>
    /// GET: Roles Search
    /// </summary>
    /// <returns></returns>
    public ActionResult RolesSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosRoles(Connections.MainConnection, ViewModel.FiltroRolNombre);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Roles Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult RolesSearch(RolesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosRoles(Connections.MainConnection, ViewModel.FiltroRolNombre);
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
    public ActionResult RolesDelete()
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
            Roles l_item = new Roles();
            l_item.ROLE_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveRoles(Connections.MainConnection, ref l_item))
            { return RedirectToAction("RolesSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("RolesSearch", ViewModel);
    }

    #endregion

    #region [ ROLES REGISTER GET : POST ]

    /// <summary>
    /// GET: Roles Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult RolesRegister(Nullable<Int32> Interno)
    {
      try
      {
        if (Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaRol(Connections.MainConnection, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Roles.Nuevo();
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaRol(Connections.MainConnection, 0);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Added; }
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
    /// POST: Roles Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RolesRegister(RolesModel Model)
    {
      try
      {
        SetValuesToModel(Model);

        string l_keyPermisos = "item.OPCI_Activo";
        string l_keyCodigos = "item.OPCI_Interno";
        if (Request.Form.HasKeys() && Request.Form.AllKeys.Contains(l_keyCodigos) && Request.Form.AllKeys.Contains(l_keyPermisos))
        {
          var l_codigos = GetSelectedCodigos(Request.Form[l_keyCodigos].ToString(), Request.Form[l_keyPermisos].ToString());
          ViewModel.Item.ROLE_OpcionesSeleccionadas = new ObservableCollection<Opciones>();
          foreach (var l_codigo in l_codigos)
          { ViewModel.Item.ROLE_OpcionesSeleccionadas.Add(new Opciones() { OPCI_Interno = l_codigo, OPCI_Activo = true, Instance = InstanceEntity.Added }); }
        }

        if (ViewModel.Item.Validar())
        {
          var l_item = ViewModel.Item;
          using (ProxyManager l_proxy = new ProxyManager())
          {
            l_item.AUDI_UsuarioCreacion = User.Identity.Name;
            l_item.AUDI_UsuarioModificacion = User.Identity.Name;
            if (l_proxy.Client.SaveRoles(Connections.MainConnection, ref l_item))
            { return RedirectToAction("RolesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.ROLE_InternoOK)
          { ModelState.AddModelError("ROLE_Interno", ViewModel.Item.ROLE_InternoMSGERROR); }
          if (!ViewModel.Item.ROLE_NombreOK)
          { ModelState.AddModelError("ROLE_Nombre", ViewModel.Item.ROLE_NombreMSGERROR); }
          if (!ViewModel.Item.ROLE_OpcionesSeleccionadasOK)
          { ModelState.AddModelError(string.Empty, ViewModel.Item.ROLE_OpcionesSeleccionadasMSGERROR); }
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
    public ActionResult GetRolesSearchData()
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
        List<Roles_Display> l_data = Roles_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.ROLE_Nombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.ROLE_Descripcion_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.ROLE_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ROLE_Nombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ROLE_Nombre_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ROLE_Descripcion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ROLE_Descripcion_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ROLE_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ROLE_Interno_Display).ToList();
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

    /// <summary>
    /// Obtiene los datos aspeados en la Tabla
    /// </summary>
    /// <param name="Codigos"></param>
    /// <param name="Seleccionados"></param>
    /// <returns></returns>
    private IEnumerable<Int16> GetSelectedCodigos(String Codigos, String Seleccionados)
    {
      List<short> l_codigosSeleccionados = new List<short>();

      if (Codigos.Contains(','))
      {
        string[] l_codigos = Codigos.Split(',');
        string[] l_seleccionados = Seleccionados.Split(',');

        int l_indice = 0;
        for (int _indexCodigo = 0; _indexCodigo < l_codigos.Length; _indexCodigo++)
        {
          if (l_seleccionados[l_indice].ToUpper() == "TRUE")
          {
            short _selectedCodigo;
            if (short.TryParse(l_codigos[_indexCodigo], out _selectedCodigo))
            { l_codigosSeleccionados.Add(_selectedCodigo); }
            l_indice++;
          }
          l_indice++;
        }
      }
      else
      {
        if (Seleccionados.Contains(','))
        {
          string[] l_seleccionados = Seleccionados.Split(',');
          if (l_seleccionados[0].ToUpper() == "TRUE")
          {
            short l_codigoSeleccionado;
            if (short.TryParse(Codigos, out l_codigoSeleccionado))
            { l_codigosSeleccionados.Add(l_codigoSeleccionado); }
          }
        }
        else
        {
          if (Seleccionados.Trim().ToUpper() == "TRUE")
          {
            short _selectedCodigo;
            if (short.TryParse(Codigos, out _selectedCodigo))
            { l_codigosSeleccionados.Add(_selectedCodigo); }
          }
        }
      }
      return l_codigosSeleccionados;
    }
    #endregion    
  }
}