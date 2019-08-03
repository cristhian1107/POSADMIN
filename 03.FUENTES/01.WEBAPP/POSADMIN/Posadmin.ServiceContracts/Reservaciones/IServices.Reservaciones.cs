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

    ObservableCollection<Reservaciones> ConsultaTodosReservaciones(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String Huesped, String Numero);
    Reservaciones ConsultaReservacion(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    String SaveReservaciones(String Conexion, ref Reservaciones Item);

    #endregion

  }
}
