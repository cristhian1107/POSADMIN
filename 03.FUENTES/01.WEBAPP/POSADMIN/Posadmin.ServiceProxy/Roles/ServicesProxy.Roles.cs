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
    public IBLRoles BL_Roles { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Roles> ConsultaTodosRoles(String Conexion, String Nombre)
    {
      try
      {
        return BL_Roles.BLConsultaTodos(Conexion, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Roles ConsultaRol(String Conexion, Int32 Interno)
    {
      try
      {
        return BL_Roles.BLConsultaRol(Conexion, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveRoles(String Conexion, ref Roles Item)
    {
      try
      {
        return BL_Roles.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
