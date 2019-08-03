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
  public partial class Entidades : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int64 m_enti_interno;
    private String m_tabl_tablatdi;
    private String m_tabl_internotdi;
    private String m_enti_id;
    private String m_enti_nombrecompleto;
    private String m_enti_direccion;
    private String m_enti_sexo;
    private Nullable<Int32> m_pais_interno;
    private Nullable<Int32> m_ubig_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Entidades.
    /// </summary>
    public Entidades()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Entidades con Instance Added.
    /// </summary>
    public static Entidades Nuevo()
    {
      try
      {
        Entidades item = new Entidades();
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
    /// Gets or sets el valor de: ENTI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 2)]
    public Int64 ENTI_Interno
    {
      get { return m_enti_interno; }
      set
      {
        if (m_enti_interno != value)
        {
          m_enti_interno = value;
          OnPropertyChanged("ENTI_Interno");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_TablaTDI.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 3)]
    public String TABL_TablaTDI
    {
      get { return m_tabl_tablatdi; }
      set
      {
        if (m_tabl_tablatdi != value)
        {
          m_tabl_tablatdi = value;
          OnPropertyChanged("TABL_TablaTDI");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_InternoTDI.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 4)]
    public String TABL_InternoTDI
    {
      get { return m_tabl_internotdi; }
      set
      {
        if (m_tabl_internotdi != value)
        {
          m_tabl_internotdi = value;
          OnPropertyChanged("TABL_InternoTDI");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Id.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Id", Description = "Id", ShortName = "Id", Order = 5)]
    public String ENTI_Id
    {
      get { return m_enti_id; }
      set
      {
        if (m_enti_id != value)
        {
          m_enti_id = value;
          OnPropertyChanged("ENTI_Id");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_NombreCompleto.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 6)]
    public String ENTI_NombreCompleto
    {
      get { return m_enti_nombrecompleto; }
      set
      {
        if (m_enti_nombrecompleto != value)
        {
          m_enti_nombrecompleto = value;
          OnPropertyChanged("ENTI_NombreCompleto");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Direccion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 7)]
    public String ENTI_Direccion
    {
      get { return m_enti_direccion; }
      set
      {
        if (m_enti_direccion != value)
        {
          m_enti_direccion = value;
          OnPropertyChanged("ENTI_Direccion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Sexo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Sexo", Description = "Sexo", ShortName = "Sexo", Order = 8)]
    public String ENTI_Sexo
    {
      get { return m_enti_sexo; }
      set
      {
        if (m_enti_sexo != value)
        {
          m_enti_sexo = value;
          OnPropertyChanged("ENTI_Sexo");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: PAIS_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Pais", Description = "Pais", ShortName = "Pais", Order = 9)]
    public Nullable<Int32> PAIS_Interno
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
    /// Gets or sets el valor de: UBIG_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Ubigeo", Description = "Ubigeo", ShortName = "Ubigeo", Order = 10)]
    public Nullable<Int32> UBIG_Interno
    {
      get
      {
        m_ubig_interno = null;
        if (ENTI_Distrito != 0) { m_ubig_interno = ENTI_Distrito; }
        return m_ubig_interno;
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
