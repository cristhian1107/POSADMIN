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
  public partial class Parametros_Display
  {
    #region [ VARIABLES ]

    private Parametros m_itemParametros;

    #endregion

    #region [ CONSTRUCTOR ]

    public Parametros_Display(Parametros Item)
    { ItemParametros = Item; }

    #endregion

    #region [ Propiedades ]
    private Parametros ItemParametros
    {
      get { if (m_itemParametros == null) { m_itemParametros = Parametros.Nuevo(); } return m_itemParametros; }
      set { m_itemParametros = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"ParametrosRegister?Empresa=" + EMPR_Interno_Display.ToString() + "&LLave=" + PARA_Llave_Display + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";

        return l_editButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemParametros.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Llave", Description = "Llave", ShortName = "Llave", Order = 2)]
    public String PARA_Llave_Display { get { return ItemParametros.PARA_Llave; } }

    [Display(AutoGenerateField = false, Name = "Valor", Description = "Valor", ShortName = "Valor", Order = 3)]
    public String PARA_Valor_Display { get { return ItemParametros.PARA_Valor; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 4)]
    public String PARA_Descripcion_Display { get { return ItemParametros.PARA_Descripcion; } }

    #endregion

    #region [ Metodos ]
    public static List<Parametros_Display> ToList(ObservableCollection<Parametros> Items)
    {
      List<Parametros_Display> _listParametros = new List<Parametros_Display>();

      foreach (Parametros item in Items)
      { _listParametros.Add(new Parametros_Display(item)); }

      return _listParametros;
    }

    public static DataTable ToDataTable(ObservableCollection<Parametros> Items)
    {
      DataTable dt_Parametros = new DataTable("Parametros");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Parametros.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_PARA_Llave = new DataColumn("PARA_Llave", System.Type.GetType("System.String"));
      dc_PARA_Llave.Caption = "Llave";
      dt_Parametros.Columns.Add(dc_PARA_Llave);
      DataColumn dc_PARA_Valor = new DataColumn("PARA_Valor", System.Type.GetType("System.String"));
      dc_PARA_Valor.Caption = "Valor";
      dt_Parametros.Columns.Add(dc_PARA_Valor);
      DataColumn dc_PARA_Descripcion = new DataColumn("PARA_Descripcion", System.Type.GetType("System.String"));
      dc_PARA_Descripcion.Caption = "Descripcion";
      dt_Parametros.Columns.Add(dc_PARA_Descripcion);

      foreach (Parametros item in Items)
      {
        DataRow dr_Parametros = dt_Parametros.NewRow();

        dr_Parametros[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Parametros[dc_PARA_Llave] = item.PARA_Llave;
        dr_Parametros[dc_PARA_Valor] = item.PARA_Valor;
        dr_Parametros[dc_PARA_Descripcion] = item.PARA_Descripcion;

        dt_Parametros.Rows.Add(dr_Parametros);
      }

      return dt_Parametros;
    }
    #endregion
  }
}
