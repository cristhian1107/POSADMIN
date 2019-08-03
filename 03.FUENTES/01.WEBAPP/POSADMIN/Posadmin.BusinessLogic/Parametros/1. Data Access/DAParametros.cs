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
  public partial class BLParametros
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Parametros> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLParametros(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Parametros>();
      Parametros l_item = new Parametros();
      Loader.EntityType = l_item.GetType();
    }
    public BLParametros()
    {
      Loader = new BusinessEntityLoader<Parametros>();
      Parametros l_item = new Parametros();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Parametros> POSAD_PARA_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, String Llave)
    {
      try
      {
        ObservableCollection<Parametros> l_items = null;
        Parametros l_item = null;
        SQL.DAAssignProcedure("POSAD_PARA_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrPARA_Llave", Llave, SqlDbType.Char, 5);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Parametros>();
          while (reader.Read())
          {
            l_item = new Parametros();
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
    private Parametros POSAD_PARA_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, String Llave)
    {
      try
      {
        Parametros l_item = null;
        SQL.DAAssignProcedure("POSAD_PARA_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrPARA_Llave", Llave, SqlDbType.Char, 5);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Parametros();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_PARA_Insertar(MyDataAccessSQL SQL, ref Parametros Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PARA_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrPARA_Llave", Item.PARA_Llave, SqlDbType.Char, 5, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchPARA_Valor", Item.PARA_Valor, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchPARA_Descripcion", Item.PARA_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Item.PARA_Llave = SQL.ParameterOutPut("@pchrPARA_Llave");
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_PARA_Actualizar(MyDataAccessSQL SQL, ref Parametros Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_PARA_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrPARA_Llave", Item.PARA_Llave, SqlDbType.Char, 5);
        SQL.DAAddParameter("@pvchPARA_Valor", Item.PARA_Valor, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchPARA_Descripcion", Item.PARA_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
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
