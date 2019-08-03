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
  public partial class BLTablas : IBLTablas
  {
    #region [ METODOS ]

    public ObservableCollection<Tablas> BLConsultaTodos(String Conexion, Int64 Empresa, String Tabla, Nullable<Boolean> Activo)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_TABL_ConsultaTodos(l_mydataaccesssql, Empresa, Tabla, Activo);
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
    public Tablas BLConsultaRegistro(String Conexion, Int64 Empresa, String Tabla, String Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_TABL_ConsultaRegistro(l_mydataaccesssql, Empresa, Tabla, Interno);
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
    public Boolean BLSave(String Conexion, ref Tablas Item)
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
                l_isCorrect = POSAD_TABL_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_TABL_Actualizar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_TABL_Eliminar(l_mydataaccesssql, ref Item);
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
