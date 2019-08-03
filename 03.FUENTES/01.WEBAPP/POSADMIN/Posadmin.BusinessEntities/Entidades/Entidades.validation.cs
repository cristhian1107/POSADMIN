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
  public partial class Entidades
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
    /// Gets or sets el valor de validación de: ENTI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_InternoOK
    {
      get
      {
        if (ENTI_Interno <= 0 && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_InternoMSGERROR
    {
      get
      {
        if (!ENTI_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "interno"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_TablaTDI.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_TablaTDIOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_TablaTDI))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_TablaTDI.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_TablaTDIMSGERROR
    {
      get
      {
        if (!TABL_TablaTDIOK)
        { return String.Format("Debe ingresar un {0}.", "tipo de doc. de identidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_IntenoTDI.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoTDIOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_InternoTDI) || TABL_InternoTDI == "0")
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_IntenoTDI.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoTDIMSGERROR
    {
      get
      {
        if (!TABL_InternoTDIOK)
        { return String.Format("Debe ingresar un {0}.", "tipo de doc. de identidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_Id.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_IdOK
    {
      get
      {
        if (String.IsNullOrEmpty(ENTI_Id))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_Id.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_IdMSGERROR
    {
      get
      {
        if (!ENTI_IdOK)
        { return String.Format("Debe ingresar un {0}.", "id"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_NombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_NombreCompletoOK
    {
      get
      {
        if (String.IsNullOrEmpty(ENTI_NombreCompleto))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_NombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_NombreCompletoMSGERROR
    {
      get
      {
        if (!ENTI_NombreCompletoOK)
        { return String.Format("Debe ingresar un {0}.", "nombre completo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_DireccionOK
    {
      get
      {
        if (String.IsNullOrEmpty(ENTI_Direccion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_Direccion.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_DireccionMSGERROR
    {
      get
      {
        if (!ENTI_DireccionOK)
        { return String.Format("Debe ingresar una {0}.", "direccion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_Sexo.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_SexoOK
    {
      get
      {
        if (String.IsNullOrEmpty(ENTI_Sexo))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_Sexo.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_SexoMSGERROR
    {
      get
      {
        if (!ENTI_SexoOK)
        { return String.Format("Debe ingresar un {0}.", "sexo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: PAIS_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean PAIS_InternoOK
    {
      get
      {
        if (!PAIS_Interno.HasValue)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: PAIS_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String PAIS_InternoMSGERROR
    {
      get
      {
        if (!PAIS_InternoOK)
        { return String.Format("Debe ingresar un {0}.", "pais"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: UBIG_Interno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean UBIG_InternoOK
    {
      get
      {
        if (!UBIG_Interno.HasValue)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: UBIG_Interno.
    /// </summary>
    [IgnoreDataMember]
    public String UBIG_InternoMSGERROR
    {
      get
      {
        if (!UBIG_InternoOK)
        { return String.Format("Debe ingresar un {0}.", "ubigeo"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_FuncionesEntidadesSeleccionadas.
    /// </summary>
    [IgnoreDataMember]
    public Boolean ENTI_FuncionesEntidadesSeleccionadasOK
    {
      get
      {
        if (ENTI_FuncionesEntidadesSeleccionadas == null || ENTI_FuncionesEntidadesSeleccionadas.Count == 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_FuncionesEntidadesSeleccionadas.
    /// </summary>
    [IgnoreDataMember]
    public String ENTI_FuncionesEntidadesSeleccionadasMSGERROR
    {
      get
      {
        if (!ENTI_FuncionesEntidadesSeleccionadasOK)
        { return String.Format("Debe seleccionar por lo menos un tipo de entidad"); }
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
        if (!EMPR_InternoOK)
        {
          l_isCorrect = false;
          m_mensajeError += EMPR_InternoMSGERROR + Environment.NewLine;
        }
        if (!ENTI_InternoOK)
        {
          l_isCorrect = false;
          m_mensajeError += ENTI_InternoMSGERROR + Environment.NewLine;
        }
        if (!TABL_TablaTDIOK)
        {
          l_isCorrect = false;
          m_mensajeError += TABL_TablaTDIMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoTDIOK)
        {
          l_isCorrect = false;
          m_mensajeError += TABL_InternoTDIMSGERROR + Environment.NewLine;
        }
        if (!ENTI_IdOK)
        {
          l_isCorrect = false;
          m_mensajeError += ENTI_IdMSGERROR + Environment.NewLine;
        }
        if (!ENTI_NombreCompletoOK)
        {
          l_isCorrect = false;
          m_mensajeError += ENTI_NombreCompletoMSGERROR + Environment.NewLine;
        }
        if (!ENTI_FuncionesEntidadesSeleccionadasOK)
        {
          l_isCorrect = false;
          m_mensajeError += ENTI_FuncionesEntidadesSeleccionadasMSGERROR + Environment.NewLine;
        }

        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
