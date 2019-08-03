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

    ObservableCollection<Tablas> ConsultaTodosTablas(String Conexion, Int64 Empresa, String Tabla, Nullable<Boolean> Activo);
    Tablas ConsultaTabla(String Conexion, Int64 Empresa, String Tabla, String Interno);
    Boolean SaveTablas(String Conexion, ref Tablas Item);

    #endregion

  }
}
