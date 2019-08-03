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
  public partial interface IBLOpciones
  {
    #region [ METODOS ]

    ObservableCollection<Opciones> BLConsultaPorRoles(MyDataAccessSQL SQL, Int32 RolInterno);
    Boolean BLAsignarPorRoles(MyDataAccessSQL SQL, ref Opciones Item, Boolean Primero);

    #endregion
  }
}
