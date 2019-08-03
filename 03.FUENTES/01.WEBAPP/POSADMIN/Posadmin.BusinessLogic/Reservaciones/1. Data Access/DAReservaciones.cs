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
  public partial class BLReservaciones
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Reservaciones> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLReservaciones(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Reservaciones>();
      Reservaciones l_item = new Reservaciones();
      Loader.EntityType = l_item.GetType();
    }
    public BLReservaciones()
    {
      Loader = new BusinessEntityLoader<Reservaciones>();
      Reservaciones l_item = new Reservaciones();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Reservaciones> POSAD_RESE_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, String Estado, String Huesped, String Numero)
    {
      try
      {
        ObservableCollection<Reservaciones> l_items = null;
        Reservaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_RESE_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrRESE_Estado", Estado, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pvchRESE_HuespedNombreCompleto", Huesped, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pchrHABI_Numero", Numero, SqlDbType.Char, 3);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Reservaciones>();
          while (reader.Read())
          {
            l_item = new Reservaciones();
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
    private Reservaciones POSAD_RESE_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        Reservaciones l_item = null;
        SQL.DAAssignProcedure("POSAD_RESE_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigRESE_Interno", Interno, SqlDbType.BigInt);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Reservaciones();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_RESE_Insertar(MyDataAccessSQL SQL, ref Reservaciones Item)
    {
      try
      {
        String l_mensaje = null;
        SQL.DAAssignProcedure("POSAD_RESE_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigRESE_Interno", Item.RESE_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchRESE_Mensaje", l_mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrRESE_Estado", Item.RESE_Estado, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pdteRESE_FechaInicio", Item.RESE_FechaInicio, SqlDbType.Date);
        SQL.DAAddParameter("@pdteRESE_FechaFin", Item.RESE_FechaFin, SqlDbType.Date);
        SQL.DAAddParameter("@pdtmRESE_FechaHoraRegistro", MyUtilities.GetDateTimeCountry("es-PE"), SqlDbType.DateTime);
        SQL.DAAddParameter("@pvchRESE_HuespedId", Item.RESE_HuespedId, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchRESE_HuespedNombreCompleto", Item.RESE_HuespedNombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchRESE_HuespedDireccion", Item.RESE_HuespedDireccion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchRESE_Descripcion", Item.RESE_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        SQL.DAExecuteNonQuery();

        l_mensaje = SQL.ParameterOutPut("@pvchRESE_Mensaje");
        Int64 EMPR_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
        { Item.EMPR_Interno = EMPR_Interno; }
        Int64 SUCU_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigSUCU_Interno"), out SUCU_Interno))
        { Item.SUCU_Interno = SUCU_Interno; }
        Int64 RESE_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigRESE_Interno"), out RESE_Interno))
        { Item.RESE_Interno = RESE_Interno; }
        return l_mensaje;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_RESE_Actualizar(MyDataAccessSQL SQL, ref Reservaciones Item)
    {
      try
      {
        String l_mensaje = null;
        SQL.DAAssignProcedure("POSAD_RESE_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigRESE_Interno", Item.RESE_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchRESE_Mensaje", l_mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pchrRESE_Estado", Item.RESE_Estado, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pdteRESE_FechaInicio", Item.RESE_FechaInicio, SqlDbType.Date);
        SQL.DAAddParameter("@pdteRESE_FechaFin", Item.RESE_FechaFin, SqlDbType.Date);
        SQL.DAAddParameter("@pdtmRESE_FechaHoraRegistro", MyUtilities.GetDateTimeCountry("es-PE"), SqlDbType.DateTime);
        SQL.DAAddParameter("@pvchRESE_HuespedId", Item.RESE_HuespedId, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchRESE_HuespedNombreCompleto", Item.RESE_HuespedNombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchRESE_HuespedDireccion", Item.RESE_HuespedDireccion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchRESE_Descripcion", Item.RESE_Descripcion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        SQL.DAExecuteNonQuery();

        l_mensaje = SQL.ParameterOutPut("@pvchRESE_Mensaje");
        return l_mensaje;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_RESE_ActualizaEstado(MyDataAccessSQL SQL, ref Reservaciones Item)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_RESE_ActualizaEstado");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigRESE_Interno", Item.RESE_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrRESE_Estado", Item.RESE_Estado, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return null; }
        else
        { return "No se realizo ninguna modificacion"; }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
