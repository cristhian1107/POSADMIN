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

    ObservableCollection<Paises> ConsultaTodosPaises(String Conexion, String Nombre);
    ObservableCollection<Paises> ConsultaActivosPaises(String Conexion);
    Paises ConsultaPais(String Conexion, Int32 Interno);
    Boolean SavePaises(String Conexion, ref Paises Item);

    #endregion

  }
}
