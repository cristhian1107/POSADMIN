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
  public partial interface IBLEntidades
  {
    #region [ METODOS ]

    ObservableCollection<Entidades> BLAyuda(String Conexion, Int64 Empresa, String TipoEntidad, String Indicio);
    ObservableCollection<Entidades> BLConsultaTodos(String Conexion, Int64 Empresa, String Id, String Nombre);
    Entidades BLConsultaEntidad(String Conexion, Int64 Empresa, Int64 Interno);
    Boolean BLSave(String Conexion, ref Entidades Item);

    #endregion
  }
}
