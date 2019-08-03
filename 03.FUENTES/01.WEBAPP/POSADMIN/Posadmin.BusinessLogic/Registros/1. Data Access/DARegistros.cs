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
  public partial class BLRegistros
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLDetallesPagosRegistros BL_DetallesPagosRegistros { get; set; }
    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Registros> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLRegistros(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Registros>();
      Registros l_item = new Registros();
      Loader.EntityType = l_item.GetType();
      BL_DetallesPagosRegistros = new BLDetallesPagosRegistros();
    }
    public BLRegistros()
    {
      Loader = new BusinessEntityLoader<Registros>();
      Registros l_item = new Registros();
      Loader.EntityType = l_item.GetType();
      BL_DetallesPagosRegistros = new BLDetallesPagosRegistros();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Registros> POSAD_REGI_ConsultaTodos(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, String Estado, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA, String Huesped, String Numero)
    {
      try
      {
        ObservableCollection<Registros> l_items = null;
        Registros l_item = null;
        SQL.DAAssignProcedure("POSAD_REGI_ConsultaTodos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrHABI_Estado", Estado, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pchrTABL_TablaPIS", TablaPIS, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoPIS", InternoPIS, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pchrTABL_TablaTHA", TablaTHA, SqlDbType.VarChar, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTHA", InternoTHA, SqlDbType.VarChar, 4);
        SQL.DAAddParameter("@pvchREGI_HuespedNombreCompleto", Huesped, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pchrHABI_Numero", Numero, SqlDbType.Char, 3);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Registros>();
          while (reader.Read())
          {
            l_item = new Registros();
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
    private Registros POSAD_REGI_ConsultaRegistro(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Registro, Int64 Habitacion, ref String Mensaje)
    {
      try
      {
        Registros l_item = null;
        Mensaje = null;
        SQL.DAAssignProcedure("POSAD_REGI_ConsultaRegistro");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigREGI_Interno", Registro, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Habitacion, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchREGI_Mensaje", Mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Registros();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Registros POSAD_REGI_CalcularCostos(MyDataAccessSQL SQL, Registros Item)
    {
      try
      {
        Registros l_item = null;
        SQL.DAAssignProcedure("POSAD_REGI_CalcularCostos");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrREGI_Tramos", Item.REGI_Tramos, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pintREGI_Cantidad", Item.REGI_Cantidad, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          if (reader.Read())
          {
            l_item = new Registros();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
          }
        }
        return l_item;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_REGI_Insertar(MyDataAccessSQL SQL, ref Registros Item)
    {
      try
      {
        String l_mensaje = null;
        SQL.DAAssignProcedure("POSAD_REGI_Insertar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigREGI_Interno", Item.REGI_Interno, SqlDbType.BigInt, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pvchREGI_Mensaje", l_mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrREGI_Tramos", Item.REGI_Tramos, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pintREGI_Cantidad", Item.REGI_Cantidad, SqlDbType.Int);
        SQL.DAAddParameter("@pdtmREGI_FechaHoraEntrada", Item.REGI_FechaHoraEntrada, SqlDbType.DateTime);
        SQL.DAAddParameter("@pdtmREGI_FechaHoraSalida", Item.REGI_FechaHoraSalida, SqlDbType.DateTime);
        SQL.DAAddParameter("@pchrTABL_TablaTDI", Item.TABL_TablaTDI, SqlDbType.Char, 3);
        SQL.DAAddParameter("@pchrTABL_InternoTDI", Item.TABL_InternoTDI, SqlDbType.Char, 4);
        SQL.DAAddParameter("@pbigENTI_Interno", Item.ENTI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchREGI_HuespedId", Item.REGI_HuespedId, SqlDbType.VarChar, 50);
        SQL.DAAddParameter("@pvchREGI_HuespedNombreCompleto", Item.REGI_HuespedNombreCompleto, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchREGI_HuespedDireccion", Item.REGI_HuespedDireccion, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pdecREGI_PrecioSugerido", Item.REGI_PrecioSugerido, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pdecREGI_PrecioCobrado", Item.REGI_PrecioCobrado, SqlDbType.Decimal, 20, 8);
        SQL.DAAddParameter("@pbigUSUA_Interno", Item.USUA_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioCreacion, SqlDbType.VarChar, 25);
        SQL.DAExecuteNonQuery();

        l_mensaje = SQL.ParameterOutPut("@pvchREGI_Mensaje");
        Int64 EMPR_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigEMPR_Interno"), out EMPR_Interno))
        { Item.EMPR_Interno = EMPR_Interno; }
        Int64 SUCU_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigSUCU_Interno"), out SUCU_Interno))
        { Item.SUCU_Interno = SUCU_Interno; }
        Int64 REGI_Interno;
        if (Int64.TryParse(SQL.ParameterOutPut("@pbigREGI_Interno"), out REGI_Interno))
        { Item.REGI_Interno = REGI_Interno; }
        return l_mensaje;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_REGI_Actualizar(MyDataAccessSQL SQL, ref Registros Item)
    {
      try
      {
        String l_mensaje = null;
        SQL.DAAssignProcedure("POSAD_REGI_Actualizar");
        SQL.DAAddParameter("@pbigEMPR_Interno", Item.EMPR_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Item.SUCU_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigREGI_Interno", Item.REGI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchREGI_Mensaje", l_mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pbigHABI_Interno", Item.HABI_Interno, SqlDbType.BigInt);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        SQL.DAExecuteNonQuery();

        l_mensaje = SQL.ParameterOutPut("@pvchREGI_Mensaje");
        return l_mensaje;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private String POSAD_REGI_Acciones(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int64 Registro, String Accion, String Motivo, String Usuario, ref Decimal Monto)
    {
      try
      {
        String l_mensaje = null;
        SQL.DAAssignProcedure("POSAD_REGI_Acciones");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigREGI_Interno", Registro, SqlDbType.BigInt);
        SQL.DAAddParameter("@pchrREGI_Accion", Accion, SqlDbType.Char, 1);
        SQL.DAAddParameter("@pvchREGI_Mensaje", l_mensaje, SqlDbType.VarChar, -1, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pdecREGI_Monto", Monto, SqlDbType.Decimal, 20, 8, ParameterDirection.InputOutput);
        SQL.DAAddParameter("@pdtmREGI_FechaHora", MyUtilities.GetDateTimeCountry("es-PE"), SqlDbType.DateTime);
        SQL.DAAddParameter("@pvchREGI_MotivoAnulacion", Motivo, SqlDbType.VarChar, 250);
        SQL.DAAddParameter("@pvchAUDI_UsuarioModificacion", Usuario, SqlDbType.VarChar, 25);
        SQL.DAExecuteNonQuery();

        l_mensaje = SQL.ParameterOutPut("@pvchREGI_Mensaje");
        Decimal MontoFinal = 0;
        if (Decimal.TryParse(SQL.ParameterOutPut("@pdecREGI_Monto"), out MontoFinal))
        { Monto = MontoFinal; }
        return l_mensaje;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ INDICADORES ]

    private DataSet POSAD_REPO_IndicadorAnual(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int32 Anio)
    {
      try
      {
        DataSet ds_result = new DataSet();
        SQL.DAAssignProcedure("POSAD_REPO_IndicadorAnual");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintREPO_Anio", Anio, SqlDbType.Int);
        ds_result = SQL.DAExecuteDataSet();
        return ds_result;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private DataSet POSAD_REPO_IndicadorMensual(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, Int32 Anio, Int32 Mes)
    {
      try
      {
        DataSet ds_result = new DataSet();
        SQL.DAAssignProcedure("POSAD_REPO_IndicadorMensual");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pintREPO_Anio", Anio, SqlDbType.Int);
        SQL.DAAddParameter("@pintREPO_Mes", Mes, SqlDbType.Int);
        ds_result = SQL.DAExecuteDataSet();
        return ds_result;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private DataSet POSAD_REPO_IndicadorSemanal(MyDataAccessSQL SQL, Int64 Empresa, Int64 Sucursal, DateTime Desde, DateTime Hasta)
    {
      try
      {
        DataSet ds_result = new DataSet();
        SQL.DAAssignProcedure("POSAD_REPO_IndicadorSemanal");
        SQL.DAAddParameter("@pbigEMPR_Interno", Empresa, SqlDbType.BigInt);
        SQL.DAAddParameter("@pbigSUCU_Interno", Sucursal, SqlDbType.BigInt);
        SQL.DAAddParameter("@pdteREPO_Desde", Desde, SqlDbType.Date);
        SQL.DAAddParameter("@pdteREPO_Hasta", Hasta, SqlDbType.Date);
        ds_result = SQL.DAExecuteDataSet();
        return ds_result;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
