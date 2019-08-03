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

    ObservableCollection<Configuraciones> ConsultaTodosConfiguraciones(String Conexion, Int64 Empresa, Int64 Usuario, String Llave);
    Configuraciones ConsultaConfiguracion(String Conexion, Int64 Empresa, Int64 Usuario, String Llave);
    Boolean SaveConfiguraciones(String Conexion, ref Configuraciones Item);

    #endregion

  }
}
