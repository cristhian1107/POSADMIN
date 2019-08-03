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
  public partial class Cierres
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
    /// Gets or sets el valor de validación de: CIER_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CIER_InternoOK
    {
      get
      {
        if (CIER_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: CIER_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String CIER_InternoMSGERROR
    {
      get
      {
        if (!CIER_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: CIER_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CIER_FechaHoraRegistroOK
    {
      get
      {
        if (CIER_FechaHoraRegistro == null)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: CIER_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public String CIER_FechaHoraRegistroMSGERROR
    {
      get
      {
        if (!CIER_FechaHoraRegistroOK)
        { return String.Format("Debe ingresar la {0}.", "fecha de registro"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: CIER_MontoReal.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CIER_MontoRealOK
    {
      get
      {
        if (CIER_MontoReal <= 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: CIER_MontoReal.
    /// </summary>
    [IgnoreDataMember]
    public String CIER_MontoRealMSGERROR
    {
      get
      {
        if (!CIER_MontoRealOK)
        { return String.Format("Debe ingresar el {0}.", "monto real"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: CIER_MontoDeuda.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CIER_MontoDeudaOK
    {
      get
      {
        if (CIER_MontoDeuda <= 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: CIER_MontoDeuda.
    /// </summary>
    [IgnoreDataMember]
    public String CIER_MontoDeudaMSGERROR
    {
      get
      {
        if (!CIER_MontoDeudaOK)
        { return String.Format("Debe ingresar el {0}.", "monto deuda"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: CIER_MontoExtra.
    /// </summary>
    [IgnoreDataMember]
    public Boolean CIER_MontoExtraOK
    {
      get
      {
        if (CIER_MontoExtra <= 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: CIER_MontoExtra.
    /// </summary>
    [IgnoreDataMember]
    public String CIER_MontoExtraMSGERROR
    {
      get
      {
        if (!CIER_MontoExtraOK)
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
        if (!CIER_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += CIER_InternoMSGERROR + Environment.NewLine;
        }
        if (!CIER_FechaHoraRegistroOK)
        {
          _isCorrect = false;
          m_mensajeError += CIER_FechaHoraRegistroMSGERROR + Environment.NewLine;
        }
        if (!CIER_MontoRealOK)
        {
          _isCorrect = false;
          m_mensajeError += CIER_MontoRealMSGERROR + Environment.NewLine;
        }
        if (!CIER_MontoDeudaOK)
        {
          _isCorrect = false;
          m_mensajeError += CIER_MontoDeudaMSGERROR + Environment.NewLine;
        }
        if (!CIER_MontoExtraOK)
        {
          _isCorrect = false;
          m_mensajeError += CIER_MontoExtraMSGERROR + Environment.NewLine;
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
