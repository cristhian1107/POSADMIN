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
  public partial class Entidades
  {
    #region [ VARIABLES ]

    private String m_tabl_nombretdi;
    private Int32 m_enti_departamento;
    private Int32 m_enti_provincia;
    private Int32 m_enti_distrito;
    private ObservableCollection<FuncionesEntidades> m_enti_funcionesentidades;
    private ObservableCollection<FuncionesEntidades> m_enti_funcionesentidadesseleccionadas;

    #endregion

    #region [ PROPIEDADES ]

    /// <summary>
    /// Gets or sets el valor de: TABL_NombreTDI.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 3)]
    public String TABL_NombreTDI
    {
      get { return m_tabl_nombretdi; }
      set
      {
        if (m_tabl_nombretdi != value)
        {
          m_tabl_nombretdi = value;
          OnPropertyChanged("TABL_NombreTDI");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Departamento.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Departamento", Description = "Departamento", ShortName = "Departamento", Order = 16)]
    public Int32 ENTI_Departamento
    {
      get { return m_enti_departamento; }
      set
      {
        if (m_enti_departamento != value)
        {
          m_enti_departamento = value;
          OnPropertyChanged("ENTI_Departamento");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Provincia.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Provincia", Description = "Provincia", ShortName = "Provincia", Order = 17)]
    public Int32 ENTI_Provincia
    {
      get { return m_enti_provincia; }
      set
      {
        if (m_enti_provincia != value)
        {
          m_enti_provincia = value;
          OnPropertyChanged("ENTI_Provincia");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_Distrito.
    /// </summary>
    [DataMember]
    [Display(AutoGenerateField = true, Name = "Distrito", Description = "Distrito", ShortName = "Distrito", Order = 18)]
    public Int32 ENTI_Distrito
    {
      get { return m_enti_distrito; }
      set
      {
        if (m_enti_distrito != value)
        {
          m_enti_distrito = value;
          OnPropertyChanged("ENTI_Distrito");
        }
      }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_FuncionesEntidades.
    /// </summary>
    [DataMember]
    public ObservableCollection<FuncionesEntidades> ENTI_FuncionesEntidades
    {
      get { if (m_enti_funcionesentidades == null) { m_enti_funcionesentidades = new ObservableCollection<FuncionesEntidades>(); } return m_enti_funcionesentidades; }
      set { m_enti_funcionesentidades = value; }
    }

    /// <summary>
    /// Gets or sets el valor de: ENTI_FuncionesEntidadesSeleccionadas.
    /// </summary>
    [DataMember]
    public ObservableCollection<FuncionesEntidades> ENTI_FuncionesEntidadesSeleccionadas
    {
      get { if (m_enti_funcionesentidadesseleccionadas == null) { m_enti_funcionesentidadesseleccionadas = new ObservableCollection<FuncionesEntidades>(); } return m_enti_funcionesentidadesseleccionadas; }
      set { m_enti_funcionesentidadesseleccionadas = value; }
    }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
