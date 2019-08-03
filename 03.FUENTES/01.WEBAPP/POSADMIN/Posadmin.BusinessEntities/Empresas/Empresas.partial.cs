using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  public partial class Empresas
  {
    #region [ VARIABLES ]

    private String m_empr_paisnombre;
    private Int32 m_empr_departamento;
    private Int32 m_empr_provincia;
    private Int32 m_empr_distrito;
    private Boolean m_empr_activo;
    private Int64 m_usua_interno;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: EMPR_PaisNombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre Pais", Description = "Nombre Pais", ShortName = "Nombre Pais", Order = 15)]
    public String EMPR_PaisNombre
    {
      get { return m_empr_paisnombre; }
      set
      {
        if (m_empr_paisnombre != value)
        {
          m_empr_paisnombre = value;
          OnPropertyChanged("EMPR_PaisNombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Departamento.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Departamento", Description = "Departamento", ShortName = "Departamento", Order = 16)]
    public Int32 EMPR_Departamento
    {
      get { return m_empr_departamento; }
      set
      {
        if (m_empr_departamento != value)
        {
          m_empr_departamento = value;
          OnPropertyChanged("EMPR_Departamento");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Provincia.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Provincia", Description = "Provincia", ShortName = "Provincia", Order = 17)]
    public Int32 EMPR_Provincia
    {
      get { return m_empr_provincia; }
      set
      {
        if (m_empr_provincia != value)
        {
          m_empr_provincia = value;
          OnPropertyChanged("EMPR_Provincia");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: EMPR_Distrito.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Distrito", Description = "Distrito", ShortName = "Distrito", Order = 18)]
    public Int32 EMPR_Distrito
    {
      get { return m_empr_distrito; }
      set
      {
        if (m_empr_distrito != value)
        {
          m_empr_distrito = value;
          OnPropertyChanged("EMPR_Distrito");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Interno.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Usuario Interno", Description = "Usuario Interno", ShortName = "Usuario Interno", Order = 19)]
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
    /// Gets or sets el valor de: EMPR_Activo.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 20)]
    public Boolean EMPR_Activo
    {
      get { return m_empr_activo; }
      set
      {
        if (m_empr_activo != value)
        {
          m_empr_activo = value;
          OnPropertyChanged("EMPR_Activo");
        }
      }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
