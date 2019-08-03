using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Runtime.Serialization;
using SoftwareFactory.Infrastructure.BusinessEntity;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  [Serializable()]
  public partial class Registros : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_regi_interno;
    private Int64 m_habi_interno;
    private String m_regi_estado;
    private String m_regi_tramos;
    private Int32 m_regi_cantidad;
    private DateTime m_regi_fechahoraentrada;
    private DateTime m_regi_fechahorasalida;
    private Nullable<DateTime> m_regi_fechahorasalidareal;
    private String m_tabl_tablatdi;
    private String m_tabl_internotdi;
    private Int64 m_enti_interno;
    private String m_regi_huespedid;
    private String m_regi_huespednombrecompleto;
    private String m_regi_huespeddireccion;
    private Double m_regi_preciosugerido;
    private Double m_regi_preciocobrado;
    private String m_regi_motivoanulacion;
    private Nullable<DateTime> m_regi_fechahoraanulacion;
    private Int64 m_turn_interno;
    private Int64 m_usua_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Registros.
    /// </summary>
    public Registros()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase Registros con Instance Added.
    /// </summary>
    public static Registros Nuevo()
    {
      try
      {
        Registros item = new Registros();
        item.Instance = InstanceEntity.Added;
        return item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: EMPR_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Empresas Interno", Description = "Empresas Interno", ShortName = "Empresas Interno", Order = 1)]
    public Int64 EMPR_Interno
    {
      get { return m_empr_interno; }
      set
      {
        if (m_empr_interno != value)
        {
          m_empr_interno = value;
          OnPropertyChanged("EMPR_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno
    {
      get { return m_sucu_interno; }
      set
      {
        if (m_sucu_interno != value)
        {
          m_sucu_interno = value;
          OnPropertyChanged("SUCU_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 REGI_Interno
    {
      get { return m_regi_interno; }
      set
      {
        if (m_regi_interno != value)
        {
          m_regi_interno = value;
          OnPropertyChanged("REGI_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 4)]
    public Int64 HABI_Interno
    {
      get { return m_habi_interno; }
      set
      {
        if (m_habi_interno != value)
        {
          m_habi_interno = value;
          OnPropertyChanged("HABI_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Estado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 5)]
    public String REGI_Estado
    {
      get { return m_regi_estado; }
      set
      {
        if (m_regi_estado != value)
        {
          m_regi_estado = value;
          OnPropertyChanged("REGI_Estado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Tramos.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tramos", Description = "Tramos", ShortName = "Tramos", Order = 6)]
    public String REGI_Tramos
    {
      get { return m_regi_tramos; }
      set
      {
        if (m_regi_tramos != value)
        {
          m_regi_tramos = value;
          OnPropertyChanged("REGI_Tramos");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_Cantidad.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Cantidad", Description = "Cantidad", ShortName = "Cantidad", Order = 7)]
    [DisplayFormat(DataFormatString = "{0:##0}", ApplyFormatInEditMode = true)]
    public Int32 REGI_Cantidad
    {
      get { return m_regi_cantidad; }
      set
      {
        if (m_regi_cantidad != value)
        {
          m_regi_cantidad = value;
          OnPropertyChanged("REGI_Cantidad");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FechaHoraEntrada.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Entrada", Description = "Fecha Hora Entrada", ShortName = "Fecha Hora Entrada", Order = 8)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime REGI_FechaHoraEntrada
    {
      get { return m_regi_fechahoraentrada; }
      set
      {
        if (m_regi_fechahoraentrada != value)
        {
          m_regi_fechahoraentrada = value;
          OnPropertyChanged("REGI_FechaHoraEntrada");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FechaHoraSalida.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Salida", Description = "Fecha Hora Salida", ShortName = "Fecha Hora Salida", Order = 9)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime REGI_FechaHoraSalida
    {
      get { return m_regi_fechahorasalida; }
      set
      {
        if (m_regi_fechahorasalida != value)
        {
          m_regi_fechahorasalida = value;
          OnPropertyChanged("REGI_FechaHoraSalida");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FechaHoraSalidaReal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Salida Real", Description = "Fecha Hora Salida Real", ShortName = "Fecha Hora Salida Real", Order = 10)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public Nullable<DateTime> REGI_FechaHoraSalidaReal
    {
      get { return m_regi_fechahorasalidareal; }
      set
      {
        if (m_regi_fechahorasalidareal != value)
        {
          m_regi_fechahorasalidareal = value;
          OnPropertyChanged("REGI_FechaHoraSalidaReal");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_TablaTDI.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Doc. Identidad", Description = "Doc. Identidad", ShortName = "Doc. Identidad", Order = 11)]
    public String TABL_TablaTDI
    {
      get { return m_tabl_tablatdi; }
      set
      {
        if (m_tabl_tablatdi != value)
        {
          m_tabl_tablatdi = value;
          OnPropertyChanged("TABL_TablaTDI");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_InternoTDI.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Doc. Identidad", Description = "Doc. Identidad", ShortName = "Doc. Identidad", Order = 12)]
    public String TABL_InternoTDI
    {
      get { return m_tabl_internotdi; }
      set
      {
        if (m_tabl_internotdi != value)
        {
          m_tabl_internotdi = value;
          OnPropertyChanged("TABL_InternoTDI");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Entidad", Description = "Entidad", ShortName = "Entidad", Order = 13)]
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

    /// <summary>
    /// Gets or sets el valor de: REGI_HuespedId.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Id", Description = "Id", ShortName = "Id", Order = 14)]
    public String REGI_HuespedId
    {
      get { return m_regi_huespedid; }
      set
      {
        if (m_regi_huespedid != value)
        {
          m_regi_huespedid = value;
          OnPropertyChanged("REGI_HuespedId");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_HuespedNombreCompleto.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 15)]
    public String REGI_HuespedNombreCompleto
    {
      get { return m_regi_huespednombrecompleto; }
      set
      {
        if (m_regi_huespednombrecompleto != value)
        {
          m_regi_huespednombrecompleto = value;
          OnPropertyChanged("REGI_HuespedNombreCompleto");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_HuespedDireccion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 16)]
    public String REGI_HuespedDireccion
    {
      get { return m_regi_huespeddireccion; }
      set
      {
        if (m_regi_huespeddireccion != value)
        {
          m_regi_huespeddireccion = value;
          OnPropertyChanged("REGI_HuespedDireccion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_PrecioSugerido.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Precio Sugerido", Description = "Precio Sugerido", ShortName = "Precio Sugerido", Order = 17)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_PrecioSugerido
    {
      get { return m_regi_preciosugerido; }
      set
      {
        if (m_regi_preciosugerido != value)
        {
          m_regi_preciosugerido = value;
          OnPropertyChanged("REGI_PrecioSugerido");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_PrecioCobrado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Precio Cobrado", Description = "Precio Cobrado", ShortName = "Precio Cobrado", Order = 18)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double REGI_PrecioCobrado
    {
      get { return m_regi_preciocobrado; }
      set
      {
        if (m_regi_preciocobrado != value)
        {
          m_regi_preciocobrado = value;
          OnPropertyChanged("REGI_PrecioCobrado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_MotivoAnulacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Motivo Anulacion", Description = "Motivo Anulacion", ShortName = "Motivo Anulacion", Order = 19)]
    public String REGI_MotivoAnulacion
    {
      get { return m_regi_motivoanulacion; }
      set
      {
        if (m_regi_motivoanulacion != value)
        {
          m_regi_motivoanulacion = value;
          OnPropertyChanged("REGI_MotivoAnulacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: REGI_FechaHoraAnulacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Anulacion", Description = "Fecha Hora Anulacion", ShortName = "Fecha Hora Anulacion", Order = 20)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public Nullable<DateTime> REGI_FechaHoraAnulacion
    {
      get { return m_regi_fechahoraanulacion; }
      set
      {
        if (m_regi_fechahoraanulacion != value)
        {
          m_regi_fechahoraanulacion = value;
          OnPropertyChanged("REGI_FechaHoraAnulacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Turno", Description = "Turno", ShortName = "Turno", Order = 21)]
    public Int64 TURN_Interno
    {
      get { return m_turn_interno; }
      set
      {
        if (m_turn_interno != value)
        {
          m_turn_interno = value;
          OnPropertyChanged("TURN_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 22)]
    public Int64 USUA_Interno
    {
      get { return m_usua_interno; }
      set
      {
        if (m_usua_interno != value)
        {
          m_usua_interno = value;
          OnPropertyChanged("USUA_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 23)]
    public String AUDI_UsuarioCreacion
    {
      get { return m_audi_usuariocreacion; }
      set
      {
        if (m_audi_usuariocreacion != value)
        {
          m_audi_usuariocreacion = value;
          OnPropertyChanged("AUDI_UsuarioCreacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_FechaCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 24)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime AUDI_FechaCreacion
    {
      get { return m_audi_fechacreacion; }
      set
      {
        if (m_audi_fechacreacion != value)
        {
          m_audi_fechacreacion = value;
          OnPropertyChanged("AUDI_FechaCreacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioModificacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 25)]
    public String AUDI_UsuarioModificacion
    {
      get { return m_audi_usuariomodificacion; }
      set
      {
        if (m_audi_usuariomodificacion != value)
        {
          m_audi_usuariomodificacion = value;
          OnPropertyChanged("AUDI_UsuarioModificacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_FechaModificacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 26)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public Nullable<DateTime> AUDI_FechaModificacion
    {
      get { return m_audi_fechamodificacion; }
      set
      {
        if (m_audi_fechamodificacion != value)
        {
          m_audi_fechamodificacion = value;
          OnPropertyChanged("AUDI_FechaModificacion");
        }
      }
    }

    #endregion
  }
}
