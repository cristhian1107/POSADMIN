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
  public partial interface IBLRetiros
  {
    #region [ METODOS ]

    ObservableCollection<Retiros> BLUltimosMovimientos(String Conexion, Int64 Empresa, Int64 Sucursal);

    #endregion
  }
}
