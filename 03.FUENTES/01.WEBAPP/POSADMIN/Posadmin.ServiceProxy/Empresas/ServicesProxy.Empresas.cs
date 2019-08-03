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
    public IBLEmpresas BL_Empresas { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Empresas> ConsultaTodosEmpresas(String Conexion, String Id, String RazonSocial)
    {
      try
      {
        return BL_Empresas.BLConsultaTodos(Conexion, Id, RazonSocial);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Empresas ConsultaEmpresa(String Conexion, Int64 Interno)
    {
      try
      {
        return BL_Empresas.BLConsultaEmpresa(Conexion, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveEmpresas(String Conexion, ref Empresas Item)
    {
      try
      {
        return BL_Empresas.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
