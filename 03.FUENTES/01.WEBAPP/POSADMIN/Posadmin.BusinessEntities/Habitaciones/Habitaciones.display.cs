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
  public partial class Habitaciones_Display
  {
    #region [ VARIABLES ]

    private Habitaciones m_itemHabitaciones;

    #endregion

    #region [ CONSTRUCTOR ]

    public Habitaciones_Display(Habitaciones Item)
    { ItemHabitaciones = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Habitaciones ItemHabitaciones
    {
      get { if (m_itemHabitaciones == null) { m_itemHabitaciones = Habitaciones.Nuevo(); } return m_itemHabitaciones; }
      set { m_itemHabitaciones = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"HabitacionesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Sucursal=" + SUCU_Interno_Display.ToString() + "&Interno=" + HABI_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + "," + SUCU_Interno_Display.ToString() + "," + HABI_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemHabitaciones.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemHabitaciones.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 HABI_Interno_Display { get { return ItemHabitaciones.HABI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 4)]
    public String TABL_TablaPIS_Display { get { return ItemHabitaciones.TABL_TablaPIS; } }

    [Display(AutoGenerateField = false, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 5)]
    public String TABL_InternoPIS_Display { get { return ItemHabitaciones.TABL_InternoPIS; } }

    [Display(AutoGenerateField = false, Name = "Piso", Description = "Piso", ShortName = "Piso", Order = 5)]
    public String TABL_NombrePIS_Display { get { return ItemHabitaciones.TABL_NombrePIS; } }

    [Display(AutoGenerateField = false, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 6)]
    public String TABL_TablaTHA_Display { get { return ItemHabitaciones.TABL_TablaTHA; } }

    [Display(AutoGenerateField = false, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 7)]
    public String TABL_InternoTHA_Display { get { return ItemHabitaciones.TABL_InternoTHA; } }

    [Display(AutoGenerateField = false, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 7)]
    public String TABL_NombreTHA_Display { get { return ItemHabitaciones.TABL_NombreTHA; } }

    [Display(AutoGenerateField = false, Name = "Numero", Description = "Numero", ShortName = "Numero", Order = 8)]
    public String HABI_Numero_Display { get { return ItemHabitaciones.HABI_Numero; } }

    [Display(AutoGenerateField = false, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 9)]
    public String HABI_Estado_Display { get { return ItemHabitaciones.HABI_Estado; } }

    [Display(AutoGenerateField = false, Name = "Limpio", Description = "Limpio", ShortName = "Limpio", Order = 10)]
    public Boolean HABI_Limpio_Display { get { return ItemHabitaciones.HABI_Limpio; } }

    [Display(AutoGenerateField = false, Name = "Activo", Description = "Activo", ShortName = "Activo", Order = 11)]
    public Boolean HABI_Activo_Display { get { return ItemHabitaciones.HABI_Activo; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 12)]
    public String HABI_Descripcion_Display { get { return ItemHabitaciones.HABI_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Precio Dia", Description = "Precio Dia", ShortName = "Precio Dia", Order = 13)]
    public Double HABI_PrecioDia_Display { get { return ItemHabitaciones.HABI_PrecioDia; } }

    [Display(AutoGenerateField = false, Name = "Precio Hora", Description = "Precio Hora", ShortName = "Precio Hora", Order = 14)]
    public Double HABI_PrecioHora_Display { get { return ItemHabitaciones.HABI_PrecioHora; } }

    [Display(AutoGenerateField = false, Name = "Precio Minimo", Description = "Precio Minimo", ShortName = "Precio Minimo", Order = 15)]
    public Double HABI_PrecioMinimo_Display { get { return ItemHabitaciones.HABI_PrecioMinimo; } }

    #endregion

    #region [ METODOS ]

    public static List<Habitaciones_Display> ToList(ObservableCollection<Habitaciones> x_listHabitaciones)
    {
      List<Habitaciones_Display> _listHabitaciones = new List<Habitaciones_Display>();

      foreach (Habitaciones item in x_listHabitaciones)
      { _listHabitaciones.Add(new Habitaciones_Display(item)); }

      return _listHabitaciones;
    }

    public static DataTable ToDataTable(ObservableCollection<Habitaciones> x_listHabitaciones)
    {
      DataTable dt_Habitaciones = new DataTable("Habitaciones");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Habitaciones.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int64"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Habitaciones.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_HABI_Interno = new DataColumn("HABI_Interno", System.Type.GetType("System.Int64"));
      dc_HABI_Interno.Caption = "Interno";
      dt_Habitaciones.Columns.Add(dc_HABI_Interno);
      DataColumn dc_TABL_TablaPIS = new DataColumn("TABL_TablaPIS", System.Type.GetType("System.String"));
      dc_TABL_TablaPIS.Caption = "Piso";
      dt_Habitaciones.Columns.Add(dc_TABL_TablaPIS);
      DataColumn dc_TABL_InternoPIS = new DataColumn("TABL_InternoPIS", System.Type.GetType("System.String"));
      dc_TABL_InternoPIS.Caption = "Piso";
      dt_Habitaciones.Columns.Add(dc_TABL_InternoPIS);
      DataColumn dc_TABL_TablaTHA = new DataColumn("TABL_TablaTHA", System.Type.GetType("System.String"));
      dc_TABL_TablaTHA.Caption = "Tipo Habitacion";
      dt_Habitaciones.Columns.Add(dc_TABL_TablaTHA);
      DataColumn dc_TABL_InternoTHA = new DataColumn("TABL_InternoTHA", System.Type.GetType("System.String"));
      dc_TABL_InternoTHA.Caption = "Tipo Habitacion";
      dt_Habitaciones.Columns.Add(dc_TABL_InternoTHA);
      DataColumn dc_HABI_Numero = new DataColumn("HABI_Numero", System.Type.GetType("System.String"));
      dc_HABI_Numero.Caption = "Numero";
      dt_Habitaciones.Columns.Add(dc_HABI_Numero);
      DataColumn dc_HABI_Estado = new DataColumn("HABI_Estado", System.Type.GetType("System.String"));
      dc_HABI_Estado.Caption = "Estado";
      dt_Habitaciones.Columns.Add(dc_HABI_Estado);
      DataColumn dc_HABI_Limpio = new DataColumn("HABI_Limpio", System.Type.GetType("System.Boolean"));
      dc_HABI_Limpio.Caption = "Limpio";
      dt_Habitaciones.Columns.Add(dc_HABI_Limpio);
      DataColumn dc_HABI_Activo = new DataColumn("HABI_Activo", System.Type.GetType("System.Boolean"));
      dc_HABI_Activo.Caption = "Activo";
      dt_Habitaciones.Columns.Add(dc_HABI_Activo);
      DataColumn dc_HABI_Descripcion = new DataColumn("HABI_Descripcion", System.Type.GetType("System.String"));
      dc_HABI_Descripcion.Caption = "Descripcion";
      dt_Habitaciones.Columns.Add(dc_HABI_Descripcion);
      DataColumn dc_HABI_PrecioDia = new DataColumn("HABI_PrecioDia", System.Type.GetType("System.Double"));
      dc_HABI_PrecioDia.Caption = "Precio Dia";
      dt_Habitaciones.Columns.Add(dc_HABI_PrecioDia);
      DataColumn dc_HABI_PrecioHora = new DataColumn("HABI_PrecioHora", System.Type.GetType("System.Double"));
      dc_HABI_PrecioHora.Caption = "Precio Hora";
      dt_Habitaciones.Columns.Add(dc_HABI_PrecioHora);
      DataColumn dc_HABI_PrecioMinimo = new DataColumn("HABI_PrecioMinimo", System.Type.GetType("System.Double"));
      dc_HABI_PrecioMinimo.Caption = "Precio Minimo";
      dt_Habitaciones.Columns.Add(dc_HABI_PrecioMinimo);

      foreach (Habitaciones item in x_listHabitaciones)
      {
        DataRow dr_Habitaciones = dt_Habitaciones.NewRow();

        dr_Habitaciones[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Habitaciones[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Habitaciones[dc_HABI_Interno] = item.HABI_Interno;
        dr_Habitaciones[dc_TABL_TablaPIS] = item.TABL_TablaPIS;
        dr_Habitaciones[dc_TABL_InternoPIS] = item.TABL_InternoPIS;
        dr_Habitaciones[dc_TABL_TablaTHA] = item.TABL_TablaTHA;
        dr_Habitaciones[dc_TABL_InternoTHA] = item.TABL_InternoTHA;
        dr_Habitaciones[dc_HABI_Numero] = item.HABI_Numero;
        dr_Habitaciones[dc_HABI_Estado] = item.HABI_Estado;
        dr_Habitaciones[dc_HABI_Limpio] = item.HABI_Limpio;
        dr_Habitaciones[dc_HABI_Activo] = item.HABI_Activo;
        dr_Habitaciones[dc_HABI_Descripcion] = item.HABI_Descripcion;
        dr_Habitaciones[dc_HABI_PrecioDia] = item.HABI_PrecioDia;
        dr_Habitaciones[dc_HABI_PrecioHora] = item.HABI_PrecioHora;
        dr_Habitaciones[dc_HABI_PrecioMinimo] = item.HABI_PrecioMinimo;

        dt_Habitaciones.Rows.Add(dr_Habitaciones);
      }

      return dt_Habitaciones;
    }

    #endregion
  }
}
