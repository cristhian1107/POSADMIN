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
    public IBLUbigeos BL_Ubigeos { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Ubigeos> ConsultaTodosUbigeos(String Conexion, String Nombre)
    {
      try
      {
        return BL_Ubigeos.BLConsultaTodos(Conexion, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Ubigeos> ConsultaUbigeosPorNiveles(String Conexion, Nullable<Int32> InternoPadre, Int32 InternoPais)
    {
      try
      {
        return BL_Ubigeos.BLConsultaPorNiveles(Conexion, InternoPadre, InternoPais);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Ubigeos> ConsultaPadresActivosUbigeos(String Conexion)
    {
      try
      {
        return BL_Ubigeos.BLConsultaPadresActivos(Conexion);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Ubigeos ConsultaUbigeo(String Conexion, Int32 Interno)
    {
      try
      {
        return BL_Ubigeos.BLConsultaUbigeo(Conexion, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveUbigeos(String Conexion, ref Ubigeos Item)
    {
      try
      {
        return BL_Ubigeos.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
