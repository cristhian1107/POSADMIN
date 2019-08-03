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
  public partial class FuncionesEntidades
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
    /// Gets or sets el valor de validación de: TABL_TablaTEN.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_TablaTENOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_TablaTEN) && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_TablaTEN.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_TablaTENMSGERROR
    {
      get
      {
        if (!TABL_TablaTENOK)
        { return String.Format("Debe ingresar el {0}.", "tipo de doc. de identidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_InternoTEN.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoTENOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_InternoTEN) && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_IntenoTEN.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoTENMSGERROR
    {
      get
      {
        if (!TABL_InternoTENOK)
        { return String.Format("Debe ingresar el {0}.", "tipo de doc. de identidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_InternoOK
    {
      get
      {
        if (ENTI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_InternoMSGERROR
    {
      get
      {
        if (!ENTI_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno de la entidad"); }
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
        if (!TABL_TablaTENOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_TablaTENMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoTENOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_InternoTENMSGERROR + Environment.NewLine;
        }
        if (!ENTI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += ENTI_InternoMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
