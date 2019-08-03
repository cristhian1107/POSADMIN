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
  public partial interface IBLFuncionesEntidades
  {
    #region [ METODOS ]

    ObservableCollection<FuncionesEntidades> BLConsultaPorEntidad(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 EntidadInterno);
    Boolean BLAsignarPorEntidad(MyDataAccessSQL SQL, ref FuncionesEntidades Item, Boolean Primero);

    #endregion
  }
}
