using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareFactory.Infrastructure.BusinessEntity;
using SoftwareFactory.Infrastructure.DataAccess;
using Posadmin.BusinessEntities;
using System.Data;
using System.Collections.ObjectModel;

namespace Posadmin.BusinessLogic
{
  public partial interface IBLRegistros
  {
    #region [ METODOS ]

    ObservableCollection<Registros> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA, String Huesped, String Numero);
    Registros BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, Int64 Habitacion, ref String Mensaje);
    Registros BLCalcularRegistro(String Conexion, Registros Item);
    String BLAcciones(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, String Accion, String Motivo, String Usuario, ref Decimal Monto);
    String BLSave(String Conexion, ref Registros Item);

    #endregion

    #region [ INDICADORES ]

    DataSet BLIndicadorAnual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio);
    DataSet BLIndicadorMensual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio, Int32 Mes);
    DataSet BLIndicadorSemanal(String Conexion, Int64 Empresa, Int64 Sucursal, DateTime Desde, DateTime Hasta);

    #endregion
  }
}
