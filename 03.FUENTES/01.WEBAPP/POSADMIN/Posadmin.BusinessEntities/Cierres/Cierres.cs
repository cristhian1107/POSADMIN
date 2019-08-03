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
  public partial class Cierres : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_cier_interno;
    private DateTime m_cier_fechahoraregistro;
    private Double m_cier_montoreal;
    private Double m_cier_montoextra;
    private Double m_cier_montodeuda;
    private Double m_cier_montodemas;
    private Int64 m_usua_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase _Cierres.
    /// </summary>
    public Cierres()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase _Cierres con Instance Added.
    /// </summary>
    public static Cierres Nuevo()
    {
      try
      {
        Cierres item = new Cierres();
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
    /// Gets or sets el valor de: CIER_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 CIER_Interno
    {
      get { return m_cier_interno; }
      set
      {
        if (m_cier_interno != value)
        {
          m_cier_interno = value;
          OnPropertyChanged("CIER_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: CIER_FechaHoraRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 4)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime CIER_FechaHoraRegistro
    {
      get { return m_cier_fechahoraregistro; }
      set
      {
        if (m_cier_fechahoraregistro != value)
        {
          m_cier_fechahoraregistro = value;
          OnPropertyChanged("CIER_FechaHoraRegistro");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: CIER_MontoReal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Real", Description = "Monto Real", ShortName = "Monto Real", Order = 5)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double CIER_MontoReal
    {
      get { return m_cier_montoreal; }
      set
      {
        if (m_cier_montoreal != value)
        {
          m_cier_montoreal = value;
          OnPropertyChanged("CIER_MontoReal");
        }
      }
    }

    /// <summary>
		/// Gets or sets el valor de: CIER_MontoExtra.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Extra", Description = "Monto Extra", ShortName = "Monto Extra", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double CIER_MontoExtra
    {
      get { return m_cier_montoextra; }
      set
      {
        if (m_cier_montoextra != value)
        {
          m_cier_montoextra = value;
          OnPropertyChanged("CIER_MontoExtra");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: CIER_MontoDeuda.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Deuda", Description = "Monto Deuda", ShortName = "Monto Deuda", Order = 7)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double CIER_MontoDeuda
    {
      get { return m_cier_montodeuda; }
      set
      {
        if (m_cier_montodeuda != value)
        {
          m_cier_montodeuda = value;
          OnPropertyChanged("CIER_MontoDeuda");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: CIER_MontoDemas.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto Demas", Description = "Monto Demas", ShortName = "Monto Demas", Order = 8)]
    [DisplayFormat(DataFormatString = "{0:#,#0.00}", ApplyFormatInEditMode = true)]
    public Double CIER_MontoDemas
    {
      get { return m_cier_montodemas; }
      set
      {
        if (m_cier_montodemas != value)
        {
          m_cier_montodemas = value;
          OnPropertyChanged("CIER_MontoDemas");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 9)]
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
