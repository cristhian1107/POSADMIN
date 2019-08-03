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
    public IBLTurnos BL_Turnos { get; set; }

    #endregion

    #region [ METODOS ]

    public ObservableCollection<Turnos> ConsultaTodosTurnos(String Conexion, Int64 Empresa, Nullable<Int64> Sucursal)
    {
      try
      {
        return BL_Turnos.BLConsultaTodos(Conexion, Empresa, Sucursal);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Turnos ConsultaTurno(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        return BL_Turnos.BLConsultaRegistro(Conexion, Empresa, Sucursal, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveTurnos(String Conexion, ref Turnos Item)
    {
      try
      {
        return BL_Turnos.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
