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
  public partial class BLConfiguraciones
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Configuraciones> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLConfiguraciones(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Configuraciones>();
      Configuraciones l_item = new Configuraciones();
      Loader.EntityType = l_item.GetType();
    }
    public BLConfiguraciones()
    {
      Loader = new BusinessEntityLoader<Configuraciones>();
      Configuraciones l_item = new Configuraciones();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Configuraciones> POSAD_CONF_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Usuario, String Llave)
    {
      try
      {
        ObservableCollection<Configuraciones> l_items = null;
        Configuraciones l_item = null;
        SQL.DAAssignProcedure("POSAD_CONF_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigUSUA_Interno", Usuario, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrCONF_Llave", Llave, SqlDbType.Char, 5);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Configuraciones>();
          while (reader.Read())
          {
            l_item = new Configuraciones();
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
    private Configuraciones POSAD_CONF_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Usuario, String Llave)
    {
      try
      {
        Configuraciones l_item = null;
        SQL.DAAssignProcedure("POSAD_CONF_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigUSUA_Interno", Usuario, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrCONF_Llave", Llave, SqlDbType.Char, 5);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Configuraciones();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_CONF_Insertar(MyDataAccessSQL SQL, ref Configuraciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_CONF_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrCONF_Llave", Item.CONF_Llave, SqlDbType.Char, 5, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchCONF_Valor", Item.CONF_Valor, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchCONF_Descripcion", Item.CONF_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        {
          Int64 EMPR_Interno;
          if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
          { Item.EMPR_Interno = EMPR_Interno; }
          Item.CONF_Llave = SQL.ParameterOutPut("@pchrCONF_Llave");
          return true;
        }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_CONF_Actualizar(MyDataAccessSQL SQL, ref Configuraciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_CONF_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrCONF_Llave", Item.CONF_Llave, SqlDbType.Char, 5);
        SQL.DAAddParameter("@pvchCONF_Valor", Item.CONF_Valor, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchCONF_Descripcion", Item.CONF_Descripcion, SqlDbType.VarChar, 250);
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
