using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class ConfiguracionesModel
  {
    #region [ VARIABLES ]

    private Configuraciones m_item;
    private ObservableCollection<Configuraciones> m_items;
    private Int64 m_internoempresa;
    private Int64 m_internousuario;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: Item
    /// </summary>
    public Configuraciones Item
    {
      get { if (m_item == null) { m_item = Configuraciones.Nuevo(); } return m_item; }
      set { m_item = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: Items
    /// </summary>
    public ObservableCollection<Configuraciones> Items
    {
      get { if (m_items == null) { m_items = new ObservableCollection<Configuraciones>(); } return m_items; }
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
    [Display(AutoGenerateField = false, Name = "Llave", Description = "Llave", ShortName = "Llave", Order = 0)]
    public String LlaveParametro { get; set; }

    /// <summary>
    /// Gets or sets el valor de: InternoUsuario
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 0)]
    public Int64 InternoUsuario
    {
      get { return m_internousuario; }
      set
      {
        if (m_internousuario != value)
        {
          m_internousuario = value;
        }
      }
    }

    #endregion
  }
}