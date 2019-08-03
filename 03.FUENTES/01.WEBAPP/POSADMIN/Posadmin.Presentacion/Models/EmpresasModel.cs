using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class EmpresasModel
  {
    #region [ VARIABLES ]

    private Empresas m_item;
    private ObservableCollection<Empresas> m_items;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Empresas Item
    {
      get { if (m_item == null) { m_item = Empresas.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Empresas> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Empresas>(); } return m_items; }
      set { m_items = value; }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroId
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 0)]
    public String FiltroId { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroRazonSocial
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Razon Social", Description = "Razon Social", ShortName = "Razon Social", Order = 0)]
    public String FiltroRazonSocial { get; set; }

    #endregion
  }
}