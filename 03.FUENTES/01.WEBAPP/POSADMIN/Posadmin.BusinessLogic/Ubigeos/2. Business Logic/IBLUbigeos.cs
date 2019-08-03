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
  public partial interface IBLUbigeos
  {
    #region [ METODOS ]

    ObservableCollection<Ubigeos> BLConsultaTodos(String Conexion, String Nombre);
    ObservableCollection<Ubigeos> BLConsultaPadresActivos(String Conexion);
    ObservableCollection<Ubigeos> BLConsultaPorNiveles(String Conexion, Nullable<Int32> InternoPadre, Int32 InternoPais);
    Ubigeos BLConsultaUbigeo(String Conexion, Int32 Interno);
    Boolean BLSave(String Conexion, ref Ubigeos Item);

    #endregion
  }
}
