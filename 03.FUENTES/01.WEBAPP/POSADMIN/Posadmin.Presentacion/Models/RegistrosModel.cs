using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class RegistrosModel
  {
    #region [ VARIABLES ]

    private Registros m_item;
    private ObservableCollection<Registros> m_items;
    private DetallesPagosRegistros m_pago;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;
    private Int64 m_internousuario;
    private Nullable<Int64> m_internoreserva;
    private String m_nombreusuario;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Registros Item
    {
      get { if (m_item == null) { m_item = Registros.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Registros> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Registros>(); } return m_items; }
      set { m_items = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Pago
    /// </summary>
    public DetallesPagosRegistros Pago
    {
      get { if (m_pago == null) { m_pago = DetallesPagosRegistros.Nuevo(); } return m_pago; }
      set { m_pago = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: InternoEmpresa
    /// </summary>
    public Int64 InternoEmpresa
    {
      get { return m_internoempresa; }
      set
      {
        if (m_internoempresa != value)
        {
          m_internoempresa = value;
        }
      }
    }
    /// <summary>
    /// Gets or sets el valor de: InternoSucursal
    /// </summary>
    public Int64 InternoSucursal
    {
      get { return m_internosucursal; }
      set
      {
        if (m_internosucursal != value)
        {
          m_internosucursal = value;
        }
      }
    }
    /// <summary>
    /// Gets or sets el valor de: InternoUsuario
    /// </summary>
    public Int64 InternoUsuario
    {
      get { return m_internousuario; }
      set
      {
        if (m_internousuario != value)
        {
          m_internousuario = value;
        }
      }
    }
    /// <summary>
    /// Gets or sets el valor de: NombreUsuario
    /// </summary>
    public String NombreUsuario
    {
      get { return m_nombreusuario; }
      set
      {
        if (m_nombreusuario != value)
        {
          m_nombreusuario = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: InternoReserva
    /// </summary>
    public Nullable<Int64> InternoReserva
    {
      get { return m_internoreserva; }
      set
      {
        if (m_internoreserva != value)
        {
          m_internoreserva = value;
        }
      }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroEstadoHabitacion
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Estado Habitacion", Description = "Estado Habitacion", ShortName = "Estado Habitacion", Order = 0)]
    public String FiltroEstadoHabitacion { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroPiso
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 0)]
    public String FiltroPiso { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroTipoHabitacion
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 0)]
    public String FiltroTipoHabitacion { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroHuesped
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Huesped", Description = "Huesped", ShortName = "Huesped", Order = 0)]
    public String FiltroHuesped { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroNumero
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nro Habitacion", Description = "Nro Habitacion", ShortName = "Nro Habitacion", Order = 0)]
    public String FiltroNumero { get; set; }

    #endregion
  }
}