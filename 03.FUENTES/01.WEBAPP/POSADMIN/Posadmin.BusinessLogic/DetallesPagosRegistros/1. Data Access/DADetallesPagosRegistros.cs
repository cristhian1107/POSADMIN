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
using SoftwareFactory.Infrastructure.Utilities;

namespace Posadmin.BusinessLogic
{
  public partial class BLDetallesPagosRegistros
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<DetallesPagosRegistros> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLDetallesPagosRegistros(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<DetallesPagosRegistros>();
      DetallesPagosRegistros l_item = new DetallesPagosRegistros();
      Loader.EntityType = l_item.GetType();
    }
    public BLDetallesPagosRegistros()
    {
      Loader = new BusinessEntityLoader<DetallesPagosRegistros>();
      DetallesPagosRegistros l_item = new DetallesPagosRegistros();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<DetallesPagosRegistros> POSAD_PAGO_ConsultaPorRegistro(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 RegistroInterno)
    {
      try
      {
        ObservableCollection<DetallesPagosRegistros> l_items = null;
        DetallesPagosRegistros l_item = null;
        SQL.DAAssignProcedure("POSAD_PAGO_ConsultaPorRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", EmpresaInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", SucursalInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigREGI_Interno", RegistroInterno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<DetallesPagosRegistros>();
          while (reader.Read())
          {
            l_item = new DetallesPagosRegistros();
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
    private ObservableCollection<DetallesPagosRegistros> POSAD_PAGO_MovimientosUsuario(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular)
    {
      try
      {
        ObservableCollection<DetallesPagosRegistros> l_items = null;
        DetallesPagosRegistros l_item = null;
        SQL.DAAssignProcedure("POSAD_PAGO_MovimientosUsuario");
        SQL.DAAddParameter("@pbigEMPR_Interno", EmpresaInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", SucursalInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigUSUA_Interno", UsuarioInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbitCIER_Calcular", Calcular, SqlDbType.Bit);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<DetallesPagosRegistros>();
          while (reader.Read())
          {
            l_item = new DetallesPagosRegistros();
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
    private Boolean POSAD_PAGO_Insertar(MyDataAccessSQL SQL, ref DetallesPagosRegistros Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PAGO_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigREGI_Interno", Item.REGI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintPAGO_Item", Item.PAGO_Item, SqlDbType.Int);
        SQL.DAAddParameter("@pchrPAGO_Tipo", Item.PAGO_Tipo, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pdecPAGO_MontoCancelado", Item.PAGO_MontoCancelado, SqlDbType.Char, 20, 8);
        SQL.DAAddParameter("@pdtmPAGO_FechaHoraRegistro", Item.PAGO_FechaHoraRegistro, SqlDbType.DateTime);
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
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
