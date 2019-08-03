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
  public partial class BLEmpresas : IBLEmpresas
  {
    #region [ METODOS ]

    public ObservableCollection<Empresas> BLConsultaPorUsuarios(MyDataAccessSQL SQL, Int64 UsuarioInterno)
    {
      try
      {
        return POSAD_EMPR_ConsultaPorUsuarios(SQL, UsuarioInterno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean BLAsignarPorUsuario(MyDataAccessSQL SQL, ref Empresas Item, Boolean Primero)
    {
      try
      {
        bool l_isCorrect = true;
        l_isCorrect = POSAD_EMPR_AsignarPorUsuario(SQL, ref Item, Primero);
        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Empresas> BLConsultaTodos(String Conexion, String Id, String RazonSocial)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_EMPR_ConsultaTodos(l_mydataaccesssql, Id, RazonSocial);
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
    public Empresas BLConsultaEmpresa(String Conexion, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_EMPR_ConsultaRegistro(l_mydataaccesssql, Interno);
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
    public Boolean BLSave(String Conexion, ref Empresas Item)
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
                l_isCorrect = POSAD_EMPR_Insertar(l_mydataaccesssql, ref Item);
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_EMPR_Actualizar(l_mydataaccesssql, ref Item);
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
