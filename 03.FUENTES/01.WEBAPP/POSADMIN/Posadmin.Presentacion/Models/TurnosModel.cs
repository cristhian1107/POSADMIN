using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class TurnosModel
  {
    #region [ VARIABLES ]

    private Turnos m_item;
    private ObservableCollection<Turnos> m_items;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Turnos Item
    {
      get { if (m_item == null) { m_item = Turnos.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Turnos> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Turnos>(); } return m_items; }
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
    /// Gets or sets el valor de: FiltroSucursal
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Sucursal", Description = "Sucursal", ShortName = "Sucursal", Order = 0)]
    public Int64 FiltroSucursal { get; set; }

    #endregion
  }
}