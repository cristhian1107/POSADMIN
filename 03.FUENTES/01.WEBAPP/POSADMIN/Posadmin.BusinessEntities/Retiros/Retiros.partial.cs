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
  public partial class Retiros
  {
    #region [ VARIABLES ]

    private String m_reti_nombreusuario;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: RETI_NombreUsuario.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 0)]
    public String RETI_NombreUsuario
    {
      get { return m_reti_nombreusuario; }
      set
      {
        if (m_reti_nombreusuario != value)
        {
          m_reti_nombreusuario = value;
          OnPropertyChanged("RETI_NombreUsuario");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
