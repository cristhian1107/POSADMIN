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
  public class LimpiezaController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public LimpiezaModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionLimpieza] == null)
        { Session[Constantes.SessionLimpieza] = new LimpiezaModel(); }
        if ((Session[Constantes.SessionLimpieza] as LimpiezaModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionLimpieza] as LimpiezaModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionLimpieza] as LimpiezaModel).InternoSucursal == 0)
        { (Session[Constantes.SessionLimpieza] as LimpiezaModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        return Session[Constantes.SessionLimpieza] as LimpiezaModel;
      }
      set { Session[Constantes.SessionLimpieza] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(LimpiezaModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.CodigoTablaPIS = ModelFrom.CodigoTablaPIS;
        ViewModel.CodigoTablaTHA = ModelFrom.CodigoTablaTHA;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
      ViewModel.Item = ModelFrom.Item;
      ViewModel.Item.TABL_TablaPIS = Constantes.TablaPIS;
      ViewModel.Item.TABL_TablaTHA = Constantes.TablaTHA;
    }

    #endregion

    #region [ LIMPIEZA SEARCH GET : POST ]

    /// <summary>
    /// GET: Limpieza Search
    /// </summary>
    /// <returns></returns>
    public ActionResult LimpiezaSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_pis = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaPIS) && ViewModel.CodigoTablaPIS != "0") { l_pis = ViewModel.CodigoTablaPIS; }
          string l_tha = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaTHA) && ViewModel.CodigoTablaTHA != "0") { l_tha = ViewModel.CodigoTablaTHA; }
          ViewModel.Items = l_proxy.Client.ConsultaEstadoHabitaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, Constantes.TablaPIS, l_pis, Constantes.TablaTHA, l_tha);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Limpieza Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult LimpiezaSearch(LimpiezaModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          string l_pis = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaPIS) && ViewModel.CodigoTablaPIS != "0") { l_pis = ViewModel.CodigoTablaPIS; }
          string l_tha = null;
          if (!string.IsNullOrEmpty(ViewModel.CodigoTablaTHA) && ViewModel.CodigoTablaTHA != "0") { l_tha = ViewModel.CodigoTablaTHA; }
          ViewModel.Items = l_proxy.Client.ConsultaEstadoHabitaciones(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, Constantes.TablaPIS, l_pis, Constantes.TablaTHA, l_tha);
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
    public ActionResult LimpiezaEstado()
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        Int64 l_internoSucursal = 0;
        Int64 l_interno = 0;
        Boolean l_limpio = false;
        if (!(Request.Form.AllKeys.Contains("UpdateInternoEmpresa") && Int64.TryParse(Request.Form["UpdateInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("UpdateInternoSucursal") && Int64.TryParse(Request.Form["UpdateInternoSucursal"].ToString(), out l_internoSucursal)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("UpdateInterno") && Int64.TryParse(Request.Form["UpdateInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }
        if (Request.Form.AllKeys.Contains("UpdateLimpio"))
        { l_limpio = Request.Form["UpdateLimpio"].ToString().Equals("1"); }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            if (l_proxy.Client.ActualizarLimpiezaHabitaciones(Connections.MainConnection, l_internoEmpresa, l_internoSucursal, l_interno, l_limpio, User.Identity.Name))
            { return RedirectToAction("LimpiezaSearch"); }
            else { ModelState.AddModelError(string.Empty, "No se ha podido eliminar el registro seleccionado."); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("HabitacionesSearch", ViewModel);
    }

    #endregion
  }
}