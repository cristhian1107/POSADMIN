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
  public partial class Sucursales : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_sucu_interno;
    private String m_sucu_nombre;
    private String m_sucu_direccion;
    private String m_sucu_correo;
    private String m_sucu_telefono;
    private String m_sucu_web;
    private Boolean m_sucu_principal;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Sucursales.
    /// </summary>
    public Sucursales()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Sucursales con Instance Added.
    /// </summary>
    public static Sucursales Nuevo()
    {
      try
      {
        Sucursales item = new Sucursales();
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
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 2)]
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
    /// Gets or sets el valor de: SUCU_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 3)]
    public String SUCU_Nombre
    {
      get { return m_sucu_nombre; }
      set
      {
        if (m_sucu_nombre != value)
        {
          m_sucu_nombre = value;
          OnPropertyChanged("SUCU_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Direccion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 4)]
    public String SUCU_Direccion
    {
      get { return m_sucu_direccion; }
      set
      {
        if (m_sucu_direccion != value)
        {
          m_sucu_direccion = value;
          OnPropertyChanged("SUCU_Direccion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Correo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Correo", Description = "Correo", ShortName = "Correo", Order = 5)]
    public String SUCU_Correo
    {
      get { return m_sucu_correo; }
      set
      {
        if (m_sucu_correo != value)
        {
          m_sucu_correo = value;
          OnPropertyChanged("SUCU_Correo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Telefono.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Telefono", Description = "Telefono", ShortName = "Telefono", Order = 6)]
    public String SUCU_Telefono
    {
      get { return m_sucu_telefono; }
      set
      {
        if (m_sucu_telefono != value)
        {
          m_sucu_telefono = value;
          OnPropertyChanged("SUCU_Telefono");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Web.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Web", Description = "Web", ShortName = "Web", Order = 7)]
    public String SUCU_Web
    {
      get { return m_sucu_web; }
      set
      {
        if (m_sucu_web != value)
        {
          m_sucu_web = value;
          OnPropertyChanged("SUCU_Web");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: SUCU_Principal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Principal", Description = "Principal", ShortName = "Principal", Order = 8)]
    public Boolean SUCU_Principal
    {
      get { return m_sucu_principal; }
      set
      {
        if (m_sucu_principal != value)
        {
          m_sucu_principal = value;
          OnPropertyChanged("SUCU_Principal");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 9)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 10)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 11)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 12)]
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
