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
  public partial class Ubigeos : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int32 m_ubig_interno;
    private Int32 m_pais_interno;
    private String m_ubig_nombre;
    private String m_ubig_nomenclatura;
    private String m_ubig_descripcion;
    private String m_ubig_codigo1;
    private String m_ubig_codigo2;
    private String m_ubig_codigo3;
    private Boolean m_ubig_activo;
    private Nullable<Int32> m_ubig_internopadre;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Ubigeos.
    /// </summary>
    public Ubigeos()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase Ubigeos con Instance Added.
    /// </summary>
    public static Ubigeos Nuevo()
    {
      try
      {
        Ubigeos l_item = new Ubigeos();
        l_item.Instance = InstanceEntity.Added;
        return l_item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: UBIG_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Ubigeo Interno", Description = "Ubigeo Interno", ShortName = "Ubigeo Interno", Order = 1)]
    public Int32 UBIG_Interno
    {
      get { return m_ubig_interno; }
      set
      {
        if (m_ubig_interno != value)
        {
          m_ubig_interno = value;
          OnPropertyChanged("UBIG_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Pais Interno", Description = "Pais Interno", ShortName = "Pais Interno", Order = 2)]
    public Int32 PAIS_Interno
    {
      get { return m_pais_interno; }
      set
      {
        if (m_pais_interno != value)
        {
          m_pais_interno = value;
          OnPropertyChanged("PAIS_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 3)]
    public String UBIG_Nombre
    {
      get { return m_ubig_nombre; }
      set
      {
        if (m_ubig_nombre != value)
        {
          m_ubig_nombre = value;
          OnPropertyChanged("UBIG_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Nomenclatura.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 4)]
    public String UBIG_Nomenclatura
    {
      get { return m_ubig_nomenclatura; }
      set
      {
        if (m_ubig_nomenclatura != value)
        {
          m_ubig_nomenclatura = value;
          OnPropertyChanged("UBIG_Nomenclatura");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 5)]
    public String UBIG_Descripcion
    {
      get { return m_ubig_descripcion; }
      set
      {
        if (m_ubig_descripcion != value)
        {
          m_ubig_descripcion = value;
          OnPropertyChanged("UBIG_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Codigo1.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 1", Description = "Codigo 1", ShortName = "Codigo 1", Order = 6)]
    public String UBIG_Codigo1
    {
      get { return m_ubig_codigo1; }
      set
      {
        if (m_ubig_codigo1 != value)
        {
          m_ubig_codigo1 = value;
          OnPropertyChanged("UBIG_Codigo1");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Codigo2.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 2", Description = "Codigo 2", ShortName = "Codigo 2", Order = 7)]
    public String UBIG_Codigo2
    {
      get { return m_ubig_codigo2; }
      set
      {
        if (m_ubig_codigo2 != value)
        {
          m_ubig_codigo2 = value;
          OnPropertyChanged("UBIG_Codigo2");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Codigo3.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 3", Description = "Codigo 3", ShortName = "Codigo 3", Order = 8)]
    public String UBIG_Codigo3
    {
      get { return m_ubig_codigo3; }
      set
      {
        if (m_ubig_codigo3 != value)
        {
          m_ubig_codigo3 = value;
          OnPropertyChanged("UBIG_Codigo3");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 9)]
    public Boolean UBIG_Activo
    {
      get { return m_ubig_activo; }
      set
      {
        if (m_ubig_activo != value)
        {
          m_ubig_activo = value;
          OnPropertyChanged("UBIG_Activo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_InternoPadre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Padre", Description = "Padre", ShortName = "Padre", Order = 10)]
    public Nullable<Int32> UBIG_InternoPadre
    {
      get { return m_ubig_internopadre; }
      set
      {
        if (m_ubig_internopadre != value)
        {
          m_ubig_internopadre = value;
          OnPropertyChanged("UBIG_InternoPadre");
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
