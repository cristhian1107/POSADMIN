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
  public partial interface IBLCierres
  {
    #region [ METODOS ]

    ObservableCollection<Cierres> BLUltimosMovimientos(String Conexion, Int64 Empresa, Int64 Sucursal);
    ObservableCollection<Cierres> BLMovimientosPendientes(String Conexion, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular);

    #endregion
  }
}
