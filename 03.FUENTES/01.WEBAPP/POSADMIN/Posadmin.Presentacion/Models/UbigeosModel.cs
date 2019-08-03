using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class UbigeosModel
  {
    #region [ VARIABLES ]

    private Ubigeos m_item;
    private ObservableCollection<Ubigeos> m_items;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Ubigeos Item
    {
      get { if (m_item == null) { m_item = Ubigeos.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Ubigeos> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Ubigeos>(); } return m_items; }
      set { m_items = value; }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroPaisNombre
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nombre del Ubigeo", Description = "Nombre del Ubigeo", ShortName = "Nombre del Ubigeo", Order = 0)]
    public String FiltroUbigeoNombre { get; set; }

    #endregion
  }
}