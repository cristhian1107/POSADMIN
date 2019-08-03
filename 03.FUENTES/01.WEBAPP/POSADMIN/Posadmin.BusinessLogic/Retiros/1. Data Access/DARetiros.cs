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
  public partial class BLRetiros
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Retiros> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLRetiros(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Retiros>();
      Retiros l_item = new Retiros();
      Loader.EntityType = l_item.GetType();
    }
    public BLRetiros()
    {
      Loader = new BusinessEntityLoader<Retiros>();
      Retiros l_item = new Retiros();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Retiros> POSAD_RETI_UltimosMovimientos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal)
    {
      try
      {
        ObservableCollection<Retiros> l_items = null;
        Retiros l_item = null;
        SQL.DAAssignProcedure("POSAD_RETI_UltimosMovimientos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Retiros>();
          while (reader.Read())
          {
            l_item = new Retiros();
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
