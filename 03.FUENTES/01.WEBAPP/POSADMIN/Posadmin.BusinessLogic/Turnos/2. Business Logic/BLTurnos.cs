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
  public partial class BLTurnos : IBLTurnos
  {
    #region [ METODOS ]

    public ObservableCollection<Turnos> BLConsultaTodos(String Conexion, Int64 Empresa, Nullable<Int64> Sucursal)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_TURN_ConsultaTodos(l_mydataaccesssql, Empresa, Sucursal);
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
    public Turnos BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_TURN_ConsultaRegistro(l_mydataaccesssql, Empresa, Sucursal, Interno);
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
    public Boolean BLSave(String Conexion, ref Turnos Item)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            bool l_isCorrect = true;
            l_mydataaccesssql.DABeginTransaction();
            switch (Item.Instance)
            {
              case InstanceEntity.Added:
                l_isCorrect = POSAD_TURN_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_TURN_Actualizar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_TURN_Eliminar(l_mydataaccesssql, ref Item);
                break;
            }
            if (l_isCorrect)
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
