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
  public partial interface IBLEmpresas
  {
    #region [ METODOS ]

    ObservableCollection<Empresas> BLConsultaPorUsuarios(MyDataAccessSQL SQL, Int64 UsuarioInterno);
    Boolean BLAsignarPorUsuario(MyDataAccessSQL SQL, ref Empresas Item, Boolean Primero);
    ObservableCollection<Empresas> BLConsultaTodos(String Conexion, String Id, String RazonSocial);
    Empresas BLConsultaEmpresa(String Conexion, Int64 Interno);
    Boolean BLSave(String Conexion, ref Empresas Item);

    #endregion
  }
}
