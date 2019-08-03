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
  public partial class BLReservaciones : IBLReservaciones
  {
    #region [ METODOS ]

    public ObservableCollection<Reservaciones> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String Huesped, String Numero)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_RESE_ConsultaTodos(l_mydataaccesssql, Empresa, Sucursal, Estado, Huesped, Numero);
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
    public Reservaciones BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_RESE_ConsultaRegistro(l_mydataaccesssql, Empresa, Sucursal, Interno);
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
    public String BLSave(String Conexion, ref Reservaciones Item)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            string l_isCorrect = null;
            l_mydataaccesssql.DABeginTransaction();
            switch (Item.Instance)
            {
              case InstanceEntity.Added:
                l_isCorrect = POSAD_RESE_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_RESE_Actualizar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_RESE_ActualizaEstado(l_mydataaccesssql, ref Item);
                break;
            }
            if (string.IsNullOrEmpty(l_isCorrect))
            { l_mydataaccesssql.DACommitTransaction(); }
            else
            { l_mydataaccesssql.DARollbackTransaction(); }
            return l_isCorrect;
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
