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
    public IBLParametros BL_Parametros { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Parametros> ConsultaTodosParametros(String Conexion, Int64 Empresa, String Llave)
    {
      try
      {
        return BL_Parametros.BLConsultaTodos(Conexion, Empresa, Llave);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Parametros ConsultaParametro(String Conexion, Int64 Empresa, String Llave)
    {
      try
      {
        return BL_Parametros.BLConsultaRegistro(Conexion, Empresa, Llave);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveParametros(String Conexion, ref Parametros Item)
    {
      try
      {
        return BL_Parametros.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
