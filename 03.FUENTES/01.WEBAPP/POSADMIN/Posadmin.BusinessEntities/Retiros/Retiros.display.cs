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
  public partial class Retiros_Display
  {
    #region [ VARIABLES ]

    private Retiros m_itemRetiros;

    #endregion

    #region [ CONSTRUCTOR ]

    public Retiros_Display(Retiros x_itemRetiros)
    { ItemRetiros = x_itemRetiros; }

    #endregion

    #region [ PROPIEDADES ]

    private Retiros ItemRetiros
    {
      get { if (m_itemRetiros == null) { m_itemRetiros = Retiros.Nuevo(); } return m_itemRetiros; }
      set { m_itemRetiros = value; }
    }

    public String Buttons
    {
      get
      {
        String _editButton = "<a href=\"RetirosRegister?EMPR_Interno=" + EMPR_Interno_Display.ToString() + "\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String _deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + ")\"  class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-trash-alt\" ></i></a>";

        return _editButton + " " + _deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemRetiros.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemRetiros.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 RETI_Interno_Display { get { return ItemRetiros.RETI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 4)]
    public DateTime RETI_FechaHoraRegistro_Display { get { return ItemRetiros.RETI_FechaHoraRegistro; } }

    [Display(AutoGenerateField = false, Name = "Monto Entregado", Description = "Monto Entregado", ShortName = "Monto Entregado", Order = 5)]
    public Double RETI_MontoEntregado_Display { get { return ItemRetiros.RETI_MontoEntregado; } }

    [Display(AutoGenerateField = false, Name = "Monto Extra", Description = "Monto Extra", ShortName = "Monto Extra", Order = 6)]
    public Double RETI_MontoExtra_Display { get { return ItemRetiros.RETI_MontoExtra; } }

    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 7)]
    public Int64 USUA_Interno_Display { get { return ItemRetiros.USUA_Interno; } }

    #endregion

    #region [ METODOS ]

    public static List<Retiros_Display> ToList(ObservableCollection<Retiros> Items)
    {
      List<Retiros_Display> l_listRetiros = new List<Retiros_Display>();

      foreach (Retiros item in Items)
      { l_listRetiros.Add(new Retiros_Display(item)); }

      return l_listRetiros;
    }

    public static DataTable ToDataTable(ObservableCollection<Retiros> Items)
    {
      DataTable dt_Retiros = new DataTable("Retiros");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int32"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Retiros.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int32"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Retiros.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_RETI_Interno = new DataColumn("RETI_Interno", System.Type.GetType("System.Int32"));
      dc_RETI_Interno.Caption = "Interno";
      dt_Retiros.Columns.Add(dc_RETI_Interno);
      DataColumn dc_RETI_FechaHoraRegistro = new DataColumn("RETI_FechaHoraRegistro", System.Type.GetType("System.Int32"));
      dc_RETI_FechaHoraRegistro.Caption = "Fecha Hora Registro";
      dt_Retiros.Columns.Add(dc_RETI_FechaHoraRegistro);
      DataColumn dc_RETI_MontoEntregado = new DataColumn("RETI_MontoEntregado", System.Type.GetType("System.Int32"));
      dc_RETI_MontoEntregado.Caption = "Monto Entregado";
      dt_Retiros.Columns.Add(dc_RETI_MontoEntregado);
      DataColumn dc_RETI_MontoExtra = new DataColumn("RETI_MontoExtra", System.Type.GetType("System.Int32"));
      dc_RETI_MontoExtra.Caption = "Monto Extra";
      dt_Retiros.Columns.Add(dc_RETI_MontoExtra);
      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", System.Type.GetType("System.Int32"));
      dc_USUA_Interno.Caption = "Usuario";
      dt_Retiros.Columns.Add(dc_USUA_Interno);

      foreach (Retiros item in Items)
      {
        DataRow dr_Retiros = dt_Retiros.NewRow();

        dr_Retiros[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Retiros[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Retiros[dc_RETI_Interno] = item.RETI_Interno;
        dr_Retiros[dc_RETI_FechaHoraRegistro] = item.RETI_FechaHoraRegistro;
        dr_Retiros[dc_RETI_MontoEntregado] = item.RETI_MontoEntregado;
        dr_Retiros[dc_RETI_MontoExtra] = item.RETI_MontoExtra;
        dr_Retiros[dc_USUA_Interno] = item.USUA_Interno;

        dt_Retiros.Rows.Add(dr_Retiros);
      }

      return dt_Retiros;
    }

    #endregion
  }
}
