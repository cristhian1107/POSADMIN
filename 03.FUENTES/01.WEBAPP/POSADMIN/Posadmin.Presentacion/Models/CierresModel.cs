using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class CierresModel
  {
    #region [ VARIABLES ]

    private ObservableCollection<Cierres> m_itemscierres;
    private ObservableCollection<DetallesPagosRegistros> m_itemspagos;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;
    private Int64 m_internousuario;
    private Double m_totalpagos;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: ItemsCierres
    /// </summary>
    public ObservableCollection<Cierres> ItemsCierres
    {
      get { if (m_itemscierres == null) { m_itemscierres = new ObservableCollection<Cierres>(); } return m_itemscierres; }
      set { m_itemscierres = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: ItemsCierres
    /// </summary>
    public ObservableCollection<DetallesPagosRegistros> ItemsPagos
    {
      get { if (m_itemspagos == null) { m_itemspagos = new ObservableCollection<DetallesPagosRegistros>(); } return m_itemspagos; }
      set { m_itemspagos = value; }
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
    /// <summary>
    /// Gets or sets el valor de: InternoUsuario
    /// </summary>
    public Int64 InternoUsuario
    {
      get { return m_internousuario; }
      set
      {
        if (m_internousuario != value)
        {
          m_internousuario = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: TotalPagos
    /// </summary>
    public Double TotalPagos
    {
      get { return m_totalpagos; }
      set
      {
        if (m_totalpagos != value)
        {
          m_totalpagos = value;
        }
      }
    }

    #endregion

    #region [ FILTROS ]


    #endregion
  }
}