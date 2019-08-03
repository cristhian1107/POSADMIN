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
    public IBLSucursales BL_Sucursales { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Sucursales> ConsultaTodosSucursales(String Conexion, Int64 Empresa, String Nombre)
    {
      try
      {
        return BL_Sucursales.BLConsultaTodos(Conexion, Empresa, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Sucursales ConsultaSucursal(String Conexion, Int64 Empresa, Int64 Interno)
    {
      try
      {
        return BL_Sucursales.BLConsultaRegistro(Conexion, Empresa, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveSucursales(String Conexion, ref Sucursales Item)
    {
      try
      {
        return BL_Sucursales.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
