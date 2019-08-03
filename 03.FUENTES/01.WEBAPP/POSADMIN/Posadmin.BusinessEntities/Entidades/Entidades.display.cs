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
  public partial class Entidades_Display
  {
    #region [ VARIABLES ]

    private Entidades m_itemEntidades;

    #endregion

    #region [ CONSTRUCTOR ]

    public Entidades_Display(Entidades Item)
    { ItemEntidades = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Entidades ItemEntidades
    {
      get { if (m_itemEntidades == null) { m_itemEntidades = Entidades.Nuevo(); } return m_itemEntidades; }
      set { m_itemEntidades = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"EntidadesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Interno=" + ENTI_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + "," + ENTI_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemEntidades.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 2)]
    public Int64 ENTI_Interno_Display { get { return ItemEntidades.ENTI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 3)]
    public String TABL_TablaTDI_Display { get { return ItemEntidades.TABL_TablaTDI; } }

    [Display(AutoGenerateField = false, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 4)]
    public String TABL_InternoTDI_Display { get { return ItemEntidades.TABL_InternoTDI; } }

    [Display(AutoGenerateField = false, Name = "Tipo Doc. Ide.", Description = "Tipo Doc. Ide.", ShortName = "Tipo Doc. Ide.", Order = 4)]
    public String TABL_NombreTDI_Display { get { return ItemEntidades.TABL_NombreTDI; } }

    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 5)]
    public String ENTI_Id_Display { get { return ItemEntidades.ENTI_Id; } }

    [Display(AutoGenerateField = false, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 6)]
    public String ENTI_NombreCompleto_Display { get { return ItemEntidades.ENTI_NombreCompleto; } }

    [Display(AutoGenerateField = false, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 7)]
    public String ENTI_Direccion_Display { get { return ItemEntidades.ENTI_Direccion; } }

    [Display(AutoGenerateField = false, Name = "Sexo", Description = "Sexo", ShortName = "Sexo", Order = 8)]
    public String ENTI_Sexo_Display { get { return ItemEntidades.ENTI_Sexo; } }

    [Display(AutoGenerateField = false, Name = "Pais", Description = "Pais", ShortName = "Pais", Order = 9)]
    public Nullable<Int32> PAIS_Interno_Display { get { return ItemEntidades.PAIS_Interno; } }

    [Display(AutoGenerateField = false, Name = "Ubigeo", Description = "Ubigeo", ShortName = "Ubigeo", Order = 10)]
    public Nullable<Int32> UBIG_Interno_Display { get { return ItemEntidades.UBIG_Interno; } }


    #endregion

    #region [ METODOS ]

    public static List<Entidades_Display> ToList(ObservableCollection<Entidades> Items)
    {
      List<Entidades_Display> _listEntidades = new List<Entidades_Display>();

      foreach (Entidades item in Items)
      { _listEntidades.Add(new Entidades_Display(item)); }

      return _listEntidades;
    }

    public static DataTable ToDataTable(ObservableCollection<Entidades> Items)
    {
      DataTable dt_Entidades = new DataTable("Entidades");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Entidades.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_ENTI_Interno = new DataColumn("ENTI_Interno", System.Type.GetType("System.Int64"));
      dc_ENTI_Interno.Caption = "Interno";
      dt_Entidades.Columns.Add(dc_ENTI_Interno);
      DataColumn dc_TABL_TablaTDI = new DataColumn("TABL_TablaTDI", System.Type.GetType("System.String"));
      dc_TABL_TablaTDI.Caption = "TablaTDI";
      dt_Entidades.Columns.Add(dc_TABL_TablaTDI);
      DataColumn dc_TABL_InternoTDI = new DataColumn("TABL_InternoTDI", System.Type.GetType("System.String"));
      dc_TABL_InternoTDI.Caption = "InternoTDI";
      dt_Entidades.Columns.Add(dc_TABL_InternoTDI);
      DataColumn dc_ENTI_Id = new DataColumn("ENTI_Id", System.Type.GetType("System.String"));
      dc_ENTI_Id.Caption = "Id";
      dt_Entidades.Columns.Add(dc_ENTI_Id);
      DataColumn dc_ENTI_NombreCompleto = new DataColumn("ENTI_NombreCompleto", System.Type.GetType("System.String"));
      dc_ENTI_NombreCompleto.Caption = "NombreCompleto";
      dt_Entidades.Columns.Add(dc_ENTI_NombreCompleto);
      DataColumn dc_ENTI_Direccion = new DataColumn("ENTI_Direccion", System.Type.GetType("System.String"));
      dc_ENTI_Direccion.Caption = "Direccion";
      dt_Entidades.Columns.Add(dc_ENTI_Direccion);
      DataColumn dc_ENTI_Sexo = new DataColumn("ENTI_Sexo", System.Type.GetType("System.String"));
      dc_ENTI_Sexo.Caption = "Sexo";
      dt_Entidades.Columns.Add(dc_ENTI_Sexo);
      DataColumn dc_PAIS_Interno = new DataColumn("PAIS_Interno", System.Type.GetType("System.Int32"));
      dc_PAIS_Interno.Caption = "Pais";
      dt_Entidades.Columns.Add(dc_PAIS_Interno);
      DataColumn dc_UBIG_Interno = new DataColumn("UBIG_Interno", System.Type.GetType("System.Int32"));
      dc_UBIG_Interno.Caption = "Ubigeo";
      dt_Entidades.Columns.Add(dc_UBIG_Interno);

      foreach (Entidades item in Items)
      {
        DataRow dr_Entidades = dt_Entidades.NewRow();

        dr_Entidades[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Entidades[dc_ENTI_Interno] = item.ENTI_Interno;
        dr_Entidades[dc_TABL_TablaTDI] = item.TABL_TablaTDI;
        dr_Entidades[dc_TABL_InternoTDI] = item.TABL_InternoTDI;
        dr_Entidades[dc_ENTI_Id] = item.ENTI_Id;
        dr_Entidades[dc_ENTI_NombreCompleto] = item.ENTI_NombreCompleto;
        dr_Entidades[dc_ENTI_Direccion] = item.ENTI_Direccion;
        dr_Entidades[dc_ENTI_Sexo] = item.ENTI_Sexo;
        dr_Entidades[dc_PAIS_Interno] = item.PAIS_Interno;
        dr_Entidades[dc_UBIG_Interno] = item.UBIG_Interno;

        dt_Entidades.Rows.Add(dr_Entidades);
      }

      return dt_Entidades;
    }

    #endregion
  }
}
