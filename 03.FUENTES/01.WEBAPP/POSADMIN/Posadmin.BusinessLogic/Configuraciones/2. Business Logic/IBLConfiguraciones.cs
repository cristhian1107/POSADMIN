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
  public partial interface IBLConfiguraciones
  {
    #region [ METODOS ]

    ObservableCollection<Configuraciones> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Usuario, String Llave);
    Configuraciones BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Usuario, String Llave);
    Boolean BLSave(String Conexion, ref Configuraciones Item);

    #endregion
  }
}
