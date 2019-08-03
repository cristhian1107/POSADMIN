#region [ USING ]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Posadmin.Models
{
  /// <summary>
  /// Model usado para la View Account/Login.cshtml
  /// </summary>
  public class AccountLoginModel
  {
    /// <summary>
    /// Gets or sets el valor de: IdEmpresa.
    /// </summary>
    [Required]
    public String IdEmpresa { get; set; }

    /// <summary>
    /// Gets or sets el valor de: Usuario.
    /// </summary>
    [Required]
    public String Usuario { get; set; }

    /// <summary>
    /// Gets or sets el valor de: Contrasena.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public String Contrasena { get; set; }

    /// <summary>
    /// Gets or sets el valor de: ReturnUrl.
    /// </summary>
    public String ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets el valor de: RememberMe.
    /// </summary>
    public Boolean RememberMe { get; set; }

  }
}