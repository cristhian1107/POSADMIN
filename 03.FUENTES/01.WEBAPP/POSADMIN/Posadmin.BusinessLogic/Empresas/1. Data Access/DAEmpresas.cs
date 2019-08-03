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
  public partial class BLEmpresas
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Empresas> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLEmpresas(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Empresas>();
      Empresas l_item = new Empresas();
      Loader.EntityType = l_item.GetType();
    }
    public BLEmpresas()
    {
      Loader = new BusinessEntityLoader<Empresas>();
      Empresas l_item = new Empresas();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Empresas> POSAD_EMPR_ConsultaTodos(MyDataAccessSQL SQL, String Id, String RazonSocial)
    {
      try
      {
        ObservableCollection<Empresas> l_items = null;
        Empresas l_item = null;
        SQL.DAAssignProcedure("POSAD_EMPR_ConsultaTodos");
        SQL.DAAddParameter("@pvchEMPR_Id", Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchEMPR_RazonSocial", RazonSocial, SqlDbType.VarChar, 250);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Empresas>();
          while (reader.Read())
          {
            l_item = new Empresas();
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
    private Empresas POSAD_EMPR_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Interno)
    {
      try
      {
        Empresas l_item = null;
        SQL.DAAssignProcedure("POSAD_EMPR_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Empresas();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_EMPR_Insertar(MyDataAccessSQL SQL, ref Empresas Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_EMPR_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchEMPR_Id", Item.EMPR_Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchEMPR_RazonSocial", Item.EMPR_RazonSocial, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchEMPR_Direccion", Item.EMPR_Direccion, SqlDbType.VarChar, 200);
        SQL.DAAddParameter("@pvchEMPR_NombreComercial", Item.EMPR_NombreComercial, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchEMPR_Correos", Item.EMPR_Correos, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchEMPR_Telefonos", Item.EMPR_Telefonos, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchEMPR_Web", Item.EMPR_Web, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int32 PAIS_Interno;
          if (Int32.TryParse(SQL.ParameterOutPut("@pintPAIS_Interno"), out PAIS_Interno))
          { Item.PAIS_Interno = PAIS_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_EMPR_Actualizar(MyDataAccessSQL SQL, ref Empresas Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_EMPR_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchEMPR_Id", Item.EMPR_Id, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchEMPR_RazonSocial", Item.EMPR_RazonSocial, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchEMPR_Direccion", Item.EMPR_Direccion, SqlDbType.VarChar, 200);
        SQL.DAAddParameter("@pvchEMPR_NombreComercial", Item.EMPR_NombreComercial, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchEMPR_Correos", Item.EMPR_Correos, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchEMPR_Telefonos", Item.EMPR_Telefonos, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchEMPR_Web", Item.EMPR_Web, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private ObservableCollection<Empresas> POSAD_EMPR_ConsultaPorUsuarios(MyDataAccessSQL SQL, Int64 UsuarioInterno)
    {
      try
      {
        ObservableCollection<Empresas> l_items = null;
        Empresas l_item = null;
        SQL.DAAssignProcedure("POSAD_EMPR_ConsultaPorUsuarios");
        SQL.DAAddParameter("@pintUSUA_Interno", UsuarioInterno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Empresas>();
          while (reader.Read())
          {
            l_item = new Empresas();
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
    private Boolean POSAD_EMPR_AsignarPorUsuario(MyDataAccessSQL SQL, ref Empresas Item, Boolean Primero)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_EMPR_AsignarPorUsuario");
        SQL.DAAddParameter("@pintUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbitEMPR_Primero", Primero, SqlDbType.Bit);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
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
