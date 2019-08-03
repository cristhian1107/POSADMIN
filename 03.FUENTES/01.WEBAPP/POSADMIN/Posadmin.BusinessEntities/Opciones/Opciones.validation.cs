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
  public partial class Opciones
  {
    #region [ PROPIEDADES VALIDACIÓN ]

    /// <summary>
    /// Gets or sets el valor de validación de: OPCI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean OPCI_InternoOK
    {
      get
      {
        if (OPCI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: OPCI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String OPCI_InternoMSGERROR
    {
      get
      {
        if (!OPCI_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno de la opcion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: OPCI_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean OPCI_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(OPCI_Nombre))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: OPCI_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String OPCI_NombreMSGERROR
    {
      get
      {
        if (!OPCI_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: OPCI_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public Boolean OPCI_NomenclaturaOK
    {
      get
      {
        if (String.IsNullOrEmpty(OPCI_Nomenclatura))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: OPCI_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public String OPCI_NomenclaturaMSGERROR
    {
      get
      {
        if (!OPCI_NomenclaturaOK)
        { return String.Format("Debe ingresar un {0}.", "nomenclatura"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: OPCI_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean OPCI_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(OPCI_Descripcion))
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: OPCI_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String OPCI_DescripcionMSGERROR
    {
      get
      {
        if (!OPCI_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: OPCI_InternoPadre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean OPCI_InternoPadreOK
    {
      get
      {
        if (!OPCI_InternoPadre.HasValue)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: OPCI_InternoPadre.
    /// </summary>
    [IgnoreDataMember]
    public String OPCI_InternoPadreMSGERROR
    {
      get
      {
        if (!OPCI_InternoPadreOK)
        { return String.Format("Debe ingresar un {0}.", "padre"); }
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
        if (!OPCI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += OPCI_InternoMSGERROR + Environment.NewLine;
        }
        if (!OPCI_NombreOK)
        {
          _isCorrect = false;
          m_mensajeError += OPCI_NombreMSGERROR + Environment.NewLine;
        }
        if (!OPCI_NomenclaturaOK)
        {
          _isCorrect = false;
          m_mensajeError += OPCI_NomenclaturaMSGERROR + Environment.NewLine;
        }
        if (!OPCI_DescripcionOK)
        {
          _isCorrect = false;
          m_mensajeError += OPCI_DescripcionMSGERROR + Environment.NewLine;
        }
        if (!OPCI_InternoPadreOK)
        {
          _isCorrect = false;
          m_mensajeError += OPCI_InternoPadreMSGERROR + Environment.NewLine;
        }
        
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
