using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SoftwareFactory.Infrastructure.BusinessEntity;
using SoftwareFactory.Infrastructure.DataAccess;
using Posadmin.BusinessEntities;
using System.Data;
using System.Collections.ObjectModel;

namespace Posadmin.BusinessLogic
{
  public partial class BLEntidades
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLFuncionesEntidades BL_FuncionesEntidades { get; set; }
    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Entidades> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLEntidades(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Entidades>();
      Entidades l_item = new Entidades();
      Loader.EntityType = l_item.GetType();
      BL_FuncionesEntidades = new BLFuncionesEntidades();
    }
    public BLEntidades()
    {
      Loader = new BusinessEntityLoader<Entidades>();
      Entidades l_item = new Entidades();
      Loader.EntityType = l_item.GetType();
      BL_FuncionesEntidades = new BLFuncionesEntidades();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Entidades> POSAD_ENTI_Ayuda(MyDataAccessSQL SQL, Int64 Empresa, String TipoEntidad, String Indicio)
    {
      try
      {
        ObservableCollection<Entidades> l_items = null;
        Entidades l_item = null;
        SQL.DAAssignProcedure("POSAD_ENTI_Ayuda");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchENTI_TipoEntidad", TipoEntidad, SqlDbType.Char, 5);
        SQL.DAAddParameter("@pvchENTI_Indicio", Indicio, SqlDbType.VarChar, 250);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Entidades>();
          while (reader.Read())
          {
            l_item = new Entidades();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
            l_items.Add(l_item);
          }
        }
        return l_items;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private ObservableCollection<Entidades> POSAD_ENTI_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, String Id, String Nombre)
    {
      try
      {
        ObservableCollection<Entidades> l_items = null;
        Entidades l_item = null;
        SQL.DAAssignProcedure("POSAD_ENTI_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchENTI_Id", Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchENTI_NombreCompleto", Nombre, SqlDbType.VarChar, 250);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Entidades>();
          while (reader.Read())
          {
            l_item = new Entidades();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
            l_items.Add(l_item);
          }
        }
        return l_items;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Entidades POSAD_ENTI_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Entidad)
    {
      try
      {
        Entidades l_item = null;
        SQL.DAAssignProcedure("POSAD_ENTI_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigENTI_Interno", Entidad, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Entidades();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ENTI_Insertar(MyDataAccessSQL SQL, ref Entidades Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ENTI_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigENTI_Interno", Item.ENTI_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrTABL_TablaTDI", Item.TABL_TablaTDI, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTDI", Item.TABL_InternoTDI, SqlDbType.Char, 4);
        SQL.DAAddParameter("@pvchENTI_Id", Item.ENTI_Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchENTI_NombreCompleto", Item.ENTI_NombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchENTI_Direccion", Item.ENTI_Direccion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pchrENTI_Sexo", Item.ENTI_Sexo, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Int64 ENTI_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigENTI_Interno"), out ENTI_Interno))
          { Item.ENTI_Interno = ENTI_Interno; }

          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ENTI_Actualizar(MyDataAccessSQL SQL, ref Entidades Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ENTI_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigENTI_Interno", Item.ENTI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaTDI", Item.TABL_TablaTDI, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTDI", Item.TABL_InternoTDI, SqlDbType.Char, 4);
        SQL.DAAddParameter("@pvchENTI_Id", Item.ENTI_Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchENTI_NombreCompleto", Item.ENTI_NombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchENTI_Direccion", Item.ENTI_Direccion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pchrENTI_Sexo", Item.ENTI_Sexo, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ENTI_Eliminar(MyDataAccessSQL SQL, ref Entidades Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ENTI_Eliminar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigENTI_Interno", Item.ENTI_Interno, SqlDbType.BigInt);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
