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
  public partial class Sucursales_Display
  {
    #region [ VARIABLES ]

    private Sucursales m_itemSucursales;

    #endregion

    #region [ CONSTRUCTOR ]

    public Sucursales_Display(Sucursales Item)
    { ItemSucursales = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Sucursales ItemSucursales
    {
      get { if (m_itemSucursales == null) { m_itemSucursales = Sucursales.Nuevo(); } return m_itemSucursales; }
      set { m_itemSucursales = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"SucursalesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Interno=" + SUCU_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + "," + SUCU_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemSucursales.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemSucursales.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 3)]
    public String SUCU_Nombre_Display { get { return ItemSucursales.SUCU_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 4)]
    public String SUCU_Direccion_Display { get { return ItemSucursales.SUCU_Direccion; } }

    [Display(AutoGenerateField = false, Name = "Correo", Description = "Correo", ShortName = "Correo", Order = 5)]
    public String SUCU_Correo_Display { get { return ItemSucursales.SUCU_Correo; } }

    [Display(AutoGenerateField = false, Name = "Telefono", Description = "Telefono", ShortName = "Telefono", Order = 6)]
    public String SUCU_Telefono_Display { get { return ItemSucursales.SUCU_Telefono; } }

    [Display(AutoGenerateField = false, Name = "Web", Description = "Web", ShortName = "Web", Order = 7)]
    public String SUCU_Web_Display { get { return ItemSucursales.SUCU_Web; } }

    [Display(AutoGenerateField = false, Name = "Principal", Description = "Principal", ShortName = "Principal", Order = 8)]
    public Boolean SUCU_Principal_Display { get { return ItemSucursales.SUCU_Principal; } }

    #endregion

    #region [ METODOS ]

    public static List<Sucursales_Display> ToList(ObservableCollection<Sucursales> Items)
    {
      List<Sucursales_Display> _listSucursales = new List<Sucursales_Display>();

      foreach (Sucursales item in Items)
      { _listSucursales.Add(new Sucursales_Display(item)); }

      return _listSucursales;
    }

    public static DataTable ToDataTable(ObservableCollection<Sucursales> Items)
    {
      DataTable dt_Sucursales = new DataTable("Sucursales");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Sucursales.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int64"));
      dc_SUCU_Interno.Caption = "Interno";
      dt_Sucursales.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_SUCU_Nombre = new DataColumn("SUCU_Nombre", System.Type.GetType("System.String"));
      dc_SUCU_Nombre.Caption = "Nombre";
      dt_Sucursales.Columns.Add(dc_SUCU_Nombre);
      DataColumn dc_SUCU_Direccion = new DataColumn("SUCU_Direccion", System.Type.GetType("System.String"));
      dc_SUCU_Direccion.Caption = "Direccion";
      dt_Sucursales.Columns.Add(dc_SUCU_Direccion);
      DataColumn dc_SUCU_Correo = new DataColumn("SUCU_Correo", System.Type.GetType("System.String"));
      dc_SUCU_Correo.Caption = "Correo";
      dt_Sucursales.Columns.Add(dc_SUCU_Correo);
      DataColumn dc_SUCU_Telefono = new DataColumn("SUCU_Telefono", System.Type.GetType("System.String"));
      dc_SUCU_Telefono.Caption = "Telefono";
      dt_Sucursales.Columns.Add(dc_SUCU_Telefono);
      DataColumn dc_SUCU_Web = new DataColumn("SUCU_Web", System.Type.GetType("System.String"));
      dc_SUCU_Web.Caption = "Web";
      dt_Sucursales.Columns.Add(dc_SUCU_Web);
      DataColumn dc_SUCU_Principal = new DataColumn("SUCU_Principal", System.Type.GetType("System.Boolean"));
      dc_SUCU_Principal.Caption = "Principal";
      dt_Sucursales.Columns.Add(dc_SUCU_Principal);

      foreach (Sucursales item in Items)
      {
        DataRow dr_Sucursales = dt_Sucursales.NewRow();

        dr_Sucursales[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Sucursales[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Sucursales[dc_SUCU_Nombre] = item.SUCU_Nombre;
        dr_Sucursales[dc_SUCU_Direccion] = item.SUCU_Direccion;
        dr_Sucursales[dc_SUCU_Correo] = item.SUCU_Correo;
        dr_Sucursales[dc_SUCU_Telefono] = item.SUCU_Telefono;
        dr_Sucursales[dc_SUCU_Web] = item.SUCU_Web;
        dr_Sucursales[dc_SUCU_Principal] = item.SUCU_Principal;

        dt_Sucursales.Rows.Add(dr_Sucursales);
      }

      return dt_Sucursales;
    }

    #endregion
  }
}
