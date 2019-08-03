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
  public partial class Tablas : MasterBusinessEntity, INotifyPropertyChanged
  {

    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private String m_tabl_tabla;
    private String m_tabl_interno;
    private String m_tabl_nombre;
    private String m_tabl_nomenclatura;
    private String m_tabl_descripcion;
    private String m_tabl_codigointernacional;
    private String m_tabl_codigo1;
    private String m_tabl_codigo2;
    private String m_tabl_codigo3;
    private Int32 m_tabl_numero1;
    private Double m_tabl_numero2;
    private Double m_tabl_numero3;
    private Boolean m_tabl_activo;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Tablas.
    /// </summary>
    public Tablas()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Tablas con Instance Added.
    /// </summary>
    public static Tablas Nuevo()
    {
      try
      {
        Tablas item = new Tablas();
        item.Instance = InstanceEntity.Added;
        item.TABL_Interno = "0000";
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
    /// Gets or sets el valor de: TABL_Tabla.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tabla", Description = "Tabla", ShortName = "Tabla", Order = 2)]
    public String TABL_Tabla
    {
      get { return m_tabl_tabla; }
      set
      {
        if (m_tabl_tabla != value)
        {
          m_tabl_tabla = value;
          OnPropertyChanged("TABL_Tabla");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Inteno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public String TABL_Interno
    {
      get { return m_tabl_interno; }
      set
      {
        if (m_tabl_interno != value)
        {
          m_tabl_interno = value;
          OnPropertyChanged("TABL_Inteno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Nombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 4)]
    public String TABL_Nombre
    {
      get { return m_tabl_nombre; }
      set
      {
        if (m_tabl_nombre != value)
        {
          m_tabl_nombre = value;
          OnPropertyChanged("TABL_Nombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Nomenclatura.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 5)]
    public String TABL_Nomenclatura
    {
      get { return m_tabl_nomenclatura; }
      set
      {
        if (m_tabl_nomenclatura != value)
        {
          m_tabl_nomenclatura = value;
          OnPropertyChanged("TABL_Nomenclatura");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Descripcion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 6)]
    public String TABL_Descripcion
    {
      get { return m_tabl_descripcion; }
      set
      {
        if (m_tabl_descripcion != value)
        {
          m_tabl_descripcion = value;
          OnPropertyChanged("TABL_Descripcion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_CodigoInternacional.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo Internacional", Description = "Codigo Internacional", ShortName = "Codigo Internacional", Order = 7)]
    public String TABL_CodigoInternacional
    {
      get { return m_tabl_codigointernacional; }
      set
      {
        if (m_tabl_codigointernacional != value)
        {
          m_tabl_codigointernacional = value;
          OnPropertyChanged("TABL_CodigoInternacional");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Codigo1.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 1", Description = "Codigo 1", ShortName = "Codigo 1", Order = 8)]
    public String TABL_Codigo1
    {
      get { return m_tabl_codigo1; }
      set
      {
        if (m_tabl_codigo1 != value)
        {
          m_tabl_codigo1 = value;
          OnPropertyChanged("TABL_Codigo1");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Codigo2.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 2", Description = "Codigo 2", ShortName = "Codigo 2", Order = 9)]
    public String TABL_Codigo2
    {
      get { return m_tabl_codigo2; }
      set
      {
        if (m_tabl_codigo2 != value)
        {
          m_tabl_codigo2 = value;
          OnPropertyChanged("TABL_Codigo2");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Codigo3.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Codigo 3", Description = "Codigo 3", ShortName = "Codigo 3", Order = 10)]
    public String TABL_Codigo3
    {
      get { return m_tabl_codigo3; }
      set
      {
        if (m_tabl_codigo3 != value)
        {
          m_tabl_codigo3 = value;
          OnPropertyChanged("TABL_Codigo3");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Numero1.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Numero 1", Description = "Numero 1", ShortName = "Numero 1", Order = 11)]
    [DisplayFormat(DataFormatString = "{0:##0}", ApplyFormatInEditMode = true)]
    public Int32 TABL_Numero1
    {
      get { return m_tabl_numero1; }
      set
      {
        if (m_tabl_numero1 != value)
        {
          m_tabl_numero1 = value;
          OnPropertyChanged("TABL_Numero1");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Numero2.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Numero 2", Description = "Numero 2", ShortName = "Numero 2", Order = 12)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double TABL_Numero2
    {
      get { return m_tabl_numero2; }
      set
      {
        if (m_tabl_numero2 != value)
        {
          m_tabl_numero2 = value;
          OnPropertyChanged("TABL_Numero2");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Numero3.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Numero 3", Description = "Numero 3", ShortName = "Numero 3", Order = 13)]
    [DisplayFormat(DataFormatString = "{0:##0.00}", ApplyFormatInEditMode = true)]
    public Double TABL_Numero3
    {
      get { return m_tabl_numero3; }
      set
      {
        if (m_tabl_numero3 != value)
        {
          m_tabl_numero3 = value;
          OnPropertyChanged("TABL_Numero3");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 14)]
    public Boolean TABL_Activo
    {
      get { return m_tabl_activo; }
      set
      {
        if (m_tabl_activo != value)
        {
          m_tabl_activo = value;
          OnPropertyChanged("TABL_Activo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: AUDI_UsuarioCreacion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Creacion", Description = "Usuario Creacion", ShortName = "Usuario Creacion", Order = 15)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Creacion", Description = "Fecha Creacion", ShortName = "Fecha Creacion", Order = 16)]
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
    [Display(AutoGenerateField = true, Name = "Usuario Modificacion", Description = "Usuario Modificacion", ShortName = "Usuario Modificacion", Order = 17)]
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
    [Display(AutoGenerateField = true, Name = "Fecha Modificacion", Description = "Fecha Modificacion", ShortName = "Fecha Modificacion", Order = 18)]
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
