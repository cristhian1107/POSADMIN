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
  public partial interface IBLParametros
  {
    #region [ METODOS ]

    ObservableCollection<Parametros> BLConsultaTodos(String Conexion, Int64 Empresa, String Llave);
    Parametros BLConsultaRegistro(String Conexion, Int64 Empresa, String Llave);
    Boolean BLSave(String Conexion, ref Parametros Item);

    #endregion
  }
}
