using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Posadmin.Models;
using Posadmin.BusinessEntities;

namespace Posadmin.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private readonly UserManager l_manager = UserManager.Create(Connections.MainConnection);

    #region [ LOGIN GET : POST]

    /// <summary>
    /// GET: /account/login 
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      NullSession();
      // No queremos utilizar ninguna información de identidad existente
      EnsureLoggedOut();
      // Almacene la URL de origen para que podamos adjuntarla a un campo de formulario
      var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };

      return View(viewModel);
    }

    /// <summary>
    /// POST: /account/login 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(AccountLoginModel viewModel)
    {

      NullSession();
      // Asegúrate de tener un viewModel válido para trabajar
      if (!ModelState.IsValid)
      { return View(viewModel); }

      // Asegurarse de verificar si un usuario existe con la información de identidad proporcionada
      l_manager.IdEmpresa = viewModel.IdEmpresa;
      var user = await l_manager.FindAsync(viewModel.Usuario, viewModel.Contrasena);

      // Si se encontró un usuario
      if (user != null)
      {
        Usuarios l_usuario = null;
        using (ProxyManager l_proxy = new ProxyManager())
        { l_usuario = l_proxy.Client.ConsultaUsuarioEmpresa(Connections.MainConnection, viewModel.IdEmpresa, Convert.ToInt64(user.Id)); }

        if (l_usuario != null)
        {
          // Luego crea una identidad para el y regístrate.
          await SignInAsync(user, viewModel.RememberMe);

          //Creamos la session del Usuario
          Session[Constantes.SessionUsuario] = l_usuario;

          // Si el usuario vino de una página específica, redirigir de nuevo a ella
          return RedirectToLocal(viewModel.ReturnUrl);
        }
      }

      // Si no se encontró un usuario existente que coincidiera con los criterios dados
      ModelState.AddModelError("", "Usuario y/o contraseña incorrectas.");

      // Si llegamos tan lejos, algo falla, volver a mostrar la forma.
      return View(viewModel);
    }

    #endregion

    #region [ LOGOUT GET : POST]

    /// <summary>
    /// POST: /account/Logout 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Logout()
    {
      // Limpiamos la Session
      NullSession();

      // Primero limpiamos el ticket de autenticación como siempre.
      FormsAuthentication.SignOut();

      // En segundo lugar, despejamos el principio para garantizar que el usuario no retenga ninguna autenticación.
      HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

      // Por último, redirigimos a un controlador / acción que requiere autenticación para garantizar que se realice una redirección
      // Esto borra la marca Request.IsAuthenticated ya que esto activa una nueva solicitud
      return RedirectToLocal();
    }

    #endregion

    #region [ METODOS ]

    /// <summary>
    /// Si la solicitud (aún) está marcada como autenticada, enviamos al usuario a la acción de cierre de sesión
    /// </summary>
    private void EnsureLoggedOut()
    {
      if (Request.IsAuthenticated)
      { Logout(); }
    }

    /// <summary>
    /// Verifica si la URL es local y redirecciona a la "accion",caso contrario a la ubicacion predeterminada
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    private ActionResult RedirectToLocal(string returnUrl = "")
    {
      // Si la URL de retorno comienza con una barra "/", asumimos que pertenece a nuestro sitio.
      // Así que nos redirigiremos a esta "acción".
      if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
      { return Redirect(returnUrl); }

      // Si no podemos verificar si la URL es local para nuestro host, redirigimos a una ubicación predeterminada
      return RedirectToAction("index", "home");
    }

    /// <summary>
    /// Crear una identidad basada en reclamaciones para el usuario actual
    /// </summary>
    /// <param name="user"></param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    private async Task SignInAsync(IdentityUser user, bool isPersistent)
    {
      // Borrar cualquier dato de autenticación persistente
      FormsAuthentication.SignOut();

      // Crear una identidad basada en reclamaciones para el usuario actual
      var identity = await l_manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

      // Escribe la cookie de autenticación
      FormsAuthentication.SetAuthCookie(identity.Name, isPersistent);
    }

    /// <summary>
    /// Agrega un Error al ModelState (DbEntityValidationException)
    /// </summary>
    /// <param name="exc"></param>
    private void AddErrors(DbEntityValidationException exc)
    {
      foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
      {
        ModelState.AddModelError("", error);
      }
    }

    /// <summary>
    /// Agrega un Error al ModelState (String)
    /// </summary>
    /// <param name="result"></param>
    private void AddErrors(IdentityResult result)
    {
      // Add all errors that were returned to the page error collection
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }

    /// <summary>
    /// Limpia las variables de Session
    /// </summary>
    private void NullSession()
    {
      try
      {
        Session[Constantes.SessionUsuario] = null;
        Session[Constantes.SessionPaises] = null;
        Session[Constantes.SessionUbigeos] = null;
        Session[Constantes.SessionRoles] = null;
        Session[Constantes.SessionEmpresas] = null;
        Session[Constantes.SessionUsuarios] = null;
        Session[Constantes.SessionTablas] = null;
        Session[Constantes.SessionSucursales] = null;
        Session[Constantes.SessionParametros] = null;
        Session[Constantes.SessionConfiguraciones] = null;
        Session[Constantes.SessionEntidades] = null;
        Session[Constantes.SessionHabitaciones] = null;
        Session[Constantes.SessionTurnos] = null;
        Session[Constantes.SessionReservaciones] = null;
        Session[Constantes.SessionRegistros] = null;
        Session[Constantes.SessionLimpieza] = null;
        Session[Constantes.SessionCierres] = null;
        Session[Constantes.SessionRetiros] = null;
        Session[Constantes.SessionIndicadoresAnuales] = null;
        Session.Clear();
      }
      catch (Exception ex)
      { throw ex; }
    }

    #endregion

    // GET: /account/forgotpassword
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      // We do not want to use any existing identity information
      EnsureLoggedOut();

      return View();
    }

    // GET: /account/error
    [AllowAnonymous]
    public ActionResult Error()
    {
      // We do not want to use any existing identity information
      EnsureLoggedOut();

      return View();
    }

    // GET: /account/register
    [AllowAnonymous]
    public ActionResult Register()
    {
      // We do not want to use any existing identity information
      EnsureLoggedOut();

      return View(new AccountRegistrationModel());
    }

    // POST: /account/register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
    {
      // Ensure we have a valid viewModel to work with
      if (!ModelState.IsValid)
        return View(viewModel);

      // Prepare the identity with the provided information
      var user = new IdentityUser
      {
        UserName = viewModel.Username ?? viewModel.Email,
        Email = viewModel.Email
      };

      // Try to create a user with the given identity
      try
      {
        var result = await l_manager.CreateAsync(user, viewModel.Password);

        // If the user could not be created
        if (!result.Succeeded)
        {
          // Add all errors to the page so they can be used to display what went wrong
          AddErrors(result);

          return View(viewModel);
        }

        // If the user was able to be created we can sign it in immediately
        // Note: Consider using the email verification proces
        await SignInAsync(user, false);

        return RedirectToLocal();
      }
      catch (DbEntityValidationException ex)
      {
        // Add all errors to the page so they can be used to display what went wrong
        AddErrors(ex);

        return View(viewModel);
      }
    }

    // GET: /account/lock
    [AllowAnonymous]
    public ActionResult Lock()
    {
      return View();
    }
  }
}