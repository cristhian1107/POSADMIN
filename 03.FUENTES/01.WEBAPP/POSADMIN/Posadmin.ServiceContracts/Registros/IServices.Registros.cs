using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using Posadmin.BusinessEntities;
using System.Data;

namespace Posadmin.ServiceContracts
{
  public partial interface IServices
  {

    #region [ METODOS ]

    ObservableCollection<Registros> ConsultaTodosRegistros(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA, String Huesped, String Numero);
    Registros ConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, Int64 Habitacion, ref String Mensaje);
    Registros CalcularRegistro(String Conexion, Registros Item);
    String AccionesRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, String Accion, String Motivo, String Usuario, ref Decimal Monto);
    String SaveRegistros(String Conexion, ref Registros Item);

    #endregion

    #region [ INDICADORES ]

    DataSet ReporteIndicadorAnual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio);
    DataSet ReporteIndicadorMensual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio, Int32 Mes);
    DataSet ReporteIndicadorSemanal(String Conexion, Int64 Empresa, Int64 Sucursal, DateTime Desde, DateTime Hasta);

    #endregion

  }
}
