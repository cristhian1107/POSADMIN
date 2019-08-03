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
  public partial class Habitaciones
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
    /// Gets or sets el valor de validación de: HABI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_InternoOK
    {
      get
      {
        if (HABI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
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
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: TABL_TablaPIS.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_TablaPISOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_TablaPIS))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_TablaPIS.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_TablaPISMSGERROR
    {
      get
      {
        if (!TABL_TablaPISOK)
        { return String.Format("Debe ingresar un {0}.", "piso"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: TABL_InternoPIS.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoPISOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_InternoPIS) || TABL_InternoPIS == "0")
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_IntenoPIS.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoPISMSGERROR
    {
      get
      {
        if (!TABL_InternoPISOK)
        { return String.Format("Debe ingresar un {0}.", "piso"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: TABL_TablaTHA.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_TablaTHAOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_TablaTHA))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_TablaTHA.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_TablaTHAMSGERROR
    {
      get
      {
        if (!TABL_TablaTHAOK)
        { return String.Format("Debe ingresar un {0}.", "tipo de habitacion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: TABL_InternoTHA.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoTHAOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_InternoTHA) || TABL_InternoTHA == "0")
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_InternoTHA.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoTHAMSGERROR
    {
      get
      {
        if (!TABL_InternoTHAOK)
        { return String.Format("Debe ingresar un {0}.", "tipo de habitacion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_Numero.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_NumeroOK
    {
      get
      {
        if (String.IsNullOrEmpty(HABI_Numero))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_Numero.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_NumeroMSGERROR
    {
      get
      {
        if (!HABI_NumeroOK)
        { return String.Format("Debe ingresar un {0}.", "numero"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_Estado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_EstadoOK
    {
      get
      {
        if (String.IsNullOrEmpty(HABI_Estado))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_Estado.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_EstadoMSGERROR
    {
      get
      {
        if (!HABI_EstadoOK)
        { return String.Format("Debe ingresar un {0}.", "estado"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(HABI_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_DescripcionMSGERROR
    {
      get
      {
        if (!HABI_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_PrecioDia.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_PrecioDiaOK
    {
      get
      {
        if (HABI_PrecioDia <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_PrecioDia.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_PrecioDiaMSGERROR
    {
      get
      {
        if (!HABI_PrecioDiaOK)
        { return String.Format("Debe ingresar el {0}.", "precio por dia"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_PrecioHora.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_PrecioHoraOK
    {
      get
      {
        if (HABI_PrecioHora <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_PrecioHora.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_PrecioHoraMSGERROR
    {
      get
      {
        if (!HABI_PrecioHoraOK)
        { return String.Format("Debe ingresar el {0}.", "precio por hora"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: HABI_PrecioMinimo.
    /// </summary>
    [IgnoreDataMember]
    public Boolean HABI_PrecioMinimoOK
    {
      get
      {
        if (HABI_PrecioMinimo <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: HABI_PrecioMinimo.
    /// </summary>
    [IgnoreDataMember]
    public String HABI_PrecioMinimoMSGERROR
    {
      get
      {
        if (!HABI_PrecioMinimoOK)
        { return String.Format("Debe ingresar el {0}.", "precio minimo"); }
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
        if (!HABI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_InternoMSGERROR + Environment.NewLine;
        }
        if (!TABL_TablaPISOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_TablaPISMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoPISOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_InternoPISMSGERROR + Environment.NewLine;
        }
        if (!TABL_TablaTHAOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_TablaTHAMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoTHAOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_InternoTHAMSGERROR + Environment.NewLine;
        }
        if (!HABI_NumeroOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_NumeroMSGERROR + Environment.NewLine;
        }
        if (!HABI_PrecioDiaOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_PrecioDiaMSGERROR + Environment.NewLine;
        }
        if (!HABI_PrecioHoraOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_PrecioHoraMSGERROR + Environment.NewLine;
        }
        if (!HABI_PrecioMinimoOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_PrecioMinimoMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
