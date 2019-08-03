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
  public partial class Paises : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int32 m_pais_interno;
    private String m_pais_nombre;
    private String m_pais_codigonumerico;
    private String m_pais_codigoalfa2;
    private String m_pais_codigoalfa3;
    private String m_pais_continente;
    private String m_pais_descripcion;
    private Boolean m_pais_activo;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Paises.
    /// </summary>
    public Paises()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase Paises con Instance Added.
    /// </summary>
    public static Paises Nuevo()
    {
      try
      {
        Paises item = new Paises();
        item.Instance = InstanceEntity.Added;
        return item;
      }
      catch (Exception) { throw; }
    }

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: PAIS_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Pais Interno", Description = "Pais Interno", ShortName = "Pais Interno", Order = 1)]
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
    /// Gets or sets el valor de: PAIS_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String PAIS_Nombre
    {
      get { return m_pais_nombre; }
      set
      {
        if (m_pais_nombre != value)
        {
          m_pais_nombre = value;
          OnPropertyChanged("PAIS_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_CodigoNumerico.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo Numerico", Description = "Codigo Numerico", ShortName = "Codigo Numerico", Order = 3)]
    public String PAIS_CodigoNumerico
    {
      get { return m_pais_codigonumerico; }
      set
      {
        if (m_pais_codigonumerico != value)
        {
          m_pais_codigonumerico = value;
          OnPropertyChanged("PAIS_CodigoNumerico");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_CodigoAlfa2.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo Alfa2", Description = "Codigo Alfa2", ShortName = "Codigo Alfa2", Order = 4)]
    public String PAIS_CodigoAlfa2
    {
      get { return m_pais_codigoalfa2; }
      set
      {
        if (m_pais_codigoalfa2 != value)
        {
          m_pais_codigoalfa2 = value;
          OnPropertyChanged("PAIS_CodigoAlfa2");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_CodigoAlfa3.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo Alfa3", Description = "Codigo Alfa3", ShortName = "Codigo Alfa3", Order = 5)]
    public String PAIS_CodigoAlfa3
    {
      get { return m_pais_codigoalfa3; }
      set
      {
        if (m_pais_codigoalfa3 != value)
        {
          m_pais_codigoalfa3 = value;
          OnPropertyChanged("PAIS_CodigoAlfa3");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_Continente.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Continente", Description = "Continente", ShortName = "Continente", Order = 6)]
    public String PAIS_Continente
    {
      get { return m_pais_continente; }
      set
      {
        if (m_pais_continente != value)
        {
          m_pais_continente = value;
          OnPropertyChanged("PAIS_Continente");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 7)]
    public String PAIS_Descripcion
    {
      get { return m_pais_descripcion; }
      set
      {
        if (m_pais_descripcion != value)
        {
          m_pais_descripcion = value;
          OnPropertyChanged("PAIS_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 8)]
    public Boolean PAIS_Activo
    {
      get { return m_pais_activo; }
      set
      {
        if (m_pais_activo != value)
        {
          m_pais_activo = value;
          OnPropertyChanged("PAIS_Activo");
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
