using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  public partial class Usuarios_Display
  {
    #region [ VARIABLES ]

    private Usuarios m_item_Usuarios;

    #endregion

    #region [ CONSTRUCTOR ]

    public Usuarios_Display(Usuarios Item)
    { Item_Usuarios = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Usuarios Item_Usuarios
    {
      get { if (m_item_Usuarios == null) { m_item_Usuarios = Usuarios.Nuevo(); } return m_item_Usuarios; }
      set { m_item_Usuarios = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"UsuariosRegister?Interno=" + USUA_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + USUA_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Usuario Interno", Description = "Usuario Interno", ShortName = "Usuario Interno", Order = 1)]
    public Int64 USUA_Interno_Display { get { return Item_Usuarios.USUA_Interno; } }

    [Display(AutoGenerateField = false, Name = "Rol Interno", Description = "Rol Interno", ShortName = "Rol Interno", Order = 2)]
    public Int32 ROLE_Interno_Display { get { return Item_Usuarios.ROLE_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 3)]
    public String USUA_NombreCompleto_Display { get { return Item_Usuarios.USUA_NombreCompleto; } }

    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 4)]
    public String USUA_Usuario_Display { get { return Item_Usuarios.USUA_Usuario; } }

    [Display(AutoGenerateField = false, Name = "Contrasena", Description = "Contrasena", ShortName = "Contrasena", Order = 5)]
    public String USUA_Contrasena_Display { get { return Item_Usuarios.USUA_Contrasena; } }

    [Display(AutoGenerateField = false, Name = "Correo", Description = "Correo", ShortName = "Correo", Order = 6)]
    public String USUA_Correo_Display { get { return Item_Usuarios.USUA_Correo; } }

    [Display(AutoGenerateField = false, Name = "Rol", Description = "Rol", ShortName = "Rol", Order = 7)]
    public String USUA_RolNombre_Display { get { return Item_Usuarios.USUA_RolNombre; } }

    #endregion

    #region [ METODOS ]

    public static List<Usuarios_Display> ToList(ObservableCollection<Usuarios> Items)
    {
      List<Usuarios_Display> l_Items = new List<Usuarios_Display>();

      foreach (Usuarios item in Items)
      { l_Items.Add(new Usuarios_Display(item)); }

      return l_Items;
    }
    public static DataTable ToDataTable(ObservableCollection<Usuarios> Items)
    {
      DataTable dt_Usuarios = new DataTable("Usuarios");

      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", Type.GetType("System.Int64"));
      dc_USUA_Interno.Caption = "Usuario Interno";
      dt_Usuarios.Columns.Add(dc_USUA_Interno);
      DataColumn dc_ROLE_Interno = new DataColumn("ROLE_Interno", Type.GetType("System.Int32"));
      dc_ROLE_Interno.Caption = "Rol Interno";
      dt_Usuarios.Columns.Add(dc_ROLE_Interno);
      DataColumn dc_USUA_NombreCompleto = new DataColumn("USUA_NombreCompleto", Type.GetType("System.String"));
      dc_USUA_NombreCompleto.Caption = "Nombre Completo";
      dt_Usuarios.Columns.Add(dc_USUA_NombreCompleto);
      DataColumn dc_USUA_Usuario = new DataColumn("USUA_Usuario", Type.GetType("System.String"));
      dc_USUA_Usuario.Caption = "Usuario";
      dt_Usuarios.Columns.Add(dc_USUA_Usuario);
      DataColumn dc_USUA_Contrasena = new DataColumn("USUA_Contrasena", Type.GetType("System.String"));
      dc_USUA_Contrasena.Caption = "Contrasena";
      dt_Usuarios.Columns.Add(dc_USUA_Contrasena);
      DataColumn dc_USUA_Correo = new DataColumn("USUA_Correo", Type.GetType("System.String"));
      dc_USUA_Correo.Caption = "Correo";
      dt_Usuarios.Columns.Add(dc_USUA_Correo);

      foreach (Usuarios item in Items)
      {
        DataRow dr_Usuarios = dt_Usuarios.NewRow();

        dr_Usuarios[dc_USUA_Interno] = item.USUA_Interno;
        dr_Usuarios[dc_ROLE_Interno] = item.ROLE_Interno;
        dr_Usuarios[dc_USUA_NombreCompleto] = item.USUA_NombreCompleto;
        dr_Usuarios[dc_USUA_Usuario] = item.USUA_Usuario;
        dr_Usuarios[dc_USUA_Contrasena] = item.USUA_Contrasena;
        dr_Usuarios[dc_USUA_Correo] = item.USUA_Correo;

        dt_Usuarios.Rows.Add(dr_Usuarios);
      }

      return dt_Usuarios;
    }

    #endregion
  }
}
