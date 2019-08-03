using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Posadmin.BusinessEntities
{
  public partial class FuncionesEntidades_Display
  {
    #region [ VARIABLES ]

    private FuncionesEntidades m_itemFuncionesEntidades;

    #endregion

    #region [ CONSTRUCTOR ]

    public FuncionesEntidades_Display(FuncionesEntidades Item)
    { ItemFuncionesEntidades = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private FuncionesEntidades ItemFuncionesEntidades
    {
      get { if (m_itemFuncionesEntidades == null) { m_itemFuncionesEntidades = FuncionesEntidades.Nuevo(); } return m_itemFuncionesEntidades; }
      set { m_itemFuncionesEntidades = value; }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemFuncionesEntidades.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Tipo Doc. Identidad", Description = "Tipo Doc. Identidad", ShortName = "Tipo Doc. Identidad", Order = 2)]
    public String TABL_TablaTEN_Display { get { return ItemFuncionesEntidades.TABL_TablaTEN; } }

    [Display(AutoGenerateField = false, Name = "Tipo Doc. Identidad", Description = "Tipo Doc. Identidad", ShortName = "Tipo Doc. Identidad", Order = 3)]
    public String TABL_InternoTEN_Display { get { return ItemFuncionesEntidades.TABL_InternoTEN; } }

    [Display(AutoGenerateField = false, Name = "Entidad Interno", Description = "Entidad Interno", ShortName = "Entidad Interno", Order = 4)]
    public Int64 ENTI_Interno_Display { get { return ItemFuncionesEntidades.ENTI_Interno; } }

    #endregion

    #region [ METODOS ]

    public static List<FuncionesEntidades_Display> ToList(ObservableCollection<FuncionesEntidades> Items)
    {
      List<FuncionesEntidades_Display> _listFuncionesEntidades = new List<FuncionesEntidades_Display>();

      foreach (FuncionesEntidades item in Items)
      { _listFuncionesEntidades.Add(new FuncionesEntidades_Display(item)); }

      return _listFuncionesEntidades;
    }

    public static DataTable ToDataTable(ObservableCollection<FuncionesEntidades> Items)
    {
      DataTable dt_FuncionesEntidades = new DataTable("FuncionesEntidades");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_FuncionesEntidades.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_TABL_TablaTEN = new DataColumn("TABL_TablaTEN", System.Type.GetType("System.String"));
      dc_TABL_TablaTEN.Caption = "Tipo Doc. Identidad";
      dt_FuncionesEntidades.Columns.Add(dc_TABL_TablaTEN);
      DataColumn dc_TABL_InternoTEN = new DataColumn("TABL_InternoTEN", System.Type.GetType("System.String"));
      dc_TABL_InternoTEN.Caption = "Tipo Doc. Identidad";
      dt_FuncionesEntidades.Columns.Add(dc_TABL_InternoTEN);
      DataColumn dc_ENTI_Interno = new DataColumn("ENTI_Interno", System.Type.GetType("System.Int64"));
      dc_ENTI_Interno.Caption = "Entidad Interno";
      dt_FuncionesEntidades.Columns.Add(dc_ENTI_Interno);

      foreach (FuncionesEntidades item in Items)
      {
        DataRow dr_FuncionesEntidades = dt_FuncionesEntidades.NewRow();

        dr_FuncionesEntidades[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_FuncionesEntidades[dc_TABL_TablaTEN] = item.TABL_TablaTEN;
        dr_FuncionesEntidades[dc_TABL_InternoTEN] = item.TABL_InternoTEN;
        dr_FuncionesEntidades[dc_ENTI_Interno] = item.ENTI_Interno;

        dt_FuncionesEntidades.Rows.Add(dr_FuncionesEntidades);
      }

      return dt_FuncionesEntidades;
    }
    #endregion
  }
}
