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
  public partial interface IBLTablas
  {
    #region [ METODOS ]

    ObservableCollection<Tablas> BLConsultaTodos(String Conexion, Int64 Empresa, String Tabla, Nullable<Boolean> Activo);
    Tablas BLConsultaRegistro(String Conexion, Int64 Empresa, String Tabla, String Interno);
    Boolean BLSave(String Conexion, ref Tablas Item);

    #endregion
  }
}
