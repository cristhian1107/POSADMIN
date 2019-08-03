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
  public partial class Ubigeos
  {
    #region [ VARIABLES ]

    private String m_ubig_nombrepais;
    private String m_ubig_nombrepadre;
    private String m_ubig_tipo;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: UBIG_NombrePais.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre del Pais", Description = "Nombre del Pais", ShortName = "Nombre del Pais", Order = 0)]
    public String UBIG_NombrePais
    {
      get { return m_ubig_nombrepais; }
      set
      {
        if (m_ubig_nombrepais != value)
        {
          m_ubig_nombrepais = value;
          OnPropertyChanged("UBIG_NombrePais");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_NombrePadre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre del Padre", Description = "Nombre del Padre", ShortName = "Nombre del Padre", Order = 0)]
    public String UBIG_NombrePadre
    {
      get { return m_ubig_nombrepadre; }
      set
      {
        if (m_ubig_nombrepadre != value)
        {
          m_ubig_nombrepadre = value;
          OnPropertyChanged("UBIG_NombrePadre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Tipo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo de Ubigeo", Description = "Tipo de Ubigeo", ShortName = "Tipo de Ubigeo", Order = 0)]
    public String UBIG_Tipo
    {
      get { return m_ubig_tipo; }
      set
      {
        if (m_ubig_tipo != value)
        {
          m_ubig_tipo = value;
          OnPropertyChanged("UBIG_Tipo");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion

    #region [ CONTROLES ]

    [DataMember]
    public Int32 Value { get { return UBIG_Interno; } }
    [DataMember]
    public String Text { get { return UBIG_Nombre + (string.IsNullOrEmpty(UBIG_Tipo) ? string.Empty : " - " + UBIG_Tipo); } }

    #endregion
  }
}
