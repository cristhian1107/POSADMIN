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
  public partial class BLUsuarios
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLEmpresas BL_Empresas { get; set; }
    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Usuarios> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLUsuarios(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Usuarios>();
      Usuarios l_item = new Usuarios();
      Loader.EntityType = l_item.GetType();
    }
    public BLUsuarios()
    {
      Loader = new BusinessEntityLoader<Usuarios>();
      Usuarios l_item = new Usuarios();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private Usuarios POSAD_USUA_VerificarUsuario(MyDataAccessSQL SQL, String IdEmpresa, String Usuario, String Contrasena)
    {
      try
      {
        Usuarios l_item = null;
        SQL.DAAssignProcedure("POSAD_USUA_VerificarUsuario");
        SQL.DAAddParameter("@pvchEMPR_Id", IdEmpresa, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchUSUA_Usuario", Usuario, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchUSUA_Contrasena", Contrasena, SqlDbType.VarChar, -1);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Usuarios();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Usuarios POSAD_USUA_ConsultaUsuarioEmpresa(MyDataAccessSQL SQL, String IdEmpresa, Int64 Interno)
    {
      try
      {
        Usuarios l_item = null;
        SQL.DAAssignProcedure("POSAD_USUA_ConsultaUsuarioEmpresa");
        SQL.DAAddParameter("@pvchEMPR_Id", IdEmpresa, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pbigUSUA_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Usuarios();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private ObservableCollection<Usuarios> POSAD_USUA_ConsultaTodos(MyDataAccessSQL SQL, String Usuario)
    {
      try
      {
        ObservableCollection<Usuarios> l_items = null;
        Usuarios l_item = null;
        SQL.DAAssignProcedure("POSAD_USUA_ConsultaTodos");
        SQL.DAAddParameter("@pvchUSUA_Usuario", Usuario, SqlDbType.VarChar, 25);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Usuarios>();
          while (reader.Read())
          {
            l_item = new Usuarios();
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
    private ObservableCollection<Usuarios> POSAD_USUA_ConsultaPorEmpresa(MyDataAccessSQL SQL, Int64 Empresa)
    {
      try
      {
        ObservableCollection<Usuarios> l_items = null;
        Usuarios l_item = null;
        SQL.DAAssignProcedure("POSAD_USUA_ConsultaPorEmpresa");
        SQL.DAAddParameter("@pintEMPR_Interno", Empresa, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Usuarios>();
          while (reader.Read())
          {
            l_item = new Usuarios();
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
    private Usuarios POSAD_USUA_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Interno)
    {
      try
      {
        Usuarios l_item = null;
        SQL.DAAssignProcedure("POSAD_USUA_ConsultaRegistro");
        SQL.DAAddParameter("@pbigUSUA_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Usuarios();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_USUA_Insertar(MyDataAccessSQL SQL, ref Usuarios Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_USUA_Insertar");
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchUSUA_NombreCompleto", Item.USUA_NombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchUSUA_Usuario", Item.USUA_Usuario, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchUSUA_Contrasena", Item.USUA_ContrasenaEncriptada, SqlDbType.VarChar, -1);
        SQL.DAAddParameter("@pvchUSUA_Correo", Item.USUA_Correo, SqlDbType.VarChar, 150);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int32 USUA_Interno;
          if (Int32.TryParse(SQL.ParameterOutPut("@pbigUSUA_Interno"), out USUA_Interno))
          { Item.USUA_Interno = USUA_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_USUA_Actualizar(MyDataAccessSQL SQL, ref Usuarios Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_USUA_Actualizar");
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchUSUA_NombreCompleto", Item.USUA_NombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchUSUA_Usuario", Item.USUA_Usuario, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchUSUA_Contrasena", Item.USUA_ContrasenaEncriptada, SqlDbType.VarChar, -1);
        SQL.DAAddParameter("@pvchUSUA_Correo", Item.USUA_Correo, SqlDbType.VarChar, 150);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_USUA_Eliminar(MyDataAccessSQL SQL, ref Usuarios Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_USUA_Eliminar");
        SQL.DAAddParameter("@pintUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
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
