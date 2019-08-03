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

namespace Posadmin.BusinessEntities
{
  public partial class Roles
  {
    #region [ VARIABLES ]

    private ObservableCollection<Opciones> m_role_opciones;
    private ObservableCollection<Opciones> m_role_opcionesseleccionadas;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: ROLE_Opciones.
    /// </summary>
    [DataMember]
    public ObservableCollection<Opciones> ROLE_Opciones
    {
      get { if (m_role_opciones == null) { m_role_opciones = new ObservableCollection<Opciones>(); } return m_role_opciones; }
      set { m_role_opciones = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: ROLE_OpcionesSeleccionadas.
    /// </summary>
    [DataMember]
    public ObservableCollection<Opciones> ROLE_OpcionesSeleccionadas
    {
      get { if (m_role_opcionesseleccionadas == null) { m_role_opcionesseleccionadas = new ObservableCollection<Opciones>(); } return m_role_opcionesseleccionadas; }
      set { m_role_opcionesseleccionadas = value; }
    }

    #endregion

    #region [ CONTROLES ]

    [DataMember]
    public Int32 Value { get { return ROLE_Interno; } }
    [DataMember]
    public String Text { get { return ROLE_Nombre; } }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
