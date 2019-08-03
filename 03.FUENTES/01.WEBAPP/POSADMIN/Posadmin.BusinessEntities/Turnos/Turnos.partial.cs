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
  public partial class Turnos
  {
    #region [ VARIABLES ]

    private String m_turn_sucursal;

    #endregion

    #region [ PROPIEDADES ]


    /// <summary>
    /// Gets or sets el valor de: TURN_Sucursal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Sucursal", Description = "Sucursal", ShortName = "Sucursal", Order = 0)]
    public String TURN_Sucursal
    {
      get { return m_turn_sucursal; }
      set
      {
        if (m_turn_sucursal != value)
        {
          m_turn_sucursal = value;
          OnPropertyChanged("TURN_Sucursal");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
