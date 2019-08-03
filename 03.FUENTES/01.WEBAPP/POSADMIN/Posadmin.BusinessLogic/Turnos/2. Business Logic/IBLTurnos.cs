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
  public partial interface IBLTurnos
  {
    #region [ METODOS ]

    ObservableCollection<Turnos> BLConsultaTodos(String Conexion, Int64 Empresa, Nullable<Int64> Sucursal);
    Turnos BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno);
    Boolean BLSave(String Conexion, ref Turnos Item);

    #endregion
  }
}
