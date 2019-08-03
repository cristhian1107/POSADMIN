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
  public partial interface IBLRoles
  {
    #region [ METODOS ]

    ObservableCollection<Roles> BLConsultaTodos(String Conexion, String Nombre);
    Roles BLConsultaRol(String Conexion, Int32 Interno);
    Boolean BLSave(String Conexion, ref Roles Item);

    #endregion
  }
}
