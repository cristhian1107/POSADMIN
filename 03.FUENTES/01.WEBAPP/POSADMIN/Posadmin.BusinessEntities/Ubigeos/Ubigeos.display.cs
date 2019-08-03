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
  public partial class Ubigeos_Display
  {
    #region [ VARIABLES ]

    private Ubigeos m_itemUbigeos;

    #endregion

    #region [ CONSTRUCTOR ]

    public Ubigeos_Display(Ubigeos Item)
    { ItemUbigeos = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Ubigeos ItemUbigeos
    {
      get { if (m_itemUbigeos == null) { m_itemUbigeos = Ubigeos.Nuevo(); } return m_itemUbigeos; }
      set { m_itemUbigeos = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"UbigeosRegister?Interno=" + UBIG_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + UBIG_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Ubigeo Interno", Description = "Ubigeo Interno", ShortName = "Ubigeo Interno", Order = 1)]
    public Int32 UBIG_Interno_Display { get { return ItemUbigeos.UBIG_Interno; } }

    [Display(AutoGenerateField = false, Name = "Pais Interno", Description = "Pais Interno", ShortName = "Pais Interno", Order = 2)]
    public Int32 PAIS_Interno_Display { get { return ItemUbigeos.PAIS_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 3)]
    public String UBIG_Nombre_Display { get { return ItemUbigeos.UBIG_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 4)]
    public String UBIG_Nomenclatura_Display { get { return ItemUbigeos.UBIG_Nomenclatura; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 5)]
    public String UBIG_Descripcion_Display { get { return ItemUbigeos.UBIG_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Codigo 1", Description = "Codigo 1", ShortName = "Codigo 1", Order = 6)]
    public String UBIG_Codigo1_Display { get { return ItemUbigeos.UBIG_Codigo1; } }

    [Display(AutoGenerateField = false, Name = "Codigo 2", Description = "Codigo 2", ShortName = "Codigo 2", Order = 7)]
    public String UBIG_Codigo2_Display { get { return ItemUbigeos.UBIG_Codigo2; } }

    [Display(AutoGenerateField = false, Name = "Codigo 3", Description = "Codigo 3", ShortName = "Codigo 3", Order = 8)]
    public String UBIG_Codigo3_Display { get { return ItemUbigeos.UBIG_Codigo3; } }

    [Display(AutoGenerateField = false, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 9)]
    public Boolean UBIG_Activo_Display { get { return ItemUbigeos.UBIG_Activo; } }

    [Display(AutoGenerateField = false, Name = "Padre Interno", Description = "Padre Interno", ShortName = "Padre Interno", Order = 10)]
    public Nullable<Int32> UBIG_InternoPadre_Display { get { return ItemUbigeos.UBIG_InternoPadre; } }

    [Display(AutoGenerateField = false, Name = "Nombre del Pais", Description = "Nombre del Pais", ShortName = "Nombre del Pais", Order = 11)]
    public String UBIG_NombrePais_Display { get { return ItemUbigeos.UBIG_NombrePais; } }

    [Display(AutoGenerateField = false, Name = "Nombre del Padre", Description = "Nombre del Padre", ShortName = "Nombre del Padre", Order = 12)]
    public String UBIG_NombrePadre_Display { get { return ItemUbigeos.UBIG_NombrePadre; } }

    #endregion

    #region [ METODOS ]
    public static List<Ubigeos_Display> ToList(ObservableCollection<Ubigeos> Items)
    {
      List<Ubigeos_Display> _listUbigeos = new List<Ubigeos_Display>();

      foreach (Ubigeos item in Items)
      { _listUbigeos.Add(new Ubigeos_Display(item)); }

      return _listUbigeos;
    }

    public static DataTable ToDataTable(ObservableCollection<Ubigeos> Items)
    {
      DataTable dt_Ubigeos = new DataTable("Ubigeos");

      DataColumn dc_UBIG_Interno = new DataColumn("UBIG_Interno", System.Type.GetType("System.Int32"));
      dc_UBIG_Interno.Caption = "Ubigeo Interno";
      dt_Ubigeos.Columns.Add(dc_UBIG_Interno);
      DataColumn dc_PAIS_Interno = new DataColumn("PAIS_Interno", System.Type.GetType("System.Int32"));
      dc_PAIS_Interno.Caption = "Pais Interno";
      dt_Ubigeos.Columns.Add(dc_PAIS_Interno);
      DataColumn dc_UBIG_Nombre = new DataColumn("UBIG_Nombre", System.Type.GetType("System.String"));
      dc_UBIG_Nombre.Caption = "Nombre";
      dt_Ubigeos.Columns.Add(dc_UBIG_Nombre);
      DataColumn dc_UBIG_Nomenclatura = new DataColumn("UBIG_Nomenclatura", System.Type.GetType("System.String"));
      dc_UBIG_Nomenclatura.Caption = "Nomenclatura";
      dt_Ubigeos.Columns.Add(dc_UBIG_Nomenclatura);
      DataColumn dc_UBIG_Descripcion = new DataColumn("UBIG_Descripcion", System.Type.GetType("System.String"));
      dc_UBIG_Descripcion.Caption = "Descripcion";
      dt_Ubigeos.Columns.Add(dc_UBIG_Descripcion);
      DataColumn dc_UBIG_Codigo1 = new DataColumn("UBIG_Codigo1", System.Type.GetType("System.String"));
      dc_UBIG_Codigo1.Caption = "Codigo 1";
      dt_Ubigeos.Columns.Add(dc_UBIG_Codigo1);
      DataColumn dc_UBIG_Codigo2 = new DataColumn("UBIG_Codigo2", System.Type.GetType("System.String"));
      dc_UBIG_Codigo2.Caption = "Codigo 2";
      dt_Ubigeos.Columns.Add(dc_UBIG_Codigo2);
      DataColumn dc_UBIG_Codigo3 = new DataColumn("UBIG_Codigo3", System.Type.GetType("System.String"));
      dc_UBIG_Codigo3.Caption = "Codigo 3";
      dt_Ubigeos.Columns.Add(dc_UBIG_Codigo3);
      DataColumn dc_UBIG_Activo = new DataColumn("UBIG_Activo", System.Type.GetType("System.Boolean"));
      dc_UBIG_Activo.Caption = "Activo";
      dt_Ubigeos.Columns.Add(dc_UBIG_Activo);
      DataColumn dc_UBIG_InternoPadre = new DataColumn("UBIG_InternoPadre", System.Type.GetType("System.Int32"));
      dc_UBIG_InternoPadre.Caption = "Padre Interno";
      dt_Ubigeos.Columns.Add(dc_UBIG_InternoPadre);

      foreach (Ubigeos item in Items)
      {
        DataRow dr_Ubigeos = dt_Ubigeos.NewRow();

        dr_Ubigeos[dc_UBIG_Interno] = item.UBIG_Interno;
        dr_Ubigeos[dc_PAIS_Interno] = item.PAIS_Interno;
        dr_Ubigeos[dc_UBIG_Nombre] = item.UBIG_Nombre;
        dr_Ubigeos[dc_UBIG_Nomenclatura] = item.UBIG_Nomenclatura;
        dr_Ubigeos[dc_UBIG_Descripcion] = item.UBIG_Descripcion;
        dr_Ubigeos[dc_UBIG_Codigo1] = item.UBIG_Codigo1;
        dr_Ubigeos[dc_UBIG_Codigo2] = item.UBIG_Codigo2;
        dr_Ubigeos[dc_UBIG_Codigo3] = item.UBIG_Codigo3;
        dr_Ubigeos[dc_UBIG_Activo] = item.UBIG_Activo;
        dr_Ubigeos[dc_UBIG_InternoPadre] = item.UBIG_InternoPadre;

        dt_Ubigeos.Rows.Add(dr_Ubigeos);
      }

      return dt_Ubigeos;
    }
    #endregion
  }
}
