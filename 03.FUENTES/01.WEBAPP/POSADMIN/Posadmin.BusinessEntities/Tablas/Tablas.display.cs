using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Runtime.Serialization;
using SoftwareFactory.Infrastructure.BusinessEntity;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  public partial class Tablas_Display
  {
    #region [ VARIABLES ]

    private Tablas m_itemTablas;

    #endregion

    #region [ CONSTRUCTOR ]

    public Tablas_Display(Tablas Item)
    { ItemTablas = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Tablas ItemTablas
    {
      get { if (m_itemTablas == null) { m_itemTablas = Tablas.Nuevo(); } return m_itemTablas; }
      set { m_itemTablas = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"TablasRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Tabla=" + TABL_Tabla_Display + "&Interno=" + TABL_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + ",'" + TABL_Tabla_Display.ToString() + "','" + TABL_Interno_Display.ToString() + "')\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemTablas.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Tabla", Description = "Tabla", ShortName = "Tabla", Order = 2)]
    public String TABL_Tabla_Display { get { return ItemTablas.TABL_Tabla; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public String TABL_Interno_Display { get { return ItemTablas.TABL_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 4)]
    public String TABL_Nombre_Display { get { return ItemTablas.TABL_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 5)]
    public String TABL_Nomenclatura_Display { get { return ItemTablas.TABL_Nomenclatura; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 6)]
    public String TABL_Descripcion_Display { get { return ItemTablas.TABL_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Codigo Internacional", Description = "Codigo Internacional", ShortName = "Codigo Internacional", Order = 7)]
    public String TABL_CodigoInternacional_Display { get { return ItemTablas.TABL_CodigoInternacional; } }

    [Display(AutoGenerateField = false, Name = "Codigo 1", Description = "Codigo 1", ShortName = "Codigo 1", Order = 8)]
    public String TABL_Codigo1_Display { get { return ItemTablas.TABL_Codigo1; } }

    [Display(AutoGenerateField = false, Name = "Codigo 2", Description = "Codigo 2", ShortName = "Codigo 2", Order = 9)]
    public String TABL_Codigo2_Display { get { return ItemTablas.TABL_Codigo2; } }

    [Display(AutoGenerateField = false, Name = "Codigo 3", Description = "Codigo 3", ShortName = "Codigo 3", Order = 10)]
    public String TABL_Codigo3_Display { get { return ItemTablas.TABL_Codigo3; } }

    [Display(AutoGenerateField = false, Name = "Numero 1", Description = "Numero 1", ShortName = "Numero 1", Order = 11)]
    public Int32 TABL_Numero1_Display { get { return ItemTablas.TABL_Numero1; } }

    [Display(AutoGenerateField = false, Name = "Numero 2", Description = "Numero 2", ShortName = "Numero 2", Order = 12)]
    public Double TABL_Numero2_Display { get { return ItemTablas.TABL_Numero2; } }

    [Display(AutoGenerateField = false, Name = "Numero 3", Description = "Numero 3", ShortName = "Numero 3", Order = 13)]
    public Double TABL_Numero3_Display { get { return ItemTablas.TABL_Numero3; } }

    [Display(AutoGenerateField = false, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 14)]
    public Boolean TABL_Activo_Display { get { return ItemTablas.TABL_Activo; } }

    #endregion

    #region [ METODOS ]

    public static List<Tablas_Display> ToList(ObservableCollection<Tablas> Items)
    {
      List<Tablas_Display> l_listTablas = new List<Tablas_Display>();

      foreach (Tablas item in Items)
      { l_listTablas.Add(new Tablas_Display(item)); }

      return l_listTablas;
    }

    public static DataTable ToDataTable(ObservableCollection<Tablas> Items)
    {
      DataTable dt_Tablas = new DataTable("Tablas");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Tablas.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_TABL_Tabla = new DataColumn("TABL_Tabla", System.Type.GetType("System.String"));
      dc_TABL_Tabla.Caption = "Tabla";
      dt_Tablas.Columns.Add(dc_TABL_Tabla);
      DataColumn dc_TABL_Inteno = new DataColumn("TABL_Interno", System.Type.GetType("System.String"));
      dc_TABL_Inteno.Caption = "Interno";
      dt_Tablas.Columns.Add(dc_TABL_Inteno);
      DataColumn dc_TABL_Nombre = new DataColumn("TABL_Nombre", System.Type.GetType("System.String"));
      dc_TABL_Nombre.Caption = "Nombre";
      dt_Tablas.Columns.Add(dc_TABL_Nombre);
      DataColumn dc_TABL_Nomenclatura = new DataColumn("TABL_Nomenclatura", System.Type.GetType("System.String"));
      dc_TABL_Nomenclatura.Caption = "Nomenclatura";
      dt_Tablas.Columns.Add(dc_TABL_Nomenclatura);
      DataColumn dc_TABL_Descripcion = new DataColumn("TABL_Descripcion", System.Type.GetType("System.String"));
      dc_TABL_Descripcion.Caption = "Descripcion";
      dt_Tablas.Columns.Add(dc_TABL_Descripcion);
      DataColumn dc_TABL_CodigoInternacional = new DataColumn("TABL_CodigoInternacional", System.Type.GetType("System.String"));
      dc_TABL_CodigoInternacional.Caption = "Codigo Internacional";
      dt_Tablas.Columns.Add(dc_TABL_CodigoInternacional);
      DataColumn dc_TABL_Codigo1 = new DataColumn("TABL_Codigo1", System.Type.GetType("System.String"));
      dc_TABL_Codigo1.Caption = "Codigo 1";
      dt_Tablas.Columns.Add(dc_TABL_Codigo1);
      DataColumn dc_TABL_Codigo2 = new DataColumn("TABL_Codigo2", System.Type.GetType("System.String"));
      dc_TABL_Codigo2.Caption = "Codigo 2";
      dt_Tablas.Columns.Add(dc_TABL_Codigo2);
      DataColumn dc_TABL_Codigo3 = new DataColumn("TABL_Codigo3", System.Type.GetType("System.String"));
      dc_TABL_Codigo3.Caption = "Codigo 3";
      dt_Tablas.Columns.Add(dc_TABL_Codigo3);
      DataColumn dc_TABL_Numero1 = new DataColumn("TABL_Numero1", System.Type.GetType("System.Int32"));
      dc_TABL_Numero1.Caption = "Numero 1";
      dt_Tablas.Columns.Add(dc_TABL_Numero1);
      DataColumn dc_TABL_Numero2 = new DataColumn("TABL_Numero2", System.Type.GetType("System.Double"));
      dc_TABL_Numero2.Caption = "Numero 2";
      dt_Tablas.Columns.Add(dc_TABL_Numero2);
      DataColumn dc_TABL_Numero3 = new DataColumn("TABL_Numero3", System.Type.GetType("System.Double"));
      dc_TABL_Numero3.Caption = "Numero 3";
      dt_Tablas.Columns.Add(dc_TABL_Numero3);
      DataColumn dc_TABL_Activo = new DataColumn("TABL_Activo", System.Type.GetType("System.Boolean"));
      dc_TABL_Activo.Caption = "Activo";
      dt_Tablas.Columns.Add(dc_TABL_Activo);

      foreach (Tablas item in Items)
      {
        DataRow dr_Tablas = dt_Tablas.NewRow();

        dr_Tablas[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Tablas[dc_TABL_Tabla] = item.TABL_Tabla;
        dr_Tablas[dc_TABL_Inteno] = item.TABL_Interno;
        dr_Tablas[dc_TABL_Nombre] = item.TABL_Nombre;
        dr_Tablas[dc_TABL_Nomenclatura] = item.TABL_Nomenclatura;
        dr_Tablas[dc_TABL_Descripcion] = item.TABL_Descripcion;
        dr_Tablas[dc_TABL_CodigoInternacional] = item.TABL_CodigoInternacional;
        dr_Tablas[dc_TABL_Codigo1] = item.TABL_Codigo1;
        dr_Tablas[dc_TABL_Codigo2] = item.TABL_Codigo2;
        dr_Tablas[dc_TABL_Codigo3] = item.TABL_Codigo3;
        dr_Tablas[dc_TABL_Numero1] = item.TABL_Numero1;
        dr_Tablas[dc_TABL_Numero2] = item.TABL_Numero2;
        dr_Tablas[dc_TABL_Numero3] = item.TABL_Numero3;
        dr_Tablas[dc_TABL_Activo] = item.TABL_Activo;

        dt_Tablas.Rows.Add(dr_Tablas);
      }

      return dt_Tablas;
    }

    #endregion
  }
}
