using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Posadmin.BusinessEntities;
using SoftwareFactory.Infrastructure.Cryptography;

namespace Posadmin
{
  public class UserManager : UserManager<IdentityUser>
  {
    #region [ PROPIEDADES ]

    private static readonly UserStore<IdentityUser> UserStore = new UserStore<IdentityUser>();
    private static readonly UserManager Instance = new UserManager();
    private static String Connection = String.Empty;
    public String IdEmpresa = String.Empty;

    #endregion

    #region [ METODOS ]

    private UserManager()
        : base(UserStore)
    {
    }
    public static UserManager Create(String conexion)
    {
      Connection = conexion;
      // We have to create our own user manager in order to override some default behavior:
      //
      //  - Override default password length requirement (6) with a length of 4
      //  - Override user name requirements to allow spaces and dots
      var passwordValidator = new MinimumLengthValidator(4);
      var userValidator = new UserValidator<IdentityUser, string>(Instance)
      {
        AllowOnlyAlphanumericUserNames = false,
        RequireUniqueEmail = true,
      };

      Instance.UserValidator = userValidator;
      Instance.PasswordValidator = passwordValidator;

      return Instance;
    }
    public override async Task<IdentityUser> FindAsync(string userName, string password)
    {
      try
      {
        UserRepository<IdentityUser> l_repositiry = new UserRepository<IdentityUser>();
        return await Task.Run(() => l_repositiry.GetByUserNamePassword(Connection, IdEmpresa, userName, password));
      }
      catch (Exception) { throw; }
    }
    public override async Task<ClaimsIdentity> CreateIdentityAsync(IdentityUser user, string authenticationType)
    {
      try
      {
        UserRepository<IdentityUser> _repositiry = new UserRepository<IdentityUser>();
        return await Task.Run(() => _repositiry.GetIdentity(Connection, IdEmpresa, user));
      }
      catch (Exception) { throw; }
    }

    #endregion
  }

  public class UserRepository<T> where T : IdentityUser
  {

    #region [ METODOS ]

    internal T GetByUserNamePassword(String connection, String IdEmpresa, String Usuario, String Contrasena)
    {
      try
      {
        Usuarios l_usuario;
        using (ProxyManager l_proxy = new ProxyManager())
        {
          String l_input = Usuario.ToUpper() + Contrasena.ToUpper() + Usuario.ToUpper();
          Contrasena = MyCryptographyMD5.EncryptData(l_input);
          l_usuario = l_proxy.Client.VerificarUsuario(connection, IdEmpresa, Usuario, Contrasena);
        }

        if (l_usuario != null)
        {
          T result = (T)Activator.CreateInstance(typeof(T));
          result.Id = l_usuario.USUA_Interno.ToString();
          result.UserName = l_usuario.USUA_Usuario;
          result.PasswordHash = l_usuario.USUA_Contrasena;
          result.Email = l_usuario.USUA_Correo;
          return result;
        }
        return null;
      }
      catch (Exception) { throw; }
    }
    internal ClaimsIdentity GetIdentity(String connection, String IdEmpresa, IdentityUser user)
    {
      try
      {
        Usuarios l_user = null;
        Int64 l_interno;
        if (user != null && !String.IsNullOrEmpty(user.Id) && Int64.TryParse(user.Id, out l_interno))
        {
          using (ProxyManager l_proxy = new ProxyManager())
          {
            l_user = l_proxy.Client.ConsultaUsuarioEmpresa(connection, IdEmpresa, l_interno);
          }
        }

        if (l_user != null)
        {
          ClaimsIdentity _identity = new ClaimsIdentity(l_user);
          return _identity;
        }
        return null;
      }
      catch (Exception) { throw; }
    }

    #endregion
  }
}