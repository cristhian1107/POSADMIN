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
  public partial class BLRegistros : IBLRegistros
  {
    #region [ METODOS ]

    public ObservableCollection<Registros> BLConsultaTodos(String Conexion, Int64 Empresa, Int64 Sucursal, String Estado, String TablaPIS, String InternoPIS, String TablaTHA, String InternoTHA, String Huesped, String Numero)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_REGI_ConsultaTodos(l_mydataaccesssql, Empresa, Sucursal, Estado, TablaPIS, InternoPIS, TablaTHA, InternoTHA, Huesped, Numero);
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
    public Registros BLConsultaRegistro(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, Int64 Habitacion, ref String Mensaje)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            Registros l_item = null;
            if (Registro != 0 || Habitacion != 0)
            { l_item = POSAD_REGI_ConsultaRegistro(l_mydataaccesssql, Empresa, Sucursal, Registro, Habitacion, ref Mensaje); }
            if (l_item == null) { l_item = new Registros(); }
            l_item.REGI_ListaPagos = BL_DetallesPagosRegistros.BLConsultaPorRegistro(l_mydataaccesssql, Empresa, Sucursal, Registro);
            return l_item;
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
    public Registros BLCalcularRegistro(String Conexion, Registros Item)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            Registros l_item = null;
            l_item = POSAD_REGI_CalcularCostos(l_mydataaccesssql, Item);
            if (l_item == null) { l_item = new Registros(); }
            l_item.REGI_ListaPagos = new ObservableCollection<DetallesPagosRegistros>();
            return l_item;
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
    public String BLAcciones(String Conexion, Int64 Empresa, Int64 Sucursal, Int64 Registro, String Accion, String Motivo, String Usuario, ref Decimal Monto)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            string l_isCorrect = null;
            l_mydataaccesssql.DABeginTransaction();
            l_isCorrect = POSAD_REGI_Acciones(l_mydataaccesssql, Empresa, Sucursal, Registro, Accion, Motivo, Usuario, ref Monto);
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
    public String BLSave(String Conexion, ref Registros Item)
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
                l_isCorrect = POSAD_REGI_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_REGI_Actualizar(l_mydataaccesssql, ref Item);
                break;
            }
            if (string.IsNullOrEmpty(l_isCorrect))
            {
              foreach (var l_opcion in Item.REGI_ListaPagos)
              {
                if (l_opcion.Instance == InstanceEntity.Added)
                {
                  var l_item = l_opcion;
                  l_item.EMPR_Interno = Item.EMPR_Interno;
                  l_item.SUCU_Interno = Item.SUCU_Interno;
                  l_item.REGI_Interno = Item.REGI_Interno;
                  l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                  l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                  BL_DetallesPagosRegistros.BLSave(l_mydataaccesssql, ref l_item);
                }
              }
              l_mydataaccesssql.DACommitTransaction();
            }
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

    #region [ INDICADORES ]

    public DataSet BLIndicadorAnual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_REPO_IndicadorAnual(l_mydataaccesssql, Empresa, Sucursal, Anio);
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
    public DataSet BLIndicadorMensual(String Conexion, Int64 Empresa, Int64 Sucursal, Int32 Anio, Int32 Mes)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_REPO_IndicadorMensual(l_mydataaccesssql, Empresa, Sucursal, Anio, Mes);
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
    public DataSet BLIndicadorSemanal(String Conexion, Int64 Empresa, Int64 Sucursal, DateTime Desde, DateTime Hasta)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_REPO_IndicadorSemanal(l_mydataaccesssql, Empresa, Sucursal, Desde, Hasta);
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

    #endregion
  }
}
