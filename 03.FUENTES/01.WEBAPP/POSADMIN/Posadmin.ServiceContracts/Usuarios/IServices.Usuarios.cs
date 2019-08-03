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

    Usuarios VerificarUsuario(String Conexion, String IdEmpresa, String Usuario, String Contrasena);
    Usuarios ConsultaUsuarioEmpresa(String Conexion, String IdEmpresa, Int64 Interno);
    ObservableCollection<Usuarios> ConsultaTodosUsuarios(String Conexion, String Nombre);
    ObservableCollection<Usuarios> ConsultaUsuariosPorEmpresa(String Conexion, Int64 Empresa);
    Usuarios ConsultaUsuario(String Conexion, Int64 Interno);
    Boolean SaveUsuarios(String Conexion, ref Usuarios Item);

    #endregion

  }
}
