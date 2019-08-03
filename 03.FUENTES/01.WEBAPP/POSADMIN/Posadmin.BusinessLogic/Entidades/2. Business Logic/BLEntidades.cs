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
  public partial class BLEntidades : IBLEntidades
  {
    #region [ METODOS ]

    public ObservableCollection<Entidades> BLAyuda(String Conexion, Int64 Empresa, String TipoEntidad, String Indicio)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_ENTI_Ayuda(l_mydataaccesssql, Empresa, TipoEntidad, Indicio);
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
    public ObservableCollection<Entidades> BLConsultaTodos(String Conexion, Int64 Empresa, String Id, String Nombre)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            return POSAD_ENTI_ConsultaTodos(l_mydataaccesssql, Empresa, Id, Nombre);
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
    public Entidades BLConsultaEntidad(String Conexion, Int64 Empresa, Int64 Interno)
    {
      try
      {
        using (MyDataAccessSQL l_mydataaccesssql = new MyDataAccessSQL(Conexion))
        {
          try
          {
            Entidades l_item = null;
            if (Interno != 0)
            { l_item = POSAD_ENTI_ConsultaRegistro(l_mydataaccesssql, Empresa, Interno); }
            if (l_item == null) { l_item = new Entidades(); }
            l_item.ENTI_FuncionesEntidades = BL_FuncionesEntidades.BLConsultaPorEntidad(l_mydataaccesssql, Empresa, Interno);
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
    public Boolean BLSave(String Conexion, ref Entidades Item)
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
                l_isCorrect = POSAD_ENTI_Insertar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  foreach (var l_opcion in Item.ENTI_FuncionesEntidadesSeleccionadas)
                  {
                    var l_item = l_opcion;
                    l_item.EMPR_Interno = Item.EMPR_Interno;
                    l_item.ENTI_Interno = Item.ENTI_Interno;
                    l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                    l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                    l_isCorrect = BL_FuncionesEntidades.BLAsignarPorEntidad(l_mydataaccesssql, ref l_item, l_contador == 0);
                    l_contador++;
                  }
                }
                break;
              case InstanceEntity.Modified:
                l_isCorrect = POSAD_ENTI_Actualizar(l_mydataaccesssql, ref Item);
                if (l_isCorrect)
                {
                  int l_contador = 0;
                  foreach (var l_opcion in Item.ENTI_FuncionesEntidadesSeleccionadas)
                  {
                    var l_item = l_opcion;
                    l_item.EMPR_Interno = Item.EMPR_Interno;
                    l_item.ENTI_Interno = Item.ENTI_Interno;
                    l_item.AUDI_UsuarioCreacion = Item.AUDI_UsuarioCreacion;
                    l_item.AUDI_UsuarioModificacion = Item.AUDI_UsuarioModificacion;
                    l_isCorrect = BL_FuncionesEntidades.BLAsignarPorEntidad(l_mydataaccesssql, ref l_item, l_contador == 0);
                    l_contador++;
                  }
                }
                break;
              case InstanceEntity.Deleted:
                l_isCorrect = POSAD_ENTI_Eliminar(l_mydataaccesssql, ref Item);
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
