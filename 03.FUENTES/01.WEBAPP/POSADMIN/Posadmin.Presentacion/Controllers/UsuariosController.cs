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
  public class UsuariosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public UsuariosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionUsuarios] == null)
        { Session[Constantes.SessionUsuarios] = new UsuariosModel(); }
        return Session[Constantes.SessionUsuarios] as UsuariosModel;
      }
      set { Session[Constantes.SessionUsuarios] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(UsuariosModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroUsuario = ModelFrom.FiltroUsuario;
      }
      var l_empresas = ViewModel.Item.USUA_Empresas;
      ViewModel.Item = ModelFrom.Item;
      ViewModel.Item.USUA_Empresas = l_empresas;
    }

    #endregion

    #region [ USUARIOS SEARCH GET : POST ]

    /// <summary>
    /// GET: Usuarios Search
    /// </summary>
    /// <returns></returns>
    public ActionResult UsuariosSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosUsuarios(Connections.MainConnection, ViewModel.FiltroUsuario);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Usuarios Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult UsuariosSearch(UsuariosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosUsuarios(Connections.MainConnection, ViewModel.FiltroUsuario);
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
    public ActionResult UsuariosDelete()
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
            Usuarios l_item = new Usuarios();
            l_item.ROLE_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveUsuarios(Connections.MainConnection, ref l_item))
            { return RedirectToAction("UsuariosSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("UsuariosSearch", ViewModel);
    }

    #endregion

    #region [ USUARIOS REGISTER GET : POST ]

    /// <summary>
    /// GET: Usuarios Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult UsuariosRegister(Nullable<Int32> Interno, Boolean ModoEditar = true)
    {
      ViewModel.ModoEditar = ModoEditar;
      try
      {
        if (Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaUsuario(Connections.MainConnection, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Usuarios.Nuevo();
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaUsuario(Connections.MainConnection, 0);
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
    /// POST: Usuarios Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UsuariosRegister(UsuariosModel Model)
    {
      try
      {
        SetValuesToModel(Model);

        if (Model.ModoEditar)
        {
          string l_keyPermisos = "empresa.EMPR_Activo";
          string l_keyCodigos = "empresa.EMPR_Interno";
          if (Request.Form.HasKeys() && Request.Form.AllKeys.Contains(l_keyCodigos) && Request.Form.AllKeys.Contains(l_keyPermisos))
          {
            var l_codigos = GetSelectedCodigos(Request.Form[l_keyCodigos].ToString(), Request.Form[l_keyPermisos].ToString());
            ViewModel.Item.USUA_EmpresasSeleccionadas = new ObservableCollection<Empresas>();
            foreach (var l_codigo in l_codigos)
            { ViewModel.Item.USUA_EmpresasSeleccionadas.Add(new Empresas() { EMPR_Interno = l_codigo, Instance = InstanceEntity.Added }); }
          }
        }

        if (ViewModel.Item.Validar(Model.ModoEditar))
        {
          var l_item = ViewModel.Item;
          using (ProxyManager l_proxy = new ProxyManager())
          {
            l_item.AUDI_UsuarioCreacion = User.Identity.Name;
            l_item.AUDI_UsuarioModificacion = User.Identity.Name;
            if (l_proxy.Client.SaveUsuarios(Connections.MainConnection, ref l_item))
            {
              if (Model.ModoEditar)
              { return RedirectToAction("UsuariosSearch"); }
              else
              { return RedirectToAction("Index", "Home", new { Area = "" }); }
            }
          }
        }
        else
        {
          if (!ViewModel.Item.USUA_InternoOK)
          { ModelState.AddModelError("USUA_Interno", ViewModel.Item.USUA_InternoMSGERROR); }
          if (!ViewModel.Item.ROLE_InternoOK)
          { ModelState.AddModelError("ROLE_Interno", ViewModel.Item.ROLE_InternoMSGERROR); }
          if (!ViewModel.Item.USUA_NombreCompletoOK)
          { ModelState.AddModelError("USUA_NombreCompleto", ViewModel.Item.USUA_NombreCompletoMSGERROR); }
          if (!ViewModel.Item.USUA_UsuarioOK)
          { ModelState.AddModelError("USUA_Usuario", ViewModel.Item.USUA_UsuarioMSGERROR); }
          if (!ViewModel.Item.USUA_ContrasenaOK)
          { ModelState.AddModelError("USUA_Contrasena", ViewModel.Item.USUA_ContrasenaMSGERROR); }
          if (!ViewModel.Item.USUA_CorreoOK)
          { ModelState.AddModelError("USUA_Correo", ViewModel.Item.USUA_CorreoMSGERROR); }
          if (!ViewModel.Item.USUA_EmpresasSeleccionadasOK)
          { ModelState.AddModelError(string.Empty, ViewModel.Item.USUA_EmpresasSeleccionadasMSGERROR); }
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
    public ActionResult GetUsuariosSearchData()
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
        List<Usuarios_Display> l_data = Usuarios_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.USUA_RolNombre_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.USUA_Usuario_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.USUA_Correo_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.USUA_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.USUA_RolNombre_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.USUA_RolNombre_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.USUA_Usuario_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.USUA_Usuario_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.USUA_Correo_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.USUA_Correo_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.USUA_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.USUA_Interno_Display).ToList();
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