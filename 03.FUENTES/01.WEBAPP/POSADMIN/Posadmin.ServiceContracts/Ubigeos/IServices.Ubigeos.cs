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

    ObservableCollection<Ubigeos> ConsultaTodosUbigeos(String Conexion, String Nombre);
    ObservableCollection<Ubigeos> ConsultaPadresActivosUbigeos(String Conexion);
    ObservableCollection<Ubigeos> ConsultaUbigeosPorNiveles(String Conexion, Nullable<Int32> InternoPadre, Int32 InternoPais);
    Ubigeos ConsultaUbigeo(String Conexion, Int32 Interno);
    Boolean SaveUbigeos(String Conexion, ref Ubigeos Item);

    #endregion

  }
}
