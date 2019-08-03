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
    public IBLEntidades BL_Entidades { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Entidades> AyudaEntidades(String Conexion, Int64 Empresa, String TipoEntidad, String Indicio)
    {
      try
      {
        return BL_Entidades.BLAyuda(Conexion, Empresa, TipoEntidad, Indicio);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Entidades> ConsultaTodosEntidades(String Conexion, Int64 Empresa, String Id, String Nombre)
    {
      try
      {
        return BL_Entidades.BLConsultaTodos(Conexion, Empresa, Id, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Entidades ConsultaEntidad(String Conexion, Int64 Empresa, Int64 Interno)
    {
      try
      {
        return BL_Entidades.BLConsultaEntidad(Conexion, Empresa, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveEntidades(String Conexion, ref Entidades Item)
    {
      try
      {
        return BL_Entidades.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
