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
  public class ConfiguracionesController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public ConfiguracionesModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionConfiguraciones] == null)
        { Session[Constantes.SessionConfiguraciones] = new ConfiguracionesModel(); }
        if ((Session[Constantes.SessionConfiguraciones] as ConfiguracionesModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionConfiguraciones] as ConfiguracionesModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        return Session[Constantes.SessionConfiguraciones] as ConfiguracionesModel;
      }
      set { Session[Constantes.SessionConfiguraciones] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(ConfiguracionesModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.LlaveParametro = ModelFrom.LlaveParametro;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoUsuario = ModelFrom.InternoUsuario;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ CONFIGURACIONES SEARCH GET : POST ]

    /// <summary>
    /// GET: Configuraciones Search
    /// </summary>
    /// <returns></returns>
    public ActionResult ConfiguracionesSearch()
    {
      try
      {
        ViewModel.Item = null;
        ViewModel.InternoUsuario = (Session[Constantes.SessionUsuario] as Usuarios).USUA_Interno;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosConfiguraciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoUsuario, ViewModel.LlaveParametro);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Configuraciones Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult ConfiguracionesSearch(ConfiguracionesModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        if (ViewModel.InternoUsuario != 0)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Items = l_proxy.Client.ConsultaTodosConfiguraciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoUsuario, ViewModel.LlaveParametro);
          }
        }
        else
        { ModelState.AddModelError("InternoUsuario", "Debe seleccionar un usuario"); }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ CONFIGURACIONES REGISTER GET : POST ]

    /// <summary>
    /// GET: Configuraciones Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult ConfiguracionesRegister(Nullable<Int64> Empresa, Nullable<Int64> Usuario, String LLave)
    {
      try
      {
        if (Empresa.HasValue && Usuario.HasValue && !string.IsNullOrEmpty(LLave))
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaConfiguracion(Connections.MainConnection, Empresa.Value, Usuario.Value, LLave);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Configuraciones.Nuevo();
          ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
          ViewModel.Item.USUA_Interno = ViewModel.InternoUsuario;
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Configuraciones Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ConfiguracionesRegister(ConfiguracionesModel Model)
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
            if (l_proxy.Client.SaveConfiguraciones(Connections.MainConnection, ref l_item))
            { return RedirectToAction("ConfiguracionesSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.USUA_InternoOK)
          { ModelState.AddModelError("USUA_Interno", ViewModel.Item.USUA_InternoMSGERROR); }
          if (!ViewModel.Item.CONF_LlaveOK)
          { ModelState.AddModelError("CONF_Llave", ViewModel.Item.CONF_LlaveMSGERROR); }
          if (!ViewModel.Item.CONF_ValorOK)
          { ModelState.AddModelError("CONF_Valor", ViewModel.Item.CONF_ValorMSGERROR); }
          if (!ViewModel.Item.CONF_DescripcionOK)
          { ModelState.AddModelError("CONF_Descripcion", ViewModel.Item.CONF_DescripcionMSGERROR); }
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
    public ActionResult GetConfiguracionesSearchData()
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
        List<Configuraciones_Display> l_data = Configuraciones_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.CONF_Llave_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.CONF_Valor_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.CONF_Descripcion_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.CONF_Llave_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.CONF_Llave_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.CONF_Valor_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.CONF_Valor_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.CONF_Descripcion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.CONF_Descripcion_Display).ToList();
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