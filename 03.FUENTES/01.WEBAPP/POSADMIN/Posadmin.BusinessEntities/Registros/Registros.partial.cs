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
  public partial class Registros
  {
    #region [ VARIABLES ]

    private String m_regi_habitacionnumero;
    private String m_tabl_tablapis;
    private String m_tabl_internopis;
    private String m_tabl_nombrepis;
    private String m_tabl_tablatha;
    private String m_tabl_internotha;
    private String m_tabl_nombretha;
    private String m_regi_habitacionestado;
    private Boolean m_regi_habitacionlimpio;
    private Double m_regi_montocancelado;
    private Double m_regi_deuda;
    private Double m_regi_vuelto;
    private Double m_regi_adicional;
    private String m_regi_formatofechaentrada;
    private String m_regi_formatofechasalida;
    private ObservableCollection<DetallesPagosRegistros> m_regi_listapagos;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: REGI_HabitacionNumero.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion Numero", Description = "Habitacion Numero", ShortName = "Habitacion Numero", Order = 0)]
    public String REGI_HabitacionNumero
    {
      get { return m_regi_habitacionnumero; }
      set
      {
        if (m_regi_habitacionnumero != value)
        {
          m_regi_habitacionnumero = value;
          OnPropertyChanged("REGI_HabitacionNumero");
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
          OnPropertyChanged("TABL_InternoPIS");
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
    [Display(AutoGenerateField = true, Name = "Tipo de Habitacion", Description = "Tipo de Habitacion", ShortName = "Tipo de Habitacion", Order = 0)]
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
    [Display(AutoGenerateField = true, Name = "Tipo de Habitacion", Description = "Tipo de Habitacion", ShortName = "Tipo de Habitacion", Order = 0)]
    public String TABL_InternoTHA
    {
      get { return m_tabl_internotha; }
      set
      {
        if (m_tabl_internotha != value)
        {
          m_tabl_internotha = value;
          OnPropertyChanged("TABL_InternoTHA");
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
    /// Gets or sets el valor de: REGI_DescripcionHabitacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion Habitacion", Description = "Descripcion Habitacion", ShortName = "Descripcion Habitacion", Order = 0)]
    public String REGI_DescripcionHabitacion
    {
      get {
        return (string.IsNullOrEmpty(TABL_NombrePIS)?"*****": TABL_NombrePIS) + " - " + (string.IsNullOrEmpty(REGI_HabitacionNumero) ? "*****" : REGI_HabitacionNumero) + "\n" + (string.IsNullOrEmpty(TABL_NombreTHA) ? "*****" : TABL_NombreTHA);
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_HabitacionEstado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion Estado", Description = "Habitacion Estado", ShortName = "Habitacion Estado", Order = 0)]
    public String REGI_HabitacionEstado
    {
      get { return m_regi_habitacionestado; }
      set
      {
        if (m_regi_habitacionestado != value)
        {
          m_regi_habitacionestado = value;
          OnPropertyChanged("REGI_HabitacionEstado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_HabitacionLimpio.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion Limpio", Description = "Habitacion Limpio", ShortName = "Habitacion Limpio", Order = 0)]
    public Boolean REGI_HabitacionLimpio
    {
      get { return m_regi_habitacionlimpio; }
      set
      {
        if (m_regi_habitacionlimpio != value)
        {
          m_regi_habitacionlimpio = value;
          OnPropertyChanged("REGI_HabitacionLimpio");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_MontoCancelado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Cancelado", Description = "Monto Cancelado", ShortName = "Monto Cancelado", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_MontoCancelado
    {
      get { return m_regi_montocancelado; }
      set
      {
        if (m_regi_montocancelado != value)
        {
          m_regi_montocancelado = value;
          OnPropertyChanged("REGI_MontoCancelado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Deuda.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Deuda", Description = "Deuda", ShortName = "Deuda", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_Deuda
    {
      get { return m_regi_deuda; }
      set
      {
        if (m_regi_deuda != value)
        {
          m_regi_deuda = value;
          OnPropertyChanged("REGI_Deuda");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Vuelto.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Vuelto", Description = "Vuelto", ShortName = "Vuelto", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_Vuelto
    {
      get { return m_regi_vuelto; }
      set
      {
        if (m_regi_vuelto != value)
        {
          m_regi_vuelto = value;
          OnPropertyChanged("REGI_Vuelto");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Adicional.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Adicional", Description = "Adicional", ShortName = "Adicional", Order = 0)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_Adicional
    {
      get { return m_regi_adicional; }
      set
      {
        if (m_regi_adicional != value)
        {
          m_regi_adicional = value;
          OnPropertyChanged("REGI_Adicional");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_ListaPagos.
    /// </summary>
    [DataMember]
    public ObservableCollection<DetallesPagosRegistros> REGI_ListaPagos
    {
      get { if (m_regi_listapagos == null) { m_regi_listapagos = new ObservableCollection<DetallesPagosRegistros>(); } return m_regi_listapagos; }
      set { m_regi_listapagos = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FormatoFechaEntrada.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Entrada", Description = "Fecha Hora Entrada", ShortName = "Fecha Hora Entrada", Order = 0)]
    public String REGI_FormatoFechaEntrada
    {
      get
      {
        m_regi_formatofechaentrada = string.Format("{0:dd/MM/yyyy HH:mm}", REGI_FechaHoraEntrada);
        return m_regi_formatofechaentrada;
      }
      set
      {
        if (m_regi_formatofechaentrada != value)
        { m_regi_formatofechaentrada = value; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FormatoFechaSalida.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Salida", Description = "Fecha Hora Salida", ShortName = "Fecha Hora Salida", Order = 0)]
    public String REGI_FormatoFechaSalida
    {
      get
      {
        m_regi_formatofechasalida = string.Format("{0:dd/MM/yyyy HH:mm}", REGI_FechaHoraSalida);
        return m_regi_formatofechasalida;
      }
      set
      {
        if (m_regi_formatofechasalida != value)
        { m_regi_formatofechasalida = value; }
      }
    }

    /// <summary>
    /// Gets el valor de: REGI_ClassColor.
    /// </summary>
    [DataMember]
    public String REGI_ClassColor
    {
      get
      {
        string l_regi_classcolor;
        switch (REGI_HabitacionEstado)
        {
          case "L":
            l_regi_classcolor = "bg-color-greenDark txt-color-white";
            break;
          case "O":
            l_regi_classcolor = "bg-color-redLight txt-color-white";
            break;
          case "R":
            l_regi_classcolor = "bg-color-orange txt-color-white";
            break;
          default:
            l_regi_classcolor = "bg-color-blueLight txt-color-white";
            break;
        }
        return l_regi_classcolor;
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
