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
  public partial class Roles : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int32 m_role_interno;
    private String m_role_nombre;
    private String m_role_descripcion;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Roles.
    /// </summary>
    public Roles()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase Roles con Instance Added.
    /// </summary>
    public static Roles Nuevo()
    {
      try
      {
        Roles l_item = new Roles();
        l_item.Instance = InstanceEntity.Added;
        return l_item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: ROLE_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Rol Interno", Description = "Rol Interno", ShortName = "Rol Interno", Order = 1)]
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
    /// Gets or sets el valor de: ROLE_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String ROLE_Nombre
    {
      get { return m_role_nombre; }
      set
      {
        if (m_role_nombre != value)
        {
          m_role_nombre = value;
          OnPropertyChanged("ROLE_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ROLE_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 3)]
    public String ROLE_Descripcion
    {
      get { return m_role_descripcion; }
      set
      {
        if (m_role_descripcion != value)
        {
          m_role_descripcion = value;
          OnPropertyChanged("ROLE_Descripcion");
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

  }
}
