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
  public partial class BLUbigeos
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Ubigeos> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLUbigeos(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Ubigeos>();
      Ubigeos l_item = new Ubigeos();
      Loader.EntityType = l_item.GetType();
    }
    public BLUbigeos()
    {
      Loader = new BusinessEntityLoader<Ubigeos>();
      Ubigeos l_item = new Ubigeos();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Ubigeos> POSAD_UBIG_ConsultaTodos(MyDataAccessSQL SQL, String Nombre)
    {
      try
      {
        ObservableCollection<Ubigeos> l_items = null;
        Ubigeos l_item = null;
        SQL.DAAssignProcedure("POSAD_UBIG_ConsultaTodos");
        SQL.DAAddParameter("@pvchUBIG_Nombre", Nombre, SqlDbType.VarChar, 100);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Ubigeos>();
          while (reader.Read())
          {
            l_item = new Ubigeos();
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
    private ObservableCollection<Ubigeos> POSAD_UBIG_ConsultaPadresActivos(MyDataAccessSQL SQL)
    {
      try
      {
        ObservableCollection<Ubigeos> l_items = null;
        Ubigeos l_item = null;
        SQL.DAAssignProcedure("POSAD_UBIG_ConsultaPadresActivos");
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Ubigeos>();
          while (reader.Read())
          {
            l_item = new Ubigeos();
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
    private ObservableCollection<Ubigeos> POSAD_UBIG_PorNiveles(MyDataAccessSQL SQL, Nullable<Int32> InternoPadre, Int32 InternoPais)
    {
      try
      {
        ObservableCollection<Ubigeos> l_items = null;
        Ubigeos l_item = null;
        SQL.DAAssignProcedure("POSAD_UBIG_PorNiveles");
        SQL.DAAddParameter("@pintUBIG_InternoPadre", InternoPadre, SqlDbType.Int);
        SQL.DAAddParameter("@pintPAIS_Interno", InternoPais, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Ubigeos>();
          while (reader.Read())
          {
            l_item = new Ubigeos();
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
    private Ubigeos POSAD_UBIG_ConsultaRegistro(MyDataAccessSQL SQL, Int32 Interno)
    {
      try
      {
        Ubigeos l_item = null;
        SQL.DAAssignProcedure("POSAD_UBIG_ConsultaRegistro");
        SQL.DAAddParameter("@pintUBIG_Interno", Interno, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Ubigeos();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_UBIG_Insertar(MyDataAccessSQL SQL, ref Ubigeos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_UBIG_Insertar");
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchUBIG_Nombre", Item.UBIG_Nombre, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchUBIG_Nomenclatura", Item.UBIG_Nomenclatura, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchUBIG_Descripcion", Item.UBIG_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchUBIG_Codigo1", Item.UBIG_Codigo1, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchUBIG_Codigo2", Item.UBIG_Codigo2, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchUBIG_Codigo3", Item.UBIG_Codigo3, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pbitUBIG_Activo", Item.UBIG_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pintUBIG_InternoPadre", Item.UBIG_InternoPadre, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int32 UBIG_Interno;
          if (Int32.TryParse(SQL.ParameterOutPut("@pintUBIG_Interno"), out UBIG_Interno))
          { Item.PAIS_Interno = UBIG_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_UBIG_Actualizar(MyDataAccessSQL SQL, ref Ubigeos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_UBIG_Actualizar");
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchUBIG_Nombre", Item.UBIG_Nombre, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchUBIG_Nomenclatura", Item.UBIG_Nomenclatura, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchUBIG_Descripcion", Item.UBIG_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchUBIG_Codigo1", Item.UBIG_Codigo1, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchUBIG_Codigo2", Item.UBIG_Codigo2, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchUBIG_Codigo3", Item.UBIG_Codigo3, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pbitUBIG_Activo", Item.UBIG_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pintUBIG_InternoPadre", Item.UBIG_InternoPadre, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int32 UBIG_Interno;
          if (Int32.TryParse(SQL.ParameterOutPut("@pintUBIG_Interno"), out UBIG_Interno))
          { Item.PAIS_Interno = UBIG_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_UBIG_Eliminar(MyDataAccessSQL SQL, ref Ubigeos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_UBIG_Eliminar");
        SQL.DAAddParameter("@pintUBIG_Interno", Item.UBIG_Interno, SqlDbType.BigInt);
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
