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

    ObservableCollection<Empresas> ConsultaTodosEmpresas(String Conexion, String Id, String RazonSocial);
    Empresas ConsultaEmpresa(String Conexion, Int64 Interno);
    Boolean SaveEmpresas(String Conexion, ref Empresas Item);

    #endregion

  }
}
