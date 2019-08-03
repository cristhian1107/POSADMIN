using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Posadmin.BusinessEntities;
using System.Collections.ObjectModel;

namespace Posadmin.Models
{
  public class RetirosModel
  {
    #region [ VARIABLES ]

    private ObservableCollection<Retiros> m_itemsretiros;
    private ObservableCollection<Cierres> m_itemscierres;
    private Int64 m_internoempresa;
    private Int64 m_internosucursal;
    private Int64 m_internousuario;
    private Double m_totalcierres;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: ItemsRetiros
    /// </summary>
    public ObservableCollection<Retiros> ItemsRetiros
    {
      get { if (m_itemsretiros == null) { m_itemsretiros = new ObservableCollection<Retiros>(); } return m_itemsretiros; }
      set { m_itemsretiros = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: ItemsRetiros
    /// </summary>
    public ObservableCollection<Cierres> ItemsCierres
    {
      get { if (m_itemscierres == null) { m_itemscierres = new ObservableCollection<Cierres>(); } return m_itemscierres; }
      set { m_itemscierres = value; }
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
    public Double TotalCierres
    {
      get { return m_totalcierres; }
      set
      {
        if (m_totalcierres != value)
        {
          m_totalcierres = value;
        }
      }
    }

    #endregion

    #region [ FILTROS ]


    #endregion
  }
}