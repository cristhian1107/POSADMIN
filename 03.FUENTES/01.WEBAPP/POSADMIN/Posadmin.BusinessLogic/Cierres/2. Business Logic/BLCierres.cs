using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftwareFactory.Infrastructure.BusinessEntity;
using SoftwareFactory.Infrastructure.DataAccess;
using Posadmin.BusinessEntities;
using System.Data;
using System.Collections.ObjectModel;

namespace Posadmin.BusinessLogic
{
  public partial class BLCierres : IBLCierres
  {
    #region [ METODOS ]

    public ObservableCollection<Cierres> BLUltimosMovimientos(String Conexion, Int64 Empresa, Int64 Sucursal)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_CIER_UltimosMovimientos(l_mydataaccesssql, Empresa, Sucursal);
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Cierres> BLMovimientosPendientes(String Conexion, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            l_mydataaccesssql.DABeginTransaction();
            var l_list = POSAD_CIER_MovimientosPendientes(l_mydataaccesssql, EmpresaInterno, SucursalInterno, UsuarioInterno, Calcular);
            l_mydataaccesssql.DACommitTransaction();
            return l_list;
          }
          catch (Exception ex)
          {
            l_mydataaccesssql.DARollbackTransaction();
            throw ex;
          }
        }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
