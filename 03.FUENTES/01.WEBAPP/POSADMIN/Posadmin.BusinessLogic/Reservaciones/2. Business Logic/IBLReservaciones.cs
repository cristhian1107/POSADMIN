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
  public partial interface IBLReservaciones
  {
    #region [ METODOS ]

    ObservableCollection<Reservaciones> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String Huesped, String Numero);
    Reservaciones BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    String BLSave(String Conexion, ref Reservaciones Item);

    #endregion
  }
}
