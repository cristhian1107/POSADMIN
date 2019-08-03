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
  public partial class DetallesPagosRegistros
  {
    #region [ VARIABLES ]

    private String m_pago_tiponombre;
    private String m_pago_tipocolor;
    private String m_pago_usuarionombre;
    private String m_pago_numerohabitacion;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: PAGO_TipoNombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo", Description = "Tipo", ShortName = "Tipo", Order = 0)]
    public String PAGO_TipoNombre
    {
      get { return m_pago_tiponombre; }
      set
      {
        if (m_pago_tiponombre != value)
        {
          m_pago_tiponombre = value;
          OnPropertyChanged("PAGO_TipoNombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_TipoColor.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Color", Description = "Color", ShortName = "Color", Order = 0)]
    public String PAGO_TipoColor
    {
      get { return m_pago_tipocolor; }
      set
      {
        if (m_pago_tipocolor != value)
        {
          m_pago_tipocolor = value;
          OnPropertyChanged("PAGO_TipoColor");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_UsuarioNombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 0)]
    public String PAGO_UsuarioNombre
    {
      get { return m_pago_usuarionombre; }
      set
      {
        if (m_pago_usuarionombre != value)
        {
          m_pago_usuarionombre = value;
          OnPropertyChanged("PAGO_UsuarioNombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_NumeroHabitacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 0)]
    public String PAGO_NumeroHabitacion
    {
      get { return m_pago_numerohabitacion; }
      set
      {
        if (m_pago_numerohabitacion != value)
        {
          m_pago_numerohabitacion = value;
          OnPropertyChanged("PAGO_NumeroHabitacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_FechaRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Registro", Description = "Fecha Registro", ShortName = "Fecha Registro", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime PAGO_FechaRegistro
    {
      get { return m_pago_fechahoraregistro; }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_HoraRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Hora Registro", Description = "Hora Registro", ShortName = "Hora Registro", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime PAGO_HoraRegistro
    {
      get { return m_pago_fechahoraregistro; }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
