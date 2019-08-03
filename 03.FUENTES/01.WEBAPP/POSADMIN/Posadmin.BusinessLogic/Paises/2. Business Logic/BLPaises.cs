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
  public partial class BLPaises : IBLPaises
  {
    #region [ METODOS ]

    public ObservableCollection<Paises> BLConsultaTodos(String Conexion, String Nombre)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_PAIS_ConsultaTodos(l_mydataaccesssql, Nombre);
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
    public ObservableCollection<Paises> BLConsultaActivos(String Conexion)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_PAIS_ConsultaActivos(l_mydataaccesssql);
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
    public Paises BLConsultaPais(String Conexion, Int32 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_PAIS_ConsultaRegistro(l_mydataaccesssql, Interno);
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
    public Boolean BLSave(String Conexion, ref Paises Item)
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
                l_isCorrect = POSAD_PAIS_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_PAIS_Actualizar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_PAIS_Eliminar(l_mydataaccesssql, ref Item);
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
