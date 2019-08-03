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
  public partial class BLFuncionesEntidades
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<FuncionesEntidades> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLFuncionesEntidades(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<FuncionesEntidades>();
      FuncionesEntidades l_item = new FuncionesEntidades();
      Loader.EntityType = l_item.GetType();
    }
    public BLFuncionesEntidades()
    {
      Loader = new BusinessEntityLoader<FuncionesEntidades>();
      FuncionesEntidades l_item = new FuncionesEntidades();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<FuncionesEntidades> POSAD_FUNC_ColsultaPorEntidad(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 EntidadInterno)
    {
      try
      {
        ObservableCollection<FuncionesEntidades> l_items = null;
        FuncionesEntidades l_item = null;
        SQL.DAAssignProcedure("POSAD_FUNC_ColsultaPorEntidad");
        SQL.DAAddParameter("@pbigEMPR_Interno", EmpresaInterno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigENTI_Interno", EntidadInterno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<FuncionesEntidades>();
          while (reader.Read())
          {
            l_item = new FuncionesEntidades();
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
    private Boolean POSAD_FUNC_AsignarPorEntidad(MyDataAccessSQL SQL, ref FuncionesEntidades Item, Boolean Primero)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_FUNC_AsignarPorEntidad");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigENTI_Interno", Item.ENTI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrTABL_TablaTEN", Item.TABL_TablaTEN, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTEN", Item.TABL_InternoTEN, SqlDbType.Char, 4);
        SQL.DAAddParameter("@pbitFUNC_Primero", Primero, SqlDbType.Bit);
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
