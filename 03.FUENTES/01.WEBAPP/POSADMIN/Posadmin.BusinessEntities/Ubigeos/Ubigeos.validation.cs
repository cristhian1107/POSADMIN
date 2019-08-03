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
using SoftwareFactory.Infrastructure.BusinessEntity;

namespace Posadmin.BusinessEntities
{
  public partial class Ubigeos
  {
    #region [ PROPIEDADES VALIDACIÓN ]

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_InternoOK
    {
      get
      {
        if (UBIG_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_InternoMSGERROR
    {
      get
      {
        if (!UBIG_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno del ubigeo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_InternoOK
    {
      get
      {
        if (PAIS_Interno <= 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_InternoMSGERROR
    {
      get
      {
        if (!PAIS_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno del pais"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Nombre))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_NombreMSGERROR
    {
      get
      {
        if (!UBIG_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_NomenclaturaOK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Nomenclatura))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_NomenclaturaMSGERROR
    {
      get
      {
        if (!UBIG_NomenclaturaOK)
        { return String.Format("Debe ingresar una {0}.", "nomenclatura"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Descripcion))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_DescripcionMSGERROR
    {
      get
      {
        if (!UBIG_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Codigo1.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_Codigo1OK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Codigo1))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Codigo1.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_Codigo1MSGERROR
    {
      get
      {
        if (!UBIG_Codigo1OK)
        { return String.Format("Debe ingresar un {0}.", "codigo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Codigo2.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_Codigo2OK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Codigo2))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Codigo2.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_Codigo2MSGERROR
    {
      get
      {
        if (!UBIG_Codigo2OK)
        { return String.Format("Debe ingresar un {0}.", "codigo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Codigo3.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_Codigo3OK
    {
      get
      {
        if (String.IsNullOrEmpty(UBIG_Codigo3))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Codigo3.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_Codigo3MSGERROR
    {
      get
      {
        if (!UBIG_Codigo3OK)
        { return String.Format("Debe ingresar un {0}.", "codigo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_InternoPadre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_InternoPadreOK
    {
      get
      {
        if (!UBIG_InternoPadre.HasValue)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_InternoPadre.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_InternoPadreMSGERROR
    {
      get
      {
        if (!UBIG_InternoPadreOK)
        { return String.Format("Debe ingresar el {0}.", "interno del padre"); }
        else { return String.Empty; }
      }
    }

    #endregion

    #region [ VALIDACION ]

    private String m_mensajeError;

    [DataMember]
    public String MensajeError
    {
      get { return m_mensajeError; }
    }

    public Boolean Validar()
    {
      try
      {
        Boolean _isCorrect = true;
        m_mensajeError = "";
        if (!UBIG_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += UBIG_InternoMSGERROR + Environment.NewLine;
        }
        if (!PAIS_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_InternoMSGERROR + Environment.NewLine;
        }
        if (!UBIG_NombreOK)
        {
          _isCorrect = false;
          m_mensajeError += UBIG_NombreMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
