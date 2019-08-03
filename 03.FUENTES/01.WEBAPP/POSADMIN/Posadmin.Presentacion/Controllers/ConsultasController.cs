using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Converters;
using System.Text;
using Newtonsoft.Json;
using Posadmin.BusinessEntities;
using Posadmin.Models;
using SoftwareFactory.Infrastructure.Utilities;

namespace Posadmin.Controllers
{
  [SessionTimeoutAttribute]
  public class ConsultasController : ApiController
  {
    #region [ PAISES ]

    public ObservableCollection<Paises> GetPaises()
    {
      ObservableCollection<Paises> Items = new ObservableCollection<Paises>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaActivosPaises(Connections.MainConnection); }
      return Items;
    }

    #endregion

    #region [ UBIGEOS ]

    public ObservableCollection<Ubigeos> GetUbigeosPadres()
    {
      ObservableCollection<Ubigeos> Items = new ObservableCollection<Ubigeos>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaPadresActivosUbigeos(Connections.MainConnection); }
      return Items;
    }
    public ObservableCollection<Ubigeos> GetUbigeosPorNivel(Int32 InternoPadre, Int32 InternoPais)
    {
      int? l_InternoPadre = null;
      if (InternoPadre != 0)
      { l_InternoPadre = InternoPadre; }
      ObservableCollection<Ubigeos> Items = new ObservableCollection<Ubigeos>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaUbigeosPorNiveles(Connections.MainConnection, l_InternoPadre, InternoPais); }
      return Items;
    }

    #endregion

    #region [ ROLES ]

    public ObservableCollection<Roles> GetRoles()
    {
      ObservableCollection<Roles> Items = new ObservableCollection<Roles>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaTodosRoles(Connections.MainConnection, null); }
      return Items;
    }

    #endregion

    #region [ TABLAS ]

    public ObservableCollection<Tablas> GetTablas(Int64 InternoEmpresa, String Tabla)
    {
      ObservableCollection<Tablas> Items = new ObservableCollection<Tablas>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaTodosTablas(Connections.MainConnection, InternoEmpresa, Tabla, null); }
      return Items;
    }
    public ObservableCollection<Tablas> GetTablasActivas(Int64 InternoEmpresa, String Tabla)
    {
      ObservableCollection<Tablas> Items = new ObservableCollection<Tablas>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaTodosTablas(Connections.MainConnection, InternoEmpresa, Tabla, true); }
      return Items;
    }

    #endregion

    #region [ USUARIOS ]

    public ObservableCollection<Usuarios> GetUsuariosPorEmpresa(Int64 InternoEmpresa)
    {
      ObservableCollection<Usuarios> Items = new ObservableCollection<Usuarios>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaUsuariosPorEmpresa(Connections.MainConnection, InternoEmpresa); }
      return Items;
    }

    #endregion

    #region [ SUCURSALES ]

    public ObservableCollection<Sucursales> GetSucursalesPorEmpresa(Int64 InternoEmpresa)
    {
      ObservableCollection<Sucursales> Items = new ObservableCollection<Sucursales>();
      using (ProxyManager l_proxy = new ProxyManager())
      { Items = l_proxy.Client.ConsultaTodosSucursales(Connections.MainConnection, InternoEmpresa, null); }
      return Items;
    }

    #endregion

    #region [ HABITACIONES ]

    public ObservableCollection<Habitaciones> GetHabitacionesLibres(Int64 InternoEmpresa, Int64 InternoSucursal, String InternoPIS, String InternoTHA)
    {
      ObservableCollection<Habitaciones> Items = new ObservableCollection<Habitaciones>();
      using (ProxyManager l_proxy = new ProxyManager())
      {
        string l_piso = null;
        if (!string.IsNullOrEmpty(InternoPIS) && InternoPIS != "0") { l_piso = InternoPIS; }
        string l_tipo = null;
        if (!string.IsNullOrEmpty(InternoTHA) && InternoTHA != "0") { l_tipo = InternoTHA; }
        Items = l_proxy.Client.ConsultaLibresHabitaciones(Connections.MainConnection, InternoEmpresa, InternoSucursal, Constantes.TablaPIS, l_piso, Constantes.TablaTHA, l_tipo);
      }
      return Items;
    }
    public ObservableCollection<Habitaciones> GetHabitaciones(Int64 InternoEmpresa, Int64 InternoSucursal, String InternoPIS, String InternoTHA)
    {
      ObservableCollection<Habitaciones> Items = new ObservableCollection<Habitaciones>();
      using (ProxyManager l_proxy = new ProxyManager())
      {
        string l_piso = null;
        if (!string.IsNullOrEmpty(InternoPIS) && InternoPIS != "0") { l_piso = InternoPIS; }
        string l_tipo = null;
        if (!string.IsNullOrEmpty(InternoTHA) && InternoTHA != "0") { l_tipo = InternoTHA; }
        Items = l_proxy.Client.ConsultaTodosHabitaciones(Connections.MainConnection, InternoEmpresa, InternoSucursal, Constantes.TablaPIS, l_piso, Constantes.TablaTHA, l_tipo);
      }
      return Items;
    }

    #endregion

    #region [ ENTIDADES ]

    public HttpResponseMessage GetHuespedAutocomplete(Int64 InternoEmpresa, String Indicio)
    {
      ObservableCollection<AutoCompleteModel> Items = new ObservableCollection<AutoCompleteModel>();
      using (ProxyManager _proxy = new ProxyManager())
      {
        var items = _proxy.Client.AyudaEntidades(Connections.MainConnection, InternoEmpresa, Constantes.ParaINHUE, Indicio);
        foreach (var item in items) { Items.Add(new AutoCompleteModel() { value = item.ENTI_Interno, label = String.Format("{0}-{1}", item.ENTI_Id, item.ENTI_NombreCompleto) }); }
      }

      var response = Request.CreateResponse(HttpStatusCode.OK);
      string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
      response.Content = new StringContent(json, Encoding.UTF8, "application/json");
      return response;
    }
    public Entidades GetOneEntidad(Int64 InternoEmpresa, Int64 InternoEntidad)
    {
      Entidades Item = new Entidades();
      using (ProxyManager _proxy = new ProxyManager())
      { Item = _proxy.Client.ConsultaEntidad(Connections.MainConnection, InternoEmpresa, InternoEntidad); }

      return Item;
    }

    #endregion

    #region [ OTROS ]

    public ObservableCollection<GenericClass> GetAnios()
    {
      ObservableCollection<GenericClass> Items = new ObservableCollection<GenericClass>();
      Int64 l_aniodesde = 2018;
      while (MyUtilities.GetDateTimeCountry("es - PE").Year >= l_aniodesde)
      {
        GenericClass l_item = new GenericClass();
        l_item.Value = l_aniodesde;
        l_item.Text = l_aniodesde.ToString();
        Items.Add(l_item);
        l_aniodesde++;
      }
      return Items;
    }
    public ObservableCollection<GenericClass> GetMeses()
    {
      ObservableCollection<GenericClass> Items = new ObservableCollection<GenericClass>();
      Items.Add(new GenericClass { Value = 1, Text = "ENERO" });
      Items.Add(new GenericClass { Value = 2, Text = "FEBRERO" });
      Items.Add(new GenericClass { Value = 3, Text = "MARZO" });
      Items.Add(new GenericClass { Value = 4, Text = "ABRIL" });
      Items.Add(new GenericClass { Value = 5, Text = "MAYO" });
      Items.Add(new GenericClass { Value = 6, Text = "JUNIO" });
      Items.Add(new GenericClass { Value = 7, Text = "JULIO" });
      Items.Add(new GenericClass { Value = 8, Text = "AGOSTO" });
      Items.Add(new GenericClass { Value = 9, Text = "SETIEMPRE" });
      Items.Add(new GenericClass { Value = 10, Text = "OCTUBRE" });
      Items.Add(new GenericClass { Value = 11, Text = "NOVIEMBRE" });
      Items.Add(new GenericClass { Value = 12, Text = "DICIEMBRE" });
      return Items;
    }

    #endregion
  }
}