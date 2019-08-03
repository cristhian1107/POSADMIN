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

    ObservableCollection<Roles> ConsultaTodosRoles(String Conexion, String Nombre);
    Roles ConsultaRol(String Conexion, Int32 Interno);
    Boolean SaveRoles(String Conexion, ref Roles Item);

    #endregion

  }
}
