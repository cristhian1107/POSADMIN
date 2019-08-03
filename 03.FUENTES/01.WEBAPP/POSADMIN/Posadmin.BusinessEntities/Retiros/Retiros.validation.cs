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
using SoftwareFactory.Infrastructure.Utilities;

namespace Posadmin.BusinessEntities
{
  public partial class Retiros
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
    /// Gets or sets el valor de validación de: SUCU_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_InternoOK
    {
      get
      {
        if (SUCU_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_InternoMSGERROR
    {
      get
      {
        if (!SUCU_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno de la sucursal"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: RETI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RETI_InternoOK
    {
      get
      {
        if (RETI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RETI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String RETI_InternoMSGERROR
    {
      get
      {
        if (!RETI_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: RETI_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RETI_FechaHoraRegistroOK
    {
      get
      {
        if (RETI_FechaHoraRegistro == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RETI_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public String RETI_FechaHoraRegistroMSGERROR
    {
      get
      {
        if (!RETI_FechaHoraRegistroOK)
        { return String.Format("Debe ingresar la {0}.", "fecha de registro"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: RETI_MontoEntregado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RETI_MontoEntregadoOK
    {
      get
      {
        if (RETI_MontoEntregado <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RETI_MontoEntregado.
    /// </summary>
    [IgnoreDataMember]
    public String RETI_MontoEntregadoMSGERROR
    {
      get
      {
        if (!RETI_MontoEntregadoOK)
        { return String.Format("Debe ingresar el {0}.", "monto entregado"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: RETI_MontoExtra.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RETI_MontoExtraOK
    {
      get
      {
        if (RETI_MontoExtra <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RETI_MontoExtra.
    /// </summary>
    [IgnoreDataMember]
    public String RETI_MontoExtraMSGERROR
    {
      get
      {
        if (!RETI_MontoExtraOK)
        { return String.Format("Debe ingresar el {0}.", "monto extra"); }
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
        if (USUA_Interno <= 0)
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
        { return String.Format("Debe ingresar el {0}.", "usuario"); }
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
        if (!SUCU_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += SUCU_InternoMSGERROR + Environment.NewLine;
        }
        if (!RETI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += RETI_InternoMSGERROR + Environment.NewLine;
        }
        if (!RETI_FechaHoraRegistroOK)
        {
          _isCorrect = false;
          m_mensajeError += RETI_FechaHoraRegistroMSGERROR + Environment.NewLine;
        }
        if (!RETI_MontoEntregadoOK)
        {
          _isCorrect = false;
          m_mensajeError += RETI_MontoEntregadoMSGERROR + Environment.NewLine;
        }
        if (!RETI_MontoExtraOK)
        {
          _isCorrect = false;
          m_mensajeError += RETI_MontoExtraMSGERROR + Environment.NewLine;
        }
        if (!USUA_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_InternoMSGERROR + Environment.NewLine;
        }

        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
