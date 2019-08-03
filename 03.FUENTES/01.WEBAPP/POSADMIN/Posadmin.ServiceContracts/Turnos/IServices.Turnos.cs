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

    ObservableCollection<Turnos> ConsultaTodosTurnos(String Conexion, Int64 Empresa, Nullable<Int64> Sucursal);
    Turnos ConsultaTurno(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    Boolean SaveTurnos(String Conexion, ref Turnos Item);

    #endregion

  }
}
