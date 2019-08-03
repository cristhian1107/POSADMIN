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
  public partial class Roles
  {
    #region [ PROPIEDADES VALIDACIÓN ]

    /// <summary>
    /// Gets or sets el valor de validación de: ROLE_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ROLE_InternoOK
    {
      get
      {
        if (ROLE_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
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
        { return String.Format("Debe ingresar el {0}.", "interno del rol"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ROLE_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ROLE_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(ROLE_Nombre))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ROLE_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String ROLE_NombreMSGERROR
    {
      get
      {
        if (!ROLE_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ROLE_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ROLE_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(ROLE_Descripcion))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ROLE_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String ROLE_DescripcionMSGERROR
    {
      get
      {
        if (!ROLE_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ROLE_Opciones.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ROLE_OpcionesSeleccionadasOK
    {
      get
      {
        if (ROLE_OpcionesSeleccionadas == null || ROLE_OpcionesSeleccionadas.Count == 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ROLE_Opciones.
    /// </summary>
    [IgnoreDataMember]
    public String ROLE_OpcionesSeleccionadasMSGERROR
    {
      get
      {
        if (!ROLE_OpcionesSeleccionadasOK)
        { return String.Format("Debe seleccionar por lo menos una opcion"); }
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
        Boolean l_isCorrect = true;
        m_mensajeError = "";
        if (!ROLE_InternoOK)
        {
          l_isCorrect = false;
          m_mensajeError += ROLE_InternoMSGERROR + Environment.NewLine;
        }
        if (!ROLE_NombreOK)
        {
          l_isCorrect = false;
          m_mensajeError += ROLE_NombreMSGERROR + Environment.NewLine;
        }
        if (!ROLE_OpcionesSeleccionadasOK)
        {
          l_isCorrect = false;
          m_mensajeError += ROLE_OpcionesSeleccionadasMSGERROR + Environment.NewLine;
        }

        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
