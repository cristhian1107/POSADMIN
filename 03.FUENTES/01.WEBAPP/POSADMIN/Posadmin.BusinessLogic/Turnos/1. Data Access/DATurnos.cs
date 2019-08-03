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
  public partial class BLTurnos
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Turnos> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLTurnos(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Turnos>();
      Turnos l_item = new Turnos();
      Loader.EntityType = l_item.GetType();
    }
    public BLTurnos()
    {
      Loader = new BusinessEntityLoader<Turnos>();
      Turnos l_item = new Turnos();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Turnos> POSAD_TURN_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, Nullable<Int64> Sucursal)
    {
      try
      {
        ObservableCollection<Turnos> l_items = null;
        Turnos l_item = null;
        SQL.DAAssignProcedure("POSAD_TURN_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Turnos>();
          while (reader.Read())
          {
            l_item = new Turnos();
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
    private Turnos POSAD_TURN_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        Turnos l_item = null;
        SQL.DAAssignProcedure("POSAD_TURN_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigTURN_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Turnos();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TURN_Insertar(MyDataAccessSQL SQL, ref Turnos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TURN_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigTURN_Interno", Item.TURN_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchTURN_Nominacion", Item.TURN_Nominacion, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@ptimTURN_HoraInicio", Item.TURN_HoraInicio, SqlDbType.DateTime);
        SQL.DAAddParameter("@ptimTURN_HoraFin", Item.TURN_HoraFin, SqlDbType.DateTime);
        SQL.DAAddParameter("@pvchTURN_Descripcion", Item.TURN_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTURN_Color", Item.TURN_Color, SqlDbType.VarChar, 20);
        SQL.DAAddParameter("@pbitTURN_Activo", Item.TURN_Activo, SqlDbType.Bit);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Int64 SUCU_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigSUCU_Interno"), out SUCU_Interno))
          { Item.SUCU_Interno = SUCU_Interno; }
          Int64 TURN_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigTURN_Interno"), out TURN_Interno))
          { Item.TURN_Interno = TURN_Interno; }
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TURN_Actualizar(MyDataAccessSQL SQL, ref Turnos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TURN_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigTURN_Interno", Item.TURN_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchTURN_Nominacion", Item.TURN_Nominacion, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@ptimTURN_HoraInicio", Item.TURN_HoraInicio, SqlDbType.DateTime);
        SQL.DAAddParameter("@ptimTURN_HoraFin", Item.TURN_HoraFin, SqlDbType.DateTime);
        SQL.DAAddParameter("@pvchTURN_Descripcion", Item.TURN_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchTURN_Color", Item.TURN_Color, SqlDbType.VarChar, 20);
        SQL.DAAddParameter("@pbitTURN_Activo", Item.TURN_Activo, SqlDbType.Bit);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_TURN_Eliminar(MyDataAccessSQL SQL, ref Turnos Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_TURN_Eliminar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigTURN_Interno", Item.TURN_Interno, SqlDbType.BigInt);
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
