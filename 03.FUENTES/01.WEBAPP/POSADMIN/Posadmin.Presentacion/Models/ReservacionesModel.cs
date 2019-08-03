using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class ReservacionesModel
  {
    #region [ VARIABLES ]

    private Reservaciones m_item;
    private ObservableCollection<Reservaciones> m_items;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Reservaciones Item
    {
      get { if (m_item == null) { m_item = Reservaciones.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Reservaciones> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Reservaciones>(); } return m_items; }
      set { m_items = value; }
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

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroEstado
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 0)]
    public String FiltroEstado { get; set; }

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