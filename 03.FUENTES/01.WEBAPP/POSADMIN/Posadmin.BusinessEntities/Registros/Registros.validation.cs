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
  public partial class Registros
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
        { return String.Format("Debe ingresar el {0}.", "interno"); }
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
    /// Gets or sets el valor de validación de: REGI_Estado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_EstadoOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_Estado))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_Estado.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_EstadoMSGERROR
    {
      get
      {
        if (!REGI_EstadoOK)
        { return String.Format("Debe ingresar un {0}.", "estado"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_Tramos.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_TramosOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_Tramos))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_Tramos.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_TramosMSGERROR
    {
      get
      {
        if (!REGI_TramosOK)
        { return String.Format("Debe ingresar un {0}.", "tramo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_Cantidad.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_CantidadOK
    {
      get
      {
        if (REGI_Cantidad <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_Cantidad.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_CantidadMSGERROR
    {
      get
      {
        if (!REGI_CantidadOK)
        { return String.Format("Debe ingresar una {0}.", "cantidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_FechaHoraEntrada.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_FechaHoraEntradaOK
    {
      get
      {
        if (REGI_FechaHoraEntrada == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_FechaHoraEntrada.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_FechaHoraEntradaMSGERROR
    {
      get
      {
        if (!REGI_FechaHoraEntradaOK)
        { return String.Format("Debe ingresar una {0}.", "fecha de entrada"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_FechaHoraSalida.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_FechaHoraSalidaOK
    {
      get
      {
        if (REGI_FechaHoraSalida == null)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_FechaHoraSalida.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_FechaHoraSalidaMSGERROR
    {
      get
      {
        if (!REGI_FechaHoraSalidaOK)
        { return String.Format("Debe ingresar una {0}.", "fecha de salida"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_FechaHoraSalidaReal.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_FechaHoraSalidaRealOK
    {
      get
      {
        if (!REGI_FechaHoraSalidaReal.HasValue)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_FechaHoraSalidaReal.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_FechaHoraSalidaRealMSGERROR
    {
      get
      {
        if (!REGI_FechaHoraSalidaRealOK)
        { return String.Format("Debe ingresar una {0}.", "fecha de salida real"); }
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
        { return String.Format("Debe ingresar un {0}.", "tipo doc. identidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: TABL_InternoTDI.
    /// </summary>
    [IgnoreDataMember]
    public Boolean TABL_InternoTDIOK
    {
      get
      {
        if (String.IsNullOrEmpty(TABL_InternoTDI))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: TABL_InternoTDI.
    /// </summary>
    [IgnoreDataMember]
    public String TABL_InternoTDIMSGERROR
    {
      get
      {
        if (!TABL_InternoTDIOK)
        { return String.Format("Debe ingresar un {0}.", "tipo doc. identidad"); }
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
        if (ENTI_Interno <= 0)
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
        { return String.Format("Debe ingresar una {0}.", "entidad"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_HuespedId.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_HuespedIdOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_HuespedId))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_HuespedId.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_HuespedIdMSGERROR
    {
      get
      {
        if (!REGI_HuespedIdOK)
        { return String.Format("Debe ingresar un {0}.", "id"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_HuespedNombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_HuespedNombreCompletoOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_HuespedNombreCompleto))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_HuespedNombreCompleto.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_HuespedNombreCompletoMSGERROR
    {
      get
      {
        if (!REGI_HuespedNombreCompletoOK)
        { return String.Format("Debe ingresar el {0}.", "nombre completo"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_HuespedDireccion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_HuespedDireccionOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_HuespedDireccion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_HuespedDireccion.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_HuespedDireccionMSGERROR
    {
      get
      {
        if (!REGI_HuespedDireccionOK)
        { return String.Format("Debe ingresar una {0}.", "direccion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_PrecioSugerido.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_PrecioSugeridoOK
    {
      get
      {
        if (REGI_PrecioSugerido <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_PrecioSugerido.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_PrecioSugeridoMSGERROR
    {
      get
      {
        if (!REGI_PrecioSugeridoOK)
        { return String.Format("Debe ingresar un {0}.", "precio sugerido"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_PrecioCobrado.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_PrecioCobradoOK
    {
      get
      {
        if (REGI_PrecioCobrado <= 0)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_PrecioCobrado.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_PrecioCobradoMSGERROR
    {
      get
      {
        if (!REGI_PrecioCobradoOK)
        { return String.Format("Debe ingresar el {0}.", "precio cobrado"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_MotivoAnulacion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_MotivoAnulacionOK
    {
      get
      {
        if (String.IsNullOrEmpty(REGI_MotivoAnulacion))
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_MotivoAnulacion.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_MotivoAnulacionMSGERROR
    {
      get
      {
        if (!REGI_MotivoAnulacionOK)
        { return String.Format("Debe ingresar el {0}.", "motivo de anulacion"); }
        else { return String.Empty; }
      }
    }
    /// <summary>
    /// Gets or sets el valor de validación de: REGI_FechaHoraAnulacion.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_FechaHoraAnulacionOK
    {
      get
      {
        if (!REGI_FechaHoraAnulacion.HasValue)
        { return false; }
        else { return true; }
      }
    }
    /// <summary>
    /// Gets or sets el mensaje de validación de: REGI_FechaHoraAnulacion.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_FechaHoraAnulacionMSGERROR
    {
      get
      {
        if (!REGI_FechaHoraAnulacionOK)
        { return String.Format("Debe ingresar la {0}.", "fecha de anulacion"); }
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
        if (TURN_Interno <= 0)
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
        { return String.Format("Debe ingresar el {0}.", "turno"); }
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
        { return String.Format("Debe ingresar el {0}.", "usuario"); }
        else { return String.Empty; }
      }
    }

    /// <summary>
    /// Gets or sets el valor de validación de: ENTI_FuncionesEntidadesSeleccionadas.
    /// </summary>
    [IgnoreDataMember]
    public Boolean REGI_ListaPagosOK
    {
      get
      {
        if (REGI_ListaPagos == null || REGI_ListaPagos.Count == 0)
        { return false; }
        else { return true; }
      }
    }

    /// <summary>
    /// Gets or sets el mensaje de validación de: ENTI_FuncionesEntidadesSeleccionadas.
    /// </summary>
    [IgnoreDataMember]
    public String REGI_ListaPagosMSGERROR
    {
      get
      {
        if (!REGI_ListaPagosOK)
        { return String.Format("Debe ingresar por lo menos un pago"); }
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
        if (!REGI_EstadoOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_EstadoMSGERROR + Environment.NewLine;
        }
        if (!REGI_TramosOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_TramosMSGERROR + Environment.NewLine;
        }
        if (!REGI_CantidadOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_CantidadMSGERROR + Environment.NewLine;
        }
        if (!REGI_FechaHoraEntradaOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_FechaHoraEntradaMSGERROR + Environment.NewLine;
        }
        if (!REGI_FechaHoraSalidaOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_FechaHoraSalidaMSGERROR + Environment.NewLine;
        }
        if (!TABL_TablaTDIOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_TablaTDIMSGERROR + Environment.NewLine;
        }
        if (!TABL_InternoTDIOK)
        {
          _isCorrect = false;
          m_mensajeError += TABL_InternoTDIMSGERROR + Environment.NewLine;
        }
        if (!REGI_HuespedIdOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_HuespedIdMSGERROR + Environment.NewLine;
        }
        if (!REGI_HuespedNombreCompletoOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_HuespedNombreCompletoMSGERROR + Environment.NewLine;
        }
        if (!REGI_HuespedDireccionOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_HuespedDireccionMSGERROR + Environment.NewLine;
        }
        if (!REGI_PrecioSugeridoOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_PrecioSugeridoMSGERROR + Environment.NewLine;
        }
        if (!REGI_PrecioCobradoOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_PrecioCobradoMSGERROR + Environment.NewLine;
        }
        if (!USUA_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += USUA_InternoMSGERROR + Environment.NewLine;
        }
        if (!REGI_ListaPagosOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_ListaPagosMSGERROR + Environment.NewLine;
        }

        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    public Boolean ValidarCalculo()
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
        if (!REGI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_InternoMSGERROR + Environment.NewLine;
        }
        if (!HABI_InternoOK)
        {
          _isCorrect = false;
          m_mensajeError += HABI_InternoMSGERROR + Environment.NewLine;
        }
        if (!REGI_TramosOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_TramosMSGERROR + Environment.NewLine;
        }
        if (!REGI_CantidadOK)
        {
          _isCorrect = false;
          m_mensajeError += REGI_CantidadMSGERROR + Environment.NewLine;
        }

        return _isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
