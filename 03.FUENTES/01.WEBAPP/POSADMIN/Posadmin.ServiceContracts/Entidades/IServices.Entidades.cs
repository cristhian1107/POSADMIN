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

    ObservableCollection<Entidades> AyudaEntidades(String Conexion, Int64 Empresa, String TipoEntidad, String Indicio);
    ObservableCollection<Entidades> ConsultaTodosEntidades(String Conexion, Int64 Empresa, String Id, String Nombre);
    Entidades ConsultaEntidad(String Conexion, Int64 Empresa, Int64 Interno);
    Boolean SaveEntidades(String Conexion, ref Entidades Item);

    #endregion

  }
}
