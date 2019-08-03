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
  public partial class Opciones : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int32 m_opci_interno;
    private String m_opci_nombre;
    private String m_opci_nomenclatura;
    private String m_opci_descripcion;
    private Nullable<Int32> m_opci_internopadre;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Opciones.
    /// </summary>
    public Opciones()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Opciones con Instance Added.
    /// </summary>
    public static Opciones Nuevo()
    {
      try
      {
        Opciones l_item = new Opciones();
        l_item.Instance = InstanceEntity.Added;
        return l_item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: OPCI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Opcion Interno", Description = "Opcion Interno", ShortName = "Opcion Interno", Order = 1)]
    public Int32 OPCI_Interno
    {
      get { return m_opci_interno; }
      set
      {
        if (m_opci_interno != value)
        {
          m_opci_interno = value;
          OnPropertyChanged("OPCI_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String OPCI_Nombre
    {
      get { return m_opci_nombre; }
      set
      {
        if (m_opci_nombre != value)
        {
          m_opci_nombre = value;
          OnPropertyChanged("OPCI_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_Nomenclatura.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 3)]
    public String OPCI_Nomenclatura
    {
      get { return m_opci_nomenclatura; }
      set
      {
        if (m_opci_nomenclatura != value)
        {
          m_opci_nomenclatura = value;
          OnPropertyChanged("OPCI_Nomenclatura");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 4)]
    public String OPCI_Descripcion
    {
      get { return m_opci_descripcion; }
      set
      {
        if (m_opci_descripcion != value)
        {
          m_opci_descripcion = value;
          OnPropertyChanged("OPCI_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: OPCI_InternoPadre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno Padre", Description = "Interno Padre", ShortName = "Interno Padre", Order = 5)]
    public Nullable<Int32> OPCI_InternoPadre
    {
      get { return m_opci_internopadre; }
      set
      {
        if (m_opci_internopadre != value)
        {
          m_opci_internopadre = value;
          OnPropertyChanged("OPCI_InternoPadre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = false, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 6)]
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
    [Display(AutoGenerateField = false, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 7)]
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
    [Display(AutoGenerateField = false, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 8)]
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
    [Display(AutoGenerateField = false, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 9)]
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
