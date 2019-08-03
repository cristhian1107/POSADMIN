using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Posadmin
{
  public class Constantes
  {
    #region [ VARIABLES DE SESION ]

    public const String SessionUsuario = "SessionUsuario";

    public const String SessionPaises = "SessionPaises";
    public const String SessionUbigeos = "SessionUbigeos";
    public const String SessionRoles = "SessionRoles";
    public const String SessionEmpresas = "SessionEmpresas";
    public const String SessionUsuarios = "SessionUsuarios";
    public const String SessionTablas = "SessionTablas";
    public const String SessionSucursales = "SessionSucursales";
    public const String SessionParametros = "SessionParametros";
    public const String SessionConfiguraciones = "SessionConfiguraciones";
    public const String SessionEntidades = "SessionEntidades";
    public const String SessionHabitaciones = "SessionHabitaciones";
    public const String SessionTurnos = "SessionTurnos";
    public const String SessionReservaciones = "SessionReservaciones";
    public const String SessionRegistros = "SessionRegistros";
    public const String SessionLimpieza = "SessionLimpieza";
    public const String SessionCierres = "SessionCierres";
    public const String SessionRetiros = "SessionRetiros";
    public const String SessionIndicadoresAnuales = "SessionIndicadoresAnuales";
    public const String SessionIndicadoresMensuales = "SessionIndicadoresMensuales";
    public const String SessionIndicadoresSemanales = "SessionIndicadoresSemanales";

    #endregion

    #region [ LLAVES PARAMETROS ]

    /// <summary>
    /// HORAS MAXIMAS PARA ANULAR UN REGISTRO
    /// </summary>
    public const String ParaHOANU = "HOANU";

    /// <summary>
    /// HORAS DE DIFERENCIAS MINIMA PARA CANCELAR REGISTRO
    /// </summary>
    public const String ParaHOCAN = "HOCAN";

    /// <summary>
    /// MINUTOS MAXIMOS PERMITIDOS PARA DEJAR LA HABITACION
    /// </summary>
    public const String ParaMIHAB = "MIHAB";

    /// <summary>
    /// HORA INICIO HOTELERO
    /// </summary>
    public const String ParaHOINI = "HOINI";

    /// <summary>
    /// HORA FIN HOTELERO
    /// </summary>
    public const String ParaHOFIN = "HOFIN";

    /// <summary>
    /// HORAS MAXIMAS POR PRECIO MINIMO
    /// </summary>
    public const String ParaHOPMI = "HOPMI";

    /// <summary>
    /// INTERNO DEL TIPO HUESPED (TEN)
    /// </summary>
    public const String ParaINHUE = "INHUE";

    #endregion

    #region [ TABLAS ]

    /// <summary>
    /// NUMERO DE NIVELES (PISOS)
    /// </summary>
    public const String TablaPIS = "PIS";

    /// <summary>
    /// TIPOS DE TABLAS
    /// </summary>
    public const String TablaTAB = "TAB";

    /// <summary>
    /// TIPOS DE DOCUMENTOS DE IDENTIDAD
    /// </summary>
    public const String TablaTDI = "TDI";

    /// <summary>
    /// TIPOS DE ENTIDADES
    /// </summary>
    public const String TablaTEN = "TEN";

    /// <summary>
    /// TIPOS DE HABITACIONES
    /// </summary>
    public const String TablaTHA = "THA";

    #endregion
  }
}