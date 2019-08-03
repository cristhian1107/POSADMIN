using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SoftwareFactory.Infrastructure.BusinessEntity;
using SoftwareFactory.Infrastructure.DataAccess;
using SoftwareFactory.Infrastructure.Utilities;
using Posadmin.BusinessEntities;
using System.Data;
using System.Collections.ObjectModel;

namespace Posadmin.BusinessLogic
{
  public partial class BLCierres
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Cierres> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLCierres(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Cierres>();
      Cierres l_item = new Cierres();
      Loader.EntityType = l_item.GetType();
    }
    public BLCierres()
    {
      Loader = new BusinessEntityLoader<Cierres>();
      Cierres l_item = new Cierres();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Cierres> POSAD_CIER_UltimosMovimientos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal)
    {
      try
      {
        ObservableCollection<Cierres> l_items = null;
        Cierres l_item = null;
        SQL.DAAssignProcedure("POSAD_CIER_UltimosMovimientos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Cierres>();
          while (reader.Read())
          {
            l_item = new Cierres();
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
    private ObservableCollection<Cierres> POSAD_CIER_MovimientosPendientes(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular)
    {
      try
      {
        ObservableCollection<Cierres> l_items = null;
        Cierres l_item = null;
        SQL.DAAssignProcedure("POSAD_CIER_MovimientosPendientes");
        SQL.DAAddParameter("@pbigEMPR_Interno", EmpresaInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", SucursalInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigUSUA_Interno", UsuarioInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbitRETI_Calcular", Calcular, SqlDbType.Bit);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Cierres>();
          while (reader.Read())
          {
            l_item = new Cierres();
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

    #endregion
  }
}
