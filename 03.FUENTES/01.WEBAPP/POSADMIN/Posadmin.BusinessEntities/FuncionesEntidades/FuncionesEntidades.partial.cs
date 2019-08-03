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
  public partial class FuncionesEntidades
  {
    #region [ VARIABLES ]

    private String m_func_nombre;
    private String m_func_descripcion;
    private Boolean m_func_activo;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
		/// Gets or sets el valor de: FUNC_Nombre.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 5)]
    public String FUNC_Nombre
    {
      get { return m_func_nombre; }
      set
      {
        if (m_func_nombre != value)
        {
          m_func_nombre = value;
          OnPropertyChanged("FUNC_Nombre");
        }
      }
    }

    /// <summary>
		/// Gets or sets el valor de: FUNC_Descripcion.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 5)]
    public String FUNC_Descripcion
    {
      get { return m_func_descripcion; }
      set
      {
        if (m_func_descripcion != value)
        {
          m_func_descripcion = value;
          OnPropertyChanged("FUNC_Descripcion");
        }
      }
    }

    /// <summary>
		/// Gets or sets el valor de: FUNC_Activo.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 5)]
    public Boolean FUNC_Activo
    {
      get { return m_func_activo; }
      set
      {
        if (m_func_activo != value)
        {
          m_func_activo = value;
          OnPropertyChanged("FUNC_Activo");
        }
      }
    }




    #endregion

    #region [ METODOS ]

    #endregion
  }
}
