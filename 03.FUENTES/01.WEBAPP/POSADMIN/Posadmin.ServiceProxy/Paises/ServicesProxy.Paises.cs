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
    public IBLPaises BL_Paises { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Paises> ConsultaTodosPaises(String Conexion, String Nombre)
    {
      try
      {
        return BL_Paises.BLConsultaTodos(Conexion, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Paises> ConsultaActivosPaises(String Conexion)
    {
      try
      {
        return BL_Paises.BLConsultaActivos(Conexion);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Paises ConsultaPais(String Conexion, Int32 Interno)
    {
      try
      {
        return BL_Paises.BLConsultaPais(Conexion, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SavePaises(String Conexion, ref Paises Item)
    {
      try
      {
        return BL_Paises.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
