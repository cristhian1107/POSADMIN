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
  public partial class Tablas
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
        if (EMPR_Interno <= 0)
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
    /// Gets or sets el valor de validación de: TABL_Tabla.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_TablaOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Tabla) || TABL_Tabla == "0")
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Tabla.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_TablaMSGERROR
    {
      get
      {
        if (!TABL_TablaOK)
        { return String.Format("Debe seleccionar una {0}.", "tabla"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Inteno.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Interno) && (Instance == InstanceEntity.Modified || Instance == InstanceEntity.Deleted))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Inteno.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoMSGERROR
    {
      get
      {
        if (!TABL_InternoOK)
        { return String.Format("Debe ingresar el {0}.", "inteno"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_NombreOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Nombre))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Nombre.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_NombreMSGERROR
    {
      get
      {
        if (!TABL_NombreOK)
        { return String.Format("Debe ingresar un {0}.", "nombre"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_NomenclaturaOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Nomenclatura))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Nomenclatura.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_NomenclaturaMSGERROR
    {
      get
      {
        if (!TABL_NomenclaturaOK)
        { return String.Format("Debe ingresar una {0}.", "nomenclatura"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_DescripcionOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Descripcion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Descripcion.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_DescripcionMSGERROR
    {
      get
      {
        if (!TABL_DescripcionOK)
        { return String.Format("Debe ingresar una {0}.", "descripcion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_CodigoInternacional.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_CodigoInternacionalOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_CodigoInternacional))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_CodigoInternacional.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_CodigoInternacionalMSGERROR
    {
      get
      {
        if (!TABL_CodigoInternacionalOK)
        { return String.Format("Debe ingresar un {0}.", "codigo internacional"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Codigo1.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Codigo1OK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Codigo1))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Codigo1.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Codigo1MSGERROR
    {
      get
      {
        if (!TABL_Codigo1OK)
        { return String.Format("Debe ingresar un {0}.", "codigo 1"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Codigo2.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Codigo2OK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Codigo2))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Codigo2.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Codigo2MSGERROR
    {
      get
      {
        if (!TABL_Codigo2OK)
        { return String.Format("Debe ingresar un {0}.", "codigo 2"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Codigo3.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Codigo3OK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_Codigo3))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Codigo3.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Codigo3MSGERROR
    {
      get
      {
        if (!TABL_Codigo3OK)
        { return String.Format("Debe ingresar un {0}.", "codigo 3"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Numero1.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Numero1OK
    {
      get
      {
        if (!TABL_Numero1.Equals(0))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Numero1.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Numero1MSGERROR
    {
      get
      {
        if (!TABL_Numero1OK)
        { return String.Format("Debe ingresar un {0}.", "numero 1"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Numero2.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Numero2OK
    {
      get
      {
        if (!TABL_Numero2.Equals(0))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Numero2.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Numero2MSGERROR
    {
      get
      {
        if (!TABL_Numero2OK)
        { return String.Format("Debe ingresar un {0}.", "numero 2"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_Numero3.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_Numero3OK
    {
      get
      {
        if (!TABL_Numero3.Equals(0))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_Numero3.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_Numero3MSGERROR
    {
      get
      {
        if (!TABL_Numero3OK)
        { return String.Format("Debe ingresar un {0}.", "numero 3"); }
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
        if (!TABL_TablaOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_TablaMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_InternoMSGERROR + Environment.NewLine;
        }
        if (!TABL_NombreOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_NombreMSGERROR + Environment.NewLine;
        }
        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
