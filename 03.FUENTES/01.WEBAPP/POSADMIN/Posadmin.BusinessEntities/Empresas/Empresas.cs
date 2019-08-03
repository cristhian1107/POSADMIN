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
  public partial class Empresas : MasterBusinessEntity, INotifyPropertyChanged
  {

    #region [ VARIABLES ]

    private Int64 m_empr_interno;
    private Int32 m_pais_interno;
    private String m_empr_id;
    private String m_empr_razonsocial;
    private String m_empr_direccion;
    private String m_empr_nombrecomercial;
    private Nullable<Int32> m_ubig_interno;
    private String m_empr_correos;
    private String m_empr_telefonos;
    private String m_empr_web;
    private String m_audi_usuariocreacion;
    private DateTime m_audi_fechacreacion;
    private String m_audi_usuariomodificacion;
    private Nullable<DateTime> m_audi_fechamodificacion;

    #endregion

    #region [ CONSTRUCTORES ]

    /// <summary>
    /// Inicializar una nueva instancia de la clase Empresas.
    /// </summary>
    public Empresas()
    { Instance = InstanceEntity.Unchanged; }

    /// <summary>
    /// Inicializar una nueva instancia de la clase Empresas con Instance Added.
    /// </summary>
    public static Empresas Nuevo()
    {
      try
      {
        Empresas item = new Empresas();
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
    [Display(AutoGenerateField = true, Name = "Empresas Interno", Description = "Empresas Interno", ShortName = "Empresas Interno", Order = 1)]
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
    /// Gets or sets el valor de: EMPR_Id.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Id", Description = "Id", ShortName = "Id", Order = 3)]
    public String EMPR_Id
    {
      get { return m_empr_id; }
      set
      {
        if (m_empr_id != value)
        {
          m_empr_id = value;
          OnPropertyChanged("EMPR_Id");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_RazonSocial.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Razon Social", Description = "Razon Social", ShortName = "Razon Social", Order = 4)]
    public String EMPR_RazonSocial
    {
      get { return m_empr_razonsocial; }
      set
      {
        if (m_empr_razonsocial != value)
        {
          m_empr_razonsocial = value;
          OnPropertyChanged("EMPR_RazonSocial");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Direccion.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 5)]
    public String EMPR_Direccion
    {
      get { return m_empr_direccion; }
      set
      {
        if (m_empr_direccion != value)
        {
          m_empr_direccion = value;
          OnPropertyChanged("EMPR_Direccion");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_NombreComercial.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Comercial", Description = "Nombre Comercial", ShortName = "Nombre Comercial", Order = 6)]
    public String EMPR_NombreComercial
    {
      get { return m_empr_nombrecomercial; }
      set
      {
        if (m_empr_nombrecomercial != value)
        {
          m_empr_nombrecomercial = value;
          OnPropertyChanged("EMPR_NombreComercial");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: UBIG_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Uigeo Interno", Description = "Uigeo Interno", ShortName = "Uigeo Interno", Order = 7)]
    public Nullable<Int32> UBIG_Interno
    {
      get
      {
        m_ubig_interno = null;
        if (EMPR_Distrito != 0) { m_ubig_interno = EMPR_Distrito; }
        return m_ubig_interno;
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Correos.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Correos", Description = "Correos", ShortName = "Correos", Order = 8)]
    public String EMPR_Correos
    {
      get { return m_empr_correos; }
      set
      {
        if (m_empr_correos != value)
        {
          m_empr_correos = value;
          OnPropertyChanged("EMPR_Correos");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Telefonos.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Telefonos", Description = "Telefonos", ShortName = "Telefonos", Order = 9)]
    public String EMPR_Telefonos
    {
      get { return m_empr_telefonos; }
      set
      {
        if (m_empr_telefonos != value)
        {
          m_empr_telefonos = value;
          OnPropertyChanged("EMPR_Telefonos");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Web.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Web", Description = "Web", ShortName = "Web", Order = 10)]
    public String EMPR_Web
    {
      get { return m_empr_web; }
      set
      {
        if (m_empr_web != value)
        {
          m_empr_web = value;
          OnPropertyChanged("EMPR_Web");
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
