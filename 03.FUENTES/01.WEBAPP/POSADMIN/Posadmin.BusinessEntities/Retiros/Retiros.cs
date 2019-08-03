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
  public partial class Retiros : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_reti_interno;
    private DateTime m_reti_fechahoraregistro;
    private Double m_reti_montoentregado;
    private Double m_reti_montoextra;
    private Int64 m_usua_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Retiros.
    /// </summary>
    public Retiros()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Retiros con Instance Added.
    /// </summary>
    public static Retiros Nuevo()
    {
      try
      {
        Retiros item = new Retiros();
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
    /// Gets or sets el valor de: RETI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 RETI_Interno
    {
      get { return m_reti_interno; }
      set
      {
        if (m_reti_interno != value)
        {
          m_reti_interno = value;
          OnPropertyChanged("RETI_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RETI_FechaHoraRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 4)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime RETI_FechaHoraRegistro
    {
      get { return m_reti_fechahoraregistro; }
      set
      {
        if (m_reti_fechahoraregistro != value)
        {
          m_reti_fechahoraregistro = value;
          OnPropertyChanged("RETI_FechaHoraRegistro");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RETI_MontoEntregado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Entregado", Description = "Monto Entregado", ShortName = "Monto Entregado", Order = 5)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double RETI_MontoEntregado
    {
      get { return m_reti_montoentregado; }
      set
      {
        if (m_reti_montoentregado != value)
        {
          m_reti_montoentregado = value;
          OnPropertyChanged("RETI_MontoEntregado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: RETI_MontoExtra.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Extra", Description = "Monto Extra", ShortName = "Monto Extra", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double RETI_MontoExtra
    {
      get { return m_reti_montoextra; }
      set
      {
        if (m_reti_montoextra != value)
        {
          m_reti_montoextra = value;
          OnPropertyChanged("RETI_MontoExtra");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 7)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 8)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 9)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 10)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 11)]
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
