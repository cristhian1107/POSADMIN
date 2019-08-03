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
  public partial class Cierres
	{
    #region [ VARIABLES ]

    private String m_cier_nombreusuario;
    private Double m_cier_total;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: CIER_NombreUsuario.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 0)]
    public String CIER_NombreUsuario
    {
      get { return m_cier_nombreusuario; }
      set
      {
        if (m_cier_nombreusuario != value)
        {
          m_cier_nombreusuario = value;
          OnPropertyChanged("CIER_NombreUsuario");
        }
      }
    }

    /// <summary>
		/// Gets or sets el valor de: CIER_MontoExtra.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Total", Description = "Monto Total", ShortName = "Monto Total", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double CIER_Total
    {
      get { return m_cier_total; }
      set
      {
        if (m_cier_total != value)
        {
          m_cier_total = value;
          OnPropertyChanged("CIER_Total");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
