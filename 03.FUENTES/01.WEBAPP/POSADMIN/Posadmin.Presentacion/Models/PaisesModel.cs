using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class PaisesModel
  {
    #region [ VARIABLES ]

    private Paises m_item;
    private ObservableCollection<Paises> m_items;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Paises Item
    {
      get { if (m_item == null) { m_item = Paises.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Paises> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Paises>(); } return m_items; }
      set { m_items = value; }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroPaisNombre
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nombre del Pais", Description = "Nombre del Pais", ShortName = "Nombre del Pais", Order = 0)]
    public String FiltroPaisNombre { get; set; }

    #endregion
  }
}