using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using Posadmin.BusinessEntities;

namespace Posadmin.ServiceContracts
{
  public partial interface IServices
  {

    #region [ METODOS ]

    ObservableCollection<Habitaciones> ConsultaTodosHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    ObservableCollection<Habitaciones> ConsultaLibresHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    ObservableCollection<Habitaciones> ConsultaEstadoHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA);
    Habitaciones ConsultaHabitacion(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    Boolean SaveHabitaciones(String Conexion, ref Habitaciones Item);
    Boolean ActualizarLimpiezaHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno, Boolean Limpio, String Usuario);

    #endregion

  }
}
