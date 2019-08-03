using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class TablasModel
  {
    #region [ VARIABLES ]

    private Tablas m_item;
    private ObservableCollection<Tablas> m_items;
    private Int64 m_internoempresa;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Tablas Item
    {
      get { if (m_item == null) { m_item = Tablas.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Tablas> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Tablas>(); } return m_items; }
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
    [Display(AutoGenerateField = false, Name = "Tablas", Description = "Tablas", ShortName = "Tablas", Order = 0)]
    public String CodigoTabla { get; set; }

    #endregion
  }
}