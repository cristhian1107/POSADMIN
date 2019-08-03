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
  public partial class DetallesPagosRegistros_Display
  {
    #region [ VARIABLES ]

    private DetallesPagosRegistros m_itemDetallesPagosRegistros;

    #endregion

    #region [ CONSTRUCTOR ]

    public DetallesPagosRegistros_Display(DetallesPagosRegistros Item)
    { ItemDetallesPagosRegistros = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private DetallesPagosRegistros ItemDetallesPagosRegistros
    {
      get { if (m_itemDetallesPagosRegistros == null) { m_itemDetallesPagosRegistros = DetallesPagosRegistros.Nuevo(); } return m_itemDetallesPagosRegistros; }
      set { m_itemDetallesPagosRegistros = value; }
    }

    public String Buttons
    {
      get
      {
        String _editButton = "<a href=\"DetallesPagosRegistrosRegister?EMPR_Interno=" + EMPR_Interno_Display.ToString() + "\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String _deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + ")\"  class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-trash-alt\" ></i></a>";

        return _editButton + " " + _deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "EMPR_Interno", Description = "EMPR_Interno", ShortName = "EMPR_Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemDetallesPagosRegistros.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "SUCU_Interno", Description = "SUCU_Interno", ShortName = "SUCU_Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemDetallesPagosRegistros.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "REGI_Interno", Description = "REGI_Interno", ShortName = "REGI_Interno", Order = 3)]
    public Int64 REGI_Interno_Display { get { return ItemDetallesPagosRegistros.REGI_Interno; } }

    [Display(AutoGenerateField = false, Name = "PAGO_Item", Description = "PAGO_Item", ShortName = "PAGO_Item", Order = 4)]
    public Int32 PAGO_Item_Display { get { return ItemDetallesPagosRegistros.PAGO_Item; } }

    [Display(AutoGenerateField = false, Name = "PAGO_Tipo", Description = "PAGO_Tipo", ShortName = "PAGO_Tipo", Order = 5)]
    public String PAGO_Tipo_Display { get { return ItemDetallesPagosRegistros.PAGO_Tipo; } }

    [Display(AutoGenerateField = false, Name = "PAGO_MontoCancelado", Description = "PAGO_MontoCancelado", ShortName = "PAGO_MontoCancelado", Order = 6)]
    public Double PAGO_MontoCancelado_Display { get { return ItemDetallesPagosRegistros.PAGO_MontoCancelado; } }

    [Display(AutoGenerateField = false, Name = "PAGO_FechaHoraRegistro", Description = "PAGO_FechaHoraRegistro", ShortName = "PAGO_FechaHoraRegistro", Order = 7)]
    public DateTime PAGO_FechaHoraRegistro_Display { get { return ItemDetallesPagosRegistros.PAGO_FechaHoraRegistro; } }

    [Display(AutoGenerateField = false, Name = "USUA_Interno", Description = "USUA_Interno", ShortName = "USUA_Interno", Order = 8)]
    public Int64 USUA_Interno_Display { get { return ItemDetallesPagosRegistros.USUA_Interno; } }

    [Display(AutoGenerateField = false, Name = "RETI_Interno", Description = "RETI_Interno", ShortName = "RETI_Interno", Order = 9)]
    public Int64 RETI_Interno_Display { get { return ItemDetallesPagosRegistros.RETI_Interno; } }

    [Display(AutoGenerateField = false, Name = "CIER_Interno", Description = "CIER_Interno", ShortName = "CIER_Interno", Order = 10)]
    public Int64 CIER_Interno_Display { get { return ItemDetallesPagosRegistros.CIER_Interno; } }

    #endregion

    #region [ Metodos ]

    public static List<DetallesPagosRegistros_Display> ToList(ObservableCollection<DetallesPagosRegistros> Items)
    {
      List<DetallesPagosRegistros_Display> l_items = new List<DetallesPagosRegistros_Display>();

      foreach (DetallesPagosRegistros item in Items)
      { l_items.Add(new DetallesPagosRegistros_Display(item)); }

      return l_items;
    }

    public static DataTable ToDataTable(ObservableCollection<DetallesPagosRegistros> Items)
    {
      DataTable dt_DetallesPagosRegistros = new DataTable("DetallesPagosRegistros");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_DetallesPagosRegistros.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int64"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_DetallesPagosRegistros.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_REGI_Interno = new DataColumn("REGI_Interno", System.Type.GetType("System.Int64"));
      dc_REGI_Interno.Caption = "Registro Interno";
      dt_DetallesPagosRegistros.Columns.Add(dc_REGI_Interno);
      DataColumn dc_PAGO_Item = new DataColumn("PAGO_Item", System.Type.GetType("System.Int32"));
      dc_PAGO_Item.Caption = "Item";
      dt_DetallesPagosRegistros.Columns.Add(dc_PAGO_Item);
      DataColumn dc_PAGO_Tipo = new DataColumn("PAGO_Tipo", System.Type.GetType("System.String"));
      dc_PAGO_Tipo.Caption = "Tipo";
      dt_DetallesPagosRegistros.Columns.Add(dc_PAGO_Tipo);
      DataColumn dc_PAGO_MontoCancelado = new DataColumn("PAGO_MontoCancelado", System.Type.GetType("System.Decimal"));
      dc_PAGO_MontoCancelado.Caption = "Monto Cancelado";
      dt_DetallesPagosRegistros.Columns.Add(dc_PAGO_MontoCancelado);
      DataColumn dc_PAGO_FechaHoraRegistro = new DataColumn("PAGO_FechaHoraRegistro", System.Type.GetType("System.DateTime"));
      dc_PAGO_FechaHoraRegistro.Caption = "Fecha Registro";
      dt_DetallesPagosRegistros.Columns.Add(dc_PAGO_FechaHoraRegistro);
      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", System.Type.GetType("System.Int64"));
      dc_USUA_Interno.Caption = "Usuario";
      dt_DetallesPagosRegistros.Columns.Add(dc_USUA_Interno);
      DataColumn dc_RETI_Interno = new DataColumn("RETI_Interno", System.Type.GetType("System.Int64"));
      dc_RETI_Interno.Caption = "Retiro";
      dt_DetallesPagosRegistros.Columns.Add(dc_RETI_Interno);
      DataColumn dc_CIER_Interno = new DataColumn("CIER_Interno", System.Type.GetType("System.Int64"));
      dc_CIER_Interno.Caption = "Cierre";
      dt_DetallesPagosRegistros.Columns.Add(dc_CIER_Interno);

      foreach (DetallesPagosRegistros item in Items)
      {
        DataRow dr_DetallesPagosRegistros = dt_DetallesPagosRegistros.NewRow();

        dr_DetallesPagosRegistros[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_DetallesPagosRegistros[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_DetallesPagosRegistros[dc_REGI_Interno] = item.REGI_Interno;
        dr_DetallesPagosRegistros[dc_PAGO_Item] = item.PAGO_Item;
        dr_DetallesPagosRegistros[dc_PAGO_Tipo] = item.PAGO_Tipo;
        dr_DetallesPagosRegistros[dc_PAGO_MontoCancelado] = item.PAGO_MontoCancelado;
        dr_DetallesPagosRegistros[dc_PAGO_FechaHoraRegistro] = item.PAGO_FechaHoraRegistro;
        dr_DetallesPagosRegistros[dc_USUA_Interno] = item.USUA_Interno;
        dr_DetallesPagosRegistros[dc_RETI_Interno] = item.RETI_Interno;
        dr_DetallesPagosRegistros[dc_CIER_Interno] = item.CIER_Interno;

        dt_DetallesPagosRegistros.Rows.Add(dr_DetallesPagosRegistros);
      }

      return dt_DetallesPagosRegistros;
    }

    #endregion
  }
}
