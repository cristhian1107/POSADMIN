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
  public partial class BLSucursales
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Sucursales> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLSucursales(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Sucursales>();
      Sucursales l_item = new Sucursales();
      Loader.EntityType = l_item.GetType();
    }
    public BLSucursales()
    {
      Loader = new BusinessEntityLoader<Sucursales>();
      Sucursales l_item = new Sucursales();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Sucursales> POSAD_SUCU_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, String Nombre)
    {
      try
      {
        ObservableCollection<Sucursales> l_items = null;
        Sucursales l_item = null;
        SQL.DAAssignProcedure("POSAD_SUCU_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchSUCU_Nombre", Nombre, SqlDbType.VarChar, 250);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Sucursales>();
          while (reader.Read())
          {
            l_item = new Sucursales();
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
    private Sucursales POSAD_SUCU_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Interno)
    {
      try
      {
        Sucursales l_item = null;
        SQL.DAAssignProcedure("POSAD_SUCU_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Sucursales();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_SUCU_Insertar(MyDataAccessSQL SQL, ref Sucursales Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_SUCU_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchSUCU_Nombre", Item.SUCU_Nombre, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchSUCU_Direccion", Item.SUCU_Direccion, SqlDbType.VarChar, 200);
        SQL.DAAddParameter("@pvchSUCU_Correo", Item.SUCU_Correo, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchSUCU_Telefono", Item.SUCU_Telefono, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchSUCU_Web", Item.SUCU_Web, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pbitSUCU_Principal", Item.SUCU_Principal, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Int64 SUCU_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigSUCU_Interno"), out SUCU_Interno))
          { Item.SUCU_Interno = SUCU_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_SUCU_Actualizar(MyDataAccessSQL SQL, ref Sucursales Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_SUCU_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchSUCU_Nombre", Item.SUCU_Nombre, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchSUCU_Direccion", Item.SUCU_Direccion, SqlDbType.VarChar, 200);
        SQL.DAAddParameter("@pvchSUCU_Correo", Item.SUCU_Correo, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchSUCU_Telefono", Item.SUCU_Telefono, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchSUCU_Web", Item.SUCU_Web, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pbitSUCU_Principal", Item.SUCU_Principal, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_SUCU_Eliminar(MyDataAccessSQL SQL, ref Sucursales Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_SUCU_Eliminar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
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
