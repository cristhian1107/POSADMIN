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
  public partial class DetallesPagosRegistros : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private Int64 m_regi_interno;
    private Int32 m_pago_item;
    private String m_pago_tipo;
    private Double m_pago_montocancelado;
    private DateTime m_pago_fechahoraregistro;
    private Int64 m_usua_interno;
    private Int64 m_reti_interno;
    private Int64 m_cier_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase DetallesPagosRegistros.
    /// </summary>
    public DetallesPagosRegistros()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase DetallesPagosRegistros con Instance Added.
    /// </summary>
    public static DetallesPagosRegistros Nuevo()
    {
      try
      {
        DetallesPagosRegistros item = new DetallesPagosRegistros();
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
    /// Gets or sets el valor de: REGI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Registro Interno", Description = "Registro Interno", ShortName = "Registro Interno", Order = 3)]
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
    /// Gets or sets el valor de: PAGO_Item.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Item", Description = "Item", ShortName = "Item", Order = 4)]
    public Int32 PAGO_Item
    {
      get { return m_pago_item; }
      set
      {
        if (m_pago_item != value)
        {
          m_pago_item = value;
          OnPropertyChanged("PAGO_Item");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_Tipo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo de Pago", Description = "Tipo de Pago", ShortName = "Tipo de Pago", Order = 5)]
    public String PAGO_Tipo
    {
      get { return m_pago_tipo; }
      set
      {
        if (m_pago_tipo != value)
        {
          m_pago_tipo = value;
          OnPropertyChanged("PAGO_Tipo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_MontoCancelado.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Monto", Description = "Monto", ShortName = "Monto", Order = 6)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double PAGO_MontoCancelado
    {
      get { return m_pago_montocancelado; }
      set
      {
        if (m_pago_montocancelado != value)
        {
          m_pago_montocancelado = value;
          OnPropertyChanged("PAGO_MontoCancelado");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAGO_FechaHoraRegistro.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Fecha Registro", Description = "Fecha Registro", ShortName = "Fecha Registro", Order = 7)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime PAGO_FechaHoraRegistro
    {
      get { return m_pago_fechahoraregistro; }
      set
      {
        if (m_pago_fechahoraregistro != value)
        {
          m_pago_fechahoraregistro = value;
          OnPropertyChanged("PAGO_FechaHoraRegistro");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 8)]
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
    /// Gets or sets el valor de: RETI_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Retiro", Description = "Retiro", ShortName = "Retiro", Order = 9)]
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
    /// Gets or sets el valor de: CIER_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Cierre", Description = "Cierre", ShortName = "Cierre", Order = 10)]
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
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 11)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 12)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 13)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 14)]
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
