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
    public IBLRetiros BL_Retiros { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Retiros> ConsultaUltimosMovimientosRetiros(String Conexion, Int64 Empresa, Int64 Sucursal)
    {
      try
      {
        return BL_Retiros.BLUltimosMovimientos(Conexion, Empresa, Sucursal);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
