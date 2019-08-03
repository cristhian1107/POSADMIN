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
  public class EntidadesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public EntidadesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionEntidades] == null)
        { Session[Constantes.SessionEntidades] = new EntidadesModel(); }
        if ((Session[Constantes.SessionEntidades] as EntidadesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionEntidades] as EntidadesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        return Session[Constantes.SessionEntidades] as EntidadesModel;
      }
      set { Session[Constantes.SessionEntidades] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(EntidadesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroEntidadId = ModelFrom.FiltroEntidadId;
        ViewModel.FiltroEntidadNombre = ModelFrom.FiltroEntidadNombre;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
      }
      var l_opciones = ViewModel.Item.ENTI_FuncionesEntidades;
      ViewModel.Item = ModelFrom.Item;
      ViewModel.Item.TABL_TablaTDI = Constantes.TablaTDI;
      ViewModel.Item.ENTI_FuncionesEntidades = l_opciones;
    }

    #endregion

    #region [ ENTIDADES SEARCH GET : POST ]

    /// <summary>
    /// GET: Entidades Search
    /// </summary>
    /// <returns></returns>
    public ActionResult EntidadesSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosEntidades(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroEntidadId, ViewModel.FiltroEntidadNombre);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Entidades Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult EntidadesSearch(EntidadesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosEntidades(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.FiltroEntidadId, ViewModel.FiltroEntidadNombre);
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
    public ActionResult EntidadesDelete()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_interno = 0;
        Int64 l_internoEmpresa = 0;
        if (!(Request.Form.AllKeys.Contains("DeleteInternoEmpresa") && Int64.TryParse(Request.Form["DeleteInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("DeleteInterno") && Int64.TryParse(Request.Form["DeleteInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }
        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Entidades l_item = new Entidades();
            l_item.EMPR_Interno = l_internoEmpresa;
            l_item.ENTI_Interno = l_interno;
            l_item.Instance = InstanceEntity.Deleted;
            if (l_proxy.Client.SaveEntidades(Connections.MainConnection, ref l_item))
            { return RedirectToAction("EntidadesSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("EntidadesSearch", ViewModel);
    }

    #endregion

    #region [ ENTIDADES REGISTER GET : POST ]

    /// <summary>
    /// GET: Entidades Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult EntidadesRegister(Nullable<Int64> Empresa, Nullable<Int64> Interno)
    {
      try
      {
        if (Empresa.HasValue && Interno.HasValue)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaEntidad(Connections.MainConnection, Empresa.Value, Interno.Value);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Entidades.Nuevo();
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaEntidad(Connections.MainConnection, ViewModel.InternoEmpresa, 0);
            ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
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
    /// POST: Entidades Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EntidadesRegister(EntidadesModel Model)
    {
      try
      {
        SetValuesToModel(Model);

        string l_keyPermisos = "tipo.FUNC_Activo";
        string l_keyCodigos = "tipo.TABL_InternoTEN";
        if (Request.Form.HasKeys() && Request.Form.AllKeys.Contains(l_keyCodigos) && Request.Form.AllKeys.Contains(l_keyPermisos))
        {
          var l_codigos = GetSelectedCodigos(Request.Form[l_keyCodigos].ToString(), Request.Form[l_keyPermisos].ToString());
          ViewModel.Item.ENTI_FuncionesEntidadesSeleccionadas = new ObservableCollection<FuncionesEntidades>();
          foreach (var l_codigo in l_codigos)
          { ViewModel.Item.ENTI_FuncionesEntidadesSeleccionadas.Add(new FuncionesEntidades() { TABL_TablaTEN = Constantes.TablaTEN, TABL_InternoTEN = l_codigo, FUNC_Activo = true, Instance = InstanceEntity.Added }); }
        }

        if (ViewModel.Item.Validar())
        {
          var l_item = ViewModel.Item;
          using (ProxyManager l_proxy = new ProxyManager())
          {
            l_item.AUDI_UsuarioCreacion = User.Identity.Name;
            l_item.AUDI_UsuarioModificacion = User.Identity.Name;
            if (l_proxy.Client.SaveEntidades(Connections.MainConnection, ref l_item))
            { return RedirectToAction("EntidadesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.ENTI_InternoOK)
          { ModelState.AddModelError("ENTI_Interno", ViewModel.Item.ENTI_InternoMSGERROR); }
          if (!ViewModel.Item.TABL_TablaTDIOK)
          { ModelState.AddModelError("TABL_TablaTDI", ViewModel.Item.TABL_TablaTDIMSGERROR); }
          if (!ViewModel.Item.TABL_InternoTDIOK)
          { ModelState.AddModelError("TABL_InternoTDI", ViewModel.Item.TABL_InternoTDIMSGERROR); }
          if (!ViewModel.Item.ENTI_IdOK)
          { ModelState.AddModelError("ENTI_Id", ViewModel.Item.ENTI_IdMSGERROR); }
          if (!ViewModel.Item.ENTI_NombreCompletoOK)
          { ModelState.AddModelError("ENTI_NombreCompleto", ViewModel.Item.ENTI_NombreCompletoMSGERROR); }
          if (!ViewModel.Item.ENTI_FuncionesEntidadesSeleccionadasOK)
          { ModelState.AddModelError(string.Empty, ViewModel.Item.ENTI_FuncionesEntidadesSeleccionadasMSGERROR); }
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
    public ActionResult GetEntidadesSearchData()
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
        List<Entidades_Display> l_data = Entidades_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.TABL_NombreTDI_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.ENTI_Id_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.ENTI_NombreCompleto_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.ENTI_Interno_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.TABL_NombreTDI_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.TABL_NombreTDI_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ENTI_Id_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ENTI_Id_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ENTI_NombreCompleto_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ENTI_NombreCompleto_Display).ToList();
                break;
            }
            break;
          case "4":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.ENTI_Interno_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.ENTI_Interno_Display).ToList();
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
    private IEnumerable<String> GetSelectedCodigos(String Codigos, String Seleccionados)
    {
      List<String> l_codigosSeleccionados = new List<String>();

      if (Codigos.Contains(','))
      {
        string[] l_codigos = Codigos.Split(',');
        string[] l_seleccionados = Seleccionados.Split(',');

        int l_indice = 0;
        for (int _indexCodigo = 0; _indexCodigo < l_codigos.Length; _indexCodigo++)
        {
          if (l_seleccionados[l_indice].ToUpper() == "TRUE")
          {
            l_codigosSeleccionados.Add(l_codigos[_indexCodigo].ToString());
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
            l_codigosSeleccionados.Add(Codigos);
          }
        }
        else
        {
          if (Seleccionados.Trim().ToUpper() == "TRUE")
          {
            l_codigosSeleccionados.Add(Codigos);
          }
        }
      }
      return l_codigosSeleccionados;
    }

    #endregion    
  }
}