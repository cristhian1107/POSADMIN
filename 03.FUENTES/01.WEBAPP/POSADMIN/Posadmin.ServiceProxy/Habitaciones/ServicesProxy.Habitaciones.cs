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

namespace Posadmin.ServiceProxy
{

  public partial class ServicesProxy : IServices
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLHabitaciones BL_Habitaciones { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Habitaciones> ConsultaTodosHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        return BL_Habitaciones.BLConsultaTodos(Conexion, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Habitaciones> ConsultaLibresHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        return BL_Habitaciones.BLConsultaLibres(Conexion, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Habitaciones> ConsultaEstadoHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        return BL_Habitaciones.BLConsultaEstado(Conexion, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Habitaciones ConsultaHabitacion(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        return BL_Habitaciones.BLConsultaRegistro(Conexion, Empresa, Sucursal, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveHabitaciones(String Conexion, ref Habitaciones Item)
    {
      try
      {
        return BL_Habitaciones.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean ActualizarLimpiezaHabitaciones(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno, Boolean Limpio, String Usuario)
    {
      try
      {
        return BL_Habitaciones.BLActualizarLimpieza(Conexion, Empresa, Sucursal, Interno, Limpio, Usuario);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
