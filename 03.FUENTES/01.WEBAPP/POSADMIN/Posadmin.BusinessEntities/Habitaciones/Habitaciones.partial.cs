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
  public partial class Habitaciones
  {
    #region [ VARIABLES ]

    private String m_tabl_nombrepis;
    private String m_tabl_nombretha;
    private String m_habi_estadonombre;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: TABL_NombrePIS.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 0)]
    public String TABL_NombrePIS
    {
      get { return m_tabl_nombrepis; }
      set
      {
        if (m_tabl_nombrepis != value)
        {
          m_tabl_nombrepis = value;
          OnPropertyChanged("TABL_NombrePIS");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_NombreTHA.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 0)]
    public String TABL_NombreTHA
    {
      get { return m_tabl_nombretha; }
      set
      {
        if (m_tabl_nombretha != value)
        {
          m_tabl_nombretha = value;
          OnPropertyChanged("TABL_NombreTHA");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_EstadoNombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 0)]
    public String HABI_EstadoNombre
    {
      get { return m_habi_estadonombre; }
      set
      {
        if (m_habi_estadonombre != value)
        {
          m_habi_estadonombre = value;
          OnPropertyChanged("HABI_EstadoNombre");
        }
      }
    }

    /// <summary>
    /// Gets el valor de: HABI_ClassColorLimpieza.
    /// </summary>
    [DataMember]
    public String HABI_ClassColorLimpieza
    {
      get
      {
        string l_regi_classcolor;
        switch (HABI_Limpio)
        {
          case true:
            l_regi_classcolor = "bg-color-greenDark txt-color-white";
            break;
          case false:
            l_regi_classcolor = "bg-color-redLight txt-color-white";
            break;
          default:
            l_regi_classcolor = "bg-color-blueLight txt-color-white";
            break;
        }
        return l_regi_classcolor;
      }
    }

    #endregion

    #region [ CONTROLES ]

    [DataMember]
    public Int64 Value { get { return HABI_Interno; } }
    [DataMember]
    public String Text { get { return HABI_Numero; } }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
