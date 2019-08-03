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
  public partial class BLDetallesPagosRegistros : IBLDetallesPagosRegistros
  {
    #region [ METODOS ]

    public ObservableCollection<DetallesPagosRegistros> BLConsultaPorRegistro(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 RegistroInterno)
    {
      try
      {
        return POSAD_PAGO_ConsultaPorRegistro(SQL, EmpresaInterno, SucursalInterno, RegistroInterno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<DetallesPagosRegistros> BLMovimientosUsuario(String Conexion, Int64 EmpresaInterno, Int64 SucursalInterno, Int64 UsuarioInterno, Boolean Calcular)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            l_mydataaccesssql.DABeginTransaction();
            var l_list = POSAD_PAGO_MovimientosUsuario(l_mydataaccesssql, EmpresaInterno, SucursalInterno, UsuarioInterno, Calcular);
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
    public Boolean BLSave(MyDataAccessSQL SQL, ref DetallesPagosRegistros Item)
    {
      try
      {
        bool l_isCorrect = true;
        l_isCorrect = POSAD_PAGO_Insertar(SQL, ref Item);
        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
