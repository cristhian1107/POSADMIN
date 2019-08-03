using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;
using System.Data;

namespace Posadmin.Models
{
  public class IndicadoresAnualesModel
  {
    #region [ VARIABLES ]

    private DataSet m_dsdata;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: DSData
    /// </summary>
    public DataSet DSData
    {
      get { if (m_dsdata == null) { m_dsdata = new DataSet(); } return m_dsdata; }
      set { m_dsdata = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: InternoEmpresa
    /// </summary>
    public Int64 InternoEmpresa
    {
      get { return m_internoempresa; }
      set
      {
        if (m_internoempresa != value)
        {
          m_internoempresa = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: InternoSucursal
    /// </summary>
    public Int64 InternoSucursal
    {
      get { return m_internosucursal; }
      set
      {
        if (m_internosucursal != value)
        {
          m_internosucursal = value;
        }
      }
    }

    #endregion

    #region [ FILTROS ]

    /// <summary>
    /// Gets or sets el valor de: FiltroSucursal
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Sucursal", Description = "Sucursal", ShortName = "Sucursal", Order = 0)]
    public Int64 FiltroSucursal { get; set; }

    /// <summary>
    /// Gets or sets el valor de: FiltroAnio
    /// </summary>
    [Display(AutoGenerateField = false, Name = "Año", Description = "Año", ShortName = "Año", Order = 0)]
    public Int32 FiltroAnio { get; set; }

    #endregion
  }
}