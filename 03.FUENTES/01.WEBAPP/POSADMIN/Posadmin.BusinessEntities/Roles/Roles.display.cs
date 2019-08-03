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
  public partial class Roles_Display
  {
    #region [ VARIABLES ]

    private Roles m_itemRoles;

    #endregion

    #region [ CONSTRUCTOR ]

    public Roles_Display(Roles Item)
    { ItemRoles = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Roles ItemRoles
    {
      get { if (m_itemRoles == null) { m_itemRoles = Roles.Nuevo(); } return m_itemRoles; }
      set { m_itemRoles = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"RolesRegister?Interno=" + ROLE_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + ROLE_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Rol Interno", Description = "Rol Interno", ShortName = "Rol Interno", Order = 1)]
    public Int32 ROLE_Interno_Display { get { return ItemRoles.ROLE_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String ROLE_Nombre_Display { get { return ItemRoles.ROLE_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 3)]
    public String ROLE_Descripcion_Display { get { return ItemRoles.ROLE_Descripcion; } }

    #endregion

    #region [ METODOS ]

    public static List<Roles_Display> ToList(ObservableCollection<Roles> Items)
    {
      List<Roles_Display> l_listRoles = new List<Roles_Display>();

      foreach (Roles item in Items)
      { l_listRoles.Add(new Roles_Display(item)); }

      return l_listRoles;
    }

    public static DataTable ToDataTable(ObservableCollection<Roles> Items)
    {
      DataTable dt_Roles = new DataTable("Roles");

      DataColumn dc_ROLE_Interno = new DataColumn("ROLE_Interno", System.Type.GetType("System.Int32"));
      dc_ROLE_Interno.Caption = "Rol Interno";
      dt_Roles.Columns.Add(dc_ROLE_Interno);
      DataColumn dc_ROLE_Nombre = new DataColumn("ROLE_Nombre", System.Type.GetType("System.String"));
      dc_ROLE_Nombre.Caption = "Nombre";
      dt_Roles.Columns.Add(dc_ROLE_Nombre);
      DataColumn dc_ROLE_Descripcion = new DataColumn("ROLE_Descripcion", System.Type.GetType("System.String"));
      dc_ROLE_Descripcion.Caption = "Descripcion";
      dt_Roles.Columns.Add(dc_ROLE_Descripcion);

      foreach (Roles item in Items)
      {
        DataRow dr_Roles = dt_Roles.NewRow();

        dr_Roles[dc_ROLE_Interno] = item.ROLE_Interno;
        dr_Roles[dc_ROLE_Nombre] = item.ROLE_Nombre;
        dr_Roles[dc_ROLE_Descripcion] = item.ROLE_Descripcion;

        dt_Roles.Rows.Add(dr_Roles);
      }

      return dt_Roles;
    }

    #endregion
  }
}
