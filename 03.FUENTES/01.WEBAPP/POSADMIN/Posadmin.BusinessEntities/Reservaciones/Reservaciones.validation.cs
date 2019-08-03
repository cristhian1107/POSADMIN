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
  public partial class Reservaciones
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
    /// Gets or sets el valor de validación de: RESE_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_InternoOK
    {
      get
      {
        if (RESE_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_InternoMSGERROR
    {
      get
      {
        if (!RESE_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_Estado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_EstadoOK
    {
      get
      {
        if (String.IsNullOrEmpty(RESE_Estado))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_Estado.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_EstadoMSGERROR
    {
      get
      {
        if (!RESE_EstadoOK)
        { return String.Format("Debe ingresar un {0}.", "estado"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: HABI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_InternoOK
    {
      get
      {
        if (HABI_Interno <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_InternoMSGERROR
    {
      get
      {
        if (!HABI_InternoOK)
        { return String.Format("Debe ingresar una {0}.", "habitacion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_FechaInicio.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_FechaInicioOK
    {
      get
      {
        if (RESE_FechaInicio.Date < MyUtilities.GetDateTimeCountry("es-PE").Date)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_FechaInicio.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_FechaInicioMSGERROR
    {
      get
      {
        if (!RESE_FechaInicioOK)
        { return String.Format("La fecha inicio debe ser mayor a la fecha actual"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_FechaFin.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_FechaFinOK
    {
      get
      {
        if ((RESE_FechaFin.Date < MyUtilities.GetDateTimeCountry("es-PE").Date) || (RESE_FechaFin.Date < RESE_FechaInicio.Date))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_FechaFin.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_FechaFinMSGERROR
    {
      get
      {
        if (!RESE_FechaFinOK)
        { return String.Format("La fecha fin debe ser mayor a la fecha inicio"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_FechaHoraRegistroOK
    {
      get
      {
        if (RESE_FechaHoraRegistro == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_FechaHoraRegistroMSGERROR
    {
      get
      {
        if (!RESE_FechaHoraRegistroOK)
        { return String.Format("Debe ingresar una {0}.", "fecha de registro"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_HuespedId.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_HuespedIdOK
    {
      get
      {
        if (String.IsNullOrEmpty(RESE_HuespedId))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_HuespedId.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_HuespedIdMSGERROR
    {
      get
      {
        if (!RESE_HuespedIdOK)
        { return String.Format("Debe ingresar un {0}.", "id"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_HuespedNombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_HuespedNombreCompletoOK
    {
      get
      {
        if (String.IsNullOrEmpty(RESE_HuespedNombreCompleto))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_HuespedNombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_HuespedNombreCompletoMSGERROR
    {
      get
      {
        if (!RESE_HuespedNombreCompletoOK)
        { return String.Format("Debe ingresar un {0}.", "nombre completo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_HuespedDireccion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_HuespedDireccionOK
    {
      get
      {
        if (String.IsNullOrEmpty(RESE_HuespedDireccion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_HuespedDireccion.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_HuespedDireccionMSGERROR
    {
      get
      {
        if (!RESE_HuespedDireccionOK)
        { return String.Format("Debe ingresar una {0}.", "direccion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: RESE_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean RESE_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(RESE_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: RESE_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String RESE_DescripcionMSGERROR
    {
      get
      {
        if (!RESE_DescripcionOK)
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
        if (!RESE_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_InternoMSGERROR + Environment.NewLine;
        }
        if (!RESE_EstadoOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_EstadoMSGERROR + Environment.NewLine;
        }
        if (!HABI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_InternoMSGERROR + Environment.NewLine;
        }
        if (!RESE_FechaInicioOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_FechaInicioMSGERROR + Environment.NewLine;
        }
        if (!RESE_FechaFinOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_FechaFinMSGERROR + Environment.NewLine;
        }
        if (!RESE_HuespedIdOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_HuespedIdMSGERROR + Environment.NewLine;
        }
        if (!RESE_HuespedNombreCompletoOK)
        {
          _isCorrect = false;
          m_mensajeError += RESE_HuespedNombreCompletoMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
