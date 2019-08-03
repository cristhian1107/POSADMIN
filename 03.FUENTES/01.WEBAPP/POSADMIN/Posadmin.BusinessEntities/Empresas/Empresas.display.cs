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
  public partial class Empresas_Display
  {
    #region [ VARIABLES ]

    private Empresas m_itemEmpresas;

    #endregion

    #region [ CONSTRUCTOR ]

    public Empresas_Display(Empresas Item)
    { ItemEmpresas = Item; }

    #endregion

    #region [ PROPIEDADES ]

    private Empresas ItemEmpresas
    {
      get { if (m_itemEmpresas == null) { m_itemEmpresas = Empresas.Nuevo(); } return m_itemEmpresas; }
      set { m_itemEmpresas = value; }
    }

    public String Buttons
    {
      get
      {
        string l_editButton = "<a href=\"EmpresasRegister?Interno=" + EMPR_Interno_Display.ToString() + "\" title=\"Editar\" class=\"btn bg-color-orange btn-primary btn-xs\" ><i class=\"fa fa-edit\"></i></a>";
        return l_editButton;
      }
    }

    [Display(AutoGenerateField = false, Name = "Empresa Interno", Description = "Empresa Interno", ShortName = "Empresa Interno", Order = 1)]
    public Int64 EMPR_Interno_Display { get { return ItemEmpresas.EMPR_Interno; } }

    [Display(AutoGenerateField = false, Name = "Pais Interno", Description = "Pais Interno", ShortName = "Pais Interno", Order = 2)]
    public Int32 PAIS_Interno_Display { get { return ItemEmpresas.PAIS_Interno; } }

    [Display(AutoGenerateField = false, Name = "Id", Description = "Id", ShortName = "Id", Order = 3)]
    public String EMPR_Id_Display { get { return ItemEmpresas.EMPR_Id; } }

    [Display(AutoGenerateField = false, Name = "Razon Social", Description = "Razon Social", ShortName = "Razon Social", Order = 4)]
    public String EMPR_RazonSocial_Display { get { return ItemEmpresas.EMPR_RazonSocial; } }

    [Display(AutoGenerateField = false, Name = "Direccion", Description = "Direccion", ShortName = "Direccion", Order = 5)]
    public String EMPR_Direccion_Display { get { return ItemEmpresas.EMPR_Direccion; } }

    [Display(AutoGenerateField = false, Name = "Nombre Comercial", Description = "Nombre Comercial", ShortName = "Nombre Comercial", Order = 6)]
    public String EMPR_NombreComercial_Display { get { return ItemEmpresas.EMPR_NombreComercial; } }

    [Display(AutoGenerateField = false, Name = "Ubigeo Interno", Description = "Ubigeo Interno", ShortName = "Ubigeo Interno", Order = 7)]
    public Nullable<Int32> UBIG_Interno_Display { get { return ItemEmpresas.UBIG_Interno; } }

    [Display(AutoGenerateField = false, Name = "Correos", Description = "Correos", ShortName = "Correos", Order = 8)]
    public String EMPR_Correos_Display { get { return ItemEmpresas.EMPR_Correos; } }

    [Display(AutoGenerateField = false, Name = "Telefonos", Description = "Telefonos", ShortName = "Telefonos", Order = 9)]
    public String EMPR_Telefonos_Display { get { return ItemEmpresas.EMPR_Telefonos; } }

    [Display(AutoGenerateField = false, Name = "Web", Description = "Web", ShortName = "Web", Order = 10)]
    public String EMPR_Web_Display { get { return ItemEmpresas.EMPR_Web; } }

    [Display(AutoGenerateField = false, Name = "Nombre Pais", Description = "Nombre Pais", ShortName = "Nombre Pais", Order = 11)]
    public String EMPR_PaisNombre_Display { get { return ItemEmpresas.EMPR_PaisNombre; } }

    #endregion

    #region [ METODOS ]

    public static List<Empresas_Display> ToList(ObservableCollection<Empresas> Items)
    {
      List<Empresas_Display> _listEmpresas = new List<Empresas_Display>();

      foreach (Empresas item in Items)
      { _listEmpresas.Add(new Empresas_Display(item)); }

      return _listEmpresas;
    }

    public static DataTable ToDataTable(ObservableCollection<Empresas> Items)
    {
      DataTable dt_Empresas = new DataTable("Empresas");

      DataColumn dc_EMPR_Interno = new DataColumn("EMPR_Interno", System.Type.GetType("System.Int64"));
      dc_EMPR_Interno.Caption = "Empresa Interno";
      dt_Empresas.Columns.Add(dc_EMPR_Interno);
      DataColumn dc_PAIS_Interno = new DataColumn("PAIS_Interno", System.Type.GetType("System.Int32"));
      dc_PAIS_Interno.Caption = "Pais Interno";
      dt_Empresas.Columns.Add(dc_PAIS_Interno);
      DataColumn dc_EMPR_Id = new DataColumn("EMPR_Id", System.Type.GetType("System.String"));
      dc_EMPR_Id.Caption = "Id";
      dt_Empresas.Columns.Add(dc_EMPR_Id);
      DataColumn dc_EMPR_RazonSocial = new DataColumn("EMPR_RazonSocial", System.Type.GetType("System.String"));
      dc_EMPR_RazonSocial.Caption = "Razon Social";
      dt_Empresas.Columns.Add(dc_EMPR_RazonSocial);
      DataColumn dc_EMPR_Direccion = new DataColumn("EMPR_Direccion", System.Type.GetType("System.String"));
      dc_EMPR_Direccion.Caption = "Direccion";
      dt_Empresas.Columns.Add(dc_EMPR_Direccion);
      DataColumn dc_EMPR_NombreComercial = new DataColumn("EMPR_NombreComercial", System.Type.GetType("System.String"));
      dc_EMPR_NombreComercial.Caption = "Nombre Comercial";
      dt_Empresas.Columns.Add(dc_EMPR_NombreComercial);
      DataColumn dc_UBIG_Interno = new DataColumn("UBIG_Interno", System.Type.GetType("System.Int32"));
      dc_UBIG_Interno.Caption = "Ubigeo Interno";
      dt_Empresas.Columns.Add(dc_UBIG_Interno);
      DataColumn dc_EMPR_Correos = new DataColumn("EMPR_Correos", System.Type.GetType("System.String"));
      dc_EMPR_Correos.Caption = "Correos";
      dt_Empresas.Columns.Add(dc_EMPR_Correos);
      DataColumn dc_EMPR_Telefonos = new DataColumn("EMPR_Telefonos", System.Type.GetType("System.String"));
      dc_EMPR_Telefonos.Caption = "Telefonos";
      dt_Empresas.Columns.Add(dc_EMPR_Telefonos);
      DataColumn dc_EMPR_Web = new DataColumn("EMPR_Web", System.Type.GetType("System.String"));
      dc_EMPR_Web.Caption = "Web";
      dt_Empresas.Columns.Add(dc_EMPR_Web);

      foreach (Empresas item in Items)
      {
        DataRow dr_Empresas = dt_Empresas.NewRow();

        dr_Empresas[dc_EMPR_Interno] = item.EMPR_Interno;
        dr_Empresas[dc_PAIS_Interno] = item.PAIS_Interno;
        dr_Empresas[dc_EMPR_Id] = item.EMPR_Id;
        dr_Empresas[dc_EMPR_RazonSocial] = item.EMPR_RazonSocial;
        dr_Empresas[dc_EMPR_Direccion] = item.EMPR_Direccion;
        dr_Empresas[dc_EMPR_NombreComercial] = item.EMPR_NombreComercial;
        dr_Empresas[dc_UBIG_Interno] = item.UBIG_Interno;
        dr_Empresas[dc_EMPR_Correos] = item.EMPR_Correos;
        dr_Empresas[dc_EMPR_Telefonos] = item.EMPR_Telefonos;
        dr_Empresas[dc_EMPR_Web] = item.EMPR_Web;

        dt_Empresas.Rows.Add(dr_Empresas);
      }

      return dt_Empresas;
    }

    #endregion
  }
}
