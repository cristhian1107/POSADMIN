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
  public partial class Paises
  {
    #region [ PROPIEDADES VALIDACIÓN ]

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_InternoOK
    {
      get
      {
        if (PAIS_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
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
    /// Gets or sets el valor de validación de: PAIS_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_Nombre))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_NombreMSGERROR
    {
      get
      {
        if (!PAIS_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_CodigoNumerico.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_CodigoNumericoOK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_CodigoNumerico))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_CodigoNumerico.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_CodigoNumericoMSGERROR
    {
      get
      {
        if (!PAIS_CodigoNumericoOK)
        { return String.Format("Debe ingresar un {0}.", "codigo numerico"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_CodigoAlfa2.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_CodigoAlfa2OK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_CodigoAlfa2))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_CodigoAlfa2.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_CodigoAlfa2MSGERROR
    {
      get
      {
        if (!PAIS_CodigoAlfa2OK)
        { return String.Format("Debe ingresar un {0}.", "codigo alfa2"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_CodigoAlfa3.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_CodigoAlfa3OK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_CodigoAlfa3))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_CodigoAlfa3.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_CodigoAlfa3MSGERROR
    {
      get
      {
        if (!PAIS_CodigoAlfa3OK)
        { return String.Format("Debe ingresar un {0}.", "codigo alfa3"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_Continente.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_ContinenteOK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_Continente))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_Continente.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_ContinenteMSGERROR
    {
      get
      {
        if (!PAIS_ContinenteOK)
        { return String.Format("Debe ingresar un {0}.", "continente"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(PAIS_Descripcion))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_DescripcionMSGERROR
    {
      get
      {
        if (!PAIS_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
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
        if (!PAIS_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_InternoMSGERROR + Environment.NewLine;
        }
        if (!PAIS_NombreOK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_NombreMSGERROR + Environment.NewLine;
        }
        if (!PAIS_CodigoNumericoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_CodigoNumericoMSGERROR + Environment.NewLine;
        }
        if (!PAIS_CodigoAlfa2OK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_CodigoAlfa2MSGERROR + Environment.NewLine;
        }
        if (!PAIS_CodigoAlfa3OK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_CodigoAlfa3MSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
