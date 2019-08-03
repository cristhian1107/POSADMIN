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
  public partial class BLFuncionesEntidades : IBLFuncionesEntidades
  {
    #region [ METODOS ]

    public ObservableCollection<FuncionesEntidades> BLConsultaPorEntidad(MyDataAccessSQL SQL, Int64 EmpresaInterno, Int64 EntidadInterno)
    {
      try
      {
        return POSAD_FUNC_ColsultaPorEntidad(SQL, EmpresaInterno, EntidadInterno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean BLAsignarPorEntidad(MyDataAccessSQL SQL, ref FuncionesEntidades Item, Boolean Primero)
    {
      try
      {
        bool l_isCorrect = true;
        l_isCorrect = POSAD_FUNC_AsignarPorEntidad(SQL, ref Item, Primero);
        return l_isCorrect;
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
