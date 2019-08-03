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
    public IBLCierres BL_Cierres { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Cierres> ConsultaUltimosMovimientosCierres(String Conexion, Int64 Empresa, Int64 Sucursal)
    {
      try
      {
        return BL_Cierres.BLUltimosMovimientos(Conexion, Empresa, Sucursal);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Cierres> ConsultaMovimientosPendientesCierres(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Usuario, Boolean Calcular)
    {
      try
      {
        return BL_Cierres.BLMovimientosPendientes(Conexion, Empresa, Sucursal, Usuario, Calcular);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
