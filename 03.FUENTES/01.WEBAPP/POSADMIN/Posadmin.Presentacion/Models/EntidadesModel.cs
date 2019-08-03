using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class EntidadesModel
  {
    #region [ VARIABLES ]

    private Entidades m_item;
    private ObservableCollection<Entidades> m_items;
    private Int64 m_internoempresa;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Entidades Item
    {
      get { if (m_item == null) { m_item = Entidades.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Entidades> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Entidades>(); } return m_items; }
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
    /// Gets or sets el valor de: FiltroEntidadId
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 0)]
    public String FiltroEntidadId { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroEntidadNombre
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 0)]
    public String FiltroEntidadNombre { get; set; }

    #endregion
  }
}