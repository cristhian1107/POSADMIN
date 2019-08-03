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
  public class RegistrosController : Controller
  {
    #region [ VIEW MODEL ]

    /// <summary>
    /// View Model 
    /// </summary>
    public RegistrosModel ViewModel
    {
      get
      {
        if (Session[Constantes.SessionRegistros] == null)
        { Session[Constantes.SessionRegistros] = new RegistrosModel(); }
        if ((Session[Constantes.SessionRegistros] as RegistrosModel).InternoEmpresa == 0)
        { (Session[Constantes.SessionRegistros] as RegistrosModel).InternoEmpresa = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoEmpresa; }
        if ((Session[Constantes.SessionRegistros] as RegistrosModel).InternoSucursal == 0)
        { (Session[Constantes.SessionRegistros] as RegistrosModel).InternoSucursal = (Session[Constantes.SessionUsuario] as Usuarios).USUA_InternoSucursal; }
        if ((Session[Constantes.SessionRegistros] as RegistrosModel).InternoUsuario == 0)
        { (Session[Constantes.SessionRegistros] as RegistrosModel).InternoUsuario = (Session[Constantes.SessionUsuario] as Usuarios).USUA_Interno; }
        if (String.IsNullOrEmpty((Session[Constantes.SessionRegistros] as RegistrosModel).NombreUsuario))
        { (Session[Constantes.SessionRegistros] as RegistrosModel).NombreUsuario = (Session[Constantes.SessionUsuario] as Usuarios).USUA_NombreCompleto; }
        return Session[Constantes.SessionRegistros] as RegistrosModel;
      }
      set { Session[Constantes.SessionRegistros] = value; }
    }

    /// <summary>
    /// Recuperamos y/o refrescamos los datos del View Model
    /// </summary>
    /// <param name="ModelFrom"></param>
    /// <param name="Busqueda"></param>
    private void SetValuesToModel(RegistrosModel ModelFrom, Boolean Detalle = false, Boolean Busqueda = false)
    {
      if (Busqueda)
      {
        ViewModel.FiltroEstadoHabitacion = ModelFrom.FiltroEstadoHabitacion == "0" ? null : ModelFrom.FiltroEstadoHabitacion;
        ViewModel.FiltroPiso = ModelFrom.FiltroPiso == "0" ? null : ModelFrom.FiltroPiso;
        ViewModel.FiltroTipoHabitacion = ModelFrom.FiltroTipoHabitacion == "0" ? null : ModelFrom.FiltroTipoHabitacion;
        ViewModel.FiltroHuesped = ModelFrom.FiltroHuesped;
        ViewModel.FiltroNumero = ModelFrom.FiltroNumero;
        ViewModel.InternoEmpresa = ModelFrom.InternoEmpresa;
        ViewModel.InternoSucursal = ModelFrom.InternoSucursal;
      }
      if (Detalle)
      {
        ViewModel.Pago = ModelFrom.Pago;
      }
      else
      {
        var l_pagos = ViewModel.Item.REGI_ListaPagos;
        ViewModel.Item = ModelFrom.Item;
        ViewModel.Item.TABL_TablaTDI = Constantes.TablaTDI;
        ViewModel.Item.REGI_ListaPagos = l_pagos;
        ViewModel.InternoReserva = ModelFrom.InternoReserva;
      }
    }

    #endregion

    #region [ REGISTROS SEARCH GET : POST ]

    /// <summary>
    /// GET: Registros Search
    /// </summary>
    /// <returns></returns>
    public ActionResult RegistrosSearch()
    {
      try
      {
        ViewModel.Item = null;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosRegistros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.FiltroEstadoHabitacion,
                                                                  Constantes.TablaPIS, ViewModel.FiltroPiso, Constantes.TablaTHA, ViewModel.FiltroTipoHabitacion, ViewModel.FiltroHuesped, ViewModel.FiltroNumero);
        }
      }
      catch (Exception ex)
      { ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex)); }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Registros Search
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult RegistrosSearch(RegistrosModel Model)
    {
      try
      {
        SetValuesToModel(Model, false, true);
        using (ProxyManager l_proxy = new ProxyManager())
        {
          ViewModel.Items = l_proxy.Client.ConsultaTodosRegistros(Connections.MainConnection, ViewModel.InternoEmpresa, ViewModel.InternoSucursal, ViewModel.FiltroEstadoHabitacion,
                                                                  Constantes.TablaPIS, ViewModel.FiltroPiso, Constantes.TablaTHA, ViewModel.FiltroTipoHabitacion, ViewModel.FiltroHuesped, ViewModel.FiltroNumero);
        }
      }
      catch (Exception ex)
      { ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex)); }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Cancel Item
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RegistrosAcciones(String MotivoAccion)
    {
      try
      {
        Boolean l_isCorrect = true;
        Int64 l_internoEmpresa = 0;
        Int64 l_internoSucursal = 0;
        Int64 l_interno = 0;
        String l_action = String.Empty;
        if (!(Request.Form.AllKeys.Contains("ActionInternoEmpresa") && Int64.TryParse(Request.Form["ActionInternoEmpresa"].ToString(), out l_internoEmpresa)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("ActionInternoSucursal") && Int64.TryParse(Request.Form["ActionInternoSucursal"].ToString(), out l_internoSucursal)))
        { l_isCorrect = false; }
        if (!(Request.Form.AllKeys.Contains("ActionInterno") && Int64.TryParse(Request.Form["ActionInterno"].ToString(), out l_interno)))
        { l_isCorrect = false; }
        if (Request.Form.AllKeys.Contains("ActionName"))
        { l_action = Request.Form["ActionName"].ToString(); }

        if (l_isCorrect)
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            Decimal l_montoPendiente = 0;
            string l_mensaje = l_proxy.Client.AccionesRegistro(Connections.MainConnection, l_internoEmpresa, l_internoSucursal, l_interno, l_action, MotivoAccion, User.Identity.Name, ref l_montoPendiente);
            if (string.IsNullOrEmpty(l_mensaje))
            { return RedirectToAction("RegistrosSearch"); }
            else { ModelState.AddModelError(string.Empty, l_mensaje); }
          }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View("RegistrosSearch", ViewModel);
    }

    #endregion

    #region [ REGISTROS REGISTER GET : POST ]

    /// <summary>
    /// GET: Registros Register
    /// </summary>
    /// <param name="Interno"></param>
    /// <returns></returns>
    public ActionResult RegistrosRegister(Nullable<Int64> Empresa, Nullable<Int64> Sucursal, Nullable<Int64> Registro, Nullable<Int64> Habitacion, Nullable<Int64> Reserva, String Huesped, Boolean Detalle = false)
    {
      try
      {
        if (!Detalle)
        {
          if (Empresa.HasValue && Sucursal.HasValue && (Registro.HasValue || Habitacion.HasValue))
          {
            using (ProxyManager l_proxy = new ProxyManager())
            {
              string l_mensaje = string.Empty;
              ViewModel.Item = l_proxy.Client.ConsultaRegistro(Connections.MainConnection, Empresa.Value, Sucursal.Value, Registro.Value, Habitacion.Value, ref l_mensaje);
              if (string.IsNullOrEmpty(l_mensaje))
              {
                if (ViewModel.Item != null)
                {
                  if (Registro.HasValue && Registro.Value != 0)
                  {
                    ViewModel.InternoReserva = null;
                    ViewModel.Item.Instance = InstanceEntity.Modified;
                  }
                  else
                  {
                    ViewModel.Item.Instance = InstanceEntity.Added;
                    ViewModel.Item.EMPR_Interno = ViewModel.InternoEmpresa;
                    ViewModel.Item.SUCU_Interno = ViewModel.InternoSucursal;
                    ViewModel.Item.USUA_Interno = ViewModel.InternoUsuario;
                    ViewModel.InternoReserva = Reserva;
                    if (!string.IsNullOrEmpty(Huesped))
                    {
                      string[] l_datos = Huesped.Split('|');
                      if (l_datos.Count() == 3)
                      {
                        ViewModel.Item.REGI_HuespedId = l_datos[0];
                        ViewModel.Item.REGI_HuespedNombreCompleto = l_datos[1];
                        ViewModel.Item.REGI_HuespedDireccion = l_datos[2];
                      }
                    }
                  }
                }
              }
              else { ModelState.AddModelError(string.Empty, l_mensaje); }
            }
          }
          else
          { ModelState.AddModelError(string.Empty, "No se puedo encontrar lo seleccionado"); }
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    /// <summary>
    /// POST: Registros Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RegistrosRegister(RegistrosModel Model, String SubmitButton)
    {
      try
      {
        SetValuesToModel(Model);
        if (SubmitButton.Equals("Guardar"))
        {
          if (ViewModel.Item.Validar())
          {
            var l_item = ViewModel.Item;
            using (ProxyManager l_proxy = new ProxyManager())
            {
              l_item.AUDI_UsuarioCreacion = User.Identity.Name;
              l_item.AUDI_UsuarioModificacion = User.Identity.Name;
              if (l_item.REGI_Interno == 0) { l_item.Instance = InstanceEntity.Added; } else { l_item.Instance = InstanceEntity.Modified; }
              string l_mensaje = l_proxy.Client.SaveRegistros(Connections.MainConnection, ref l_item);
              if (string.IsNullOrEmpty(l_mensaje))
              {
                if (ViewModel.InternoReserva.HasValue && ViewModel.InternoReserva.Value != 0)
                {
                  Reservaciones l_itemReservaciones = new Reservaciones();
                  l_itemReservaciones.EMPR_Interno = l_item.EMPR_Interno;
                  l_itemReservaciones.SUCU_Interno = l_item.SUCU_Interno;
                  l_itemReservaciones.RESE_Interno = ViewModel.InternoReserva.Value;
                  l_itemReservaciones.RESE_Estado = "E";
                  l_itemReservaciones.AUDI_UsuarioCreacion = User.Identity.Name;
                  l_itemReservaciones.AUDI_UsuarioModificacion = User.Identity.Name;
                  l_itemReservaciones.Instance = InstanceEntity.Deleted;
                  l_proxy.Client.SaveReservaciones(Connections.MainConnection, ref l_itemReservaciones);
                }
                return RedirectToAction("RegistrosSearch");
              }
              else { ModelState.AddModelError(string.Empty, l_mensaje); }
            }
          }
          else
          {
            if (!ViewModel.Item.EMPR_InternoOK)
            { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
            if (!ViewModel.Item.SUCU_InternoOK)
            { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
            if (!ViewModel.Item.REGI_InternoOK)
            { ModelState.AddModelError("REGI_Interno", ViewModel.Item.REGI_InternoMSGERROR); }
            if (!ViewModel.Item.HABI_InternoOK)
            { ModelState.AddModelError("HABI_Interno", ViewModel.Item.HABI_InternoMSGERROR); }
            if (!ViewModel.Item.REGI_EstadoOK)
            { ModelState.AddModelError("REGI_Estado", ViewModel.Item.REGI_EstadoMSGERROR); }
            if (!ViewModel.Item.REGI_TramosOK)
            { ModelState.AddModelError("REGI_Tramos", ViewModel.Item.REGI_TramosMSGERROR); }
            if (!ViewModel.Item.REGI_CantidadOK)
            { ModelState.AddModelError("REGI_Cantidad", ViewModel.Item.REGI_CantidadMSGERROR); }
            if (!ViewModel.Item.REGI_FechaHoraEntradaOK)
            { ModelState.AddModelError("REGI_FechaHoraEntrada", ViewModel.Item.REGI_FechaHoraEntradaMSGERROR); }
            if (!ViewModel.Item.REGI_FechaHoraSalidaOK)
            { ModelState.AddModelError("REGI_FechaHoraSalida", ViewModel.Item.REGI_FechaHoraSalidaMSGERROR); }
            if (!ViewModel.Item.TABL_InternoTDIOK)
            { ModelState.AddModelError("TABL_InternoTDI", ViewModel.Item.TABL_InternoTDIMSGERROR); }
            if (!ViewModel.Item.REGI_HuespedIdOK)
            { ModelState.AddModelError("REGI_HuespedId", ViewModel.Item.REGI_HuespedIdMSGERROR); }
            if (!ViewModel.Item.REGI_HuespedNombreCompletoOK)
            { ModelState.AddModelError("REGI_HuespedNombreCompleto", ViewModel.Item.REGI_HuespedNombreCompletoMSGERROR); }
            if (!ViewModel.Item.REGI_HuespedDireccionOK)
            { ModelState.AddModelError("REGI_HuespedDireccion", ViewModel.Item.REGI_HuespedDireccionMSGERROR); }
            if (!ViewModel.Item.REGI_PrecioSugeridoOK)
            { ModelState.AddModelError("REGI_PrecioSugerido", ViewModel.Item.REGI_PrecioSugeridoMSGERROR); }
            if (!ViewModel.Item.REGI_PrecioCobradoOK)
            { ModelState.AddModelError("REGI_PrecioCobrado", ViewModel.Item.REGI_PrecioCobradoMSGERROR); }
            if (!ViewModel.Item.USUA_InternoOK)
            { ModelState.AddModelError("", ViewModel.Item.USUA_InternoMSGERROR); }
            if (!ViewModel.Item.REGI_ListaPagosOK)
            { ModelState.AddModelError(string.Empty, ViewModel.Item.REGI_ListaPagosMSGERROR); }
          }
        }
        else if (SubmitButton.Equals("Calcular"))
        {
          if (ViewModel.Item.ValidarCalculo())
          {
            var l_item = ViewModel.Item;
            using (ProxyManager l_proxy = new ProxyManager())
            {
              var l_itemCalculado = l_proxy.Client.CalcularRegistro(Connections.MainConnection, l_item);
              ViewModel.Item = l_itemCalculado;
              ViewModel.Item.TABL_TablaTDI = Constantes.TablaTDI;
              ViewModel.Item.USUA_Interno = ViewModel.InternoUsuario;
              ViewModel.Item.REGI_HuespedId = Model.Item.REGI_HuespedId;
              ViewModel.Item.REGI_HuespedNombreCompleto = Model.Item.REGI_HuespedNombreCompleto;
              ViewModel.Item.REGI_HuespedDireccion = Model.Item.REGI_HuespedDireccion;
              /*return View(ViewModel);*/
              return RedirectToAction("RegistrosRegister", new { Detalle = true });
            }
          }
          else
          {
            if (!ViewModel.Item.EMPR_InternoOK)
            { ModelState.AddModelError("EMPR_Interno", ViewModel.Item.EMPR_InternoMSGERROR); }
            if (!ViewModel.Item.SUCU_InternoOK)
            { ModelState.AddModelError("SUCU_Interno", ViewModel.Item.SUCU_InternoMSGERROR); }
            if (!ViewModel.Item.REGI_InternoOK)
            { ModelState.AddModelError("REGI_Interno", ViewModel.Item.REGI_InternoMSGERROR); }
            if (!ViewModel.Item.HABI_InternoOK)
            { ModelState.AddModelError("HABI_Interno", ViewModel.Item.HABI_InternoMSGERROR); }
            if (!ViewModel.Item.REGI_TramosOK)
            { ModelState.AddModelError("REGI_Tramos", ViewModel.Item.REGI_TramosMSGERROR); }
            if (!ViewModel.Item.REGI_CantidadOK)
            { ModelState.AddModelError("REGI_Cantidad", ViewModel.Item.REGI_CantidadMSGERROR); }
          }
        }
        else if (SubmitButton.Equals("Pagar"))
        { return RedirectToAction("PagoRegistrosRegister", "Registros"); }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, MyUtilities.ExceptionToString(ex));
      }
      return View(ViewModel);
    }

    #endregion

    #region [ PAGOS REGISTROS REGISTER GET : POST ]

    /// <summary>
    /// GET: Pago Registros Register
    /// </summary>
    /// <returns></returns>
    public ActionResult PagoRegistrosRegister()
    {
      ViewModel.Pago = DetallesPagosRegistros.Nuevo();
      ViewModel.Pago.PAGO_FechaHoraRegistro = MyUtilities.GetDateTimeCountry("es-PE");
      ViewModel.Pago.USUA_Interno = ViewModel.InternoUsuario;
      return View(ViewModel);
    }

    /// <summary>
    ///  POST: Pago Registros Register
    /// </summary>
    /// <param name="Model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult PagoRegistrosRegister(RegistrosModel Model)
    {
      try
      {
        SetValuesToModel(Model, true, false);
        if (ViewModel.Pago.Validar())
        {
          int l_item = 0;
          string l_tipo = string.Empty;
          if (ViewModel.Item.REGI_ListaPagos != null && ViewModel.Item.REGI_ListaPagos.Count > 0)
          { l_item = ViewModel.Item.REGI_ListaPagos.Max(detalle => detalle.PAGO_Item); }
          else
          { ViewModel.Item.REGI_ListaPagos = new ObservableCollection<DetallesPagosRegistros>(); }
          l_item = l_item + 1;
          ViewModel.Pago.PAGO_Item = l_item;
          ViewModel.Pago.PAGO_UsuarioNombre = ViewModel.NombreUsuario;
          switch (ViewModel.Pago.PAGO_Tipo)
          {
            case "E":
              ViewModel.Pago.PAGO_TipoNombre = "ENTRADA";
              break;
            case "S":
              ViewModel.Pago.PAGO_TipoNombre = "SALIDA";
              break;
            case "A":
              ViewModel.Pago.PAGO_TipoNombre = "ADICIONAL";
              break;
            default:
              ViewModel.Pago.PAGO_TipoNombre = "INDEFINIDO";
              break;
          }
          ViewModel.Item.REGI_ListaPagos.Add(ViewModel.Pago.Clone() as DetallesPagosRegistros);
          return RedirectToAction("RegistrosRegister", new { Detalle = true });
        }
        else
        {
          if (!ViewModel.Pago.PAGO_TipoOK)
          { ModelState.AddModelError("PAGO_Tipo", ViewModel.Pago.PAGO_TipoMSGERROR); }
          if (!ViewModel.Pago.PAGO_FechaHoraRegistroOK)
          { ModelState.AddModelError("PAGO_FechaHoraRegistro", ViewModel.Pago.PAGO_FechaHoraRegistroMSGERROR); }
          if (!ViewModel.Pago.PAGO_MontoCanceladoOK)
          { ModelState.AddModelError("PAGO_MontoCancelado", ViewModel.Pago.PAGO_MontoCanceladoMSGERROR); }
          if (!ViewModel.Pago.USUA_InternoOK)
          { ModelState.AddModelError("", ViewModel.Pago.USUA_InternoMSGERROR); }
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