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
  public class CierresController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public CierresModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionCierres] == null)
        { Session[Constantes.SessionCierres] = new CierresModel(); }
        if ((Session[Constantes.SessionCierres] as CierresModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionCierres] as CierresModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionCierres] as CierresModel).InternoSucursal == 0)
        { (Session[Constantes.SessionCierres] as CierresModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        if ((Session[Constantes.SessionCierres] as CierresModel).InternoUsuario == 0)
        { (Session[Constantes.SessionCierres] as CierresModel).InternoUsuario = (Session[Constantes.SessionUsuario] as Usuarios).USUA_Interno; }
        return Session[Constantes.SessionCierres] as CierresModel;
      }
      set { Session[Constantes.SessionCierres] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(CierresModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
    }

    #endregion

    #region [ CIERRES SEARCH GET : POST ]

    /// <summary>
    /// GET: Cierres Search
    /// </summary>
    /// <returns></returns>
    public ActionResult CierresSearch(Boolean Calcular = false)
    {
      try
      {
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.TotalPagos = 0;
          ViewModel.ItemsPagos = l_proxy.Client.ConsultaMovimientosUsuarioPagos(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.InternoUsuario, Calcular);
          ViewModel.ItemsCierres = l_proxy.Client.ConsultaUltimosMovimientosCierres(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal);
          if (ViewModel.ItemsPagos == null || ViewModel.ItemsPagos.Count == 0)
          { ModelState.AddModelError("Pago", "Aun no tiene ningun pago registrado"); }
          else
          { ViewModel.TotalPagos = ViewModel.ItemsPagos.Sum(pago => pago.PAGO_MontoCancelado); }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Cierres Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CierresSearch(CierresModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          //Primero Actualizamos
          //Luego Mostramos Resultdos
          ViewModel.ItemsCierres = l_proxy.Client.ConsultaUltimosMovimientosCierres(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal);
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion
  }
}