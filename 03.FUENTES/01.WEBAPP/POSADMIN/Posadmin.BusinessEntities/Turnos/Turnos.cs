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
  public partial class Turnos : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_turn_interno;
    private String m_turn_nominacion;
    private DateTime m_turn_horainicio;
    private DateTime m_turn_horafin;
    private String m_turn_descripcion;
    private String m_turn_color;
    private Boolean m_turn_activo;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Turnos.
    /// </summary>
    public Turnos()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Turnos con Instance Added.
    /// </summary>
    public static Turnos Nuevo()
    {
      try
      {
        Turnos item = new Turnos();
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
    /// Gets or sets el valor de: TURN_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
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
    /// Gets or sets el valor de: TURN_Nominacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nominacion", Description = "Nominacion", ShortName = "Nominacion", Order = 4)]
    public String TURN_Nominacion
    {
      get { return m_turn_nominacion; }
      set
      {
        if (m_turn_nominacion != value)
        {
          m_turn_nominacion = value;
          OnPropertyChanged("TURN_Nominacion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_HoraInicio.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Hora Inicio", Description = "Hora Inicio", ShortName = "Hora Inicio", Order = 5)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime TURN_HoraInicio
    {
      get { return m_turn_horainicio; }
      set
      {
        if (m_turn_horainicio != value)
        {
          m_turn_horainicio = value;
          OnPropertyChanged("TURN_HoraInicio");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_HoraFin.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Hora Fin", Description = "Hora Fin", ShortName = "Hora Fin", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime TURN_HoraFin
    {
      get { return m_turn_horafin; }
      set
      {
        if (m_turn_horafin != value)
        {
          m_turn_horafin = value;
          OnPropertyChanged("TURN_HoraFin");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 7)]
    public String TURN_Descripcion
    {
      get { return m_turn_descripcion; }
      set
      {
        if (m_turn_descripcion != value)
        {
          m_turn_descripcion = value;
          OnPropertyChanged("TURN_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_Color.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Color", Description = "Color", ShortName = "Color", Order = 8)]
    public String TURN_Color
    {
      get { return m_turn_color; }
      set
      {
        if (m_turn_color != value)
        {
          m_turn_color = value;
          OnPropertyChanged("TURN_Color");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TURN_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 8)]
    public Boolean TURN_Activo
    {
      get { return m_turn_activo; }
      set
      {
        if (m_turn_activo != value)
        {
          m_turn_activo = value;
          OnPropertyChanged("TURN_Activo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 10)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 11)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 12)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 13)]
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
