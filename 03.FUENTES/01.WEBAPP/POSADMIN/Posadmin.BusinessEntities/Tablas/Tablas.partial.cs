using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Posadmin.BusinessEntities
{
  public partial class Tablas
  {
    #region [ VARIABLES ]

    #endregion

    #region [ PROPIEDADES ]

    #endregion

    #region [ CONTROLES ]

    [DataMember]
    public String Value { get { return TABL_Interno; } }
    [DataMember]
    public String Text { get { return TABL_Nombre; } }

    #endregion

    #region [ METODOS ]

    #endregion
  }
}
