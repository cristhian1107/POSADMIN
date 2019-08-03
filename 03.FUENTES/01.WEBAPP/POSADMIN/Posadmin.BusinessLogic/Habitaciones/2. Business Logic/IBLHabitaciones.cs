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
  public partial interface IBLHabitaciones
  {
    #region [ METODOS ]

    ObservableCollection<Habitaciones> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    ObservableCollection<Habitaciones> BLConsultaLibres(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    ObservableCollection<Habitaciones> BLConsultaEstado(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    Habitaciones BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    Boolean BLSave(String Conexion, ref Habitaciones Item);
    Boolean BLActualizarLimpieza(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno, Boolean Limpio, String Usuario);

    #endregion
  }
}
