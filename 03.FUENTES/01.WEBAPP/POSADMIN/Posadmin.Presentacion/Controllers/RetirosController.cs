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
  public class RetirosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public RetirosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionRetiros] == null)
        { Session[Constantes.SessionRetiros] = new RetirosModel(); }
        if ((Session[Constantes.SessionRetiros] as RetirosModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionRetiros] as RetirosModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionRetiros] as RetirosModel).InternoSucursal == 0)
        { (Session[Constantes.SessionRetiros] as RetirosModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        if ((Session[Constantes.SessionRetiros] as RetirosModel).InternoUsuario == 0)
        { (Session[Constantes.SessionRetiros] as RetirosModel).InternoUsuario = (Session[Constantes.SessionUsuario] as Usuarios).USUA_Interno; }
        return Session[Constantes.SessionRetiros] as RetirosModel;
      }
      set { Session[Constantes.SessionRetiros] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(RetirosModel ModelFrom, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
    }

    #endregion

    #region [ RETIROS SEARCH GET : POST ]

    /// <summary>
    /// GET: Retiros Search
    /// </summary>
    /// <returns></returns>
    public ActionResult RetirosSearch(Boolean Calcular = false)
    {
      try
      {
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.TotalCierres = 0;
          ViewModel.ItemsCierres = l_proxy.Client.ConsultaMovimientosPendientesCierres(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.InternoUsuario, Calcular);
          ViewModel.ItemsRetiros = l_proxy.Client.ConsultaUltimosMovimientosRetiros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal);
          if (ViewModel.ItemsCierres == null || ViewModel.ItemsCierres.Count == 0)
          { ModelState.AddModelError("Cierre", "Aun no existe ningun cierre registrado"); }
          else
          { ViewModel.TotalCierres = ViewModel.ItemsCierres.Sum(pago => pago.CIER_Total); }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Retiros Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult RetirosSearch(RetirosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          //Primero Actualizamos
          //Luego Mostramos Resultdos
          ViewModel.ItemsRetiros = l_proxy.Client.ConsultaUltimosMovimientosRetiros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal);
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