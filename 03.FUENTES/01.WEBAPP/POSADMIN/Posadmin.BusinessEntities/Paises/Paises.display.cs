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
  public partial class Paises_Display
  {
    #region [ VARIABLES ]

    private Paises m_item_Paises;

    #endregion

    #region [ CONSTRUCTOR ]
    public Paises_Display(Paises Item)
    { Item_Paises = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Paises Item_Paises
    {
      get { if (m_item_Paises == null) { m_item_Paises = Paises.Nuevo(); } return m_item_Paises; }
      set { m_item_Paises = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"PaisesRegister?Interno=" + PAIS_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + PAIS_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Pais Interno", Description = "Pais Interno", ShortName = "Pais Interno", Order = 1)]
    public Int32 PAIS_Interno_Display { get { return Item_Paises.PAIS_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String PAIS_Nombre_Display { get { return Item_Paises.PAIS_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Codigo Numerico", Description = "Codigo Numerico", ShortName = "Codigo Numerico", Order = 3)]
    public String PAIS_CodigoNumerico_Display { get { return Item_Paises.PAIS_CodigoNumerico; } }

    [Display(AutoGenerateField = false, Name = "Codigo Alfa2", Description = "Codigo Alfa2", ShortName = "Codigo Alfa2", Order = 4)]
    public String PAIS_CodigoAlfa2_Display { get { return Item_Paises.PAIS_CodigoAlfa2; } }

    [Display(AutoGenerateField = false, Name = "Codigo Alfa3", Description = "Codigo Alfa3", ShortName = "Codigo Alfa3", Order = 5)]
    public String PAIS_CodigoAlfa3_Display { get { return Item_Paises.PAIS_CodigoAlfa3; } }

    [Display(AutoGenerateField = false, Name = "Continente", Description = "Continente", ShortName = "Continente", Order = 6)]
    public String PAIS_Continente_Display { get { return Item_Paises.PAIS_Continente; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 7)]
    public String PAIS_Descripcion_Display { get { return Item_Paises.PAIS_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 8)]
    public Boolean PAIS_Activo_Display { get { return Item_Paises.PAIS_Activo; } }

    #endregion

    #region [ METODOS ]

    public static List<Paises_Display> ToList(ObservableCollection<Paises> Items)
    {
      List<Paises_Display> l_listPaises = new List<Paises_Display>();

      foreach (Paises item in Items)
      { l_listPaises.Add(new Paises_Display(item)); }

      return l_listPaises;
    }
    public static DataTable ToDataTable(ObservableCollection<Paises> Items)
    {
      DataTable dt_Paises = new DataTable("Paises");

      DataColumn dc_PAIS_Interno = new DataColumn("PAIS_Interno", Type.GetType("System.Int32"));
      dc_PAIS_Interno.Caption = "Pais Interno";
      dt_Paises.Columns.Add(dc_PAIS_Interno);
      DataColumn dc_PAIS_Nombre = new DataColumn("PAIS_Nombre", Type.GetType("System.String"));
      dc_PAIS_Nombre.Caption = "Nombre";
      dt_Paises.Columns.Add(dc_PAIS_Nombre);
      DataColumn dc_PAIS_CodigoNumerico = new DataColumn("PAIS_CodigoNumerico", Type.GetType("System.String"));
      dc_PAIS_CodigoNumerico.Caption = "Codigo Numerico";
      dt_Paises.Columns.Add(dc_PAIS_CodigoNumerico);
      DataColumn dc_PAIS_CodigoAlfa2 = new DataColumn("PAIS_CodigoAlfa2", Type.GetType("System.String"));
      dc_PAIS_CodigoAlfa2.Caption = "Codigo Alfa2";
      dt_Paises.Columns.Add(dc_PAIS_CodigoAlfa2);
      DataColumn dc_PAIS_CodigoAlfa3 = new DataColumn("PAIS_CodigoAlfa3", Type.GetType("System.String"));
      dc_PAIS_CodigoAlfa3.Caption = "Codigo Alfa3";
      dt_Paises.Columns.Add(dc_PAIS_CodigoAlfa3);
      DataColumn dc_PAIS_Continente = new DataColumn("PAIS_Continente", Type.GetType("System.String"));
      dc_PAIS_Continente.Caption = "Continente";
      dt_Paises.Columns.Add(dc_PAIS_Continente);
      DataColumn dc_PAIS_Descripcion = new DataColumn("PAIS_Descripcion", Type.GetType("System.String"));
      dc_PAIS_Descripcion.Caption = "Descripcion";
      dt_Paises.Columns.Add(dc_PAIS_Descripcion);
      DataColumn dc_PAIS_Activo = new DataColumn("PAIS_Activo", Type.GetType("System.Boolean"));
      dc_PAIS_Activo.Caption = "Activo";
      dt_Paises.Columns.Add(dc_PAIS_Activo);

      foreach (Paises item in Items)
      {
        DataRow dr_Paises = dt_Paises.NewRow();

        dr_Paises[dc_PAIS_Interno] = item.PAIS_Interno;
        dr_Paises[dc_PAIS_Nombre] = item.PAIS_Nombre;
        dr_Paises[dc_PAIS_CodigoNumerico] = item.PAIS_CodigoNumerico;
        dr_Paises[dc_PAIS_CodigoAlfa2] = item.PAIS_CodigoAlfa2;
        dr_Paises[dc_PAIS_CodigoAlfa3] = item.PAIS_CodigoAlfa3;
        dr_Paises[dc_PAIS_Continente] = item.PAIS_Continente;
        dr_Paises[dc_PAIS_Descripcion] = item.PAIS_Descripcion;
        dr_Paises[dc_PAIS_Activo] = item.PAIS_Activo;

        dt_Paises.Rows.Add(dr_Paises);
      }

      return dt_Paises;
    }

    #endregion
  }
}
