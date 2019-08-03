using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Posadmin.ServiceContracts;
using Posadmin.BusinessLogic;
using Posadmin.BusinessEntities;


namespace Posadmin.ServiceProxy
{

  public partial class ServicesProxy : IServices
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLTablas BL_Tablas { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Tablas> ConsultaTodosTablas(String Conexion, Int64 Empresa, String Tabla, Nullable<Boolean> Activo)
    {
      try
      {
        return BL_Tablas.BLConsultaTodos(Conexion,Empresa, Tabla, Activo);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Tablas ConsultaTabla(String Conexion, Int64 Empresa, String Tabla, String Interno)
    {
      try
      {
        return BL_Tablas.BLConsultaRegistro(Conexion, Empresa, Tabla, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveTablas(String Conexion, ref Tablas Item)
    {
      try
      {
        return BL_Tablas.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
