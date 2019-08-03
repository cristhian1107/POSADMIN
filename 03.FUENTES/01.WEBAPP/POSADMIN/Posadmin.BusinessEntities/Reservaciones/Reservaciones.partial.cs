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
  public partial class Reservaciones
  {
    #region [ VARIABLES ]

    private String m_rese_nombreestado;
    private String m_rese_habitacion;
    private String m_rese_tipohabitacion;
    private String m_tabl_tablapis;
    private String m_tabl_internopis;
    private String m_tabl_nombrepis;
    private String m_tabl_tablatha;
    private String m_tabl_internotha;
    private String m_tabl_nombretha;
    private Int64 m_enti_interno;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: RESE_NombreEstado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 0)]
    public String RESE_NombreEstado
    {
      get { return m_rese_nombreestado; }
      set
      {
        if (m_rese_nombreestado != value)
        {
          m_rese_nombreestado = value;
          OnPropertyChanged("RESE_NombreEstado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_Habitacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 0)]
    public String RESE_Habitacion
    {
      get { return m_rese_habitacion; }
      set
      {
        if (m_rese_habitacion != value)
        {
          m_rese_habitacion = value;
          OnPropertyChanged("RESE_Habitacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_TablaPIS.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 0)]
    public String TABL_TablaPIS
    {
      get { return m_tabl_tablapis; }
      set
      {
        if (m_tabl_tablapis != value)
        {
          m_tabl_tablapis = value;
          OnPropertyChanged("TABL_TablaPIS");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_InternoPIS.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 0)]
    public String TABL_InternoPIS
    {
      get { return m_tabl_internopis; }
      set
      {
        if (m_tabl_internopis != value)
        {
          m_tabl_internopis = value;
          OnPropertyChanged("TABL_IntenoPIS");
        }
      }
    }

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
    /// Gets or sets el valor de: TABL_TablaTHA.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 0)]
    public String TABL_TablaTHA
    {
      get { return m_tabl_tablatha; }
      set
      {
        if (m_tabl_tablatha != value)
        {
          m_tabl_tablatha = value;
          OnPropertyChanged("TABL_TablaTHA");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_InternoTHA.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 0)]
    public String TABL_InternoTHA
    {
      get { return m_tabl_internotha; }
      set
      {
        if (m_tabl_internotha != value)
        {
          m_tabl_internotha = value;
          OnPropertyChanged("TABL_IntenoTHA");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_NombreTHA.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo de Habitacion", Description = "Tipo de Habitacion", ShortName = "Tipo de Habitacion", Order = 0)]
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
    /// Gets or sets el valor de: ENTI_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Entidad", Description = "Entidad", ShortName = "Entidad", Order = 0)]
    public Int64 ENTI_Interno
    {
      get { return m_enti_interno; }
      set
      {
        if (m_enti_interno != value)
        {
          m_enti_interno = value;
          OnPropertyChanged("ENTI_Interno");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
