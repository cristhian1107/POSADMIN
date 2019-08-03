using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class SucursalesModel
  {
    #region [ VARIABLES ]

    private Sucursales m_item;
    private ObservableCollection<Sucursales> m_items;
    private Int64 m_internoempresa;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Sucursales Item
    {
      get { if (m_item == null) { m_item = Sucursales.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Sucursales> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Sucursales>(); } return m_items; }
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

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroPaisNombre
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 0)]
    public String NombreSucursal { get; set; }

    #endregion
  }
}