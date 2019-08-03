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
  public partial class Habitaciones : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_habi_interno;
    private String m_tabl_tablapis;
    private String m_tabl_internopis;
    private String m_tabl_tablatha;
    private String m_tabl_internotha;
    private String m_habi_numero;
    private String m_habi_estado;
    private Boolean m_habi_limpio;
    private Boolean m_habi_activo;
    private String m_habi_descripcion;
    private Double m_habi_preciodia;
    private Double m_habi_preciohora;
    private Double m_habi_preciominimo;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Habitaciones.
    /// </summary>
    public Habitaciones()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Habitaciones con Instance Added.
    /// </summary>
    public static Habitaciones Nuevo()
    {
      try
      {
        Habitaciones item = new Habitaciones();
        item.Instance = InstanceEntity.Added;
        return item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ Propiedades ]

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
    /// Gets or sets el valor de: HABI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
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
    /// Gets or sets el valor de: TABL_TablaPIS.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 4)]
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
    [Display(AutoGenerateField = true, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 5)]
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
    /// Gets or sets el valor de: TABL_TablaTHA.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 6)]
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
    [Display(AutoGenerateField = true, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 7)]
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
    /// Gets or sets el valor de: HABI_Numero.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Numero", Description = "Numero", ShortName = "Numero", Order = 8)]
    public String HABI_Numero
    {
      get { return m_habi_numero; }
      set
      {
        if (m_habi_numero != value)
        {
          m_habi_numero = value;
          OnPropertyChanged("HABI_Numero");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Estado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 9)]
    public String HABI_Estado
    {
      get { return m_habi_estado; }
      set
      {
        if (m_habi_estado != value)
        {
          m_habi_estado = value;
          OnPropertyChanged("HABI_Estado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Limpio.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Limpio", Description = "Limpio", ShortName = "Limpio", Order = 10)]
    public Boolean HABI_Limpio
    {
      get { return m_habi_limpio; }
      set
      {
        if (m_habi_limpio != value)
        {
          m_habi_limpio = value;
          OnPropertyChanged("HABI_Limpio");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 11)]
    public Boolean HABI_Activo
    {
      get { return m_habi_activo; }
      set
      {
        if (m_habi_activo != value)
        {
          m_habi_activo = value;
          OnPropertyChanged("HABI_Activo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 12)]
    public String HABI_Descripcion
    {
      get { return m_habi_descripcion; }
      set
      {
        if (m_habi_descripcion != value)
        {
          m_habi_descripcion = value;
          OnPropertyChanged("HABI_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_PrecioDia.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Precio Dia", Description = "Precio Dia", ShortName = "Precio Dia", Order = 13)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double HABI_PrecioDia
    {
      get { return m_habi_preciodia; }
      set
      {
        if (m_habi_preciodia != value)
        {
          m_habi_preciodia = value;
          OnPropertyChanged("HABI_PrecioDia");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_PrecioHora.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Precio Hora", Description = "Precio Hora", ShortName = "Precio Hora", Order = 14)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double HABI_PrecioHora
    {
      get { return m_habi_preciohora; }
      set
      {
        if (m_habi_preciohora != value)
        {
          m_habi_preciohora = value;
          OnPropertyChanged("HABI_PrecioHora");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: HABI_PrecioMinimo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Precio Minimo", Description = "Precio Minimo", ShortName = "Precio Minimo", Order = 15)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double HABI_PrecioMinimo
    {
      get { return m_habi_preciominimo; }
      set
      {
        if (m_habi_preciominimo != value)
        {
          m_habi_preciominimo = value;
          OnPropertyChanged("HABI_PrecioMinimo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 16)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 17)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 18)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 19)]
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
