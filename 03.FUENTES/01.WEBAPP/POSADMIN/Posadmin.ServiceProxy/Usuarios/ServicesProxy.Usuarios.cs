using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Posadmin.ServiceContracts;
using Posadmin.BusinessLogic;
using Posadmin.BusinessEntities;


namespace Posadmin.ServiceProxy
{

  public partial class ServicesProxy : IServices
  {
    #region [ PROPIEDADES ]

    [Dependency]
    public IBLUsuarios BL_Usuarios { get; set; }

    #endregion

    #region [ METODOS ]

    public Usuarios VerificarUsuario(String Conexion, String IdEmpresa, String Usuario, String Contrasena)
    {
      try
      {
        return BL_Usuarios.BLVerificarUsuario(Conexion, IdEmpresa, Usuario, Contrasena);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Usuarios ConsultaUsuarioEmpresa(String Conexion, String IdEmpresa, Int64 Interno)
    {
      try
      {
        return BL_Usuarios.BLConsultaUsuarioEmpresa(Conexion, IdEmpresa, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Usuarios> ConsultaTodosUsuarios(String Conexion, String Nombre)
    {
      try
      {
        return BL_Usuarios.BLConsultaTodos(Conexion, Nombre);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public ObservableCollection<Usuarios> ConsultaUsuariosPorEmpresa(String Conexion, Int64 Empresa)
    {
      try
      {
        return BL_Usuarios.BLConsultaPorEmpresa(Conexion, Empresa);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Usuarios ConsultaUsuario(String Conexion, Int64 Interno)
    {
      try
      {
        return BL_Usuarios.BLConsultaUsuario(Conexion, Interno);
      }
      catch (Exception ex)
      { throw ex; }
    }
    public Boolean SaveUsuarios(String Conexion, ref Usuarios Item)
    {
      try
      {
        return BL_Usuarios.BLSave(Conexion, ref Item);
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion
  }
}
