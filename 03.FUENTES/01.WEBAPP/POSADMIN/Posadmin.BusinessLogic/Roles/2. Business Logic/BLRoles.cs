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
  public partial class BLRoles : IBLRoles
  {
    #region [ METODOS ]

    public ObservableCollection<Roles> BLConsultaTodos(String Conexion, String Nombre)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_ROLE_ConsultaTodos(l_mydataaccesssql, Nombre);
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
    public Roles BLConsultaRol(String Conexion, Int32 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            Roles l_item = null;
            if (Interno != 0)
            { l_item = POSAD_ROLE_ConsultaRegistro(l_mydataaccesssql, Interno); }
            if (l_item == null) { l_item = new Roles(); }
            l_item.ROLE_Opciones = BL_Opciones.BLConsultaPorRoles(l_mydataaccesssql, Interno);
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
    public Boolean BLSave(String Conexion, ref Roles Item)
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
                l_isCorrect = POSAD_ROLE_Insertar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  foreach (var l_opcion in Item.ROLE_OpcionesSeleccionadas)
                  {
                    var l_item = l_opcion;
                    l_item.ROLE_Interno = Item.ROLE_Interno;
                    l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                    l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                    l_isCorrect = BL_Opciones.BLAsignarPorRoles(l_mydataaccesssql, ref l_item, l_contador == 0);
                    l_contador++;
                  }
                }
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_ROLE_Actualizar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  foreach (var l_opcion in Item.ROLE_OpcionesSeleccionadas)
                  {
                    var l_item = l_opcion;
                    l_item.ROLE_Interno = Item.ROLE_Interno;
                    l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                    l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                    l_isCorrect = BL_Opciones.BLAsignarPorRoles(l_mydataaccesssql, ref l_item, l_contador == 0);
                    l_contador++;
                  }
                }
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_ROLE_Eliminar(l_mydataaccesssql, ref Item);
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
