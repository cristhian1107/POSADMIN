using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class UsuariosModel
  {
    #region [ VARIABLES ]

    private Usuarios m_item;
    private ObservableCollection<Usuarios> m_items;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Usuarios Item
    {
      get { if (m_item == null) { m_item = Usuarios.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Usuarios> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Usuarios>(); } return m_items; }
      set { m_items = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: ModoEditar
    /// </summary>
    public Boolean ModoEditar { get; set; }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroUsuario
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 0)]
    public String FiltroUsuario { get; set; }

    #endregion
  }
}