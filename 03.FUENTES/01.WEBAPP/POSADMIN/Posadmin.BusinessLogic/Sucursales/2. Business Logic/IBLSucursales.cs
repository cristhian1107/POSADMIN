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
  public partial interface IBLSucursales
  {
    #region [ METODOS ]

    ObservableCollection<Sucursales> BLConsultaTodos(String Conexion, Int64 Empresa, String Nombre);
    Sucursales BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Interno);
    Boolean BLSave(String Conexion, ref Sucursales Item);

    #endregion
  }
}
