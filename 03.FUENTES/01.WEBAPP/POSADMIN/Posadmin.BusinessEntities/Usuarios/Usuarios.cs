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
  public partial class Usuarios : MasterBusinessEntity, IIdentity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_usua_interno;
    private Int32 m_role_interno;
    private String m_usua_nombrecompleto;
    private String m_usua_usuario;
    private String m_usua_contrasena;
    private String m_usua_correo;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase _Usuarios.
    /// </summary>
    public Usuarios()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase _Usuarios con Instance Added.
    /// </summary>
    public static Usuarios Nuevo()
    {
      try
      {
        Usuarios item = new Usuarios();
        item.Instance = InstanceEntity.Added;
        return item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Interno", Description = "Usuario Interno", ShortName = "Usuario Interno", Order = 1)]
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
    /// Gets or sets el valor de: ROLE_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Rol Interno", Description = "Rol Interno", ShortName = "Rol Interno", Order = 2)]
    public Int32 ROLE_Interno
    {
      get { return m_role_interno; }
      set
      {
        if (m_role_interno != value)
        {
          m_role_interno = value;
          OnPropertyChanged("ROLE_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_NombreCompleto.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 3)]
    public String USUA_NombreCompleto
    {
      get { return m_usua_nombrecompleto; }
      set
      {
        if (m_usua_nombrecompleto != value)
        {
          m_usua_nombrecompleto = value;
          OnPropertyChanged("USUA_NombreCompleto");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Usuario.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 4)]
    public String USUA_Usuario
    {
      get { return m_usua_usuario; }
      set
      {
        if (m_usua_usuario != value)
        {
          m_usua_usuario = value;
          OnPropertyChanged("USUA_Usuario");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Contrasena.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Contrasena", Description = "Contrasena", ShortName = "Contrasena", Order = 5)]
    public String USUA_Contrasena
    {
      get { return m_usua_contrasena; }
      set
      {
        if (m_usua_contrasena != value)
        {
          m_usua_contrasena = value;
          OnPropertyChanged("USUA_Contrasena");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Correo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Correo", Description = "Correo", ShortName = "Correo", Order = 6)]
    public String USUA_Correo
    {
      get { return m_usua_correo; }
      set
      {
        if (m_usua_correo != value)
        {
          m_usua_correo = value;
          OnPropertyChanged("USUA_Correo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 7)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 8)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 9)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 10)]
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

    #region [ IIDENTITY ]

    private Boolean m_isAuthenticated;

    /// <summary>
    /// Gets el valor de: Name.
    /// </summary>
    public String Name
    {
      get
      { return USUA_Usuario; }
    }

    /// <summary>
    /// Gets el valor de: AuthenticationType.
    /// </summary>
    public string AuthenticationType
    { get { return "Basic Authentication"; } }

    /// <summary>
    /// Gets el valor de: IsAuthenticated.
    /// </summary>
    public bool IsAuthenticated
    {
      get
      { return m_isAuthenticated; }
    }

    /// <summary>
    /// Indica que se encuentra autentificado
    /// </summary>
    /// <param name="IsAuthenticated"></param>
    public void Authenticate(Boolean IsAuthenticated)
    {
      m_isAuthenticated = IsAuthenticated;
    }

    #endregion
  }
}
