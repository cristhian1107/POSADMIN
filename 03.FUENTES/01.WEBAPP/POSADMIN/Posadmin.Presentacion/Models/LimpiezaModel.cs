using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class LimpiezaModel
  {
    #region [ VARIABLES ]

    private Habitaciones m_item;
    private ObservableCollection<Habitaciones> m_items;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Habitaciones Item
    {
      get { if (m_item == null) { m_item = Habitaciones.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Habitaciones> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Habitaciones>(); } return m_items; }
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
    /// Gets or sets el valor de: CodigoTablaPIS
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 0)]
    public String CodigoTablaPIS { get; set; }

    /// <summary>
    /// Gets or sets el valor de: CodigoTablaTHA
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Tipo de habitacion", Description = "Tipo de habitacion", ShortName = "Tipo de habitacion", Order = 0)]
    public String CodigoTablaTHA { get; set; }

    #endregion
  }
}