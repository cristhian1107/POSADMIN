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
using System.ComponentModel.DataAnnotations;
using SoftwareFactory.Infrastructure.Cryptography;

namespace Posadmin.BusinessEntities
{
  public partial class Usuarios
  {
    #region [ VARIABLES ]

    private String m_usua_rolnombre;
    private Int64 m_usua_internoempresa;
    private Int64 m_usua_internosucursal;
    private String m_usua_nombreempresa;
    private String m_usua_nombresucursal;
    private ObservableCollection<Empresas> m_usua_empresas;
    private ObservableCollection<Empresas> m_usua_empresasseleccionadas;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: USUA_RolNombre.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Rol", Description = "Rol", ShortName = "Rol", Order = 5)]
    public String USUA_RolNombre
    {
      get { return m_usua_rolnombre; }
      set
      {
        if (m_usua_rolnombre != value)
        {
          m_usua_rolnombre = value;
          OnPropertyChanged("USUA_RolNombre");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_InternoEmpresa.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno de la Empresa", Description = "Interno de la Empresa", ShortName = "Interno de la Empresa", Order = 5)]
    public Int64 USUA_InternoEmpresa
    {
      get { return m_usua_internoempresa; }
      set
      {
        if (m_usua_internoempresa != value)
        {
          m_usua_internoempresa = value;
          OnPropertyChanged("USUA_InternoEmpresa");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_InternoSucursal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Interno de la Sucursal", Description = "Interno de la Sucursal", ShortName = "Interno de la Sucursal", Order = 5)]
    public Int64 USUA_InternoSucursal
    {
      get { return m_usua_internosucursal; }
      set
      {
        if (m_usua_internosucursal != value)
        {
          m_usua_internosucursal = value;
          OnPropertyChanged("USUA_InternoSucursal");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_NombreEmpresa.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre de la Empresa", Description = "Nombre de la Empresa", ShortName = "Nombre de la Empresa", Order = 5)]
    public String USUA_NombreEmpresa
    {
      get { return m_usua_nombreempresa; }
      set
      {
        if (m_usua_nombreempresa != value)
        {
          m_usua_nombreempresa = value;
          OnPropertyChanged("USUA_NombreEmpresa");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_NombreSucursal.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Nombre de la Sucursal", Description = "Nombre de la Sucursal", ShortName = "Nombre de la Sucursal", Order = 5)]
    public String USUA_NombreSucursal
    {
      get { return m_usua_nombresucursal; }
      set
      {
        if (m_usua_nombresucursal != value)
        {
          m_usua_nombresucursal = value;
          OnPropertyChanged("USUA_NombreSucursal");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_Empresas.
    /// </summary>
    [DataMember]
    public ObservableCollection<Empresas> USUA_Empresas
    {
      get { if (m_usua_empresas == null) { m_usua_empresas = new ObservableCollection<Empresas>(); } return m_usua_empresas; }
      set { m_usua_empresas = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: USUA_EmpresasSeleccionadas.
    /// </summary>
    [DataMember]
    public ObservableCollection<Empresas> USUA_EmpresasSeleccionadas
    {
      get { if (m_usua_empresasseleccionadas == null) { m_usua_empresasseleccionadas = new ObservableCollection<Empresas>(); } return m_usua_empresasseleccionadas; }
      set { m_usua_empresasseleccionadas = value; }
    }

    [DataMember]
    public String USUA_ContrasenaEncriptada
    {
      get
      {
        if (!String.IsNullOrEmpty(USUA_Usuario) && !String.IsNullOrEmpty(USUA_Contrasena))
        { return MyCryptographyMD5.EncryptData(USUA_Usuario.ToUpper() + USUA_Contrasena.ToUpper() + USUA_Usuario.ToUpper()); }
        else { return null; }
      }
    }

    #endregion

    #region [ CONTROLES ]

    [DataMember]
    public Int64 Value { get { return USUA_Interno; } }
    [DataMember]
    public String Text { get { return USUA_Usuario; } }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
