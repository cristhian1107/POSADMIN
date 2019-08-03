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
  public partial class Configuraciones
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
    /// Gets or sets el valor de validación de: USUA_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_InternoOK
    {
      get
      {
        if (USUA_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_InternoMSGERROR
    {
      get
      {
        if (!USUA_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno del usuario"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: CONF_Llave.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CONF_LlaveOK
    {
      get
      {
        if (String.IsNullOrEmpty(CONF_Llave) && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: CONF_Llave.
    /// </summary>
    [IgnoreDataMember]
    public String CONF_LlaveMSGERROR
    {
      get
      {
        if (!CONF_LlaveOK)
        { return String.Format("Debe ingresar una {0}.", "llave"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: CONF_Valor.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CONF_ValorOK
    {
      get
      {
        if (String.IsNullOrEmpty(CONF_Valor))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: CONF_Valor.
    /// </summary>
    [IgnoreDataMember]
    public String CONF_ValorMSGERROR
    {
      get
      {
        if (!CONF_ValorOK)
        { return String.Format("Debe ingresar un {0}.", "valor"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: CONF_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CONF_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(CONF_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: CONF_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String CONF_DescripcionMSGERROR
    {
      get
      {
        if (!CONF_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }

    #endregion

    #region [ Validacion ]

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
        if (!USUA_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_InternoMSGERROR + Environment.NewLine;
        }
        if (!CONF_LlaveOK)
        {
          _isCorrect = false;
          m_mensajeError += CONF_LlaveMSGERROR + Environment.NewLine;
        }
        if (!CONF_ValorOK)
        {
          _isCorrect = false;
          m_mensajeError += CONF_ValorMSGERROR + Environment.NewLine;
        }
        if (!CONF_DescripcionOK)
        {
          _isCorrect = false;
          m_mensajeError += CONF_DescripcionMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
