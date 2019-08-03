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
  public partial class BLTablas
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Tablas> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLTablas(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Tablas>();
      Tablas l_item = new Tablas();
      Loader.EntityType = l_item.GetType();
    }
    public BLTablas()
    {
      Loader = new BusinessEntityLoader<Tablas>();
      Tablas l_item = new Tablas();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Tablas> POSAD_TABL_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, String Tabla, Nullable<Boolean> Activo)
    {
      try
      {
        ObservableCollection<Tablas> l_items = null;
        Tablas l_item = null;
        SQL.DAAssignProcedure("POSAD_TABL_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_Tabla", Tabla, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pbitTABL_Activo", Activo, SqlDbType.Bit, 1);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Tablas>();
          while (reader.Read())
          {
            l_item = new Tablas();
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
    private Tablas POSAD_TABL_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, String Tabla, String Interno)
    {
      try
      {
        Tablas l_item = null;
        SQL.DAAssignProcedure("POSAD_TABL_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_Tabla", Tabla, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_Interno", Interno, SqlDbType.VarChar, 4);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Tablas();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TABL_Insertar(MyDataAccessSQL SQL, ref Tablas Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TABL_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrTABL_Tabla", Item.TABL_Tabla, SqlDbType.VarChar, 3, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrTABL_Interno", Item.TABL_Interno, SqlDbType.VarChar, 4, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchTABL_Nombre", Item.TABL_Nombre, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTABL_Nomenclatura", Item.TABL_Nomenclatura, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchTABL_Descripcion", Item.TABL_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTABL_CodigoInternacional", Item.TABL_CodigoInternacional, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo1", Item.TABL_Codigo1, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo2", Item.TABL_Codigo2, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo3", Item.TABL_Codigo3, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pintTABL_Numero1", Item.TABL_Numero1, SqlDbType.Int);
        SQL.DAAddParameter("@pdecTABL_Numero2", Item.TABL_Numero2, SqlDbType.Decimal, 20, 2);
        SQL.DAAddParameter("@pdecTABL_Numero3", Item.TABL_Numero3, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pbitTABL_Activo", Item.TABL_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Item.TABL_Tabla = SQL.ParameterOutPut("@pchrTABL_Tabla");
          Item.TABL_Interno = SQL.ParameterOutPut("@pchrTABL_Interno");
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TABL_Actualizar(MyDataAccessSQL SQL, ref Tablas Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TABL_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_Tabla", Item.TABL_Tabla, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_Interno", Item.TABL_Interno, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pvchTABL_Nombre", Item.TABL_Nombre, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTABL_Nomenclatura", Item.TABL_Nomenclatura, SqlDbType.VarChar, 25);
        SQL.DAAddParameter("@pvchTABL_Descripcion", Item.TABL_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTABL_CodigoInternacional", Item.TABL_CodigoInternacional, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo1", Item.TABL_Codigo1, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo2", Item.TABL_Codigo2, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchTABL_Codigo3", Item.TABL_Codigo3, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pintTABL_Numero1", Item.TABL_Numero1, SqlDbType.Int);
        SQL.DAAddParameter("@pdecTABL_Numero2", Item.TABL_Numero2, SqlDbType.Decimal, 20, 2);
        SQL.DAAddParameter("@pdecTABL_Numero3", Item.TABL_Numero3, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pbitTABL_Activo", Item.TABL_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TABL_Eliminar(MyDataAccessSQL SQL, ref Tablas Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TABL_Eliminar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_Tabla", Item.TABL_Tabla, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_Interno", Item.TABL_Interno, SqlDbType.VarChar, 4);
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
