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
  public partial class DetallesPagosRegistros
  {
    #region [ Propiedades Validación ]

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
    /// Gets or sets el valor de validación de: REGI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_InternoOK
    {
      get
      {
        if (REGI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_InternoMSGERROR
    {
      get
      {
        if (!REGI_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno del registro"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PAGO_Item.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAGO_ItemOK
    {
      get
      {
        if (PAGO_Item <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PAGO_Item.
    /// </summary>
    [IgnoreDataMember]
    public String PAGO_ItemMSGERROR
    {
      get
      {
        if (!PAGO_ItemOK)
        { return String.Format("Debe ingresar el {0}.", "item"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PAGO_Tipo.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAGO_TipoOK
    {
      get
      {
        if (String.IsNullOrEmpty(PAGO_Tipo) || PAGO_Tipo == "0")
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PAGO_Tipo.
    /// </summary>
    [IgnoreDataMember]
    public String PAGO_TipoMSGERROR
    {
      get
      {
        if (!PAGO_TipoOK)
        { return String.Format("Debe ingresar un {0}.", "tipo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PAGO_MontoCancelado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAGO_MontoCanceladoOK
    {
      get
      {
        if (PAGO_MontoCancelado <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PAGO_MontoCancelado.
    /// </summary>
    [IgnoreDataMember]
    public String PAGO_MontoCanceladoMSGERROR
    {
      get
      {
        if (!PAGO_MontoCanceladoOK)
        { return String.Format("Debe ingresar un {0}.", "monto"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PAGO_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAGO_FechaHoraRegistroOK
    {
      get
      {
        if (PAGO_FechaHoraRegistro == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PAGO_FechaHoraRegistro.
    /// </summary>
    [IgnoreDataMember]
    public String PAGO_FechaHoraRegistroMSGERROR
    {
      get
      {
        if (!PAGO_FechaHoraRegistroOK)
        { return String.Format("Debe ingresar la {0}.", "fecha de registro"); }
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
        { return String.Format("Debe ingresar un {0}.", "usuario"); }
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
        if (RETI_Interno <= 0)
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
        { return String.Format("Debe ingresar un {0}.", "retiro"); }
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
        if (CIER_Interno <= 0)
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
        { return String.Format("Debe ingresar un {0}.", "cierre"); }
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
        if (!PAGO_TipoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAGO_TipoMSGERROR + Environment.NewLine;
        }
        if (!PAGO_FechaHoraRegistroOK)
        {
          _isCorrect = false;
          m_mensajeError += PAGO_FechaHoraRegistroMSGERROR + Environment.NewLine;
        }
        if (!PAGO_MontoCanceladoOK)
        {
          _isCorrect = false;
          m_mensajeError += PAGO_MontoCanceladoMSGERROR + Environment.NewLine;
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
