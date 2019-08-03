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
  public partial class FuncionesEntidades : MasterBusinessEntity, INotifyPropertyChanged
  {
    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private String m_tabl_tablaten;
    private String m_tabl_internoten;
    private Int64 m_enti_interno;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase FuncionesEntidades.
    /// </summary>
    public FuncionesEntidades()
    { Instance = InstanceEntity.Unchanged; }
    /// <summary>
    /// Inicializar una nueva instancia de la clase FuncionesEntidades con Instance Added.
    /// </summary>
    public static FuncionesEntidades Nuevo()
    {
      try
      {
        FuncionesEntidades item = new FuncionesEntidades();
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
    /// Gets or sets el valor de: TABL_TablaTEN.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Doc. Identidad", Description = "Tipo Doc. Identidad", ShortName = "Tipo Doc. Identidad", Order = 2)]
    public String TABL_TablaTEN
    {
      get { return m_tabl_tablaten; }
      set
      {
        if (m_tabl_tablaten != value)
        {
          m_tabl_tablaten = value;
          OnPropertyChanged("TABL_TablaTEN");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TABL_InternoTEN.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Doc. Identidad", Description = "Tipo Doc. Identidad", ShortName = "Tipo Doc. Identidad", Order = 3)]
    public String TABL_InternoTEN
    {
      get { return m_tabl_internoten; }
      set
      {
        if (m_tabl_internoten != value)
        {
          m_tabl_internoten = value;
          OnPropertyChanged("TABL_IntenoTEN");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Interno.
    /// </summary>
    [Key]
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Entidad Interno", Description = "Entidad Interno", ShortName = "Entidad Interno", Order = 4)]
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
		/// Gets or sets el valor de: AUDI_UsuarioCreacion.
		/// </summary>
		[DataMember]
    [Display(AutoGenerateField = true, Name = "AUDI_UsuarioCreacion", Description = "AUDI_UsuarioCreacion", ShortName = "AUDI_UsuarioCreacion", Order = 5)]
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
    [Display(AutoGenerateField = true, Name = "AUDI_FechaCreacion", Description = "AUDI_FechaCreacion", ShortName = "AUDI_FechaCreacion", Order = 6)]
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
    [Display(AutoGenerateField = true, Name = "AUDI_UsuarioModificacion", Description = "AUDI_UsuarioModificacion", ShortName = "AUDI_UsuarioModificacion", Order = 7)]
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
    [Display(AutoGenerateField = true, Name = "AUDI_FechaModificacion", Description = "AUDI_FechaModificacion", ShortName = "AUDI_FechaModificacion", Order = 8)]
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
