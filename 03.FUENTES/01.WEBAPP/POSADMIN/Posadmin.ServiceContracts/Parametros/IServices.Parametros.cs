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

    ObservableCollection<Parametros> ConsultaTodosParametros(String Conexion, Int64 Empresa, String Llave);
    Parametros ConsultaParametro(String Conexion, Int64 Empresa, String Llave);
    Boolean SaveParametros(String Conexion, ref Parametros Item);

    #endregion

  }
}
