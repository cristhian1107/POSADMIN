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
  public partial class Configuraciones_Display
  {
    #region [ VARIABLES ]

    private Configuraciones m_itemConfiguraciones;

    #endregion

    #region [ CONSTRUCTOR ]

    public Configuraciones_Display(Configuraciones Item)
    { ItemConfiguraciones = Item; }

    #endregion

    #region [ Propiedades ]

    private Configuraciones ItemConfiguraciones
    {
      get { if (m_itemConfiguraciones == null) { m_itemConfiguraciones = Configuraciones.Nuevo(); } return m_itemConfiguraciones; }
      set { m_itemConfiguraciones = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"ConfiguracionesRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&Usuario=" + USUA_Interno_Display.ToString() + "&LLave=" + CONF_Llave_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";

        return l_editButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemConfiguraciones.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Usuario Interno", Description = "Usuario Interno", ShortName = "Usuario Interno", Order = 2)]
    public Int64 USUA_Interno_Display { get { return ItemConfiguraciones.USUA_Interno; } }

    [Display(AutoGenerateField = false, Name = "Llave", Description = "Llave", ShortName = "Llave", Order = 3)]
    public String CONF_Llave_Display { get { return ItemConfiguraciones.CONF_Llave; } }

    [Display(AutoGenerateField = false, Name = "Valor", Description = "Valor", ShortName = "Valor", Order = 4)]
    public String CONF_Valor_Display { get { return ItemConfiguraciones.CONF_Valor; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 5)]
    public String CONF_Descripcion_Display { get { return ItemConfiguraciones.CONF_Descripcion; } }


    #endregion

    #region [ Metodos ]

    public static List<Configuraciones_Display> ToList(ObservableCollection<Configuraciones> Items)
    {
      List<Configuraciones_Display> l_listConfiguraciones = new List<Configuraciones_Display>();

      foreach (Configuraciones item in Items)
      { l_listConfiguraciones.Add(new Configuraciones_Display(item)); }

      return l_listConfiguraciones;
    }

    public static DataTable ToDataTable(ObservableCollection<Configuraciones> Items)
    {
      DataTable dt_Configuraciones = new DataTable("Configuraciones");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Configuraciones.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_USUA_Interno = new DataColumn("USUA_Interno", System.Type.GetType("System.Int64"));
      dc_USUA_Interno.Caption = "Usuario Interno";
      dt_Configuraciones.Columns.Add(dc_USUA_Interno);
      DataColumn dc_CONF_Llave = new DataColumn("CONF_Llave", System.Type.GetType("System.String"));
      dc_CONF_Llave.Caption = "Llave";
      dt_Configuraciones.Columns.Add(dc_CONF_Llave);
      DataColumn dc_CONF_Valor = new DataColumn("CONF_Valor", System.Type.GetType("System.String"));
      dc_CONF_Valor.Caption = "Valor";
      dt_Configuraciones.Columns.Add(dc_CONF_Valor);
      DataColumn dc_CONF_Descripcion = new DataColumn("CONF_Descripcion", System.Type.GetType("System.String"));
      dc_CONF_Descripcion.Caption = "Descripcion";
      dt_Configuraciones.Columns.Add(dc_CONF_Descripcion);

      foreach (Configuraciones item in Items)
      {
        DataRow dr_Configuraciones = dt_Configuraciones.NewRow();

        dr_Configuraciones[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Configuraciones[dc_USUA_Interno] = item.USUA_Interno;
        dr_Configuraciones[dc_CONF_Llave] = item.CONF_Llave;
        dr_Configuraciones[dc_CONF_Valor] = item.CONF_Valor;
        dr_Configuraciones[dc_CONF_Descripcion] = item.CONF_Descripcion;

        dt_Configuraciones.Rows.Add(dr_Configuraciones);
      }

      return dt_Configuraciones;
    }

    #endregion
  }
}
