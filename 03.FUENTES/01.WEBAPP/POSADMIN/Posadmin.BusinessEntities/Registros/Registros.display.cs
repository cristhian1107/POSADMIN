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
  public partial class Registros_Display
  {
    #region [ VARIABLES ]

    private Registros m_itemRegistros;

    #endregion

    #region [ CONSTRUCTOR ]

    public Registros_Display(Registros x_itemRegistros)
    { ItemRegistros = x_itemRegistros; }

    #endregion

    #region [ PROPIEDADES ]

    private Registros ItemRegistros
    {
      get { if (m_itemRegistros == null) { m_itemRegistros = Registros.Nuevo(); } return m_itemRegistros; }
      set { m_itemRegistros = value; }
    }

    public String Buttons
    {
      get
      {
        String _editButton = "<a href=\"RegistrosRegister?EMPR_Interno=" + EMPR_Interno_Display.ToString() + "\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String _deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + ")\"  class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-trash-alt\" ></i></a>";

        return _editButton + " " + _deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresas Interno", Description = "Empresas Interno", ShortName = "Empresas Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemRegistros.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemRegistros.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 REGI_Interno_Display { get { return ItemRegistros.REGI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Habitacion", Description = "Habitacion", ShortName = "Habitacion", Order = 4)]
    public Int64 HABI_Interno_Display { get { return ItemRegistros.HABI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Estado", Description = "Estado", ShortName = "Estado", Order = 5)]
    public String REGI_Estado_Display { get { return ItemRegistros.REGI_Estado; } }

    [Display(AutoGenerateField = false, Name = "Tramos", Description = "Tramos", ShortName = "Tramos", Order = 6)]
    public String REGI_Tramos_Display { get { return ItemRegistros.REGI_Tramos; } }

    [Display(AutoGenerateField = false, Name = "Cantidad", Description = "Cantidad", ShortName = "Cantidad", Order = 7)]
    public Int32 REGI_Cantidad_Display { get { return ItemRegistros.REGI_Cantidad; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Entrada", Description = "Fecha Hora Entrada", ShortName = "Fecha Hora Entrada", Order = 8)]
    public DateTime REGI_FechaHoraEntrada_Display { get { return ItemRegistros.REGI_FechaHoraEntrada; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Salida", Description = "Fecha Hora Salida", ShortName = "Fecha Hora Salida", Order = 9)]
    public DateTime REGI_FechaHoraSalida_Display { get { return ItemRegistros.REGI_FechaHoraSalida; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Salida Real", Description = "Fecha Hora Salida Real", ShortName = "Fecha Hora Salida Real", Order = 10)]
    public Nullable<DateTime> REGI_FechaHoraSalidaReal_Display { get { return ItemRegistros.REGI_FechaHoraSalidaReal; } }

    [Display(AutoGenerateField = false, Name = "Doc. Identidad", Description = "Doc. Identidad", ShortName = "Doc. Identidad", Order = 11)]
    public String TABL_TablaTDI_Display { get { return ItemRegistros.TABL_TablaTDI; } }

    [Display(AutoGenerateField = false, Name = "Doc. Identidad", Description = "Doc. Identidad", ShortName = "Doc. Identidad", Order = 12)]
    public String TABL_InternoTDI_Display { get { return ItemRegistros.TABL_InternoTDI; } }

    [Display(AutoGenerateField = false, Name = "Entidad", Description = "Entidad", ShortName = "Entidad", Order = 13)]
    public Int64 ENTI_Interno_Display { get { return ItemRegistros.ENTI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 14)]
    public String REGI_HuespedId_Display { get { return ItemRegistros.REGI_HuespedId; } }

    [Display(AutoGenerateField = false, Name = "Nombre Completo", Description = "Nombre Completo", ShortName = "Nombre Completo", Order = 15)]
    public String REGI_HuespedNombreCompleto_Display { get { return ItemRegistros.REGI_HuespedNombreCompleto; } }

    [Display(AutoGenerateField = false, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 16)]
    public String REGI_HuespedDireccion_Display { get { return ItemRegistros.REGI_HuespedDireccion; } }

    [Display(AutoGenerateField = false, Name = "Precio Sugerido", Description = "Precio Sugerido", ShortName = "Precio Sugerido", Order = 17)]
    public Double REGI_PrecioSugerido_Display { get { return ItemRegistros.REGI_PrecioSugerido; } }

    [Display(AutoGenerateField = false, Name = "Precio Cobrado", Description = "Precio Cobrado", ShortName = "Precio Cobrado", Order = 18)]
    public Double REGI_PrecioCobrado_Display { get { return ItemRegistros.REGI_PrecioCobrado; } }

    [Display(AutoGenerateField = false, Name = "Motivo Anulacion", Description = "Motivo Anulacion", ShortName = "Motivo Anulacion", Order = 19)]
    public String REGI_MotivoAnulacion_Display { get { return ItemRegistros.REGI_MotivoAnulacion; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Anulacion", Description = "Fecha Hora Anulacion", ShortName = "Fecha Hora Anulacion", Order = 20)]
    public Nullable<DateTime> REGI_FechaHoraAnulacion_Display { get { return ItemRegistros.REGI_FechaHoraAnulacion; } }

    [Display(AutoGenerateField = false, Name = "Turno", Description = "Turno", ShortName = "Turno", Order = 21)]
    public Int64 TURN_Interno_Display { get { return ItemRegistros.TURN_Interno; } }

    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 22)]
    public Int64 USUA_Interno_Display { get { return ItemRegistros.USUA_Interno; } }

    #endregion

    #region [ METODOS ]

    public static List<Registros_Display> ToList(ObservableCollection<Registros> Items)
    {
      List<Registros_Display> l_items = new List<Registros_Display>();

      foreach (Registros item in Items)
      { l_items.Add(new Registros_Display(item)); }

      return l_items;
    }

    public static DataTable ToDataTable(ObservableCollection<Registros> Items)
    {
      DataTable dt_Registros = new DataTable("Registros");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Registros.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int64"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Registros.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_REGI_Interno = new DataColumn("REGI_Interno", System.Type.GetType("System.Int64"));
      dc_REGI_Interno.Caption = "Interno";
      dt_Registros.Columns.Add(dc_REGI_Interno);
      DataColumn dc_HABI_Interno = new DataColumn("HABI_Interno", System.Type.GetType("System.Int64"));
      dc_HABI_Interno.Caption = "Habitacion";
      dt_Registros.Columns.Add(dc_HABI_Interno);
      DataColumn dc_REGI_Estado = new DataColumn("REGI_Estado", System.Type.GetType("System.String"));
      dc_REGI_Estado.Caption = "Estado";
      dt_Registros.Columns.Add(dc_REGI_Estado);
      DataColumn dc_REGI_Tramos = new DataColumn("REGI_Tramos", System.Type.GetType("System.String"));
      dc_REGI_Tramos.Caption = "Tramos";
      dt_Registros.Columns.Add(dc_REGI_Tramos);
      DataColumn dc_REGI_Cantidad = new DataColumn("REGI_Cantidad", System.Type.GetType("System.Int32"));
      dc_REGI_Cantidad.Caption = "Cantidad";
      dt_Registros.Columns.Add(dc_REGI_Cantidad);
      DataColumn dc_REGI_FechaHoraEntrada = new DataColumn("REGI_FechaHoraEntrada", System.Type.GetType("System.DateTime"));
      dc_REGI_FechaHoraEntrada.Caption = "Fecha Hora Entrada";
      dt_Registros.Columns.Add(dc_REGI_FechaHoraEntrada);
      DataColumn dc_REGI_FechaHoraSalida = new DataColumn("REGI_FechaHoraSalida", System.Type.GetType("System.DateTime"));
      dc_REGI_FechaHoraSalida.Caption = "Fecha Hora Salida";
      dt_Registros.Columns.Add(dc_REGI_FechaHoraSalida);
      DataColumn dc_REGI_FechaHoraSalidaReal = new DataColumn("REGI_FechaHoraSalidaReal", System.Type.GetType("System.DateTime"));
      dc_REGI_FechaHoraSalidaReal.Caption = "Fecha Hora Salida Real";
      dt_Registros.Columns.Add(dc_REGI_FechaHoraSalidaReal);
      DataColumn dc_TABL_TablaTDI = new DataColumn("TABL_TablaTDI", System.Type.GetType("System.String"));
      dc_TABL_TablaTDI.Caption = "Doc. Identidad";
      dt_Registros.Columns.Add(dc_TABL_TablaTDI);
      DataColumn dc_TABL_InternoTDI = new DataColumn("TABL_InternoTDI", System.Type.GetType("System.String"));
      dc_TABL_InternoTDI.Caption = "Doc. Identidad";
      dt_Registros.Columns.Add(dc_TABL_InternoTDI);
      DataColumn dc_ENTI_Interno = new DataColumn("ENTI_Interno", System.Type.GetType("System.Int64"));
      dc_ENTI_Interno.Caption = "Entidad";
      dt_Registros.Columns.Add(dc_ENTI_Interno);
      DataColumn dc_REGI_HuespedId = new DataColumn("REGI_HuespedId", System.Type.GetType("System.String"));
      dc_REGI_HuespedId.Caption = "Id";
      dt_Registros.Columns.Add(dc_REGI_HuespedId);
      DataColumn dc_REGI_HuespedNombreCompleto = new DataColumn("REGI_HuespedNombreCompleto", System.Type.GetType("System.String"));
      dc_REGI_HuespedNombreCompleto.Caption = "Nombre Completo";
      dt_Registros.Columns.Add(dc_REGI_HuespedNombreCompleto);
      DataColumn dc_REGI_HuespedDireccion = new DataColumn("REGI_HuespedDireccion", System.Type.GetType("System.String"));
      dc_REGI_HuespedDireccion.Caption = "Direccion";
      dt_Registros.Columns.Add(dc_REGI_HuespedDireccion);
      DataColumn dc_REGI_PrecioSugerido = new DataColumn("REGI_PrecioSugerido", System.Type.GetType("System.Double"));
      dc_REGI_PrecioSugerido.Caption = "Precio Sugerido";
      dt_Registros.Columns.Add(dc_REGI_PrecioSugerido);
      DataColumn dc_REGI_PrecioCobrado = new DataColumn("REGI_PrecioCobrado", System.Type.GetType("System.Double"));
      dc_REGI_PrecioCobrado.Caption = "Precio Cobrado";
      dt_Registros.Columns.Add(dc_REGI_PrecioCobrado);
      DataColumn dc_REGI_MotivoAnulacion = new DataColumn("REGI_MotivoAnulacion", System.Type.GetType("System.String"));
      dc_REGI_MotivoAnulacion.Caption = "Motivo Anulacion";
      dt_Registros.Columns.Add(dc_REGI_MotivoAnulacion);
      DataColumn dc_REGI_FechaHoraAnulacion = new DataColumn("REGI_FechaHoraAnulacion", System.Type.GetType("System.DateTime"));
      dc_REGI_FechaHoraAnulacion.Caption = "Fecha Hora Anulacion";
      dt_Registros.Columns.Add(dc_REGI_FechaHoraAnulacion);
      DataColumn dc_TURN_Interno = new DataColumn("TURN_Interno", System.Type.GetType("System.Int64"));
      dc_TURN_Interno.Caption = "Turno";
      dt_Registros.Columns.Add(dc_TURN_Interno);
      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", System.Type.GetType("System.Int64"));
      dc_USUA_Interno.Caption = "Usuario";
      dt_Registros.Columns.Add(dc_USUA_Interno);

      foreach (Registros item in Items)
      {
        DataRow dr_Registros = dt_Registros.NewRow();

        dr_Registros[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Registros[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Registros[dc_REGI_Interno] = item.REGI_Interno;
        dr_Registros[dc_HABI_Interno] = item.HABI_Interno;
        dr_Registros[dc_REGI_Estado] = item.REGI_Estado;
        dr_Registros[dc_REGI_Tramos] = item.REGI_Tramos;
        dr_Registros[dc_REGI_Cantidad] = item.REGI_Cantidad;
        dr_Registros[dc_REGI_FechaHoraEntrada] = item.REGI_FechaHoraEntrada;
        dr_Registros[dc_REGI_FechaHoraSalida] = item.REGI_FechaHoraSalida;
        dr_Registros[dc_REGI_FechaHoraSalidaReal] = item.REGI_FechaHoraSalidaReal;
        dr_Registros[dc_TABL_TablaTDI] = item.TABL_TablaTDI;
        dr_Registros[dc_TABL_InternoTDI] = item.TABL_InternoTDI;
        dr_Registros[dc_ENTI_Interno] = item.ENTI_Interno;
        dr_Registros[dc_REGI_HuespedId] = item.REGI_HuespedId;
        dr_Registros[dc_REGI_HuespedNombreCompleto] = item.REGI_HuespedNombreCompleto;
        dr_Registros[dc_REGI_HuespedDireccion] = item.REGI_HuespedDireccion;
        dr_Registros[dc_REGI_PrecioSugerido] = item.REGI_PrecioSugerido;
        dr_Registros[dc_REGI_PrecioCobrado] = item.REGI_PrecioCobrado;
        dr_Registros[dc_REGI_MotivoAnulacion] = item.REGI_MotivoAnulacion;
        dr_Registros[dc_REGI_FechaHoraAnulacion] = item.REGI_FechaHoraAnulacion;
        dr_Registros[dc_TURN_Interno] = item.TURN_Interno;
        dr_Registros[dc_USUA_Interno] = item.USUA_Interno;

        dt_Registros.Rows.Add(dr_Registros);
      }

      return dt_Registros;
    }

    #endregion
  }
}
