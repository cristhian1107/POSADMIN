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
  public partial class BLRoles
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLOpciones BL_Opciones { get; set; }
    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Roles> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLRoles(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Roles>();
      Roles l_item = new Roles();
      Loader.EntityType = l_item.GetType();
      BL_Opciones = new BLOpciones();
    }
    public BLRoles()
    {
      Loader = new BusinessEntityLoader<Roles>();
      Roles l_item = new Roles();
      Loader.EntityType = l_item.GetType();
      BL_Opciones = new BLOpciones();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Roles> POSAD_ROLE_ConsultaTodos(MyDataAccessSQL SQL, String Nombre)
    {
      try
      {
        ObservableCollection<Roles> l_items = null;
        Roles l_item = null;
        SQL.DAAssignProcedure("POSAD_ROLE_ConsultaTodos");
        SQL.DAAddParameter("@pvchROLE_Nombre", Nombre, SqlDbType.VarChar, 25);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Roles>();
          while (reader.Read())
          {
            l_item = new Roles();
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
    private Roles POSAD_ROLE_ConsultaRegistro(MyDataAccessSQL SQL, Int32 Interno)
    {
      try
      {
        Roles l_item = null;
        SQL.DAAssignProcedure("POSAD_ROLE_ConsultaRegistro");
        SQL.DAAddParameter("@pintROLE_Interno", Interno, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Roles();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ROLE_Insertar(MyDataAccessSQL SQL, ref Roles Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ROLE_Insertar");
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchROLE_Nombre", Item.ROLE_Nombre, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchROLE_Descripcion", Item.ROLE_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int32 ROLE_Interno;
          if (Int32.TryParse(SQL.ParameterOutPut("@pintROLE_Interno"), out ROLE_Interno))
          { Item.ROLE_Interno = ROLE_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ROLE_Actualizar(MyDataAccessSQL SQL, ref Roles Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ROLE_Actualizar");
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pvchROLE_Nombre", Item.ROLE_Nombre, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchROLE_Descripcion", Item.ROLE_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_ROLE_Eliminar(MyDataAccessSQL SQL, ref Roles Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_ROLE_Eliminar");
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int);
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
