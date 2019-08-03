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
    public IBLConfiguraciones BL_Configuraciones { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Configuraciones> ConsultaTodosConfiguraciones(String Conexion, Int64 Empresa, Int64 Usuario, String Llave)
    {
      try
      {
        return BL_Configuraciones.BLConsultaTodos(Conexion, Empresa, Usuario, Llave);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Configuraciones ConsultaConfiguracion(String Conexion, Int64 Empresa, Int64 Usuario, String Llave)
    {
      try
      {
        return BL_Configuraciones.BLConsultaRegistro(Conexion, Empresa, Usuario, Llave);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveConfiguraciones(String Conexion, ref Configuraciones Item)
    {
      try
      {
        return BL_Configuraciones.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
