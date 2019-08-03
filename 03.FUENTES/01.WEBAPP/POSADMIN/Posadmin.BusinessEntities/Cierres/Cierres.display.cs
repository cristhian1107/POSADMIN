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
  public partial class Cierres_Display
  {
    #region [ VARIABLES ]

    private Cierres m_itemCierres;

    #endregion

    #region [ CONSTRUCTOR ]

    public Cierres_Display(Cierres x_itemCierres)
    { ItemCierres = x_itemCierres; }

    #endregion

    #region [ PROPIEDADES ]

    private Cierres ItemCierres
    {
      get { if (m_itemCierres == null) { m_itemCierres = Cierres.Nuevo(); } return m_itemCierres; }
      set { m_itemCierres = value; }
    }

    public String Buttons
    {
      get
      {
        String _editButton = "<a href=\"CierresRegister?EMPR_Interno=" + EMPR_Interno_Display.ToString() + "\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String _deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + EMPR_Interno_Display.ToString() + ")\"  class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-trash-alt\" ></i></a>";

        return _editButton + " " + _deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemCierres.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Sucursal Interno", Description = "Sucursal Interno", ShortName = "Sucursal Interno", Order = 2)]
    public Int64 SUCU_Interno_Display { get { return ItemCierres.SUCU_Interno; } }

    [Display(AutoGenerateField = false, Name = "Interno", Description = "Interno", ShortName = "Interno", Order = 3)]
    public Int64 CIER_Interno_Display { get { return ItemCierres.CIER_Interno; } }

    [Display(AutoGenerateField = false, Name = "Fecha Hora Registro", Description = "Fecha Hora Registro", ShortName = "Fecha Hora Registro", Order = 4)]
    public DateTime CIER_FechaHoraRegistro_Display { get { return ItemCierres.CIER_FechaHoraRegistro; } }

    [Display(AutoGenerateField = false, Name = "Monto Real", Description = "Monto Real", ShortName = "Monto Real", Order = 5)]
    public Double CIER_MontoReal_Display { get { return ItemCierres.CIER_MontoReal; } }

    [Display(AutoGenerateField = false, Name = "Monto Extra", Description = "Monto Extra", ShortName = "Monto Extra", Order = 6)]
    public Double CIER_MontoExtra_Display { get { return ItemCierres.CIER_MontoExtra; } }

    [Display(AutoGenerateField = false, Name = "Monto Deuda", Description = "Monto Deuda", ShortName = "Monto Deuda", Order = 7)]
    public Double CIER_MontoDeuda_Display { get { return ItemCierres.CIER_MontoDeuda; } }

    [Display(AutoGenerateField = false, Name = "Monto Demas", Description = "Monto Demas", ShortName = "Monto Demas", Order = 8)]
    public Double CIER_MontoDemas_Display { get { return ItemCierres.CIER_MontoDemas; } }

    [Display(AutoGenerateField = false, Name = "Usuario", Description = "Usuario", ShortName = "Usuario", Order = 9)]
    public Int64 USUA_Interno_Display { get { return ItemCierres.USUA_Interno; } }

    #endregion

    #region [ METODOS ]

    public static List<Cierres_Display> ToList(ObservableCollection<Cierres> Items)
    {
      List<Cierres_Display> l_listCierres = new List<Cierres_Display>();

      foreach (Cierres item in Items)
      { l_listCierres.Add(new Cierres_Display(item)); }

      return l_listCierres;
    }

    public static DataTable ToDataTable(ObservableCollection<Cierres> Items)
    {
      DataTable dt_Cierres = new DataTable("Cierres");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int32"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Cierres.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_SUCU_Interno = new DataColumn("SUCU_Interno", System.Type.GetType("System.Int32"));
      dc_SUCU_Interno.Caption = "Sucursal Interno";
      dt_Cierres.Columns.Add(dc_SUCU_Interno);
      DataColumn dc_CIER_Interno = new DataColumn("CIER_Interno", System.Type.GetType("System.Int32"));
      dc_CIER_Interno.Caption = "Interno";
      dt_Cierres.Columns.Add(dc_CIER_Interno);
      DataColumn dc_CIER_FechaHoraRegistro = new DataColumn("CIER_FechaHoraRegistro", System.Type.GetType("System.Int32"));
      dc_CIER_FechaHoraRegistro.Caption = "Fecha Hora Registro";
      dt_Cierres.Columns.Add(dc_CIER_FechaHoraRegistro);
      DataColumn dc_CIER_MontoReal = new DataColumn("CIER_MontoReal", System.Type.GetType("System.Int32"));
      dc_CIER_MontoReal.Caption = "Monto Real";
      dt_Cierres.Columns.Add(dc_CIER_MontoReal);
      DataColumn dc_CIER_MontoExtra = new DataColumn("CIER_MontoExtra", System.Type.GetType("System.Int32"));
      dc_CIER_MontoExtra.Caption = "Monto Extra";
      dt_Cierres.Columns.Add(dc_CIER_MontoExtra);
      DataColumn dc_CIER_MontoDeuda = new DataColumn("CIER_MontoDeuda", System.Type.GetType("System.Int32"));
      dc_CIER_MontoDeuda.Caption = "Monto Deuda";
      dt_Cierres.Columns.Add(dc_CIER_MontoDeuda);
      DataColumn dc_CIER_MontoDemas = new DataColumn("CIER_MontoDemas", System.Type.GetType("System.Int32"));
      dc_CIER_MontoDemas.Caption = "Monto Demas";
      dt_Cierres.Columns.Add(dc_CIER_MontoDemas);
      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", System.Type.GetType("System.Int32"));
      dc_USUA_Interno.Caption = "Usuario";
      dt_Cierres.Columns.Add(dc_USUA_Interno);

      foreach (Cierres item in Items)
      {
        DataRow dr_Cierres = dt_Cierres.NewRow();

        dr_Cierres[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Cierres[dc_SUCU_Interno] = item.SUCU_Interno;
        dr_Cierres[dc_CIER_Interno] = item.CIER_Interno;
        dr_Cierres[dc_CIER_FechaHoraRegistro] = item.CIER_FechaHoraRegistro;
        dr_Cierres[dc_CIER_MontoReal] = item.CIER_MontoReal;
        dr_Cierres[dc_CIER_MontoExtra] = item.CIER_MontoExtra;
        dr_Cierres[dc_CIER_MontoDeuda] = item.CIER_MontoDeuda;
        dr_Cierres[dc_CIER_MontoDemas] = item.CIER_MontoDemas;
        dr_Cierres[dc_USUA_Interno] = item.USUA_Interno;

        dt_Cierres.Rows.Add(dr_Cierres);
      }

      return dt_Cierres;
    }

    #endregion
  }
}
