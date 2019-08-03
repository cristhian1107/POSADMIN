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
  public class ParametrosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public ParametrosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionParametros] == null)
        { Session[Constantes.SessionParametros] = new ParametrosModel(); }
        if ((Session[Constantes.SessionParametros] as ParametrosModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionParametros] as ParametrosModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        return Session[Constantes.SessionParametros] as ParametrosModel;
      }
      set { Session[Constantes.SessionParametros] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(ParametrosModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.LlaveParametro = ModelFrom.LlaveParametro;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
      }
      ViewModel.Item = ModelFrom.Item;
    }

    #endregion

    #region [ PARAMETROS SEARCH GET : POST ]

    /// <summary>
    /// GET: Parametros Search
    /// </summary>
    /// <returns></returns>
    public ActionResult ParametrosSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosParametros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.LlaveParametro);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Parametros Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult ParametrosSearch(ParametrosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosParametros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.LlaveParametro);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ PARAMETROS REGISTER GET : POST ]

    /// <summary>
    /// GET: Parametros Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult ParametrosRegister(Nullable<Int64> Empresa, String LLave)
    {
      try
      {
        if (Empresa.HasValue && !string.IsNullOrEmpty(LLave))
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            ViewModel.Item = l_proxy.Client.ConsultaParametro(Connections.MainConnection, Empresa.Value, LLave);
            if (ViewModel.Item != null) { ViewModel.Item.Instance = InstanceEntity.Modified; }
          }
        }
        else
        {
          ViewModel.Item = Parametros.Nuevo();
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
    /// POST: Parametros Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ParametrosRegister(ParametrosModel Model)
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
            if (l_proxy.Client.SaveParametros(Connections.MainConnection, ref l_item))
            { return RedirectToAction("ParametrosSearch"); }
          }
        }
        else
        {
          if (!ViewModel.Item.EMPR_InternoOK)
          { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
          if (!ViewModel.Item.PARA_LlaveOK)
          { ModelState.AddModelError("PARA_Llave", ViewModel.Item.PARA_LlaveMSGERROR); }
          if (!ViewModel.Item.PARA_ValorOK)
          { ModelState.AddModelError("PARA_Valor", ViewModel.Item.PARA_ValorMSGERROR); }
          if (!ViewModel.Item.PARA_DescripcionOK)
          { ModelState.AddModelError("PARA_Descripcion", ViewModel.Item.PARA_DescripcionMSGERROR); }
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
    public ActionResult GetParametrosSearchData()
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
        List<Parametros_Display> l_data = Parametros_Display.ToList(ViewModel.Items);

        // Total record count.
        int l_totalRecords = l_data.Count;

        // Verification.
        if (!string.IsNullOrEmpty(l_search) &&
            !string.IsNullOrWhiteSpace(l_search))
        {
          // Apply search
          l_data = l_data.Where(p =>
                                p.PARA_Llave_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PARA_Valor_Display.ToString().ToLower().Contains(l_search.ToLower()) ||
                                p.PARA_Descripcion_Display.ToString().ToLower().Contains(l_search.ToLower())).ToList();
        }

        // Sorting.
        switch (l_order)
        {
          case "1":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PARA_Llave_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PARA_Llave_Display).ToList();
                break;
            }
            break;
          case "2":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PARA_Valor_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PARA_Valor_Display).ToList();
                break;
            }
            break;
          case "3":
            switch (l_orderDir.ToUpper())
            {
              case "ASC":
                l_data = l_data.OrderBy(o => o.PARA_Descripcion_Display).ToList();
                break;
              case "DESC":
                l_data = l_data.OrderByDescending(o => o.PARA_Descripcion_Display).ToList();
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