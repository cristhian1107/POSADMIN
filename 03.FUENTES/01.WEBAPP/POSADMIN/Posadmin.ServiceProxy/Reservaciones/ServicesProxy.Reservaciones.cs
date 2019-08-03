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
    public IBLReservaciones BL_Reservaciones { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Reservaciones> ConsultaTodosReservaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String Huesped, String Numero)
    {
      try
      {
        return BL_Reservaciones.BLConsultaTodos(Conexion, Empresa, Sucursal, Estado, Huesped, Numero);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Reservaciones ConsultaReservacion(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        return BL_Reservaciones.BLConsultaRegistro(Conexion, Empresa, Sucursal, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public String SaveReservaciones(String Conexion, ref Reservaciones Item)
    {
      try
      {
        return BL_Reservaciones.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
