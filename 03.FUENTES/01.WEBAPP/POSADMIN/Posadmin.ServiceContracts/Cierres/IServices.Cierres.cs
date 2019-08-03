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

    ObservableCollection<Cierres> ConsultaUltimosMovimientosCierres(String Conexion, Int64 Empresa, Int64 Sucursal);
    ObservableCollection<Cierres> ConsultaMovimientosPendientesCierres(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Usuario, Boolean Calcular);

    #endregion

  }
}
