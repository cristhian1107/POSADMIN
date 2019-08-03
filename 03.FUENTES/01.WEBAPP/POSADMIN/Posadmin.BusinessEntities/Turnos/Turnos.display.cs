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
  public partial class Turnos_Display
  {
    #region [ VARIABLES ]

    private Turnos m_itemTurnos;

    #endregion

    #region [ CONSTRUCTOR ]

    public Turnos_Display(Turnos Item)
    { ItemTurnos = Item; }
    
    #endregion

    #region [ PROPIEDADES ]

    private Turnos ItemTurnos
    {
      get { if (m_itemTurnos == null) { m_itemTurnos = Turnos.Nuevo(); } return m_itemTurnos; }
      set { m_itemTurnos = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"TurnosRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Sucursal=" + SUCU_Interno_Display.ToString() + "&Interno=" + TURN_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + "," + SUCU_Interno_Display.ToString() + "," + TURN_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemTurnos.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemTurnos.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 TURN_Interno_Display { get { return ItemTurnos.TURN_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nominacion", Description = "Nominacion", ShortName = "Nominacion", Order = 4)]
    public String TURN_Nominacion_Display { get { return ItemTurnos.TURN_Nominacion; } }

    [Display(AutoGenerateField = false, Name = "Hora Inicio", Description = "Hora Inicio", ShortName = "Hora Inicio", Order = 5)]
    public String TURN_HoraInicio_Display { get { return ItemTurnos.TURN_HoraInicio.ToString("HH:mm"); } }

    [Display(AutoGenerateField = false, Name = "Hora Fin", Description = "Hora Fin", ShortName = "Hora Fin", Order = 6)]
    public String TURN_HoraFin_Display { get { return ItemTurnos.TURN_HoraFin.ToString("HH:mm"); } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 7)]
    public String TURN_Descripcion_Display { get { return ItemTurnos.TURN_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Color", Description = "Color", ShortName = "Color", Order = 8)]
    public String TURN_Color_Display { get { return ItemTurnos.TURN_Color; } }

    [Display(AutoGenerateField = false, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 8)]
    public Boolean TURN_Activo_Display { get { return ItemTurnos.TURN_Activo; } }

    [Display(AutoGenerateField = false, Name = "Sucursal", Description = "Sucursal", ShortName = "Sucursal", Order = 8)]
    public String TURN_Sucursal_Display { get { return ItemTurnos.TURN_Sucursal; } }

    #endregion

    #region [ METODOS ]

    public static List<Turnos_Display> ToList(ObservableCollection<Turnos> Items)
    {
      List<Turnos_Display> l_listTurnos = new List<Turnos_Display>();

      foreach (Turnos item in Items)
      { l_listTurnos.Add(new Turnos_Display(item)); }

      return l_listTurnos;
    }

    public static DataTable ToDataTable(ObservableCollection<Turnos> Items)
    {
      DataTable dt_Turnos = new DataTable("Turnos");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Turnos.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int64"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Turnos.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_TURN_Interno = new DataColumn("TURN_Interno", System.Type.GetType("System.Int64"));
      dc_TURN_Interno.Caption = "Interno";
      dt_Turnos.Columns.Add(dc_TURN_Interno);
      DataColumn dc_TURN_Nominacion = new DataColumn("TURN_Nominacion", System.Type.GetType("System.String"));
      dc_TURN_Nominacion.Caption = "Nominacion";
      dt_Turnos.Columns.Add(dc_TURN_Nominacion);
      DataColumn dc_TURN_HoraInicio = new DataColumn("TURN_HoraInicio", System.Type.GetType("System.DateTime"));
      dc_TURN_HoraInicio.Caption = "Hora Inicio";
      dt_Turnos.Columns.Add(dc_TURN_HoraInicio);
      DataColumn dc_TURN_HoraFin = new DataColumn("TURN_HoraFin", System.Type.GetType("System.DateTime"));
      dc_TURN_HoraFin.Caption = "Hora Fin";
      dt_Turnos.Columns.Add(dc_TURN_HoraFin);
      DataColumn dc_TURN_Descripcion = new DataColumn("TURN_Descripcion", System.Type.GetType("System.String"));
      dc_TURN_Descripcion.Caption = "Descripcion";
      dt_Turnos.Columns.Add(dc_TURN_Descripcion);
      DataColumn dc_TURN_Color = new DataColumn("TURN_Color", System.Type.GetType("System.String"));
      dc_TURN_Color.Caption = "Color";
      dt_Turnos.Columns.Add(dc_TURN_Color);
      DataColumn dc_TURN_Activo = new DataColumn("TURN_Activo", System.Type.GetType("System.Boolean"));
      dc_TURN_Activo.Caption = "Activo";
      dt_Turnos.Columns.Add(dc_TURN_Activo);

      foreach (Turnos item in Items)
      {
        DataRow dr_Turnos = dt_Turnos.NewRow();

        dr_Turnos[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Turnos[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Turnos[dc_TURN_Interno] = item.TURN_Interno;
        dr_Turnos[dc_TURN_Nominacion] = item.TURN_Nominacion;
        dr_Turnos[dc_TURN_HoraInicio] = item.TURN_HoraInicio;
        dr_Turnos[dc_TURN_HoraFin] = item.TURN_HoraFin;
        dr_Turnos[dc_TURN_Descripcion] = item.TURN_Descripcion;
        dr_Turnos[dc_TURN_Color] = item.TURN_Color;
        dr_Turnos[dc_TURN_Activo] = item.TURN_Activo;

        dt_Turnos.Rows.Add(dr_Turnos);
      }

      return dt_Turnos;
    }

    #endregion
  }
}
