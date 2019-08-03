using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SoftwareFactory.Infrastructure.BusinessEntity;
using SoftwareFactory.Infrastructure.DataAccess;
using Posadmin.BusinessEntities;
using System.Data;
using System.Collections.ObjectModel;

namespace Posadmin.BusinessLogic
{
  public partial class BLOpciones
  {
    #region [ PROPIEDADES ]

    public IUnityContainer ContainerService { get; set; }
    public BusinessEntityLoader<Opciones> Loader { get; set; }

    #endregion

    #region [ CONSTRUCTOR ]

    public BLOpciones(IUnityContainer Container)
    {
      this.ContainerService = Container;
      Loader = new BusinessEntityLoader<Opciones>();
      Opciones l_item = new Opciones();
      Loader.EntityType = l_item.GetType();
    }
    public BLOpciones()
    {
      Loader = new BusinessEntityLoader<Opciones>();
      Opciones l_item = new Opciones();
      Loader.EntityType = l_item.GetType();
    }

    #endregion

    #region [ METODOS ]

    private ObservableCollection<Opciones> POSAD_OPCI_ConsultaPorRoles(MyDataAccessSQL SQL, Int32 RolInterno)
    {
      try
      {
        ObservableCollection<Opciones> l_items = null;
        Opciones l_item = null;
        SQL.DAAssignProcedure("POSAD_OPCI_ConsultaPorRoles");
        SQL.DAAddParameter("@pintROLE_Interno", RolInterno, SqlDbType.Int);
        using (IDataReader reader = SQL.DAExecuteReader())
        {
          l_items = new ObservableCollection<Opciones>();
          while (reader.Read())
          {
            l_item = new Opciones();
            Loader.LoadEntity(reader, l_item);
            l_item.Instance = InstanceEntity.Unchanged;
            l_items.Add(l_item);
          }
        }
        return l_items;
      }
      catch (Exception ex)
      { throw ex; }
    }
    private Boolean POSAD_OPCI_AsignarPorRoles(MyDataAccessSQL SQL, ref Opciones Item, Boolean Primero)
    {
      try
      {
        SQL.DAAssignProcedure("POSAD_OPCI_AsignarPorRoles");
        SQL.DAAddParameter("@pintROLE_Interno", Item.ROLE_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pintOPCI_Interno", Item.OPCI_Interno, SqlDbType.Int);
        SQL.DAAddParameter("@pbitOPCI_Primero", Primero, SqlDbType.Bit);
        SQL.DAAddParameter("@pvchAUDI_UsuarioCreacion", Item.AUDI_UsuarioModificacion, SqlDbType.VarChar, 25);
        if (SQL.DAExecuteNonQuery() > 0)
        { return true; }
        else
        { return false; }
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
