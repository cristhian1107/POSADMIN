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
  public partial class Opciones_Display
  {
    #region [ VARIABLES ]

    private Opciones m_itemOpciones;

    #endregion

    #region [ CONSTRUCTOR ]

    public Opciones_Display(Opciones x_itemOpciones)
    { ItemOpciones = x_itemOpciones; }

    #endregion

    #region [ PROPIEDADES ]

    private Opciones ItemOpciones
    {
      get { if (m_itemOpciones == null) { m_itemOpciones = Opciones.Nuevo(); } return m_itemOpciones; }
      set { m_itemOpciones = value; }
    }

    public String Buttons
    {
      get
      {
        String l_editButton = "<a href=\"OpcionesRegister?Interno=" + OPCI_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        String l_deleteButton = "<a data-toggle=\"modal\" onclick =\"DeleteItem(" + OPCI_Interno_Display.ToString() + ")\" title=\"Eliminar\" class=\"btn bg-color-red btn-primary btn-xs\" ><i class=\"fa fa-eraser\" ></i></a>";

        return l_editButton + " " + l_deleteButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Opcion Interno", Description = "Opcion Interno", ShortName = "Opcion Interno", Order = 1)]
    public Int32 OPCI_Interno_Display { get { return ItemOpciones.OPCI_Interno; } }

    [Display(AutoGenerateField = false, Name = "Nombre", Description = "Nombre", ShortName = "Nombre", Order = 2)]
    public String OPCI_Nombre_Display { get { return ItemOpciones.OPCI_Nombre; } }

    [Display(AutoGenerateField = false, Name = "Nomenclatura", Description = "Nomenclatura", ShortName = "Nomenclatura", Order = 3)]
    public String OPCI_Nomenclatura_Display { get { return ItemOpciones.OPCI_Nomenclatura; } }

    [Display(AutoGenerateField = false, Name = "Descripcion", Description = "Descripcion", ShortName = "Descripcion", Order = 4)]
    public String OPCI_Descripcion_Display { get { return ItemOpciones.OPCI_Descripcion; } }

    [Display(AutoGenerateField = false, Name = "Interno Padre", Description = "Interno Padre", ShortName = "Interno Padre", Order = 5)]
    public Nullable<Int32> OPCI_InternoPadre_Display { get { return ItemOpciones.OPCI_InternoPadre; } }

    #endregion

    #region [ METODOS ]

    public static List<Opciones_Display> ToList(ObservableCollection<Opciones> Items)
    {
      List<Opciones_Display> _listOpciones = new List<Opciones_Display>();

      foreach (Opciones item in Items)
      { _listOpciones.Add(new Opciones_Display(item)); }

      return _listOpciones;
    }

    public static DataTable ToDataTable(ObservableCollection<Opciones> Items)
    {
      DataTable dt_Opciones = new DataTable("Opciones");

      DataColumn dc_OPCI_Interno = new DataColumn("OPCI_Interno", System.Type.GetType("System.Int32"));
      dc_OPCI_Interno.Caption = "Opcion Interno";
      dt_Opciones.Columns.Add(dc_OPCI_Interno);
      DataColumn dc_OPCI_Nombre = new DataColumn("OPCI_Nombre", System.Type.GetType("System.String"));
      dc_OPCI_Nombre.Caption = "Nombre";
      dt_Opciones.Columns.Add(dc_OPCI_Nombre);
      DataColumn dc_OPCI_Nomenclatura = new DataColumn("OPCI_Nomenclatura", System.Type.GetType("System.String"));
      dc_OPCI_Nomenclatura.Caption = "Nomenclatura";
      dt_Opciones.Columns.Add(dc_OPCI_Nomenclatura);
      DataColumn dc_OPCI_Descripcion = new DataColumn("OPCI_Descripcion", System.Type.GetType("System.String"));
      dc_OPCI_Descripcion.Caption = "Descripcion";
      dt_Opciones.Columns.Add(dc_OPCI_Descripcion);
      DataColumn dc_OPCI_InternoPadre = new DataColumn("OPCI_InternoPadre", System.Type.GetType("System.Int32"));
      dc_OPCI_InternoPadre.Caption = "Interno Padre";
      dt_Opciones.Columns.Add(dc_OPCI_InternoPadre);

      foreach (Opciones item in Items)
      {
        DataRow dr_Opciones = dt_Opciones.NewRow();

        dr_Opciones[dc_OPCI_Interno] = item.OPCI_Interno;
        dr_Opciones[dc_OPCI_Nombre] = item.OPCI_Nombre;
        dr_Opciones[dc_OPCI_Nomenclatura] = item.OPCI_Nomenclatura;
        dr_Opciones[dc_OPCI_Descripcion] = item.OPCI_Descripcion;
        dr_Opciones[dc_OPCI_InternoPadre] = item.OPCI_InternoPadre;

        dt_Opciones.Rows.Add(dr_Opciones);
      }

      return dt_Opciones;
    }

    #endregion
  }
}
