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
  public partial class BLOpciones : IBLOpciones
  {
    #region [ METODOS ]

    public ObservableCollection<Opciones> BLConsultaPorRoles(MyDataAccessSQL SQL, Int32 RolInterno)
    {
      try
      {
        return POSAD_OPCI_ConsultaPorRoles(SQL, RolInterno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean BLAsignarPorRoles(MyDataAccessSQL SQL, ref Opciones Item, Boolean Primero)
    {
      try
      {
        bool l_isCorrect = true;
        l_isCorrect = POSAD_OPCI_AsignarPorRoles(SQL, ref Item, Primero);
        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
