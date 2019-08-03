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
  public partial interface IBLPaises
  {
    #region [ METODOS ]

    ObservableCollection<Paises> BLConsultaTodos(String Conexion, String Nombre);
    ObservableCollection<Paises> BLConsultaActivos(String Conexion);
    Paises BLConsultaPais(String Conexion, Int32 Interno);
    Boolean BLSave(String Conexion, ref Paises Item);

    #endregion
  }
}
