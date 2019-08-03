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
  public partial interface IBLUsuarios
  {
    #region [ METODOS ]

    Usuarios BLVerificarUsuario(String Conexion, String IdEmpresa, String Usuario, String Contrasena);
    Usuarios BLConsultaUsuarioEmpresa(String Conexion, String IdEmpresa, Int64 Interno);
    ObservableCollection<Usuarios> BLConsultaTodos(String Conexion, String Nombre);
    ObservableCollection<Usuarios> BLConsultaPorEmpresa(String Conexion, Int64 Empresa);
    Usuarios BLConsultaUsuario(String Conexion, Int64 Interno);
    Boolean BLSave(String Conexion, ref Usuarios Item);

    #endregion
  }
}
