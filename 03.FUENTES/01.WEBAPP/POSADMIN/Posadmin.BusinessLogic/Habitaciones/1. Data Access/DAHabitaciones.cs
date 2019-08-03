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
  public partial class BLHabitaciones
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Habitaciones> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLHabitaciones(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Habitaciones>();
      Habitaciones l_item = new Habitaciones();
      Loader.EntityType = l_item.GetType();
    }
    public BLHabitaciones()
    {
      Loader = new BusinessEntityLoader<Habitaciones>();
      Habitaciones l_item = new Habitaciones();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Habitaciones> POSAD_HABI_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        ObservableCollection<Habitaciones> l_items = null;
        Habitaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_HABI_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", TablaPIS, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", InternoPIS, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", TablaTHA, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", InternoTHA, SqlDbType.VarChar, 4);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Habitaciones>();
          while (reader.Read())
          {
            l_item = new Habitaciones();
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
    private ObservableCollection<Habitaciones> POSAD_HABI_ConsultaLibres(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        ObservableCollection<Habitaciones> l_items = null;
        Habitaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_HABI_ConsultaLibres");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", TablaPIS, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", InternoPIS, SqlDbType.Char, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", TablaTHA, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", InternoTHA, SqlDbType.Char, 4);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Habitaciones>();
          while (reader.Read())
          {
            l_item = new Habitaciones();
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
    private ObservableCollection<Habitaciones> POSAD_HABI_ConsultaEstado(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        ObservableCollection<Habitaciones> l_items = null;
        Habitaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_HABI_ConsultaEstado");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", TablaPIS, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", InternoPIS, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", TablaTHA, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", InternoTHA, SqlDbType.VarChar, 4);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Habitaciones>();
          while (reader.Read())
          {
            l_item = new Habitaciones();
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
    private Habitaciones POSAD_HABI_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        Habitaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_HABI_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Habitaciones();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_HABI_Insertar(MyDataAccessSQL SQL, ref Habitaciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_HABI_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", Item.TABL_TablaPIS, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", Item.TABL_InternoPIS, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", Item.TABL_TablaTHA, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", Item.TABL_InternoTHA, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrHABI_Numero", Item.HABI_Numero, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pbitHABI_Activo", Item.HABI_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchHABI_Descripcion", Item.HABI_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pdecHABI_PrecioDia", Item.HABI_PrecioDia, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pdecHABI_PrecioHora", Item.HABI_PrecioHora, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pdecHABI_PrecioMinimo", Item.HABI_PrecioMinimo, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Int64 SUCU_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigSUCU_Interno"), out SUCU_Interno))
          { Item.SUCU_Interno = SUCU_Interno; }
          Int64 HABI_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigHABI_Interno"), out HABI_Interno))
          { Item.HABI_Interno = HABI_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_HABI_Actualizar(MyDataAccessSQL SQL, ref Habitaciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_HABI_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", Item.TABL_TablaPIS, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", Item.TABL_InternoPIS, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", Item.TABL_TablaTHA, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", Item.TABL_InternoTHA, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrHABI_Numero", Item.HABI_Numero, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pbitHABI_Activo", Item.HABI_Activo, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchHABI_Descripcion", Item.HABI_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pdecHABI_PrecioDia", Item.HABI_PrecioDia, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pdecHABI_PrecioHora", Item.HABI_PrecioHora, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pdecHABI_PrecioMinimo", Item.HABI_PrecioMinimo, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_HABI_Eliminar(MyDataAccessSQL SQL, ref Habitaciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_HABI_Eliminar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_HABI_ActualizarLimpieza(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Interno, Boolean Limpio, String Usuario)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_HABI_ActualizarLimpieza");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbitHABI_Limpio", Limpio, SqlDbType.Bit, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Usuario, SqlDbType.VarChar, 25);
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
