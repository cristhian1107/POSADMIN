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
  public partial class BLUsuarios : IBLUsuarios
  {
    #region [ METODOS ]

    public Usuarios BLVerificarUsuario(String Conexion, String IdEmpresa, String Usuario, String Contrasena)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_USUA_VerificarUsuario(l_mydataaccesssql, IdEmpresa, Usuario, Contrasena);
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
    public Usuarios BLConsultaUsuarioEmpresa(String Conexion, String IdEmpresa, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_USUA_ConsultaUsuarioEmpresa(l_mydataaccesssql, IdEmpresa, Interno);
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
    public ObservableCollection<Usuarios> BLConsultaTodos(String Conexion, String Nombre)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_USUA_ConsultaTodos(l_mydataaccesssql, Nombre);
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
    public ObservableCollection<Usuarios> BLConsultaPorEmpresa(String Conexion, Int64 Empresa)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_USUA_ConsultaPorEmpresa(l_mydataaccesssql, Empresa);
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
    public Usuarios BLConsultaUsuario(String Conexion, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            Usuarios l_item = null;
            if (Interno != 0)
            { l_item = POSAD_USUA_ConsultaRegistro(l_mydataaccesssql, Interno); }
            if (l_item == null) { l_item = new Usuarios(); }
            l_item.USUA_Empresas = BL_Empresas.BLConsultaPorUsuarios(l_mydataaccesssql, Interno);
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
    public Boolean BLSave(String Conexion, ref Usuarios Item)
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
                l_isCorrect = POSAD_USUA_Insertar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  if (Item.USUA_EmpresasSeleccionadas != null && Item.USUA_EmpresasSeleccionadas.Count > 0)
                  {
                    foreach (var l_empresa in Item.USUA_EmpresasSeleccionadas)
                    {
                      var l_item = l_empresa;
                      l_item.USUA_Interno = Item.USUA_Interno;
                      l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                      l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                      l_isCorrect = BL_Empresas.BLAsignarPorUsuario(l_mydataaccesssql, ref l_item, l_contador == 0);
                      l_contador++;
                    }
                  }
                }
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_USUA_Actualizar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  if (Item.USUA_EmpresasSeleccionadas != null && Item.USUA_EmpresasSeleccionadas.Count > 0)
                  {
                    foreach (var l_opcion in Item.USUA_EmpresasSeleccionadas)
                    {
                      var l_item = l_opcion;
                      l_item.USUA_Interno = Item.USUA_Interno;
                      l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                      l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                      l_isCorrect = BL_Empresas.BLAsignarPorUsuario(l_mydataaccesssql, ref l_item, l_contador == 0);
                      l_contador++;
                    }
                  }
                }
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_USUA_Eliminar(l_mydataaccesssql, ref Item);
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
