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

    ObservableCollection<Sucursales> ConsultaTodosSucursales(String Conexion, Int64 Empresa, String Nombre);
    Sucursales ConsultaSucursal(String Conexion, Int64 Empresa, Int64 Interno);
    Boolean SaveSucursales(String Conexion, ref Sucursales Item);

    #endregion

  }
}
