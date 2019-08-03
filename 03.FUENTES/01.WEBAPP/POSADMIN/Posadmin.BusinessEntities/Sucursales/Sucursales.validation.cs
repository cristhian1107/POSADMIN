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
  public partial class Sucursales
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
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: SUCU_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(SUCU_Nombre))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_NombreMSGERROR
    {
      get
      {
        if (!SUCU_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: SUCU_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_DireccionOK
    {
      get
      {
        if (String.IsNullOrEmpty(SUCU_Direccion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_DireccionMSGERROR
    {
      get
      {
        if (!SUCU_DireccionOK)
        { return String.Format("Debe ingresar una {0}.", "direccion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: SUCU_Correo.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_CorreoOK
    {
      get
      {
        if (String.IsNullOrEmpty(SUCU_Correo))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Correo.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_CorreoMSGERROR
    {
      get
      {
        if (!SUCU_CorreoOK)
        { return String.Format("Debe ingresar un {0}.", "correo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: SUCU_Telefono.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_TelefonoOK
    {
      get
      {
        if (String.IsNullOrEmpty(SUCU_Telefono))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Telefono.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_TelefonoMSGERROR
    {
      get
      {
        if (!SUCU_TelefonoOK)
        { return String.Format("Debe ingresar un {0}.", "telefono"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: SUCU_Web.
    /// </summary>
    [IgnoreDataMember]
    public Boolean SUCU_WebOK
    {
      get
      {
        if (String.IsNullOrEmpty(SUCU_Web))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: SUCU_Web.
    /// </summary>
    [IgnoreDataMember]
    public String SUCU_WebMSGERROR
    {
      get
      {
        if (!SUCU_WebOK)
        { return String.Format("Debe ingresar una {0}.", "pag. web"); }
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
        if (!SUCU_NombreOK)
        {
          _isCorrect = false;
          m_mensajeError += SUCU_NombreMSGERROR + Environment.NewLine;
        }
        if (!SUCU_DireccionOK)
        {
          _isCorrect = false;
          m_mensajeError += SUCU_DireccionMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
