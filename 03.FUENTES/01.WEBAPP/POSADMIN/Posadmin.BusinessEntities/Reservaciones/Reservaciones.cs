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
  public partial class Reservaciones : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_rese_interno;
    private String m_rese_estado;
    private Int64 m_habi_interno;
    private DateTime m_rese_fechainicio;
    private DateTime m_rese_fechafin;
    private DateTime m_rese_fechahoraregistro;
    private String m_rese_huespedid;
    private String m_rese_huespednombrecompleto;
    private String m_rese_huespeddireccion;
    private String m_rese_descripcion;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Reservaciones.
    /// </summary>
    public Reservaciones()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Reservaciones con Instance Added.
    /// </summary>
    public static Reservaciones Nuevo()
    {
      try
      {
        Reservaciones item = new Reservaciones();
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
    [Display(AutoGenerateField = true, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
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
    /// Gets or sets el valor de: RESE_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 RESE_Interno
    {
      get { return m_rese_interno; }
      set
      {
        if (m_rese_interno != value)
        {
          m_rese_interno = value;
          OnPropertyChanged("RESE_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_Estado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 4)]
    public String RESE_Estado
    {
      get { return m_rese_estado; }
      set
      {
        if (m_rese_estado != value)
        {
          m_rese_estado = value;
          OnPropertyChanged("RESE_Estado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 5)]
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
    /// Gets or sets el valor de: RESE_FechaInicio.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Inicio", Description = "Fecha Inicio", ShortName = "Fecha Inicio", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime RESE_FechaInicio
    {
      get { return m_rese_fechainicio; }
      set
      {
        if (m_rese_fechainicio != value)
        {
          m_rese_fechainicio = value;
          OnPropertyChanged("RESE_FechaInicio");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_FechaFin.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Fin", Description = "Fecha Fin", ShortName = "Fecha Fin", Order = 7)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime RESE_FechaFin
    {
      get { return m_rese_fechafin; }
      set
      {
        if (m_rese_fechafin != value)
        {
          m_rese_fechafin = value;
          OnPropertyChanged("RESE_FechaFin");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_FechaHoraRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 8)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime RESE_FechaHoraRegistro
    {
      get { return m_rese_fechahoraregistro; }
      set
      {
        if (m_rese_fechahoraregistro != value)
        {
          m_rese_fechahoraregistro = value;
          OnPropertyChanged("RESE_FechaHoraRegistro");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_HuespedId.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Id", Description = "Id", ShortName = "Id", Order = 9)]
    public String RESE_HuespedId
    {
      get { return m_rese_huespedid; }
      set
      {
        if (m_rese_huespedid != value)
        {
          m_rese_huespedid = value;
          OnPropertyChanged("RESE_HuespedId");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_HuespedNombreCompleto.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 10)]
    public String RESE_HuespedNombreCompleto
    {
      get { return m_rese_huespednombrecompleto; }
      set
      {
        if (m_rese_huespednombrecompleto != value)
        {
          m_rese_huespednombrecompleto = value;
          OnPropertyChanged("RESE_HuespedNombreCompleto");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_HuespedDireccion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 11)]
    public String RESE_HuespedDireccion
    {
      get { return m_rese_huespeddireccion; }
      set
      {
        if (m_rese_huespeddireccion != value)
        {
          m_rese_huespeddireccion = value;
          OnPropertyChanged("RESE_HuespedDireccion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RESE_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 12)]
    public String RESE_Descripcion
    {
      get { return m_rese_descripcion; }
      set
      {
        if (m_rese_descripcion != value)
        {
          m_rese_descripcion = value;
          OnPropertyChanged("RESE_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 13)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 14)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 15)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 16)]
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
