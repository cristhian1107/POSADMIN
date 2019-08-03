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
  public partial class BLPaises
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Paises> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLPaises(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Paises>();
      Paises l_item = new Paises();
      Loader.EntityType = l_item.GetType();
    }
    public BLPaises()
    {
      Loader = new BusinessEntityLoader<Paises>();
      Paises l_item = new Paises();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Paises> POSAD_PAIS_ConsultaTodos(MyDataAccessSQL SQL, String Nombre)
    {
      try
      {
        ObservableCollection<Paises> l_items = null;
        Paises l_item = null;
        SQL.DAAssignProcedure("POSAD_PAIS_ConsultaTodos");
        SQL.DAAddParameter("@pvchPAIS_Nombre", Nombre, SqlDbType.VarChar, 100);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Paises>();
          while (reader.Read())
          {
            l_item = new Paises();
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
    private ObservableCollection<Paises> POSAD_PAIS_ConsultaActivos(MyDataAccessSQL SQL)
    {
      try
      {
        ObservableCollection<Paises> l_items = null;
        Paises l_item = null;
        SQL.DAAssignProcedure("POSAD_PAIS_ConsultaActivos");
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Paises>();
          while (reader.Read())
          {
            l_item = new Paises();
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
    private Paises POSAD_PAIS_ConsultaRegistro(MyDataAccessSQL SQL, Int32 Interno)
    {
      try
      {
        Paises l_item = null;
        SQL.DAAssignProcedure("POSAD_PAIS_ConsultaRegistro");
        SQL.DAAddParameter("@pintPAIS_Interno", Interno, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Paises();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_PAIS_Insertar(MyDataAccessSQL SQL, ref Paises Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PAIS_Insertar");
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchPAIS_Nombre", Item.PAIS_Nombre, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchPAIS_CodigoNumerico", Item.PAIS_CodigoNumerico, SqlDbType.VarChar, 5);
        SQL.DAAddParameter("@pchrPAIS_CodigoAlfa2", Item.PAIS_CodigoAlfa2, SqlDbType.VarChar, 2);
        SQL.DAAddParameter("@pchrPAIS_CodigoAlfa3", Item.PAIS_CodigoAlfa3, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pvchPAIS_Continente", Item.PAIS_Continente, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchPAIS_Descripcion", Item.PAIS_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pbitPAIS_Activo", Item.PAIS_Activo, SqlDbType.Bit, 1);
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
    private Boolean POSAD_PAIS_Actualizar(MyDataAccessSQL SQL, ref Paises Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PAIS_Actualizar");
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchPAIS_Nombre", Item.PAIS_Nombre, SqlDbType.VarChar, 100);
        SQL.DAAddParameter("@pvchPAIS_CodigoNumerico", Item.PAIS_CodigoNumerico, SqlDbType.VarChar, 5);
        SQL.DAAddParameter("@pchrPAIS_CodigoAlfa2", Item.PAIS_CodigoAlfa2, SqlDbType.VarChar, 2);
        SQL.DAAddParameter("@pchrPAIS_CodigoAlfa3", Item.PAIS_CodigoAlfa3, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pvchPAIS_Continente", Item.PAIS_Continente, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchPAIS_Descripcion", Item.PAIS_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pbitPAIS_Activo", Item.PAIS_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
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
    private Boolean POSAD_PAIS_Eliminar(MyDataAccessSQL SQL, ref Paises Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PAIS_Eliminar");
        SQL.DAAddParameter("@pintPAIS_Interno", Item.PAIS_Interno, SqlDbType.BigInt);
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
