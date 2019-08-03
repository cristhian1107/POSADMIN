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
  public partial class Empresas
  {
    #region [ PROPIEDADES VALIDACIÓN ]

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_InternoOK
    {
      get
      {
        if (EMPR_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_InternoMSGERROR
    {
      get
      {
        if (!EMPR_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno de la empresa"); }
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
        { return String.Format("Debe seleccionar un {0}.", "pais"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Id.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_IdOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_Id))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Id.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_IdMSGERROR
    {
      get
      {
        if (!EMPR_IdOK)
        { return String.Format("Debe ingresar un {0}.", "id"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_RazonSocial.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_RazonSocialOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_RazonSocial))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_RazonSocial.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_RazonSocialMSGERROR
    {
      get
      {
        if (!EMPR_RazonSocialOK)
        { return String.Format("Debe ingresar una {0}.", "razon social"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_DireccionOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_Direccion))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_DireccionMSGERROR
    {
      get
      {
        if (!EMPR_DireccionOK)
        { return String.Format("Debe ingresar una {0}.", "direccion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_NombreComercial.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_NombreComercialOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_NombreComercial))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_NombreComercial.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_NombreComercialMSGERROR
    {
      get
      {
        if (!EMPR_NombreComercialOK)
        { return String.Format("Debe ingresar un {0}.", "nombre comercial"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_InternoOK
    {
      get
      {
        if (!UBIG_Interno.HasValue)
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
        { return String.Format("Debe seleccionar un {0}.", "ubigeo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Correos.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_CorreosOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_Correos))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Correos.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_CorreosMSGERROR
    {
      get
      {
        if (!EMPR_CorreosOK)
        { return String.Format("Debe ingresar un {0}.", "correo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Telefonos.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_TelefonosOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_Telefonos))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Telefonos.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_TelefonosMSGERROR
    {
      get
      {
        if (!EMPR_TelefonosOK)
        { return String.Format("Debe ingresar un {0}.", "telefono"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: EMPR_Web.
    /// </summary>
    [IgnoreDataMember]
    public Boolean EMPR_WebOK
    {
      get
      {
        if (String.IsNullOrEmpty(EMPR_Web))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: EMPR_Web.
    /// </summary>
    [IgnoreDataMember]
    public String EMPR_WebMSGERROR
    {
      get
      {
        if (!EMPR_WebOK)
        { return String.Format("Debe ingresar una {0}.", "web"); }
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
        if (!EMPR_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += EMPR_InternoMSGERROR + Environment.NewLine;
        }
        if (!PAIS_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAIS_InternoMSGERROR + Environment.NewLine;
        }
        if (!EMPR_IdOK)
        {
          _isCorrect = false;
          m_mensajeError += EMPR_IdMSGERROR + Environment.NewLine;
        }
        if (!EMPR_RazonSocialOK)
        {
          _isCorrect = false;
          m_mensajeError += EMPR_RazonSocialMSGERROR + Environment.NewLine;
        }
        if (!EMPR_DireccionOK)
        {
          _isCorrect = false;
          m_mensajeError += EMPR_DireccionMSGERROR + Environment.NewLine;
        }
       
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
