using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  public partial class Opciones
  {
    #region [ VARIABLES ]

    private Int32 m_role_interno;
    private String m_opci_nombrepadre;
    private Boolean m_opci_activo;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: ROLE_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Rol Interno", Description = "Rol Interno", ShortName = "Rol Interno", Order = 10)]
    public Int32 ROLE_Interno
    {
      get { return m_role_interno; }
      set
      {
        if (m_role_interno != value)
        {
          m_role_interno = value;
          OnPropertyChanged("ROLE_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_NombrePadre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre del Padre", Description = "Nombre del Padre", ShortName = "Nombre del Padre", Order = 11)]
    public String OPCI_NombrePadre
    {
      get { return m_opci_nombrepadre; }
      set
      {
        if (m_opci_nombrepadre != value)
        {
          m_opci_nombrepadre = value;
          OnPropertyChanged("OPCI_NombrePadre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 12)]
    public Boolean OPCI_Activo
    {
      get { return m_opci_activo; }
      set
      {
        if (m_opci_activo != value)
        {
          m_opci_activo = value;
          OnPropertyChanged("OPCI_Activo");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
