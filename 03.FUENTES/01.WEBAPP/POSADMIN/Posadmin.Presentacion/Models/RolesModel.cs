using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class RolesModel
  {
    #region [ VARIABLES ]

    private Roles m_item;
    private ObservableCollection<Roles> m_items;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Roles Item
    {
      get { if (m_item == null) { m_item = Roles.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Roles> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Roles>(); } return m_items; }
      set { m_items = value; }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroRolNombre
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nombre del Rol", Description = "Nombre del Rol", ShortName = "Nombre del Rol", Order = 0)]
    public String FiltroRolNombre { get; set; }

    #endregion
  }
}