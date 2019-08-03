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
  public partial interface IBLDetallesPagosRegistros
  {
    #region [ METODOS ]

    ObservableCollection<DetallesPagosRegistros> BLConsultaPorRegistro(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 RegistroInterno);
    ObservableCollection<DetallesPagosRegistros> BLMovimientosUsuario(String Conexion, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular);
    Boolean BLSave(MyDataAccessSQL SQL, ref DetallesPagosRegistros Item);

    #endregion
  }
}
