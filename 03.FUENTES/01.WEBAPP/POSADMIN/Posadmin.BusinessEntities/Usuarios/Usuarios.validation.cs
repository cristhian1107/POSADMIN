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
  public partial class Usuarios
  {
    #region [ PROPIEDADES VALIDACIÓN ]

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
        { return String.Format("Debe ingresar el {0}.", "Interno del Usuario"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ROLE_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ROLE_InternoOK
    {
      get
      {
        if (ROLE_Interno <= 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ROLE_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String ROLE_InternoMSGERROR
    {
      get
      {
        if (!ROLE_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "Interno del Rol"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: USUA_NombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_NombreCompletoOK
    {
      get
      {
        if (String.IsNullOrEmpty(USUA_NombreCompleto))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_NombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_NombreCompletoMSGERROR
    {
      get
      {
        if (!USUA_NombreCompletoOK)
        { return String.Format("Debe ingresar el {0}.", "Nombre Completo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: USUA_Usuario.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_UsuarioOK
    {
      get
      {
        if (String.IsNullOrEmpty(USUA_Usuario))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_Usuario.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_UsuarioMSGERROR
    {
      get
      {
        if (!USUA_UsuarioOK)
        { return String.Format("Debe ingresar el {0}.", "Usuario"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: USUA_Contrasena.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_ContrasenaOK
    {
      get
      {
        if (String.IsNullOrEmpty(USUA_Contrasena))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_Contrasena.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_ContrasenaMSGERROR
    {
      get
      {
        if (!USUA_ContrasenaOK)
        { return String.Format("Debe ingresar la {0}.", "Contraseña"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: USUA_Correo.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_CorreoOK
    {
      get
      {
        if (String.IsNullOrEmpty(USUA_Correo))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_Correo.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_CorreoMSGERROR
    {
      get
      {
        if (!USUA_CorreoOK)
        { return String.Format("Debe ingresar el {0}.", "Correo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: USUA_Empresas.
    /// </summary>
    [IgnoreDataMember]
    public Boolean USUA_EmpresasSeleccionadasOK
    {
      get
      {
        if (USUA_EmpresasSeleccionadas == null || USUA_EmpresasSeleccionadas.Count == 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: USUA_Empresas.
    /// </summary>
    [IgnoreDataMember]
    public String USUA_EmpresasSeleccionadasMSGERROR
    {
      get
      {
        if (!USUA_EmpresasSeleccionadasOK)
        { return String.Format("Debe seleccionar por lo menos una empresa"); }
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

    public Boolean Validar(Boolean ModoEditar)
    {
      try
      {
        Boolean _isCorrect = true;
        m_mensajeError = "";
        if (!USUA_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_InternoMSGERROR + Environment.NewLine;
        }
        if (!ROLE_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += ROLE_InternoMSGERROR + Environment.NewLine;
        }
        if (!USUA_NombreCompletoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_NombreCompletoMSGERROR + Environment.NewLine;
        }
        if (!USUA_UsuarioOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_UsuarioMSGERROR + Environment.NewLine;
        }
        if (!USUA_ContrasenaOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_ContrasenaMSGERROR + Environment.NewLine;
        }
        if (!USUA_CorreoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_CorreoMSGERROR + Environment.NewLine;
        }
        if (ModoEditar)
        {
          if (!USUA_EmpresasSeleccionadasOK)
          {
            _isCorrect = false;
            m_mensajeError += USUA_EmpresasSeleccionadasMSGERROR + Environment.NewLine;
          }
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
