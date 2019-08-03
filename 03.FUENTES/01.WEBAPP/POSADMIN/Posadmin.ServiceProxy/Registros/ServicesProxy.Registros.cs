using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Posadmin.ServiceContracts;
using Posadmin.BusinessLogic;
using Posadmin.BusinessEntities;
using System.Data;

namespace Posadmin.ServiceProxy
{

  public partial class ServicesProxy : IServices
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLRegistros BL_Registros { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Registros> ConsultaTodosRegistros(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA, String Huesped, String Numero)
    {
      try
      {
        return BL_Registros.BLConsultaTodos(Conexion, Empresa, Sucursal, Estado, TablaPIS, InternoPIS, TablaTHA, InternoTHA, Huesped, Numero);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Registros ConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, Int64 Habitacion, ref String Mensaje)
    {
      try
      {
        return BL_Registros.BLConsultaRegistro(Conexion, Empresa, Sucursal, Registro, Habitacion, ref Mensaje);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Registros CalcularRegistro(String Conexion, Registros Item)
    {
      try
      {
        return BL_Registros.BLCalcularRegistro(Conexion, Item);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public String AccionesRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, String Accion, String Motivo, String Usuario, ref Decimal Monto)
    {
      try
      {
        return BL_Registros.BLAcciones(Conexion, Empresa, Sucursal, Registro, Accion, Motivo, Usuario, ref Monto);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public String SaveRegistros(String Conexion, ref Registros Item)
    {
      try
      {
        return BL_Registros.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    #region [ INDICADORES ]

    public DataSet ReporteIndicadorAnual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio)
    {
      try
      {
        return BL_Registros.BLIndicadorAnual(Conexion, Empresa, Sucursal, Anio);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public DataSet ReporteIndicadorMensual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio, Int32 Mes)
    {
      try
      {
        return BL_Registros.BLIndicadorMensual(Conexion, Empresa, Sucursal, Anio, Mes);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public DataSet ReporteIndicadorSemanal(String Conexion, Int64 Empresa, Int64 Sucursal, DateTime Desde, DateTime Hasta)
    {
      try
      {
        return BL_Registros.BLIndicadorSemanal(Conexion, Empresa, Sucursal, Desde, Hasta);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
