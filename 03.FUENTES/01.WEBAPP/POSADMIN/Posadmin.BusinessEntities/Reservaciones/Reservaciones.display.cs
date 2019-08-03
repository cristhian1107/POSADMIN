using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SoftwareFactory.Infrastructure.Utilities;

namespace Posadmin.BusinessEntities
{
  public partial class Reservaciones_Display
  {
    #region [ VARIABLES ]

    private Reservaciones m_itemReservaciones;

    #endregion

    #region [ CONSTRUCTOR ]

    public Reservaciones_Display(Reservaciones Item)
    { ItemReservaciones = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Reservaciones ItemReservaciones
    {
      get { if (m_itemReservaciones == null) { m_itemReservaciones = Reservaciones.Nuevo(); } return m_itemReservaciones; }
      set { m_itemReservaciones = value; }
    }

    public String Buttons
    {
      get
      {
        string l_buttons = null;
        string l_editButton = "<a href=\"ReservacionesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Sucursal=" + SUCU_Interno_Display.ToString() + "&Interno=" + RESE_Interno_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        string l_viewButton = "<a href=\"ReservacionesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Sucursal=" + SUCU_Interno_Display.ToString() + "&Interno=" + RESE_Interno_Display + "\" title=\"Ver\" class=\"btn btn-primary btn-xs\" ><i class=\"fa fa-search\"></i></a>";
        string l_cancelButton = "<a data-toggle=\"modal\" onclick =\"CancelItem(" + EMPR_Interno_Display.ToString() + "," + SUCU_Interno_Display.ToString() + "," + RESE_Interno_Display.ToString() + ")\" title=\"Anular\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-ban\" ></i></a>";
        string l_registrarButton = "<a href=\"../Registros/RegistrosRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Sucursal=" + SUCU_Interno_Display.ToString() + "&Registro=0" + "&Habitacion=" + HABI_Interno_Display + "&Huesped=" + string.Format("{0}|{1}|{2}", RESE_HuespedId_Display, RESE_HuespedNombreCompleto_Display, RESE_HuespedDireccion_Display) + "&Reserva=" + RESE_Interno_Display + "\" title=\"Registrar\" class=\"btn bg-color-green btn-primary btn-xs\" ><i class=\"fa fa-book\"></i></a>";

        switch (RESE_Estado_Display)
        {
          case "A":
            if (MyUtilities.GetDateTimeCountry("es-PE").Date >= ItemReservaciones.RESE_FechaInicio.Date && MyUtilities.GetDateTimeCountry("es-PE").Date <= ItemReservaciones.RESE_FechaFin.Date)
            { l_buttons = l_editButton + " " + l_registrarButton + " " + l_cancelButton; }
            else
            { l_buttons = l_editButton + " " + l_cancelButton; }
            break;
          case "X":
            l_buttons = l_viewButton;
            break;
          case "E":
            l_buttons = l_viewButton;
            break;
          default:
            l_buttons = l_viewButton;
            break;
        }
        return l_buttons;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemReservaciones.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemReservaciones.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 RESE_Interno_Display { get { return ItemReservaciones.RESE_Interno; } }

    [Display(AutoGenerateField = false, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 4)]
    public String RESE_Estado_Display { get { return ItemReservaciones.RESE_Estado; } }

    [Display(AutoGenerateField = false, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 5)]
    public Int64 HABI_Interno_Display { get { return ItemReservaciones.HABI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Fecha Inicio", Description = "Fecha Inicio", ShortName = "Fecha Inicio", Order = 6)]
    public String RESE_FechaInicio_Display { get { return ItemReservaciones.RESE_FechaInicio.ToString("dd/MM/yyyy"); } }

    [Display(AutoGenerateField = false, Name = "Fecha Fin", Description = "Fecha Fin", ShortName = "Fecha Fin", Order = 7)]
    public String RESE_FechaFin_Display { get { return ItemReservaciones.RESE_FechaFin.ToString("dd/MM/yyyy"); } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 8)]
    public DateTime RESE_FechaHoraRegistro_Display { get { return ItemReservaciones.RESE_FechaHoraRegistro; } }

    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 9)]
    public String RESE_HuespedId_Display { get { return ItemReservaciones.RESE_HuespedId; } }

    [Display(AutoGenerateField = false, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 10)]
    public String RESE_HuespedNombreCompleto_Display { get { return ItemReservaciones.RESE_HuespedNombreCompleto; } }

    [Display(AutoGenerateField = false, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 11)]
    public String RESE_HuespedDireccion_Display { get { return ItemReservaciones.RESE_HuespedDireccion; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 12)]
    public String RESE_Descripcion_Display { get { return ItemReservaciones.RESE_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 13)]
    public String RESE_NombreEstado_Display { get { return ItemReservaciones.RESE_NombreEstado; } }

    [Display(AutoGenerateField = false, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 14)]
    public String RESE_Habitacion_Display { get { return ItemReservaciones.RESE_Habitacion; } }

    [Display(AutoGenerateField = false, Name = "Tipo Habitacion", Description = "Tipo Habitacion", ShortName = "Tipo Habitacion", Order = 15)]
    public String RESE_TipoHabitacion_Display { get { return ItemReservaciones.TABL_NombreTHA; } }

    [Display(AutoGenerateField = false, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 16)]
    public String RESE_HabitacionDescripcion_Display { get { return string.Format("{0} - {1} <br /> {2}", ItemReservaciones.TABL_NombrePIS, ItemReservaciones.RESE_Habitacion, ItemReservaciones.TABL_NombreTHA); } }

    [Display(AutoGenerateField = false, Name = "Fechas", Description = "Fechas", ShortName = "Fechas", Order = 17)]
    public String RESE_Fechas_Display { get { return string.Format("FEC. INICIO: {0:dd/MM/yyyy} <br /> FEC. FIN: {1:dd/MM/yyyy}", ItemReservaciones.RESE_FechaInicio, ItemReservaciones.RESE_FechaFin); } }

    #endregion

    #region [ METODOS ]

    public static List<Reservaciones_Display> ToList(ObservableCollection<Reservaciones> x_listReservaciones)
    {
      List<Reservaciones_Display> _listReservaciones = new List<Reservaciones_Display>();

      foreach (Reservaciones item in x_listReservaciones)
      { _listReservaciones.Add(new Reservaciones_Display(item)); }

      return _listReservaciones;
    }

    public static DataTable ToDataTable(ObservableCollection<Reservaciones> x_listReservaciones)
    {
      DataTable dt_Reservaciones = new DataTable("Reservaciones");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int32"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Reservaciones.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int32"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Reservaciones.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_RESE_Interno = new DataColumn("RESE_Interno", System.Type.GetType("System.Int32"));
      dc_RESE_Interno.Caption = "Interno";
      dt_Reservaciones.Columns.Add(dc_RESE_Interno);
      DataColumn dc_RESE_Estado = new DataColumn("RESE_Estado", System.Type.GetType("System.Int32"));
      dc_RESE_Estado.Caption = "Estado";
      dt_Reservaciones.Columns.Add(dc_RESE_Estado);
      DataColumn dc_HABI_Interno = new DataColumn("HABI_Interno", System.Type.GetType("System.Int32"));
      dc_HABI_Interno.Caption = "Habitacion";
      dt_Reservaciones.Columns.Add(dc_HABI_Interno);
      DataColumn dc_RESE_FechaInicio = new DataColumn("RESE_FechaInicio", System.Type.GetType("System.Int32"));
      dc_RESE_FechaInicio.Caption = "Fecha Inicio";
      dt_Reservaciones.Columns.Add(dc_RESE_FechaInicio);
      DataColumn dc_RESE_FechaFin = new DataColumn("RESE_FechaFin", System.Type.GetType("System.Int32"));
      dc_RESE_FechaFin.Caption = "Fecha Fin";
      dt_Reservaciones.Columns.Add(dc_RESE_FechaFin);
      DataColumn dc_RESE_FechaHoraRegistro = new DataColumn("RESE_FechaHoraRegistro", System.Type.GetType("System.Int32"));
      dc_RESE_FechaHoraRegistro.Caption = "Fecha Hora Registro";
      dt_Reservaciones.Columns.Add(dc_RESE_FechaHoraRegistro);
      DataColumn dc_RESE_HuespedId = new DataColumn("RESE_HuespedId", System.Type.GetType("System.Int32"));
      dc_RESE_HuespedId.Caption = "Id";
      dt_Reservaciones.Columns.Add(dc_RESE_HuespedId);
      DataColumn dc_RESE_HuespedNombreCompleto = new DataColumn("RESE_HuespedNombreCompleto", System.Type.GetType("System.Int32"));
      dc_RESE_HuespedNombreCompleto.Caption = "Nombre Completo";
      dt_Reservaciones.Columns.Add(dc_RESE_HuespedNombreCompleto);
      DataColumn dc_RESE_HuespedDireccion = new DataColumn("RESE_HuespedDireccion", System.Type.GetType("System.Int32"));
      dc_RESE_HuespedDireccion.Caption = "Direccion";
      dt_Reservaciones.Columns.Add(dc_RESE_HuespedDireccion);
      DataColumn dc_RESE_Descripcion = new DataColumn("RESE_Descripcion", System.Type.GetType("System.Int32"));
      dc_RESE_Descripcion.Caption = "Descripcion";
      dt_Reservaciones.Columns.Add(dc_RESE_Descripcion);

      foreach (Reservaciones item in x_listReservaciones)
      {
        DataRow dr_Reservaciones = dt_Reservaciones.NewRow();

        dr_Reservaciones[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Reservaciones[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Reservaciones[dc_RESE_Interno] = item.RESE_Interno;
        dr_Reservaciones[dc_RESE_Estado] = item.RESE_Estado;
        dr_Reservaciones[dc_HABI_Interno] = item.HABI_Interno;
        dr_Reservaciones[dc_RESE_FechaInicio] = item.RESE_FechaInicio;
        dr_Reservaciones[dc_RESE_FechaFin] = item.RESE_FechaFin;
        dr_Reservaciones[dc_RESE_FechaHoraRegistro] = item.RESE_FechaHoraRegistro;
        dr_Reservaciones[dc_RESE_HuespedId] = item.RESE_HuespedId;
        dr_Reservaciones[dc_RESE_HuespedNombreCompleto] = item.RESE_HuespedNombreCompleto;
        dr_Reservaciones[dc_RESE_HuespedDireccion] = item.RESE_HuespedDireccion;
        dr_Reservaciones[dc_RESE_Descripcion] = item.RESE_Descripcion;

        dt_Reservaciones.Rows.Add(dr_Reservaciones);
      }

      return dt_Reservaciones;
    }

    #endregion
  }
}
