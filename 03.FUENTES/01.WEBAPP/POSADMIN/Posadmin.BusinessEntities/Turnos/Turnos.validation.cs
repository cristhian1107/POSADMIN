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
  public partial class Turnos
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
    /// Gets or sets el valor de validación de: TURN_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_InternoOK
    {
      get
      {
        if (TURN_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_InternoMSGERROR
    {
      get
      {
        if (!TURN_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TURN_Nominacion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_NominacionOK
    {
      get
      {
        if (String.IsNullOrEmpty(TURN_Nominacion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_Nominacion.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_NominacionMSGERROR
    {
      get
      {
        if (!TURN_NominacionOK)
        { return String.Format("Debe ingresar una {0}.", "nominacion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TURN_HoraInicio.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_HoraInicioOK
    {
      get
      {
        if (TURN_HoraInicio == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_HoraInicio.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_HoraInicioMSGERROR
    {
      get
      {
        if (!TURN_HoraInicioOK)
        { return String.Format("Debe ingresar una {0}.", "hora inicio"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TURN_HoraFin.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_HoraFinOK
    {
      get
      {
        if (TURN_HoraFin == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_HoraFin.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_HoraFinMSGERROR
    {
      get
      {
        if (!TURN_HoraFinOK)
        { return String.Format("Debe ingresar una {0}.", "hora fin"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TURN_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(TURN_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_DescripcionMSGERROR
    {
      get
      {
        if (!TURN_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TURN_Color.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TURN_ColorOK
    {
      get
      {
        if (String.IsNullOrEmpty(TURN_Color))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TURN_Color.
    /// </summary>
    [IgnoreDataMember]
    public String TURN_ColorMSGERROR
    {
      get
      {
        if (!TURN_ColorOK)
        { return String.Format("Debe ingresar un {0}.", "color"); }
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
        if (!TURN_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += TURN_InternoMSGERROR + Environment.NewLine;
        }
        if (!TURN_NominacionOK)
        {
          _isCorrect = false;
          m_mensajeError += TURN_NominacionMSGERROR + Environment.NewLine;
        }
        if (!TURN_HoraInicioOK)
        {
          _isCorrect = false;
          m_mensajeError += TURN_HoraInicioMSGERROR + Environment.NewLine;
        }
        if (!TURN_HoraFinOK)
        {
          _isCorrect = false;
          m_mensajeError += TURN_HoraFinMSGERROR + Environment.NewLine;
        }
        if (!TURN_ColorOK)
        {
          _isCorrect = false;
          m_mensajeError += TURN_ColorMSGERROR + Environment.NewLine;
        }

        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
