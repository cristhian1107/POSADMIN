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
  public partial class Parametros
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
    /// Gets or sets el valor de validación de: PARA_Llave.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PARA_LlaveOK
    {
      get
      {
        if (String.IsNullOrEmpty(PARA_Llave) && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PARA_Llave.
    /// </summary>
    [IgnoreDataMember]
    public String PARA_LlaveMSGERROR
    {
      get
      {
        if (!PARA_LlaveOK)
        { return String.Format("Debe ingresar una {0}.", "llave"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PARA_Valor.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PARA_ValorOK
    {
      get
      {
        if (String.IsNullOrEmpty(PARA_Valor))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PARA_Valor.
    /// </summary>
    [IgnoreDataMember]
    public String PARA_ValorMSGERROR
    {
      get
      {
        if (!PARA_ValorOK)
        { return String.Format("Debe ingresar una {0}.", "valor"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PARA_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PARA_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(PARA_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PARA_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String PARA_DescripcionMSGERROR
    {
      get
      {
        if (!PARA_DescripcionOK)
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
        if (!PARA_LlaveOK)
        {
          _isCorrect = false;
          m_mensajeError += PARA_LlaveMSGERROR + Environment.NewLine;
        }
        if (!PARA_ValorOK)
        {
          _isCorrect = false;
          m_mensajeError += PARA_ValorMSGERROR + Environment.NewLine;
        }
        if (!PARA_DescripcionOK)
        {
          _isCorrect = false;
          m_mensajeError += PARA_DescripcionMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
