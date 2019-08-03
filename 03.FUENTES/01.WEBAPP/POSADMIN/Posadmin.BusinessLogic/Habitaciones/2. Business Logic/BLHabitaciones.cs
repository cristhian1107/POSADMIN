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
  public partial class BLHabitaciones : IBLHabitaciones
  {
    #region [ METODOS ]

    public ObservableCollection<Habitaciones> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_HABI_ConsultaTodos(l_mydataaccesssql, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
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
    public ObservableCollection<Habitaciones> BLConsultaLibres(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_HABI_ConsultaLibres(l_mydataaccesssql, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
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
    public ObservableCollection<Habitaciones> BLConsultaEstado(String Conexion, Int64 Empresa, Int64 Sucursal, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_HABI_ConsultaEstado(l_mydataaccesssql, Empresa, Sucursal, TablaPIS, InternoPIS, TablaTHA, InternoTHA);
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
    public Habitaciones BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_HABI_ConsultaRegistro(l_mydataaccesssql, Empresa, Sucursal, Interno);
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
    public Boolean BLSave(String Conexion, ref Habitaciones Item)
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
                l_isCorrect = POSAD_HABI_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_HABI_Actualizar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_HABI_Eliminar(l_mydataaccesssql, ref Item);
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
    public Boolean BLActualizarLimpieza(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Interno, Boolean Limpio, String Usuario)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            bool l_isCorrect = true;
            l_mydataaccesssql.DABeginTransaction();
            l_isCorrect = POSAD_HABI_ActualizarLimpieza(l_mydataaccesssql, Empresa, Sucursal, Interno, Limpio, Usuario);
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
