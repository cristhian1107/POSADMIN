IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REPO_IndicadorSemanal]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REPO_IndicadorSemanal]
GO
CREATE PROCEDURE [dbo].[POSAD_REPO_IndicadorSemanal]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pdteREPO_Desde        DATE
   ,@pdteREPO_Hasta        DATE
) 
AS
BEGIN 
   
   SET NOCOUNT ON;
   CREATE TABLE #TMP_RangoFechas(ORDEN INT, FECHA DATE)
   DECLARE @date DATE, @contador INT   
   SET @date = @pdteREPO_Desde
   SET @contador = 1
   WHILE @date <= @pdteREPO_Hasta 
   BEGIN
      INSERT INTO #TMP_RangoFechas
      VALUES(@contador, @date)
      SET @date = DATEADD(DAY, 1, @date)
      SET @contador = @contador + 1
   END

/************************* INDICADOR 1 - 2 *** COMPARACION DE VENTAS DIARIAS ***********************************/

   /*IDEAL*/
   SELECT 
    CONVERT(DATE,REGI.REGI_FechaHoraEntrada)          AS [FECHA]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(REGI.REGI_PrecioSugerido,0), 2, 0))) AS [IDEAL]
   INTO #TMP_MontoIdeal
   FROM POSAD_Registros AS REGI
   WHERE
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   CONVERT(DATE,REGI.REGI_FechaHoraEntrada) BETWEEN @pdteREPO_Desde AND @pdteREPO_Hasta
   GROUP BY CONVERT(DATE,REGI.REGI_FechaHoraEntrada)

   /*ESPERADA*/
   SELECT 
    CONVERT(DATE,REGI.REGI_FechaHoraEntrada)          AS [FECHA]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(REGI.REGI_PrecioCobrado,0), 2, 0))) AS [ESPERADO]
   INTO #TMP_MontoEsperado
   FROM POSAD_Registros AS REGI
   WHERE
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   REGI.REGI_Estado <> 'X' AND
   CONVERT(DATE,REGI.REGI_FechaHoraEntrada) BETWEEN @pdteREPO_Desde AND @pdteREPO_Hasta
   GROUP BY CONVERT(DATE,REGI.REGI_FechaHoraEntrada)

   /*OBTENIDA*/
   SELECT 
    CONVERT(DATE,PAGO.PAGO_FechaHoraRegistro)         AS [FECHA]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(PAGO.PAGO_MontoCancelado,0), 2, 0))) AS [OBTENIDO]
   INTO #TMP_MontoObtenido
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_DetallesPagosRegistros AS PAGO ON REGI.EMPR_Interno = PAGO.EMPR_Interno AND
                                                      REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                                      REGI.REGI_Interno = PAGO.REGI_Interno 
   WHERE
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   REGI.REGI_Estado <> 'X' AND
   CONVERT(DATE,PAGO.PAGO_FechaHoraRegistro) BETWEEN @pdteREPO_Desde AND @pdteREPO_Hasta
   GROUP BY CONVERT(DATE,PAGO.PAGO_FechaHoraRegistro)

   SELECT 
    CONVERT(VARCHAR(20),FECHA.FECHA  ,103)   AS [FECHA]
   ,ISNULL(IDEAL.IDEAL,0)                    AS [IDEAL]
   ,ISNULL(ESPER.ESPERADO,0)                 AS [ESPERADO]
   ,ISNULL(OBTEN.OBTENIDO,0)                 AS [OBTENIDO]
   FROM #TMP_RangoFechas AS FECHA
   LEFT JOIN #TMP_MontoIdeal AS IDEAL ON FECHA.FECHA = IDEAL.FECHA
   LEFT JOIN #TMP_MontoEsperado AS ESPER ON FECHA.FECHA = ESPER.FECHA
   LEFT JOIN #TMP_MontoObtenido AS OBTEN ON FECHA.FECHA = OBTEN.FECHA
   ORDER BY FECHA.ORDEN

   SELECT 
    CONVERT(VARCHAR(20),FECHA.FECHA  ,103)   AS [FECHA]
   ,ISNULL(OBTEN.OBTENIDO,0)                 AS [MONTO]
   FROM #TMP_RangoFechas AS FECHA
   LEFT JOIN #TMP_MontoObtenido AS OBTEN ON FECHA.FECHA = OBTEN.FECHA
   ORDER BY [MONTO] DESC

/************************* INDICADOR 3 *** INFORME DE REGISTROS ***********************************/

   SELECT 
    CONVERT(VARCHAR(20),FECH.FECHA  ,103)   AS [FECHA]
   ,ISNULL(SUM(CASE REGI.REGI_Estado
         WHEN 'A' THEN 1
         ELSE 0 END),0)          AS [ACTIVOS]
   ,ISNULL(SUM(CASE REGI.REGI_Estado
         WHEN 'X' THEN 1
         ELSE 0 END),0)          AS [ANULADOS]
   ,ISNULL(SUM(CASE REGI.REGI_Estado
         WHEN 'C' THEN 1
         ELSE 0 END),0)          AS [CANCELADOS]
   FROM #TMP_RangoFechas AS FECH
   LEFT JOIN POSAD_Registros AS REGI ON REGI.EMPR_Interno = @pbigEMPR_Interno AND
                                        REGI.SUCU_Interno = @pbigSUCU_Interno AND
                                        CONVERT(DATE,REGI.REGI_FechaHoraEntrada) = FECH.FECHA
   GROUP BY CONVERT(VARCHAR(20),FECH.FECHA  ,103),FECH.ORDEN
   ORDER BY FECH.ORDEN

/************************* INDICADOR 4 *** INFORME DE RESERVAS ***********************************/

   SELECT 
    CONVERT(VARCHAR(20),FECH.FECHA  ,103)     AS [FECHA]
   ,ISNULL(SUM(CASE RESE.RESE_Estado
         WHEN 'A' THEN 1
         ELSE 0 END),0)          AS [INCONCLUSOS]
   ,ISNULL(SUM(CASE RESE.RESE_Estado
         WHEN 'X' THEN 1
         ELSE 0 END),0)          AS [ANULADOS]
   ,ISNULL(SUM(CASE RESE.RESE_Estado
         WHEN 'E' THEN 1
         ELSE 0 END),0)          AS [CONCLUIDOS]
   FROM #TMP_RangoFechas AS FECH
   LEFT JOIN POSAD_Reservaciones AS RESE ON RESE.EMPR_Interno = @pbigEMPR_Interno AND
                                            RESE.SUCU_Interno = @pbigSUCU_Interno AND
                                            CONVERT(DATE,RESE.RESE_FechaHoraRegistro) = FECH.FECHA
   GROUP BY CONVERT(VARCHAR(20),FECH.FECHA  ,103),FECH.ORDEN
   ORDER BY FECH.ORDEN

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REPO_IndicadorMensual]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REPO_IndicadorMensual]
GO
CREATE PROCEDURE [dbo].[POSAD_REPO_IndicadorMensual]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pintREPO_Anio         INT
   ,@pintREPO_Mes          INT 
) 
AS
BEGIN 

/************************* INDICADOR 1 - INFORME MENSUAL REGISTROS (ANULADOS, CANCELADOS, ACTIVOS) ***********************************/

   SELECT 
    CASE REGI.REGI_Estado
      WHEN 'A' THEN 'Activos'
      WHEN 'X' THEN 'Anulados'
      WHEN 'C' THEN 'Cancelado'
      END                     AS [ESTADO]
   ,COUNT(REGI.REGI_Interno)  AS [CANTIDAD]
   FROM POSAD_Registros AS REGI
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio 
   GROUP BY REGI.REGI_Estado

/************************* INDICADOR 2 - TOP DIA CON MAS RECAUDACION - (FECHA DE PAGO) ***********************************/

   SELECT TOP 5
    CONVERT(VARCHAR(20),DETA.PAGO_FechaHoraRegistro ,103) AS [DIA]
   ,SUM(DETA.PAGO_MontoCancelado) AS [MONTO]
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_DetallesPagosRegistros AS DETA ON DETA.EMPR_Interno = REGI.EMPR_Interno AND 
                                                      DETA.SUCU_Interno = REGI.SUCU_Interno AND
                                                      DETA.REGI_Interno = REGI.REGI_Interno
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   MONTH(DETA.PAGO_FechaHoraRegistro) = @pintREPO_Mes AND
   YEAR(DETA.PAGO_FechaHoraRegistro) = @pintREPO_Anio AND
   REGI.REGI_Estado <> 'X'
   GROUP BY CONVERT(VARCHAR(20),DETA.PAGO_FechaHoraRegistro ,103)
   ORDER BY SUM(DETA.PAGO_MontoCancelado) DESC

/************************* INDICADOR 3 - INFORME MENSUAL RESERVAS (INCONCLUSOS, CANCELADOS, CONCLUIDOS) ***********************************/

   SELECT 
    CASE RESE.RESE_Estado
      WHEN 'A' THEN 'Inconclusos'
      WHEN 'X' THEN 'Anulados'
      WHEN 'E' THEN 'Concluidos'
      END                     AS [ESTADO]
   ,COUNT(RESE.RESE_Interno)  AS [CANTIDAD]
   FROM POSAD_Reservaciones AS RESE
   WHERE 
   RESE.EMPR_Interno = @pbigEMPR_Interno AND
   RESE.SUCU_Interno = @pbigSUCU_Interno AND
   MONTH(RESE.RESE_FechaHoraRegistro) = @pintREPO_Mes AND
   YEAR(RESE.RESE_FechaHoraRegistro) = @pintREPO_Anio 
   GROUP BY RESE.RESE_Estado

/************************* INDICADOR 4 - TOP HABITACIONES MAS USADAS ***********************************/   

   SELECT TOP 5
    HABI.HABI_Numero AS [HABITACION]
   ,COUNT(REGI.REGI_Interno) AS [CANTIDAD]
   FROM POSAD_Habitaciones AS HABI
   INNER JOIN POSAD_Registros AS REGI ON  HABI.EMPR_Interno = REGI.EMPR_Interno AND HABI.SUCU_Interno = REGI.SUCU_Interno AND
                                          REGI.HABI_Interno = HABI.HABI_Interno
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY 
   HABI.HABI_Numero
   ORDER BY [CANTIDAD] DESC

/************************* INDICADOR 5 - TOP HABITACIONES CON MAYOR RECAUDACION ***********************************/   

   SELECT TOP 5
    HABI.HABI_Numero                                  AS [HABITACION]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(PAGO.PAGO_MontoCancelado,0), 2, 0))) AS [MONTO]
   FROM POSAD_Habitaciones AS HABI
   INNER JOIN POSAD_Registros AS REGI ON  HABI.EMPR_Interno = REGI.EMPR_Interno AND HABI.SUCU_Interno = REGI.SUCU_Interno AND
                                          REGI.HABI_Interno = HABI.HABI_Interno
   INNER JOIN POSAD_DetallesPagosRegistros AS PAGO ON PAGO.EMPR_Interno = REGI.EMPR_Interno AND PAGO.SUCU_Interno = REGI.SUCU_Interno AND
                                                      PAGO.REGI_Interno = REGI.REGI_Interno
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Anio AND
   MONTH(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY 
   HABI.HABI_Numero
   ORDER BY [MONTO] DESC

/************************* INDICADOR 6 - CANTIDAD DE VISITAS POR TURNO ***********************************/   

   SELECT
    TURN.TURN_Nominacion      AS [TURNO]
   ,COUNT(REGI.REGI_Interno)  AS [VISITAS]
   FROM POSAD_Turnos AS TURN
   LEFT JOIN POSAD_Registros AS REGI ON TURN.EMPR_Interno = REGI.EMPR_Interno AND
                                        TURN.SUCU_Interno = REGI.SUCU_Interno AND
                                        TURN.TURN_Interno = REGI.TURN_Interno AND
                                        YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
                                        MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
                                        REGI.REGI_Estado <> 'X'
   WHERE 
   TURN.EMPR_Interno = @pbigEMPR_Interno AND
   TURN.SUCU_Interno = @pbigSUCU_Interno
   GROUP BY TURN.TURN_Nominacion

/************************* INDICADOR 7 - MONTO GENERADO POR TURNO ***********************************/ 
     
   SELECT
    TURN.TURN_Nominacion                                 AS [TURNO]
   ,SUM(CONVERT(DECIMAL(30,2), 
       ROUND(ISNULL(
       CASE REGI.REGI_Estado 
         WHEN 'X' THEN 0 
         ELSE PAGO.PAGO_MontoCancelado END ,0), 2, 0)))  AS [MONTO]  
   FROM POSAD_Turnos AS TURN
   LEFT JOIN POSAD_Registros AS REGI ON TURN.EMPR_Interno = REGI.EMPR_Interno AND
                                        TURN.SUCU_Interno = REGI.SUCU_Interno AND
                                        TURN.TURN_Interno = REGI.TURN_Interno AND
                                        YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
                                        MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes
   LEFT JOIN POSAD_DetallesPagosRegistros AS PAGO ON  REGI.EMPR_Interno = PAGO.EMPR_Interno AND
                                                      REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                                      REGI.REGI_Interno = PAGO.REGI_Interno 
   WHERE 
   TURN.EMPR_Interno = @pbigEMPR_Interno AND
   TURN.SUCU_Interno = @pbigSUCU_Interno
   GROUP BY TURN.TURN_Nominacion

/************************* INDICADOR 8 - TOP USUARIOS CON MAYOR REGISTROS ***********************************/   

   SELECT TOP 5
    USUA.USUA_Usuario         AS [USUARIO]
   ,COUNT(REGI.REGI_Interno)  AS [CANTIDAD]
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_Usuarios AS USUA ON REGI.USUA_Interno = USUA.USUA_Interno
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY USUA.USUA_Usuario
   ORDER BY COUNT(REGI.REGI_Interno) DESC

/************************* INDICADOR 9 - TOP USUARIOS CON MAYOR VENTA ***********************************/   

   SELECT TOP 5
    USUA.USUA_Usuario                                 AS [USUARIO]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(PAGO.PAGO_MontoCancelado,0), 2, 0))) AS [MONTO]
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_Usuarios AS USUA ON REGI.USUA_Interno = USUA.USUA_Interno
   INNER JOIN POSAD_DetallesPagosRegistros AS PAGO ON REGI.EMPR_Interno = PAGO.EMPR_Interno AND
                                                      REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                                      REGI.REGI_Interno = PAGO.REGI_Interno
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY USUA.USUA_Usuario
   ORDER BY SUM(ISNULL(PAGO.PAGO_MontoCancelado,0)) DESC

/************************* INDICADOR 10 *** TOP HUESPED CON MAS VISITAS ***********************************/

   SELECT TOP 5
    REGI.REGI_HuespedId       AS [HUESPED]
   ,COUNT(REGI.REGI_Interno)  AS [CANTIDAD]
   FROM POSAD_Registros AS REGI
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY 
    REGI.REGI_HuespedId
   ORDER BY 
    COUNT(REGI.REGI_Interno) DESC

/************************* INDICADOR 11 - 12 *** VENTAS Y CANTIDAD DE VISITAS POR DIA ***********************************/
   
   SELECT 
    DIA.DIA_Interno           AS [DIA]
   ,COUNT(REGI.REGI_Interno)  AS [CANTIDAD]
   INTO #TMP_DiaCantidad
   FROM POSAD_Dias AS DIA
   INNER JOIN POSAD_Registros AS REGI ON DATEPART(DW,REGI.REGI_FechaHoraEntrada) = DIA.DIA_Orden
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   MONTH(REGI.REGI_FechaHoraEntrada) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY
   DIA.DIA_Interno

   SELECT 
    DIA.DIA_Interno                             AS [DIA]
   ,SUM(ROUND(PAGO.PAGO_MontoCancelado, 2, 0))  AS [MONTO]
   INTO #TMP_DiaMonto
   FROM POSAD_Dias AS DIA
   INNER JOIN POSAD_DetallesPagosRegistros AS PAGO ON DATEPART(DW,PAGO_FechaHoraRegistro) = DIA.DIA_Orden
   INNER JOIN POSAD_Registros AS REGI ON  REGI.EMPR_Interno = PAGO.EMPR_Interno AND REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                          REGI.REGI_Interno = PAGO.REGI_Interno
   WHERE 
   PAGO.EMPR_Interno = @pbigEMPR_Interno AND
   PAGO.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Anio AND
   MONTH(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Mes AND
   REGI.REGI_Estado <> 'X'
   GROUP BY
   DIA.DIA_Interno

   SELECT
    DIA.DIA_Abreviatura1                        AS [DIA]
   ,ISNULL(CANT.CANTIDAD,0)                     AS [CANTIDAD]
   ,CONVERT(DECIMAL(20,2),ISNULL(MONT.MONTO,0)) AS [MONTO]
   FROM POSAD_Dias AS DIA
   LEFT JOIN #TMP_DiaCantidad AS CANT ON DIA.DIA_Interno = CANT.DIA
   LEFT JOIN #TMP_DiaMonto AS MONT ON DIA.DIA_Interno = MONT.DIA
   ORDER BY 
   DIA.DIA_Orden


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REPO_IndicadorAnual]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REPO_IndicadorAnual]
GO
CREATE PROCEDURE [dbo].[POSAD_REPO_IndicadorAnual]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pintREPO_Anio         INT
) 
AS
BEGIN   

/************************* INDICADOR 1 *** VENTAS POR CADA MES Y AÑO ***********************************/
   SELECT 
    MES.MES_Orden                                     AS [ORDEN]
   ,MES.MES_Abreviatura1                              AS [MES]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(
    CASE REGI.REGI_Estado 
      WHEN 'X' THEN 0 
      ELSE PAGO.PAGO_MontoCancelado END ,0), 2, 0)))  AS [PAGO]
   INTO #TMP_PagosMes
   FROM POSAD_Meses AS MES
   LEFT JOIN POSAD_DetallesPagosRegistros AS PAGO ON  MONTH(PAGO.PAGO_FechaHoraRegistro) = MES.MES_Orden AND 
                                                      YEAR(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Anio AND
                                                      PAGO.EMPR_Interno = @pbigEMPR_Interno AND
                                                      PAGO.SUCU_Interno = @pbigSUCU_Interno                                                             
   LEFT JOIN POSAD_Registros AS REGI ON  REGI.EMPR_Interno = PAGO.EMPR_Interno AND REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                         REGI.REGI_Interno = PAGO.REGI_Interno 
   GROUP BY
    MES.MES_Abreviatura1
   ,MES.MES_Orden
   ORDER BY
    MES.MES_Orden 

   SELECT 
    MES.MES_Orden                                     AS [ORDEN]
   ,MES.MES_Abreviatura1                              AS [MES]
   ,SUM(CONVERT(DECIMAL(30,2), 
    ROUND(ISNULL(
    CASE REGI.REGI_Estado 
      WHEN 'X' THEN 0 
      ELSE PAGO.PAGO_MontoCancelado END ,0), 2, 0)))  AS [MONTO]
   INTO #TMP_MontoMes
   FROM POSAD_Meses AS MES
   LEFT JOIN POSAD_Registros AS REGI ON MONTH(REGI.REGI_FechaHoraEntrada) = MES.MES_Orden AND
                                        YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
                                        REGI.EMPR_Interno = @pbigEMPR_Interno AND
                                        REGI.SUCU_Interno = @pbigSUCU_Interno                                     
   LEFT JOIN POSAD_DetallesPagosRegistros AS PAGO ON REGI.EMPR_Interno = PAGO.EMPR_Interno AND REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                                      REGI.REGI_Interno = PAGO.REGI_Interno 
   GROUP BY
    MES.MES_Abreviatura1
   ,MES.MES_Orden
   ORDER BY
    MES.MES_Orden 

   SELECT
    PAGO.ORDEN
   ,PAGO.MES
   ,MONT.MONTO
   ,PAGO.PAGO
   FROM #TMP_PagosMes AS PAGO
   INNER JOIN #TMP_MontoMes AS MONT ON PAGO.ORDEN = MONT.ORDEN AND PAGO.MES = MONT.MES
   ORDER BY PAGO.ORDEN

/************************* INDICADOR 2 *** INGRESOS, ANULADOS Y CANCELADOS ***********************************/
   SELECT 
    MES.MES_Abreviatura1      AS [MES]
   ,SUM(CASE REGI.REGI_Estado
         WHEN 'A' THEN 1
         ELSE 0 END)          AS [ACTIVOS]
   ,SUM(CASE REGI.REGI_Estado
         WHEN 'X' THEN 1
         ELSE 0 END)          AS [ANULADOS]
   ,SUM(CASE REGI.REGI_Estado
         WHEN 'C' THEN 1
         ELSE 0 END)          AS [CANCELADOS]
   FROM POSAD_Meses AS MES
   LEFT JOIN POSAD_Registros AS REGI ON MONTH(REGI.REGI_FechaHoraEntrada) = MES.MES_Orden AND
                                        YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
                                        REGI.EMPR_Interno = @pbigEMPR_Interno AND
                                        REGI.SUCU_Interno = @pbigSUCU_Interno              
   GROUP BY
    MES.MES_Orden
   ,MES.MES_Abreviatura1
   ORDER BY MES.MES_Orden

/************************* INDICADOR 3-4 *** VENTAS Y CANTIDAD DE VISITAS POR MES ***********************************/
   
   SELECT 
    MES.MES_Interno           AS [MES]
   ,COUNT(REGI.REGI_Interno)  AS [CANTIDAD]
   INTO #TMP_MesCantidad
   FROM POSAD_Meses AS MES
   INNER JOIN POSAD_Registros AS REGI ON MONTH(REGI.REGI_FechaHoraEntrada) = MES.MES_Orden
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   REGI.REGI_Estado <> 'X'
   GROUP BY
   MES.MES_Interno

   SELECT 
    MES.MES_Interno                             AS [MES]
   ,SUM(ROUND(PAGO.PAGO_MontoCancelado, 2, 0))  AS [MONTO]
   INTO #TMP_MesMonto
   FROM POSAD_Meses AS MES
   INNER JOIN POSAD_DetallesPagosRegistros AS PAGO ON MONTH(PAGO.PAGO_FechaHoraRegistro) = MES.MES_Orden
   INNER JOIN POSAD_Registros AS REGI ON  REGI.EMPR_Interno = PAGO.EMPR_Interno AND REGI.SUCU_Interno = PAGO.SUCU_Interno AND
                                          REGI.REGI_Interno = PAGO.REGI_Interno
   WHERE 
   PAGO.EMPR_Interno = @pbigEMPR_Interno AND
   PAGO.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(PAGO.PAGO_FechaHoraRegistro) = @pintREPO_Anio AND
   REGI.REGI_Estado <> 'X'
   GROUP BY
   MES.MES_Interno

   SELECT
    MES.MES_Abreviatura1                        AS [MES]
   ,ISNULL(CANT.CANTIDAD,0)                     AS [CANTIDAD]
   ,CONVERT(DECIMAL(20,2),ISNULL(MONT.MONTO,0)) AS [MONTO]
   FROM POSAD_Meses AS MES
   LEFT JOIN #TMP_MesCantidad AS CANT ON MES.MES_Interno = CANT.MES
   LEFT JOIN #TMP_MesMonto AS MONT ON MES.MES_Interno = MONT.MES
   ORDER BY 
   MES.MES_Orden

/************************* INDICADOR 5 *** RESERVACIONES ENTREGADOS, NO ENTREGADOS Y ANULADOS ***********************************/
   SELECT 
    MES.MES_Abreviatura1      AS [MES]
   ,SUM(CASE RESE.RESE_Estado
         WHEN 'A' THEN 1
         ELSE 0 END)          AS [INCONCLUSOS]
   ,SUM(CASE RESE.RESE_Estado
         WHEN 'X' THEN 1
         ELSE 0 END)          AS [ANULADOS]
   ,SUM(CASE RESE.RESE_Estado
         WHEN 'E' THEN 1
         ELSE 0 END)          AS [CONCLUIDOS]
   FROM POSAD_Meses AS MES
   LEFT JOIN POSAD_Reservaciones AS RESE ON MONTH(RESE.RESE_FechaHoraRegistro) = MES.MES_Orden AND
                                            YEAR(RESE.RESE_FechaHoraRegistro) = @pintREPO_Anio AND
                                            RESE.EMPR_Interno = @pbigEMPR_Interno AND
                                            RESE.SUCU_Interno = @pbigSUCU_Interno              
   GROUP BY
    MES.MES_Orden
   ,MES.MES_Abreviatura1
   ORDER BY MES.MES_Orden

/************************* INDICADOR 6 *** TOP HUESPED CON MAS VISITAS ***********************************/
   SELECT TOP 5
    REGI.REGI_HuespedId
   ,COUNT(REGI.REGI_Interno) AS [CANTIDAD]
   FROM POSAD_Registros AS REGI
   WHERE 
   REGI.EMPR_Interno = @pbigEMPR_Interno AND
   REGI.SUCU_Interno = @pbigSUCU_Interno AND
   YEAR(REGI.REGI_FechaHoraEntrada) = @pintREPO_Anio AND
   REGI.REGI_Estado <> 'X'
   GROUP BY 
    REGI.REGI_HuespedId
   ORDER BY 
    COUNT(REGI.REGI_Interno) DESC

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CIER_MovimientosPendientes]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CIER_MovimientosPendientes]
GO
CREATE PROCEDURE [dbo].[POSAD_CIER_MovimientosPendientes]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pbigUSUA_Interno      BIGINT 
   ,@pbitRETI_Calcular     BIT   
) 
AS
BEGIN
   
   IF (@pbitRETI_Calcular = 1)
   BEGIN
      DECLARE @pbigRETI_Interno BIGINT, @pvchAUDI_Usuario VARCHAR(25) 

      SELECT @pbigRETI_Interno = (ISNULL(MAX(RETI_Interno), 0)+1)
      FROM [POSAD_Retiros]
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [SUCU_Interno] = @pbigSUCU_Interno

      SELECT @pvchAUDI_Usuario = USUA_Usuario
      FROM [POSAD_Usuarios] 
      WHERE 
      [USUA_Interno] = @pbigUSUA_Interno 

      SELECT 
       CIER.EMPR_Interno      AS [EMPR_Interno]
      ,CIER.SUCU_Interno      AS [SUCU_Interno]
      ,CIER.CIER_Interno      AS [CIER_Interno]
      ,CIER.CIER_MontoReal + CIER.CIER_MontoExtra +
       CIER.CIER_MontoDemas   AS [CIER_Total]
      INTO #TMP_Cierres
      FROM POSAD_Cierres AS CIER
      WHERE 
      CIER.EMPR_Interno = @pbigEMPR_Interno AND 
      CIER.SUCU_Interno = @pbigSUCU_Interno AND 
      CIER.CIER_Interno IN (SELECT DISTINCT PAGO.CIER_Interno FROM POSAD_DetallesPagosRegistros AS PAGO
                              WHERE  PAGO.EMPR_Interno = @pbigEMPR_Interno
                                 AND PAGO.SUCU_Interno = @pbigSUCU_Interno
                                 AND PAGO.CIER_Interno IS NOT NULL
                                 AND PAGO.RETI_Interno IS NULL )

      INSERT INTO POSAD_Retiros
      (EMPR_Interno     ,SUCU_Interno  ,RETI_Interno           ,RETI_FechaHoraRegistro ,RETI_MontoEntregado
      ,RETI_MontoExtra  ,USUA_Interno  ,AUDI_UsuarioCreacion   ,AUDI_FechaCreacion)
      SELECT 
       @pbigEMPR_Interno   ,@pbigSUCU_Interno   ,@pbigRETI_Interno   ,[dbo].[GetDateCountry]('es-PE') ,SUM(CIER.CIER_Total)
      ,0                   ,@pbigUSUA_Interno   ,@pvchAUDI_Usuario   ,GETDATE()
      FROM #TMP_Cierres AS CIER

      UPDATE DETA
         SET DETA.RETI_Interno = @pbigRETI_Interno
      FROM POSAD_DetallesPagosRegistros AS DETA
      INNER JOIN #TMP_Cierres AS CIER ON  DETA.EMPR_Interno = CIER.EMPR_Interno AND DETA.SUCU_Interno = CIER.SUCU_Interno AND
                                          DETA.CIER_Interno = CIER.CIER_Interno
      WHERE 
      DETA.EMPR_Interno = @pbigEMPR_Interno AND
      DETA.SUCU_Interno = @pbigSUCU_Interno AND
      DETA.RETI_Interno IS NULL 

   END

   SELECT 
    CIER.CIER_Interno            AS CIER_Interno
   ,CIER.CIER_FechaHoraRegistro  AS CIER_FechaHoraRegistro 
   ,USUA.USUA_NombreCompleto     AS CIER_NombreUsuario
   ,CIER.CIER_MontoReal          AS CIER_MontoReal
   ,CIER.CIER_MontoExtra         AS CIER_MontoExtra
   ,CIER.CIER_MontoDemas         AS CIER_MontoDemas
   ,CIER.CIER_MontoReal + CIER.CIER_MontoExtra +
    CIER.CIER_MontoDemas         AS CIER_Total
   FROM POSAD_Cierres AS CIER
   INNER JOIN POSAD_Usuarios AS USUA ON CIER.USUA_Interno = USUA.USUA_Interno
   WHERE 
   CIER.EMPR_Interno = @pbigEMPR_Interno AND 
   CIER.SUCU_Interno = @pbigSUCU_Interno AND 
   CIER.CIER_Interno IN (SELECT DISTINCT PAGO.CIER_Interno FROM POSAD_DetallesPagosRegistros AS PAGO
                           WHERE  PAGO.EMPR_Interno = @pbigEMPR_Interno
                              AND PAGO.SUCU_Interno = @pbigSUCU_Interno
                              AND PAGO.CIER_Interno IS NOT NULL
                              AND PAGO.RETI_Interno IS NULL )
   ORDER BY CIER.CIER_FechaHoraRegistro ASC

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RETI_UltimosMovimientos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RETI_UltimosMovimientos]
GO
CREATE PROCEDURE [dbo].[POSAD_RETI_UltimosMovimientos]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
) 
AS
BEGIN
   
   SELECT TOP 5
    RETI.RETI_Interno            AS RETI_Interno
   ,RETI.RETI_FechaHoraRegistro  AS RETI_FechaHoraRegistro 
   ,USUA.USUA_NombreCompleto     AS RETI_NombreUsuario
   ,RETI.RETI_MontoEntregado     AS RETI_MontoEntregado
   FROM POSAD_Retiros AS RETI
   INNER JOIN POSAD_Usuarios AS USUA ON RETI.USUA_Interno = USUA.USUA_Interno
   WHERE 
   RETI.EMPR_Interno = @pbigEMPR_Interno AND 
   RETI.SUCU_Interno = @pbigSUCU_Interno AND 
   RETI.RETI_Interno IN (SELECT TOP 5 RETI2.RETI_Interno FROM POSAD_Retiros AS RETI2 
                           WHERE RETI2.EMPR_Interno = @pbigEMPR_Interno AND 
                                 RETI2.SUCU_Interno = @pbigSUCU_Interno
                           ORDER BY RETI2.RETI_FechaHoraRegistro DESC)
   ORDER BY RETI.RETI_FechaHoraRegistro ASC
   
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAGO_MovimientosUsuario]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAGO_MovimientosUsuario]
GO
CREATE PROCEDURE [dbo].[POSAD_PAGO_MovimientosUsuario]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pbigUSUA_Interno      BIGINT 
   ,@pbitCIER_Calcular     BIT   
) 
AS
BEGIN
   
   IF (@pbitCIER_Calcular = 1)
   BEGIN 
      
      DECLARE @pbigCIER_Interno BIGINT, @pvchAUDI_Usuario VARCHAR(25) 

      SELECT @pbigCIER_Interno = (ISNULL(MAX(CIER_Interno), 0)+1)
      FROM [POSAD_Cierres]
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [SUCU_Interno] = @pbigSUCU_Interno

      SELECT @pvchAUDI_Usuario = USUA_Usuario
      FROM [POSAD_Usuarios] 
      WHERE 
      [USUA_Interno] = @pbigUSUA_Interno 

      SELECT 
       REGI.EMPR_Interno                  AS [EMPR_Interno]
      ,REGI.SUCU_Interno                  AS [SUCU_Interno]
      ,REGI.REGI_Interno                  AS [REGI_Interno]  
      ,REGI.REGI_Estado                   AS [REGI_Estado]
      ,CONVERT(DECIMAL(20,8),0)           AS [CIER_MontoReal]
      ,SUM(CONVERT(DECIMAL(20,8),CASE WHEN PAGO.PAGO_Tipo = 'A'
            THEN ISNULL(PAGO.PAGO_MontoCancelado,0)
            ELSE 0
            END))                         AS [CIER_MontoExtra]
      ,CONVERT(DECIMAL(20,8),0)           AS [CIER_MontoDeuda]
      ,CONVERT(DECIMAL(20,8),0)           AS [CIER_MontoDemas]
      ,CASE WHEN REGI.REGI_Estado <> 'X' 
            THEN ISNULL(REGI.REGI_PrecioCobrado,0)
            ELSE CONVERT(DECIMAL(20,8),0)
            END                           AS [REGI_PrecioCobrado]
      ,SUM(CONVERT(DECIMAL(20,8),CASE WHEN PAGO.PAGO_Tipo IN ('E','S') AND REGI.REGI_Estado <> 'X'
            THEN ISNULL(PAGO.PAGO_MontoCancelado,0)
            ELSE CONVERT(DECIMAL(20,8),0)
            END))                         AS [REGI_MontoCancelado]
      INTO #TMP_Pagos
      FROM POSAD_DetallesPagosRegistros AS PAGO
      INNER JOIN POSAD_Registros AS REGI ON  PAGO.EMPR_Interno = REGI.EMPR_Interno AND PAGO.SUCU_Interno = REGI.SUCU_Interno AND
                                             PAGO.REGI_Interno = REGI.REGI_Interno
      WHERE
      PAGO.EMPR_Interno = @pbigEMPR_Interno AND
      PAGO.SUCU_Interno = @pbigSUCU_Interno AND
      PAGO.USUA_Interno = @pbigUSUA_Interno AND
      PAGO.CIER_Interno IS NULL AND
      PAGO.RETI_Interno IS NULL 
      GROUP BY 
       REGI.EMPR_Interno 
      ,REGI.SUCU_Interno 
      ,REGI.REGI_Interno
      ,REGI.REGI_Estado
      ,ISNULL(REGI.REGI_PrecioCobrado,0) 

      UPDATE #TMP_Pagos
      SET CIER_MontoDeuda = CASE WHEN 
                              (ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0)) > 0 AND REGI_Estado = 'A'
                              THEN ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0)
                              ELSE 0 END
         ,CIER_MontoDemas = CASE WHEN 
                              ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0) < 0
                              THEN ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0)
                              ELSE 0 END 
         ,CIER_MontoReal  = CASE WHEN
                              ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0) >= 0
                              THEN ISNULL(REGI_MontoCancelado,0)
                              ELSE ISNULL(REGI_PrecioCobrado,0)
                              END

      INSERT INTO POSAD_Cierres  
      (EMPR_Interno     ,SUCU_Interno     ,CIER_Interno  ,CIER_FechaHoraRegistro ,CIER_MontoReal      ,CIER_MontoExtra
      ,CIER_MontoDeuda  ,CIER_MontoDemas  ,USUA_Interno  ,AUDI_UsuarioCreacion   ,AUDI_FechaCreacion)
      SELECT 
       @pbigEMPR_Interno      ,@pbigSUCU_Interno      ,@pbigCIER_Interno   ,[dbo].[GetDateCountry]('es-PE') ,SUM(CIER_MontoReal) ,SUM(CIER_MontoExtra)
      ,SUM(CIER_MontoDeuda)   ,SUM(CIER_MontoDemas)   ,@pbigUSUA_Interno   ,@pvchAUDI_Usuario               ,GETDATE()
      FROM #TMP_Pagos

      UPDATE DETA
         SET DETA.CIER_Interno = @pbigCIER_Interno
      FROM POSAD_DetallesPagosRegistros AS DETA
      INNER JOIN #TMP_Pagos AS PAGO ON DETA.EMPR_Interno = PAGO.EMPR_Interno AND DETA.SUCU_Interno = PAGO.SUCU_Interno AND
                                       DETA.REGI_Interno = PAGO.REGI_Interno
      WHERE 
      DETA.EMPR_Interno = @pbigEMPR_Interno AND
      DETA.SUCU_Interno = @pbigSUCU_Interno AND
      DETA.USUA_Interno = @pbigUSUA_Interno AND
      DETA.CIER_Interno IS NULL AND
      DETA.RETI_Interno IS NULL 

   END

   SELECT 
    PAGO.PAGO_FechaHoraRegistro                    AS [PAGO_FechaHoraRegistro]
   ,HABI.HABI_Numero                               AS [PAGO_NumeroHabitacion]
   ,PAGO.PAGO_Tipo                                 AS [PAGO_Tipo]
   ,CASE PAGO.PAGO_Tipo WHEN 'E' THEN 'ENTRADA'
                        WHEN 'S' THEN 'SALIDA'
                        WHEN 'A' THEN 'ADICIONAL'
                        ELSE 'INDEFINIDO' END      AS [PAGO_TipoNombre]
   ,CASE PAGO.PAGO_Tipo WHEN 'E' THEN 'success'
                        WHEN 'S' THEN 'danger'
                        WHEN 'A' THEN 'success'
                        ELSE 'info' END            AS [PAGO_TipoColor]
   ,PAGO.PAGO_MontoCancelado                       AS [PAGO_MontoCancelado]
   FROM POSAD_DetallesPagosRegistros AS PAGO
   INNER JOIN POSAD_Registros AS REGI ON  PAGO.EMPR_Interno = REGI.EMPR_Interno AND PAGO.SUCU_Interno = REGI.SUCU_Interno AND
                                          PAGO.REGI_Interno = REGI.REGI_Interno
   INNER JOIN POSAD_Habitaciones AS HABI ON  REGI.EMPR_Interno = HABI.EMPR_Interno AND REGI.SUCU_Interno = HABI.SUCU_Interno AND
                                             REGI.HABI_Interno = HABI.HABI_Interno
   WHERE
   PAGO.EMPR_Interno = @pbigEMPR_Interno AND
   PAGO.SUCU_Interno = @pbigSUCU_Interno AND
   PAGO.USUA_Interno = @pbigUSUA_Interno AND
   REGI.REGI_Estado <> 'X' AND
   PAGO.CIER_Interno IS NULL AND
   PAGO.RETI_Interno IS NULL 

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CIER_UltimosMovimientos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CIER_UltimosMovimientos]
GO
CREATE PROCEDURE [dbo].[POSAD_CIER_UltimosMovimientos]
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
) 
AS
BEGIN

   SELECT TOP 5
    CIER.CIER_Interno            AS CIER_Interno
   ,CIER.CIER_FechaHoraRegistro  AS CIER_FechaHoraRegistro 
   ,USUA.USUA_NombreCompleto     AS CIER_NombreUsuario
   ,CIER.CIER_MontoReal          AS CIER_MontoReal
   ,CIER.CIER_MontoExtra         AS CIER_MontoExtra
   ,CIER.CIER_MontoDemas         AS CIER_MontoDemas
   ,CIER.CIER_MontoReal + CIER.CIER_MontoExtra +
    CIER.CIER_MontoDemas         AS CIER_Total
   ,CIER.CIER_MontoDeuda         AS CIER_MontoDeuda
   FROM POSAD_Cierres AS CIER
   INNER JOIN POSAD_Usuarios AS USUA ON CIER.USUA_Interno = USUA.USUA_Interno
   WHERE 
   CIER.EMPR_Interno = @pbigEMPR_Interno AND 
   CIER.SUCU_Interno = @pbigSUCU_Interno AND 
   --CIER.CIER_Interno IN (SELECT DISTINCT PAGO.CIER_Interno FROM POSAD_DetallesPagosRegistros AS PAGO 
   --                        WHERE PAGO.EMPR_Interno = CIER.EMPR_Interno AND
   --                              PAGO.SUCU_Interno = CIER.SUCU_Interno AND
   --                              PAGO.CIER_Interno = CIER.CIER_Interno AND 
   --                              PAGO.RETI_Interno IS NULL )
   CIER.CIER_Interno IN (SELECT TOP 5 CIER2.CIER_Interno FROM POSAD_Cierres AS CIER2 
                           WHERE CIER2.EMPR_Interno = @pbigEMPR_Interno AND 
                                 CIER2.SUCU_Interno = @pbigSUCU_Interno
                           ORDER BY CIER2.CIER_FechaHoraRegistro DESC)
   ORDER BY CIER.CIER_FechaHoraRegistro ASC

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_ActualizarLimpieza]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_ActualizarLimpieza]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_ActualizarLimpieza]
( 
    @pbigEMPR_Interno               BIGINT 
   ,@pbigSUCU_Interno               BIGINT 
   ,@pbigHABI_Interno               BIGINT 
   ,@pbitHABI_Limpio                BIT
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS
BEGIN

   UPDATE [POSAD_Habitaciones]
      SET [HABI_Limpio]              = @pbitHABI_Limpio
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno
      AND [HABI_Interno]                 = @pbigHABI_Interno


END
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_ConsultaEstado]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_ConsultaEstado]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_ConsultaEstado]
(    
     @pbigEMPR_Interno           BIGINT
   , @pbigSUCU_Interno           BIGINT
   , @pchrTABL_TablaPIS          CHAR(3)
   , @pchrTABL_InternoPIS        CHAR(4)
   , @pchrTABL_TablaTHA          CHAR(3)
   , @pchrTABL_InternoTHA        CHAR(4)
) 
AS 
BEGIN
   
   SELECT 
       HABI.EMPR_Interno   AS EMPR_Interno
      ,HABI.SUCU_Interno   AS SUCU_Interno
      ,HABI.HABI_Interno   AS HABI_Interno
      ,TPIS.TABL_Nombre    AS TABL_NombrePIS 
      ,HABI.HABI_Numero    AS HABI_Numero
      ,TTHA.TABL_Nombre    AS TABL_NombreTHA
      ,HABI.HABI_Estado    AS HABI_Estado
      ,CONVERT(VARCHAR(50),CASE HABI.HABI_Estado 
         WHEN 'L' THEN 'LIBRE' 
         WHEN 'O' THEN 'OCUPADO'
         END)              AS HABI_EstadoNombre
      ,HABI.HABI_Limpio    AS HABI_Limpio
   INTO #TMP_Habitaciones
   FROM POSAD_Habitaciones AS HABI 
   INNER JOIN POSAD_Tablas AS TPIS ON  HABI.EMPR_Interno = TPIS.EMPR_Interno AND HABI.TABL_TablaPIS = TPIS.TABL_Tabla AND
                                       HABI.TABL_InternoPIS = TPIS.TABL_Interno
   INNER JOIN POSAD_Tablas AS TTHA ON  HABI.EMPR_Interno = TTHA.EMPR_Interno AND HABI.TABL_TablaTHA = TTHA.TABL_Tabla AND
                                       HABI.TABL_InternoTHA = TTHA.TABL_Interno
   WHERE
   HABI.EMPR_Interno = @pbigEMPR_Interno AND
   HABI.SUCU_Interno = @pbigSUCU_Interno AND
   HABI.TABL_TablaPIS = @pchrTABL_TablaPIS AND
   HABI.TABL_InternoPIS = ISNULL(@pchrTABL_InternoPIS,HABI.TABL_InternoPIS) AND
   HABI.TABL_TablaTHA = @pchrTABL_TablaTHA AND 
   HABI.TABL_InternoTHA = ISNULL(@pchrTABL_InternoTHA,HABI.TABL_InternoTHA)
   
   UPDATE HABI
      SET HABI.HABI_Estado = 'R'
         ,HABI_EstadoNombre = 'RESERVADO'
   FROM #TMP_Habitaciones AS HABI
   INNER JOIN POSAD_Reservaciones AS RESE ON HABI.EMPR_Interno = RESE.EMPR_Interno AND HABI.SUCU_Interno = RESE.SUCU_Interno AND 
                                             HABI.HABI_Interno = RESE.HABI_Interno AND RESE.RESE_Estado = 'A' AND 
                                             CONVERT(DATE,[dbo].[GetDateCountry]('es-PE')) BETWEEN RESE.RESE_FechaInicio AND RESE.RESE_FechaFin

   SELECT * FROM #TMP_Habitaciones

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_Ayuda]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_Ayuda]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_Ayuda] 
(
    @pbigEMPR_Interno         BIGINT
   ,@pvchENTI_TipoEntidad     CHAR(5) 
   ,@pvchENTI_Indicio         VARCHAR(250) 
)
AS
BEGIN
   
   DECLARE @pvchTipoEntidad VARCHAR(4)
   SET @pvchTipoEntidad = (SELECT PARA_Valor FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = @pvchENTI_TipoEntidad)

   SELECT DISTINCT
    ENTI.ENTI_Interno
   ,ENTI.TABL_TablaTDI
   ,ENTI.TABL_InternoTDI
   ,ENTI.ENTI_Id
   ,ENTI.ENTI_NombreCompleto
   ,ENTI.ENTI_Direccion
   FROM POSAD_Entidades AS ENTI
   INNER JOIN POSAD_FuncionesEntidades AS FUNC ON FUNC.EMPR_Interno = ENTI.EMPR_Interno AND FUNC.ENTI_Interno = ENTI.ENTI_Interno
   WHERE 
   ENTI.EMPR_Interno = @pbigEMPR_Interno AND 
   FUNC.TABL_TablaTEN = 'TEN' AND
   FUNC.TABL_InternoTEN = ISNULL(@pvchTipoEntidad,FUNC.TABL_InternoTEN) AND
   (ENTI.ENTI_Id LIKE ISNULL(@pvchENTI_Indicio,'') + '%' OR 
   ENTI.ENTI_NombreCompleto LIKE ISNULL(@pvchENTI_Indicio,'') + '%')

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAGO_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAGO_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_PAGO_Insertar]
(    
     @pbigEMPR_Interno           BIGINT
   , @pbigSUCU_Interno           BIGINT
   , @pbigREGI_Interno           BIGINT
   , @pintPAGO_Item              INT
   , @pchrPAGO_Tipo              CHAR(1)
   , @pdecPAGO_MontoCancelado    DECIMAL(20,8)
   , @pdtmPAGO_FechaHoraRegistro DATETIME
   , @pbigUSUA_Interno           BIGINT
   , @pvchAUDI_UsuarioCreacion   VARCHAR(25)
) 
AS 
BEGIN

   SELECT @pintPAGO_Item = (ISNULL(MAX(PAGO_Item), 0)+1)
   FROM [POSAD_DetallesPagosRegistros]
   WHERE 
   [EMPR_Interno] = @pbigEMPR_Interno AND 
   [SUCU_Interno] = @pbigSUCU_Interno AND 
   [REGI_Interno] = @pbigREGI_Interno

   INSERT INTO [POSAD_DetallesPagosRegistros]
      ( EMPR_Interno                , SUCU_Interno             , REGI_Interno                , PAGO_Item             
      , PAGO_Tipo                   , PAGO_MontoCancelado      
      , PAGO_FechaHoraRegistro      , USUA_Interno             , AUDI_UsuarioCreacion        , AUDI_FechaCreacion )
   VALUES
      ( @pbigEMPR_Interno           , @pbigSUCU_Interno        , @pbigREGI_Interno           , @pintPAGO_Item         
      , @pchrPAGO_Tipo              , CASE @pchrPAGO_Tipo WHEN 'S' THEN ABS(@pdecPAGO_MontoCancelado) * -1 ELSE ABS(@pdecPAGO_MontoCancelado) END
      , @pdtmPAGO_FechaHoraRegistro , @pbigUSUA_Interno        , @pvchAUDI_UsuarioCreacion   , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAGO_ConsultaPorRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAGO_ConsultaPorRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_PAGO_ConsultaPorRegistro]
( 
     @pbigEMPR_Interno BIGINT
   , @pbigSUCU_Interno BIGINT
   , @pbigREGI_Interno BIGINT
) 
AS
BEGIN
                                                                     
   SELECT PAGO.EMPR_Interno                           AS [EMPR_Interno]
        , PAGO.SUCU_Interno                           AS [SUCU_Interno]
        , PAGO.REGI_Interno                           AS [REGI_Interno]
        , PAGO.PAGO_Item                              AS [PAGO_Item]
        , PAGO.PAGO_Tipo                              AS [PAGO_Tipo]
        , CASE PAGO.PAGO_Tipo WHEN 'E' THEN 'ENTRADA'
                              WHEN 'S' THEN 'SALIDA'
                              WHEN 'A' THEN 'ADICIONAL'
                              ELSE 'INDEFINIDO' END   AS [PAGO_TipoNombre]
        , PAGO.PAGO_MontoCancelado                    AS [PAGO_MontoCancelado]
        , PAGO.PAGO_FechaHoraRegistro                 AS [PAGO_FechaHoraRegistro]
        , PAGO.USUA_Interno                           AS [USUA_Interno]
        , USUA.USUA_NombreCompleto                    AS [PAGO_UsuarioNombre]
   FROM POSAD_DetallesPagosRegistros AS PAGO
   INNER JOIN POSAD_Usuarios AS USUA ON USUA.USUA_Interno = PAGO.USUA_Interno
   WHERE 
   PAGO.EMPR_Interno= @pbigEMPR_Interno AND 
   PAGO.SUCU_Interno= @pbigSUCU_Interno AND 
   PAGO.REGI_Interno= @pbigREGI_Interno
   ORDER BY 
     PAGO.EMPR_Interno 
   , PAGO.SUCU_Interno 
   , PAGO.REGI_Interno
   , PAGO_Item

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_Acciones]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_Acciones]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_Acciones]
( 
     @pbigEMPR_Interno                 BIGINT
   , @pbigSUCU_Interno                 BIGINT
   , @pbigREGI_Interno                 BIGINT
   , @pchrREGI_Accion                  CHAR(1)
   , @pvchREGI_Mensaje                 VARCHAR(MAX) OUTPUT
   , @pdecREGI_Monto                   DECIMAL(20,8) OUTPUT
   , @pdtmREGI_FechaHora               DATETIME 
   , @pvchREGI_MotivoAnulacion         VARCHAR(250)
   , @pvchAUDI_UsuarioModificacion     VARCHAR(25) 
) 
AS 
BEGIN
   
   DECLARE    @pbigHABI_Interno BIGINT
            , @pdecREGI_PrecioCobrado DECIMAL(20,8)
            , @pdtmREGI_FechaHoraEntrada DATETIME
            , @pdtmREGI_FechaHoraSalida DATETIME
            , @pdecHABI_PrecioHora DECIMAL(20,8)
   SELECT
        @pbigHABI_Interno = REGI.HABI_Interno
      , @pdecREGI_PrecioCobrado = REGI.REGI_PrecioCobrado
      , @pdtmREGI_FechaHoraEntrada = REGI.REGI_FechaHoraEntrada
      , @pdtmREGI_FechaHoraSalida = REGI.REGI_FechaHoraSalida
      , @pdecHABI_PrecioHora = HABI.HABI_PrecioHora
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_Habitaciones AS HABI ON  REGI.EMPR_Interno = HABI.EMPR_Interno AND REGI.SUCU_Interno = HABI.SUCU_Interno AND
                                             REGI.HABI_Interno = HABI.HABI_Interno                     
   WHERE
   REGI.EMPR_Interno = @pbigEMPR_Interno AND 
   REGI.SUCU_Interno = @pbigSUCU_Interno AND 
   REGI.REGI_Interno = @pbigREGI_Interno AND
   REGI.REGI_FechaHoraSalidaReal IS NULL AND
   REGI.REGI_Estado = 'A'

   IF (@pbigHABI_Interno IS NOT NULL)
   BEGIN
   IF (@pchrREGI_Accion = 'L')
   BEGIN
      --LIBERAR
      DECLARE @pintREGI_MinutosMaximos INT, @pintREGI_MinutosDiferencia INT, @pdecREGI_PrecioPagado DECIMAL(20,8), @pdecREGI_MontoAdicional DECIMAL(20,8);

      SET @pintREGI_MinutosMaximos = (SELECT PARA_Valor FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'MIHAB')
      SELECT 
        @pdecREGI_PrecioPagado = SUM(CASE WHEN PAGO_Tipo IN ('E','S') THEN PAGO_MontoCancelado ELSE 0 END) 
      , @pdecREGI_MontoAdicional = SUM(CASE WHEN PAGO_Tipo = 'A' THEN PAGO_MontoCancelado ELSE 0 END) 
      FROM  POSAD_DetallesPagosRegistros
      WHERE
      EMPR_Interno = @pbigEMPR_Interno AND 
      SUCU_Interno = @pbigSUCU_Interno AND 
      REGI_Interno = @pbigREGI_Interno  

      SET @pintREGI_MinutosDiferencia = DATEDIFF(MINUTE,@pdtmREGI_FechaHoraSalida, @pdtmREGI_FechaHora);
      IF (@pintREGI_MinutosDiferencia > @pintREGI_MinutosMaximos AND @pdecREGI_MontoAdicional <= 0 )
      BEGIN
         SET @pdecREGI_Monto = CONVERT(DECIMAL(20,2),((@pintREGI_MinutosDiferencia / 60) + CASE WHEN @pintREGI_MinutosDiferencia % 1 > 0 THEN 1 ELSE 0 END) * @pdecHABI_PrecioHora)
         SET @pvchREGI_Mensaje = 'El cliente debe pagar un REINTEGRO de ' + FORMAT(@pdecREGI_Monto,'###,##0.00')
      END
      ELSE IF (@pdecREGI_PrecioCobrado = @pdecREGI_PrecioPagado)
      BEGIN
         UPDATE POSAD_Registros
            SET REGI_FechaHoraSalidaReal = @pdtmREGI_FechaHora
               ,AUDI_FechaModificacion = GETDATE()
               ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
         WHERE
         EMPR_Interno = @pbigEMPR_Interno AND 
         SUCU_Interno = @pbigSUCU_Interno AND 
         REGI_Interno = @pbigREGI_Interno    

         UPDATE POSAD_Habitaciones  
            SET HABI_Estado = 'L'
               ,HABI_Limpio = 0
               ,AUDI_FechaModificacion = GETDATE()
               ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
         WHERE
         EMPR_Interno = @pbigEMPR_Interno AND 
         SUCU_Interno = @pbigSUCU_Interno AND 
         HABI_Interno = @pbigHABI_Interno    
      END
      ELSE IF (@pdecREGI_PrecioCobrado > @pdecREGI_PrecioPagado)
      BEGIN
         SET @pvchREGI_Mensaje = 'El cliente DEBE un monto de ' + FORMAT((@pdecREGI_PrecioCobrado - @pdecREGI_PrecioPagado),'###,##0.00')
      END
      ELSE IF (@pdecREGI_PrecioCobrado < @pdecREGI_PrecioPagado)
      BEGIN
         SET @pvchREGI_Mensaje = 'Le DEBES al cliente un monto de ' + FORMAT((@pdecREGI_PrecioPagado - @pdecREGI_PrecioCobrado),'###,##0.00')
      END
   END
   ELSE IF (@pchrREGI_Accion = 'A')
   BEGIN
      --ANULAR
      DECLARE @pintREGI_HorasMaximas INT
      SET @pintREGI_HorasMaximas = (SELECT PARA_Valor FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'HOANU')
      IF (DATEDIFF(HOUR,@pdtmREGI_FechaHoraEntrada,@pdtmREGI_FechaHora) > @pintREGI_HorasMaximas)
      BEGIN
         SET @pvchREGI_Mensaje = 'No puedes anular este registro por que ya pasaron mas de ' + CONVERT(VARCHAR(5),@pintREGI_HorasMaximas) + ' horas'
      END
      ELSE
      BEGIN 
         UPDATE POSAD_Registros
            SET REGI_FechaHoraSalidaReal = @pdtmREGI_FechaHora
               ,REGI_FechaHoraAnulacion = @pdtmREGI_FechaHora
               ,REGI_MotivoAnulacion = @pvchREGI_MotivoAnulacion
               ,REGI_Estado = 'X'
               ,AUDI_FechaModificacion = GETDATE()
               ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
         WHERE
         EMPR_Interno = @pbigEMPR_Interno AND 
         SUCU_Interno = @pbigSUCU_Interno AND 
         REGI_Interno = @pbigREGI_Interno    

         UPDATE POSAD_Habitaciones  
            SET HABI_Estado = 'L'
               ,HABI_Limpio = 1
               ,AUDI_FechaModificacion = GETDATE()
               ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
         WHERE
         EMPR_Interno = @pbigEMPR_Interno AND 
         SUCU_Interno = @pbigSUCU_Interno AND 
         HABI_Interno = @pbigHABI_Interno 
      END
   END
   ELSE IF (@pchrREGI_Accion = 'C')
   BEGIN
      --CANCELAR
      DECLARE @pintREGI_HorasMinimas INT
      SET @pintREGI_HorasMinimas = (SELECT PARA_Valor FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'HOCAN')

      IF (@pdtmREGI_FechaHoraSalida > @pdtmREGI_FechaHora)
      BEGIN
         IF (DATEDIFF(HOUR,@pdtmREGI_FechaHora,@pdtmREGI_FechaHoraSalida) <= @pintREGI_HorasMinimas)
         BEGIN
            SET @pvchREGI_Mensaje = 'No puedes cancelar este registro por que faltan menos de ' + CONVERT(VARCHAR(5),@pintREGI_HorasMinimas) + ' horas para culminar el contrato'
         END
         ELSE
         BEGIN 
            UPDATE POSAD_Registros
            SET REGI_FechaHoraSalidaReal = @pdtmREGI_FechaHora
               ,REGI_FechaHoraAnulacion = @pdtmREGI_FechaHora
               ,REGI_MotivoAnulacion = @pvchREGI_MotivoAnulacion
               ,REGI_Estado = 'C'
               ,AUDI_FechaModificacion = GETDATE()
               ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
            WHERE
            EMPR_Interno = @pbigEMPR_Interno AND 
            SUCU_Interno = @pbigSUCU_Interno AND 
            REGI_Interno = @pbigREGI_Interno    

            UPDATE POSAD_Habitaciones  
               SET HABI_Estado = 'L'
                  ,HABI_Limpio = 0
                  ,AUDI_FechaModificacion = GETDATE()
                  ,AUDI_UsuarioModificacion = @pvchAUDI_UsuarioModificacion
            WHERE
            EMPR_Interno = @pbigEMPR_Interno AND 
            SUCU_Interno = @pbigSUCU_Interno AND 
            HABI_Interno = @pbigHABI_Interno  
         END
      END
      ELSE
      BEGIN
         SET @pvchREGI_Mensaje = 'No puede cancelar el registro porque se ha cumplido el contrato'   
      END
   END
   END
   ELSE
   BEGIN
      SET @pvchREGI_Mensaje = 'El registro no se encuentra disponible'
   END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_Actualizar]
( 
     @pbigEMPR_Interno                 BIGINT
   , @pbigSUCU_Interno                 BIGINT
   , @pbigREGI_Interno                 BIGINT
   , @pvchREGI_Mensaje                 VARCHAR(MAX) OUTPUT
   , @pbigHABI_Interno                 BIGINT
   , @pvchAUDI_UsuarioModificacion     VARCHAR(25) 
) 
AS 
BEGIN

   DECLARE @pbigHABI_InternoAnterior BIGINT, @pchrTABL_InternoTHA CHAR(4)
   SELECT 
     @pbigHABI_InternoAnterior = HABI.HABI_Interno
   , @pchrTABL_InternoTHA = TABL_InternoTHA
   FROM POSAD_Registros AS REGI
   INNER JOIN POSAD_Habitaciones AS HABI ON  HABI.EMPR_Interno = REGI.EMPR_Interno AND HABI.SUCU_Interno = REGI.SUCU_Interno AND 
                                             HABI.HABI_Interno = REGI.HABI_Interno
   WHERE
   REGI.EMPR_Interno = @pbigEMPR_Interno AND 
   REGI.SUCU_Interno = @pbigSUCU_Interno AND 
   REGI.REGI_Interno = @pbigREGI_Interno 

   IF (@pbigHABI_InternoAnterior <> @pbigHABI_Interno )
   BEGIN
      
      IF (@pchrTABL_InternoTHA = (SELECT TABL_InternoTHA FROM POSAD_Habitaciones 
                                    WHERE 
                                    EMPR_Interno = @pbigEMPR_Interno AND
                                    SUCU_Interno = @pbigSUCU_Interno AND 
                                    HABI_Interno = @pbigHABI_Interno AND 
                                    HABI_Estado = 'L' ))
      BEGIN
         UPDATE [POSAD_Habitaciones]
            SET [HABI_Estado] = 'L'
               ,[HABI_Limpio] = 0
         WHERE 
         [EMPR_Interno] = @pbigEMPR_Interno AND 
         [SUCU_Interno] = @pbigSUCU_Interno AND 
         [HABI_Interno] = @pbigHABI_InternoAnterior
                
         UPDATE [POSAD_Registros]
            SET [HABI_Interno]               = @pbigHABI_Interno
              , [AUDI_UsuarioModificacion]   = @pvchAUDI_UsuarioModificacion
              , [AUDI_FechaModificacion]     = GETDATE()
         WHERE 
         [EMPR_Interno] = @pbigEMPR_Interno AND 
         [SUCU_Interno] = @pbigSUCU_Interno AND 
         [REGI_Interno] = @pbigREGI_Interno

         UPDATE [POSAD_Habitaciones]
            SET [HABI_Estado] = 'O'
               ,[HABI_Limpio] = 1
         WHERE 
         [EMPR_Interno] = @pbigEMPR_Interno AND 
         [SUCU_Interno] = @pbigSUCU_Interno AND 
         [HABI_Interno] = @pbigHABI_Interno

      END
      ELSE
      BEGIN
         SET @pvchREGI_Mensaje = 'La habitacion a la cual se quiere cambiar no se encuentra LIBRE, o no es del mismo TIPO';
      END
   END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_Insertar]
( 
     @pbigEMPR_Interno                    BIGINT OUTPUT
   , @pbigSUCU_Interno                    BIGINT OUTPUT
   , @pbigREGI_Interno                    BIGINT OUTPUT
   , @pvchREGI_Mensaje                    VARCHAR(MAX) OUTPUT
   , @pbigHABI_Interno                    BIGINT
   , @pchrREGI_Tramos                     CHAR(1)
   , @pintREGI_Cantidad                   INT
   , @pdtmREGI_FechaHoraEntrada           DATETIME
   , @pdtmREGI_FechaHoraSalida            DATETIME
   , @pchrTABL_TablaTDI                   CHAR(3) 
   , @pchrTABL_InternoTDI                 CHAR(4) 
   , @pbigENTI_Interno                    BIGINT 
   , @pvchREGI_HuespedId                  VARCHAR(50)
   , @pvchREGI_HuespedNombreCompleto      VARCHAR(250) 
   , @pvchREGI_HuespedDireccion           VARCHAR(250) 
   , @pdecREGI_PrecioSugerido             DECIMAL(20,8)
   , @pdecREGI_PrecioCobrado              DECIMAL(20,8)
   , @pbigUSUA_Interno                    BIGINT 
   , @pvchAUDI_UsuarioCreacion            VARCHAR(25)
) 
AS 
BEGIN

   DECLARE @pbigTURN_Interno BIGINT, @pvchTipoEntidad VARCHAR(4)
   SET @pbigTURN_Interno = (SELECT TOP 1 TURN.TURN_Interno FROM POSAD_Turnos AS TURN
                              WHERE 
                              EMPR_Interno = @pbigEMPR_Interno AND
                              SUCU_Interno = @pbigSUCU_Interno AND
                              CONVERT(TIME,@pdtmREGI_FechaHoraEntrada) BETWEEN TURN.TURN_HoraInicio AND TURN.TURN_HoraFin)

   SET @pbigENTI_Interno = (SELECT TOP 1  ENTI.ENTI_Interno FROM POSAD_Entidades AS ENTI
                              WHERE 
                              ENTI.EMPR_Interno = @pbigEMPR_Interno AND
                              RTRIM(LTRIM(ENTI.ENTI_Id)) = RTRIM(LTRIM(@pvchREGI_HuespedId)))
   
   SET @pvchTipoEntidad = (SELECT PARA_Valor FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'INHUE')

   IF (@pbigTURN_Interno IS NULL)
      BEGIN
         SET @pvchREGI_Mensaje = 'No se pudo asignar un turno para este registro';
      END
   ELSE IF NOT EXISTS (SELECT HABI.HABI_Interno FROM POSAD_Habitaciones AS HABI 
               WHERE HABI.EMPR_Interno = @pbigEMPR_Interno AND HABI.SUCU_Interno = @pbigSUCU_Interno AND 
                     HABI.HABI_Interno = @pbigHABI_Interno AND HABI.HABI_Estado = 'L')
      BEGIN
         SET @pvchREGI_Mensaje = 'La habitacion no se encuentra libre';
      END
   ELSE
      BEGIN

         IF (@pbigENTI_Interno IS NULL OR @pbigENTI_Interno = 0)
         BEGIN
            EXEC [dbo].[POSAD_ENTI_Insertar]
                   @pbigEMPR_Interno = @pbigEMPR_Interno OUTPUT      
                  ,@pbigENTI_Interno = @pbigENTI_Interno OUTPUT
                  ,@pchrTABL_TablaTDI = @pchrTABL_TablaTDI           
                  ,@pchrTABL_InternoTDI = @pchrTABL_InternoTDI
                  ,@pvchENTI_Id = @pvchREGI_HuespedId                
                  ,@pvchENTI_NombreCompleto = @pvchREGI_HuespedNombreCompleto
                  ,@pvchENTI_Direccion = @pvchREGI_HuespedDireccion  
                  ,@pchrENTI_Sexo = NULL
                  ,@pintPAIS_Interno = NULL                          
                  ,@pintUBIG_Interno = NULL
                  ,@pvchAUDI_UsuarioCreacion = @pvchAUDI_UsuarioCreacion

            EXEC [dbo].[POSAD_FUNC_AsignarPorEntidad] 
                   @pbigEMPR_Interno = @pbigEMPR_Interno
                  ,@pbigENTI_Interno = @pbigENTI_Interno
                  ,@pchrTABL_TablaTEN = 'TEN'
                  ,@pchrTABL_InternoTEN = @pvchTipoEntidad
                  ,@pbitFUNC_Primero = 1
                  ,@pvchAUDI_UsuarioCreacion = @pvchAUDI_UsuarioCreacion
         END
         ELSE
         BEGIN
            UPDATE POSAD_Entidades 
               SET TABL_TablaTDI = @pchrTABL_TablaTDI
                  ,TABL_InternoTDI = @pchrTABL_InternoTDI
                  ,ENTI_Id = @pvchREGI_HuespedId
                  ,ENTI_NombreCompleto = @pvchREGI_HuespedNombreCompleto
                  ,ENTI_Direccion = @pvchREGI_HuespedDireccion
            WHERE EMPR_Interno = @pbigEMPR_Interno AND ENTI_Interno = @pbigENTI_Interno
         END

         SELECT @pbigREGI_Interno = (ISNULL(MAX(REGI_Interno), 0)+1)
         FROM [POSAD_Registros]
         WHERE 
         [EMPR_Interno] = @pbigEMPR_Interno AND 
         [SUCU_Interno] = @pbigSUCU_Interno

         INSERT INTO [POSAD_Registros]
              ( EMPR_Interno             , SUCU_Interno                 , REGI_Interno                   , HABI_Interno             
              , REGI_Estado              , REGI_Tramos                  , REGI_Cantidad                  , REGI_FechaHoraEntrada    
              , REGI_FechaHoraSalida     , REGI_FechaHoraSalidaReal     , TABL_TablaTDI                  , TABL_InternoTDI          
              , ENTI_Interno             , REGI_HuespedId               , REGI_HuespedNombreCompleto     , REGI_HuespedDireccion    
              , REGI_PrecioSugerido      , REGI_PrecioCobrado           , REGI_MotivoAnulacion           , REGI_FechaHoraAnulacion  
              , TURN_Interno             , USUA_Interno                 , AUDI_UsuarioCreacion           , AUDI_FechaCreacion       )
         VALUES
              ( @pbigEMPR_Interno         , @pbigSUCU_Interno        , @pbigREGI_Interno               , @pbigHABI_Interno         
              , 'A'                       , @pchrREGI_Tramos         , @pintREGI_Cantidad              , @pdtmREGI_FechaHoraEntrada
              , @pdtmREGI_FechaHoraSalida , NULL                     , @pchrTABL_TablaTDI              , @pchrTABL_InternoTDI      
              , @pbigENTI_Interno         , @pvchREGI_HuespedId      , @pvchREGI_HuespedNombreCompleto , @pvchREGI_HuespedDireccion
              , @pdecREGI_PrecioSugerido  , @pdecREGI_PrecioCobrado  , NULL                            , NULL
              , @pbigTURN_Interno         , @pbigUSUA_Interno        , @pvchAUDI_UsuarioCreacion       , GETDATE() )
         
         UPDATE [POSAD_Habitaciones]
            SET [HABI_Estado] = 'O'
               ,[HABI_Limpio] = 1
         WHERE 
         EMPR_Interno = @pbigEMPR_Interno AND 
         SUCU_Interno = @pbigSUCU_Interno AND 
         HABI_Interno = @pbigHABI_Interno 

      END

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_CalcularCostos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_CalcularCostos]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_CalcularCostos]
( 
     @pbigEMPR_Interno     BIGINT
   , @pbigSUCU_Interno     BIGINT
   , @pbigHABI_Interno     BIGINT 
   , @pchrREGI_Tramos      CHAR(1)
   , @pintREGI_Cantidad    INT
) 
AS
BEGIN

   DECLARE @ptimHoraInicio TIME, @ptimHoraFin TIME,  @pintHorasMaximas INT, @pintREGI_CantidadDias INT, @pdtmFechaActual DATETIME, @pintHorasAdicionales INT
   SELECT @ptimHoraInicio = CONVERT(TIME, PARA_Valor,108) FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'HOINI'
   SELECT @ptimHoraFin = CONVERT(TIME, PARA_Valor,108) FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'HOFIN'
   SELECT @pintHorasMaximas = CONVERT(INT, PARA_Valor) FROM POSAD_Parametros WHERE EMPR_Interno = @pbigEMPR_Interno AND PARA_Llave = 'HOPMI'

   SET @pdtmFechaActual  = [dbo].[GetDateCountry]('es-PE')
   SET @pintREGI_CantidadDias = CASE WHEN CONVERT(TIME,@pdtmFechaActual) >= @ptimHoraInicio THEN @pintREGI_Cantidad ELSE @pintREGI_Cantidad - 1 END
   SET @pintHorasAdicionales = CASE WHEN @pintREGI_Cantidad >= @pintHorasMaximas THEN @pintREGI_Cantidad - @pintHorasMaximas ELSE 0 END

   SELECT 
        HABI.EMPR_Interno                 AS [EMPR_Interno]
      , HABI.SUCU_Interno                 AS [SUCU_Interno]
      , CONVERT(BIGINT,0)                 AS [REGI_Interno]
      , HABI.HABI_Interno                 AS [HABI_Interno]
      , @pchrREGI_Tramos                  AS [REGI_Tramos]
      , 'A'                               AS [REGI_Estado]
      , @pintREGI_Cantidad                AS [REGI_Cantidad]
      , @pdtmFechaActual                  AS [REGI_FechaHoraEntrada]
      , CASE @pchrREGI_Tramos 
         WHEN 'D' THEN CONVERT(DATETIME,CONVERT(VARCHAR, DATEADD(DAY,@pintREGI_CantidadDias,@pdtmFechaActual),23) + ' ' + CONVERT(VARCHAR,@ptimHoraFin,108))
         WHEN 'H' THEN DATEADD(HOUR,@pintREGI_Cantidad,@pdtmFechaActual)
         END                              AS [REGI_FechaHoraSalida]
      , @pdtmFechaActual                  AS [REGI_FechaHoraSalidaReal]
      , 'TDI'                             AS [TABL_TablaTDI]
      , CONVERT(CHAR(4),NULL)             AS [TABL_InternoTDI]
      , CONVERT(BIGINT,0)                 AS [ENTI_Interno]
      , CONVERT(VARCHAR(50),NULL)         AS [REGI_HuespedId]
      , CONVERT(VARCHAR(250),NULL)        AS [REGI_HuespedNombreCompleto]
      , CONVERT(VARCHAR(250),NULL)        AS [REGI_HuespedDireccion]
      , CONVERT(DECIMAL(20,8),
         CASE @pchrREGI_Tramos 
         WHEN 'D' THEN @pintREGI_Cantidad * HABI.HABI_PrecioDia
         WHEN 'H' THEN HABI.HABI_PrecioMinimo + (@pintHorasAdicionales * HABI.HABI_PrecioHora)
         END)                             AS [REGI_PrecioSugerido]
      , CONVERT(DECIMAL(20,8),0)          AS [REGI_PrecioCobrado]
      , HABI.HABI_Numero                  AS [REGI_HabitacionNumero]
      , HABI.TABL_TablaPIS                AS [TABL_TablaPIS]
      , HABI.TABL_InternoPIS              AS [TABL_InternoPIS]
      , HABI.TABL_TablaTHA                AS [TABL_TablaTHA]
      , HABI.TABL_InternoTHA              AS [TABL_InternoTHA]
      , HABI.HABI_Estado                  AS [REGI_HabitacionEstado]
      , HABI.HABI_Limpio                  AS [REGI_HabitacionLimpio]
   FROM POSAD_Habitaciones AS HABI
   WHERE 
   HABI.EMPR_Interno = @pbigEMPR_Interno AND 
   HABI.SUCU_Interno = @pbigSUCU_Interno AND 
   HABI.HABI_Interno = @pbigHABI_Interno 
           
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_ConsultaRegistro]
( 
     @pbigEMPR_Interno     BIGINT
   , @pbigSUCU_Interno     BIGINT
   , @pbigREGI_Interno     BIGINT 
   , @pbigHABI_Interno     BIGINT 
   , @pvchREGI_Mensaje     VARCHAR(MAX) OUTPUT
) 
AS
BEGIN

   DECLARE @FechaActual DATETIME = [dbo].[GetDateCountry]('es-PE')

   IF (@pbigREGI_Interno IS NULL OR @pbigREGI_Interno = 0)
            BEGIN
               SELECT 
                    HABI.EMPR_Interno                 AS [EMPR_Interno]
                  , HABI.SUCU_Interno                 AS [SUCU_Interno]
                  , CONVERT(BIGINT,0)                 AS [REGI_Interno]
                  , HABI.HABI_Interno                 AS [HABI_Interno]
                  , CONVERT(CHAR(1),NULL)             AS [REGI_Tramos]
                  , CONVERT(INT,0)                    AS [REGI_Cantidad]
                  , @FechaActual                      AS [REGI_FechaHoraEntrada]
                  , @FechaActual                      AS [REGI_FechaHoraSalida]
                  , @FechaActual                      AS [REGI_FechaHoraSalidaReal]
                  , 'TDI'                             AS [TABL_TablaTDI]
                  , CONVERT(CHAR(4),NULL)             AS [TABL_InternoTDI]
                  , CONVERT(BIGINT,0)                 AS [ENTI_Interno]
                  , CONVERT(VARCHAR(50),NULL)         AS [REGI_HuespedId]
                  , CONVERT(VARCHAR(250),NULL)        AS [REGI_HuespedNombreCompleto]
                  , CONVERT(VARCHAR(250),NULL)        AS [REGI_HuespedDireccion]
                  , CONVERT(DECIMAL(20,8),0)          AS [REGI_PrecioSugerido]
                  , CONVERT(DECIMAL(20,8),0)          AS [REGI_PrecioCobrado]
                  , 'A'                               AS [REGI_Estado]
                  , HABI.HABI_Numero                  AS [REGI_HabitacionNumero]
                  , HABI.TABL_TablaPIS                AS [TABL_TablaPIS]
                  , HABI.TABL_InternoPIS              AS [TABL_InternoPIS]
                  , HABI.TABL_TablaTHA                AS [TABL_TablaTHA]
                  , HABI.TABL_InternoTHA              AS [TABL_InternoTHA]
                  , HABI.HABI_Estado                  AS [REGI_HabitacionEstado]
                  , HABI.HABI_Limpio                  AS [REGI_HabitacionLimpio]
                  , 0                                 AS [USUA_Interno]
               FROM POSAD_Habitaciones AS HABI
               WHERE 
               HABI.EMPR_Interno = @pbigEMPR_Interno AND 
               HABI.SUCU_Interno = @pbigSUCU_Interno AND 
               HABI.HABI_Interno = @pbigHABI_Interno 
            END
         ELSE
            BEGIN
               SELECT 
                    REGI.EMPR_Interno                          AS [EMPR_Interno]
                  , REGI.SUCU_Interno                          AS [SUCU_Interno]
                  , REGI.REGI_Interno                          AS [REGI_Interno]
                  , REGI.HABI_Interno                          AS [HABI_Interno]
                  , REGI.REGI_Tramos                           AS [REGI_Tramos]
                  , REGI.REGI_Cantidad                         AS [REGI_Cantidad]
                  , CONVERT(DATE,REGI.REGI_FechaHoraEntrada)   AS [REGI_FechaEntrada]
                  , CONVERT(TIME,REGI.REGI_FechaHoraEntrada)   AS [REGI_HoraEntrada]
                  , REGI.REGI_FechaHoraEntrada                 AS [REGI_FechaHoraEntrada]
                  , CONVERT(DATE,REGI.REGI_FechaHoraSalida)    AS [REGI_FechaSalida]
                  , CONVERT(TIME,REGI.REGI_FechaHoraSalida)    AS [REGI_HoraSalida]
                  , REGI.REGI_FechaHoraSalida                  AS [REGI_FechaHoraSalida]
                  , REGI.REGI_FechaHoraSalidaReal              AS [REGI_FechaHoraSalidaReal]
                  , REGI.TABL_TablaTDI                         AS [TABL_TablaTDI]
                  , REGI.TABL_InternoTDI                       AS [TABL_InternoTDI]
                  , REGI.ENTI_Interno                          AS [ENTI_Interno]
                  , REGI.REGI_HuespedId                        AS [REGI_HuespedId]
                  , REGI.REGI_HuespedNombreCompleto            AS [REGI_HuespedNombreCompleto]
                  , REGI.REGI_HuespedDireccion                 AS [REGI_HuespedDireccion]
                  , REGI.REGI_PrecioSugerido                   AS [REGI_PrecioSugerido]
                  , REGI.REGI_PrecioCobrado                    AS [REGI_PrecioCobrado]
                  , REGI.REGI_Estado                           AS [REGI_Estado]
                  , HABI.HABI_Numero                           AS [REGI_HabitacionNumero]
                  , HABI.TABL_TablaPIS                         AS [TABL_TablaPIS]
                  , HABI.TABL_InternoPIS                       AS [TABL_InternoPIS]
                  , HABI.TABL_TablaTHA                         AS [TABL_TablaTHA]
                  , HABI.TABL_InternoTHA                       AS [TABL_InternoTHA]
                  , HABI.HABI_Estado                           AS [REGI_HabitacionEstado]
                  , HABI.HABI_Limpio                           AS [REGI_HabitacionLimpio]
                  , REGI.USUA_Interno                          AS [USUA_Interno]
               FROM POSAD_Registros AS REGI
               INNER JOIN POSAD_Habitaciones AS HABI ON  REGI.EMPR_Interno = HABI.EMPR_Interno AND REGI.SUCU_Interno = HABI.SUCU_Interno AND 
                                                         REGI.HABI_Interno = HABI.HABI_Interno     
               WHERE 
               REGI.EMPR_Interno = @pbigEMPR_Interno AND 
               REGI.SUCU_Interno = @pbigSUCU_Interno AND 
               REGI.REGI_Interno = @pbigREGI_Interno AND
               HABI.HABI_Interno = @pbigHABI_Interno 
   END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_REGI_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_REGI_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_REGI_ConsultaTodos] 
( 
     @pbigEMPR_Interno                 BIGINT 
   , @pbigSUCU_Interno                 BIGINT 
   , @pchrHABI_Estado                  CHAR(1) 
   , @pchrTABL_TablaPIS                CHAR(3)
   , @pchrTABL_InternoPIS              CHAR(4)
   , @pchrTABL_TablaTHA                CHAR(3)
   , @pchrTABL_InternoTHA              CHAR(4)
   , @pvchREGI_HuespedNombreCompleto   VARCHAR(250) 
   , @pchrHABI_Numero                  CHAR(3) 
)
AS
BEGIN

   /*LIBRES*/
   SELECT 
       HABI.EMPR_Interno               AS [EMPR_Interno]
      ,HABI.SUCU_Interno               AS [SUCU_Interno]
      ,CONVERT(BIGINT,0)               AS [TURN_Interno]
      ,CONVERT(INT,0)                  AS [REGI_Interno]
      ,HABI.HABI_Interno               AS [HABI_Interno]
      ,HABI.HABI_Numero                AS [REGI_HabitacionNumero]
      ,HABI.TABL_TablaPIS              AS [TABL_TablaPIS]
      ,HABI.TABL_InternoPIS            AS [TABL_InternoPIS]
      ,TPIS.TABL_Nombre                AS [TABL_NombrePIS]
      ,HABI.TABL_TablaTHA              AS [TABL_TablaTHA]
      ,HABI.TABL_InternoTHA            AS [TABL_InternoTHA]
      ,TTHA.TABL_Nombre                AS [TABL_NombreTHA]
      ,CONVERT(VARCHAR(250),'******')  AS [REGI_HuespedNombreCompleto]
	  ,CONVERT(DATETIME,NULL)	       AS [REGI_FechaHoraEntrada]
	  ,CONVERT(DATETIME,NULL)	       AS [REGI_FechaHoraSalida]
      ,HABI.HABI_Estado                AS [REGI_HabitacionEstado]
      ,HABI.HABI_Limpio                AS [REGI_HabitacionLimpio]
      ,CONVERT(DECIMAL(20,8),0)        AS [REGI_PrecioCobrado]
      ,CONVERT(DECIMAL(20,8),0)        AS [REGI_MontoCancelado]
      ,CONVERT(DECIMAL(20,8),0)        AS [REGI_Deuda]
      ,CONVERT(DECIMAL(20,8),0)        AS [REGI_Vuelto]
      ,CONVERT(DECIMAL(20,8),0)        AS [REGI_Adicional]
   INTO #TMP_Registros
   FROM POSAD_Habitaciones AS HABI
   INNER JOIN POSAD_Tablas AS TPIS ON  HABI.EMPR_Interno = TPIS.EMPR_Interno AND HABI.TABL_TablaPIS = TPIS.TABL_Tabla AND 
                                       HABI.TABL_InternoPIS = TPIS.TABL_Interno
   INNER JOIN POSAD_Tablas AS TTHA ON  HABI.EMPR_Interno = TTHA.EMPR_Interno AND HABI.TABL_TablaTHA = TTHA.TABL_Tabla AND 
                                       HABI.TABL_InternoTHA = TTHA.TABL_Interno
   WHERE 
   HABI.EMPR_Interno = @pbigEMPR_Interno AND 
   HABI.SUCU_Interno = @pbigSUCU_Interno AND 
   HABI.HABI_Estado = 'L'
   

   /*OCUPADAS*/
   INSERT INTO #TMP_Registros
   SELECT 
       HABI.EMPR_Interno                  AS [EMPR_Interno]
      ,HABI.SUCU_Interno                  AS [SUCU_Interno]
      ,REGI.TURN_Interno                  AS [TURN_Interno]
      ,REGI.REGI_Interno                  AS [REGI_Interno]
      ,HABI.HABI_Interno                  AS [HABI_Interno]
      ,HABI.HABI_Numero                   AS [REGI_HabitacionNumero]
      ,HABI.TABL_TablaPIS                 AS [TABL_TablaPIS]
      ,HABI.TABL_InternoPIS               AS [TABL_InternoPIS]
      ,TPIS.TABL_Nombre                   AS [TABL_NombrePIS]
      ,HABI.TABL_TablaTHA                 AS [TABL_TablaTHA]
      ,HABI.TABL_InternoTHA               AS [TABL_InternoTHA]
      ,TTHA.TABL_Nombre                   AS [TABL_NombreTHA]
      ,REGI.REGI_HuespedNombreCompleto    AS [REGI_HuespedNombreCompleto]
	  ,REGI.REGI_FechaHoraEntrada		  AS [REGI_FechaHoraEntrada]
	  ,REGI.REGI_FechaHoraSalida		  AS [REGI_FechaHoraSalida]
      ,HABI.HABI_Estado                   AS [REGI_HabitacionEstado]
      ,HABI.HABI_Limpio                   AS [REGI_HabitacionLimpio]
      ,ISNULL(REGI.REGI_PrecioCobrado,0)  AS [REGI_PrecioCobrado]
      ,SUM(CONVERT(DECIMAL(20,8),CASE WHEN DETA.PAGO_Tipo IN ('E','S')
            THEN ISNULL(DETA.PAGO_MontoCancelado,0)
            ELSE 0
            END))                         AS [REGI_MontoCancelado]
      ,CONVERT(DECIMAL(20,8),0)           AS [REGI_Deuda]
      ,CONVERT(DECIMAL(20,8),0)           AS [REGI_Vuelto]
      ,SUM(CONVERT(DECIMAL(20,8),CASE WHEN DETA.PAGO_Tipo = 'A'
            THEN ISNULL(DETA.PAGO_MontoCancelado,0)
            ELSE 0
            END))                         AS [REGI_Adicional]
   FROM POSAD_Habitaciones AS HABI
   INNER JOIN POSAD_Tablas AS TPIS ON  HABI.EMPR_Interno = TPIS.EMPR_Interno AND HABI.TABL_TablaPIS = TPIS.TABL_Tabla AND 
                                       HABI.TABL_InternoPIS = TPIS.TABL_Interno
   INNER JOIN POSAD_Tablas AS TTHA ON  HABI.EMPR_Interno = TTHA.EMPR_Interno AND HABI.TABL_TablaTHA = TTHA.TABL_Tabla AND 
                                       HABI.TABL_InternoTHA = TTHA.TABL_Interno
   INNER JOIN POSAD_Registros AS REGI ON  HABI.EMPR_Interno = REGI.EMPR_Interno AND HABI.SUCU_Interno = REGI.SUCU_Interno AND 
                                          HABI.HABI_Interno = REGI.HABI_Interno
   LEFT JOIN POSAD_DetallesPagosRegistros AS DETA ON  REGI.EMPR_Interno = DETA.EMPR_Interno AND REGI.SUCU_Interno = DETA.SUCU_Interno AND
                                                      REGI.REGI_Interno = DETA.REGI_Interno 
   WHERE 
   HABI.EMPR_Interno = @pbigEMPR_Interno AND 
   HABI.SUCU_Interno = @pbigSUCU_Interno AND 
   REGI.REGI_FechaHoraSalidaReal IS NULL AND 
   REGI.REGI_FechaHoraAnulacion IS NULL AND
   HABI.HABI_Estado = 'O'
   GROUP BY 
       HABI.EMPR_Interno                
      ,HABI.SUCU_Interno                
      ,REGI.TURN_Interno                
      ,REGI.REGI_Interno                   
      ,HABI.HABI_Interno            
      ,HABI.HABI_Numero     
      ,HABI.TABL_TablaPIS   
      ,HABI.TABL_InternoPIS 
      ,TPIS.TABL_Nombre     
      ,HABI.TABL_TablaTHA   
      ,HABI.TABL_InternoTHA 
      ,TTHA.TABL_Nombre     
      ,REGI.REGI_HuespedNombreCompleto  
	  ,REGI.REGI_FechaHoraEntrada
	  ,REGI.REGI_FechaHoraSalida
      ,HABI.HABI_Estado                 
      ,HABI.HABI_Limpio                 
      ,ISNULL(REGI.REGI_PrecioCobrado,0)
   
   UPDATE #TMP_Registros
      SET REGI_Deuda  = CASE WHEN 
                        ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0) > 0
                        THEN ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0)
                        ELSE 0 END
         ,REGI_Vuelto = CASE WHEN 
                        ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0) < 0
                        THEN ISNULL(REGI_PrecioCobrado,0) - ISNULL(REGI_MontoCancelado,0)
                        ELSE 0 END 
   
   /*RESERVADAS*/
   UPDATE REGI
      SET REGI_HabitacionEstado = 'R'
   FROM #TMP_Registros AS REGI
   INNER JOIN POSAD_Reservaciones AS RESE ON REGI.EMPR_Interno = RESE.EMPR_Interno AND REGI.SUCU_Interno = RESE.SUCU_Interno AND 
                                             REGI.HABI_Interno = RESE.HABI_Interno AND RESE.RESE_Estado = 'A' AND 
                                             CONVERT(DATE,[dbo].[GetDateCountry]('es-PE')) BETWEEN RESE.RESE_FechaInicio AND RESE.RESE_FechaFin
   WHERE REGI.REGI_Interno IS NULL OR REGI.REGI_Interno = 0
   
   /*SELECCION FINAL*/
   SELECT * FROM #TMP_Registros
   WHERE 
   REGI_HabitacionEstado = ISNULL(@pchrHABI_Estado,REGI_HabitacionEstado) AND
   REGI_HuespedNombreCompleto LIKE '%' + ISNULL(@pvchREGI_HuespedNombreCompleto,'') + '%' AND
   REGI_HabitacionNumero LIKE '%' + ISNULL(CONVERT(VARCHAR(3),RTRIM(LTRIM(@pchrHABI_Numero))),'') + '%' AND
   TABL_TablaPIS = @pchrTABL_TablaPIS AND
   TABL_InternoPIS = ISNULL(@pchrTABL_InternoPIS,TABL_InternoPIS) AND
   TABL_TablaTHA = @pchrTABL_TablaTHA AND 
   TABL_InternoTHA = ISNULL(@pchrTABL_InternoTHA,TABL_InternoTHA)
   ORDER BY TABL_TablaPIS, TABL_InternoPIS, REGI_HabitacionNumero

END
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RESE_ActualizaEstado]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RESE_ActualizaEstado]
GO
CREATE PROCEDURE [dbo].[POSAD_RESE_ActualizaEstado]
(
     @pbigEMPR_Interno                 BIGINT 
   , @pbigSUCU_Interno                 BIGINT 
   , @pbigRESE_Interno                 BIGINT 
   , @pchrRESE_Estado                  CHAR(1)
   , @pvchAUDI_UsuarioModificacion     VARCHAR(25) 
)
AS
BEGIN

   UPDATE [POSAD_Reservaciones]
      SET [RESE_Estado] = @pchrRESE_Estado
         ,[AUDI_FechaModificacion] = GETDATE()
         ,[AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
   WHERE 
   [EMPR_Interno] = @pbigEMPR_Interno AND 
   [SUCU_Interno] = @pbigSUCU_Interno AND 
   [RESE_Interno] = @pbigRESE_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RESE_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RESE_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_RESE_Actualizar]
( 
     @pbigEMPR_Interno                 BIGINT 
   , @pbigSUCU_Interno                 BIGINT 
   , @pbigRESE_Interno                 BIGINT 
   , @pvchRESE_Mensaje                 VARCHAR(MAX) OUTPUT
   , @pchrRESE_Estado                  CHAR(1)
   , @pbigHABI_Interno                 BIGINT 
   , @pdteRESE_FechaInicio             DATE
   , @pdteRESE_FechaFin                DATE
   , @pdtmRESE_FechaHoraRegistro       DATETIME
   , @pvchRESE_HuespedId               VARCHAR(50) 
   , @pvchRESE_HuespedNombreCompleto   VARCHAR(250)
   , @pvchRESE_HuespedDireccion        VARCHAR(250)
   , @pvchRESE_Descripcion             VARCHAR(250)
   , @pvchAUDI_UsuarioModificacion     VARCHAR(25) 
) 
AS 
BEGIN
   
   SET @pvchRESE_Mensaje = NULL
   IF(EXISTS(SELECT RESE_Interno FROM POSAD_Reservaciones 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND RESE_Estado = 'A' AND
	  RESE_Interno <> @pbigRESE_Interno AND
      @pdteRESE_FechaInicio BETWEEN RESE_FechaInicio AND RESE_FechaFin) OR
   EXISTS(SELECT REGI_Interno FROM POSAD_Registros 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND REGI_Estado = 'A' AND
      @pdteRESE_FechaInicio BETWEEN CONVERT(DATE,REGI_FechaHoraEntrada) AND CONVERT(DATE,ISNULL(REGI_FechaHoraSalidaReal,REGI_FechaHoraSalida)))) 
   BEGIN
      SET @pvchRESE_Mensaje = 'La habitacion se encuentra ocupada y/o reservada para la fecha ' + CONVERT(VARCHAR(25), @pdteRESE_FechaInicio,103) 
   END

   IF(EXISTS(SELECT RESE_Interno FROM POSAD_Reservaciones 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND RESE_Estado = 'A' AND
	  RESE_Interno <> @pbigRESE_Interno AND
      @pdteRESE_FechaFin BETWEEN RESE_FechaInicio AND RESE_FechaFin) OR
   EXISTS(SELECT REGI_Interno FROM POSAD_Registros 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND REGI_Estado = 'A' AND
      @pdteRESE_FechaFin BETWEEN CONVERT(DATE,REGI_FechaHoraEntrada) AND CONVERT(DATE,ISNULL(REGI_FechaHoraSalidaReal,REGI_FechaHoraSalida)))) 
   BEGIN
      SET @pvchRESE_Mensaje = 'La habitacion se encuentra ocupada y/o reservada para la fecha ' + CONVERT(VARCHAR(25), @pdteRESE_FechaFin,103) 
   END

   IF (@pvchRESE_Mensaje IS NULL)
   BEGIN
      UPDATE [POSAD_Reservaciones]
         SET [RESE_Estado]                = @pchrRESE_Estado
           , [HABI_Interno]               = @pbigHABI_Interno
           , [RESE_FechaInicio]           = @pdteRESE_FechaInicio
           , [RESE_FechaFin]              = @pdteRESE_FechaFin
           , [RESE_FechaHoraRegistro]     = @pdtmRESE_FechaHoraRegistro
           , [RESE_HuespedId]             = @pvchRESE_HuespedId
           , [RESE_HuespedNombreCompleto] = @pvchRESE_HuespedNombreCompleto
           , [RESE_HuespedDireccion]      = @pvchRESE_HuespedDireccion
           , [RESE_Descripcion]           = @pvchRESE_Descripcion
           , [AUDI_UsuarioModificacion]   = @pvchAUDI_UsuarioModificacion
           , [AUDI_FechaModificacion]     = GETDATE() 
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [SUCU_Interno] = @pbigSUCU_Interno AND 
      [RESE_Interno] = @pbigRESE_Interno
   END

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RESE_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RESE_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_RESE_Insertar]
( 
     @pbigEMPR_Interno                 BIGINT OUTPUT
   , @pbigSUCU_Interno                 BIGINT OUTPUT
   , @pbigRESE_Interno                 BIGINT OUTPUT
   , @pvchRESE_Mensaje                 VARCHAR(MAX) OUTPUT
   , @pchrRESE_Estado                  CHAR(1)
   , @pbigHABI_Interno                 BIGINT 
   , @pdteRESE_FechaInicio             DATE
   , @pdteRESE_FechaFin                DATE
   , @pdtmRESE_FechaHoraRegistro       DATETIME
   , @pvchRESE_HuespedId               VARCHAR(50) 
   , @pvchRESE_HuespedNombreCompleto   VARCHAR(250)
   , @pvchRESE_HuespedDireccion        VARCHAR(250)
   , @pvchRESE_Descripcion             VARCHAR(250)
   , @pvchAUDI_UsuarioCreacion         VARCHAR(25) 
) 
AS 
BEGIN

   SET @pvchRESE_Mensaje = NULL
   IF(EXISTS(SELECT RESE_Interno FROM POSAD_Reservaciones 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND RESE_Estado = 'A' AND
      @pdteRESE_FechaInicio BETWEEN RESE_FechaInicio AND RESE_FechaFin) OR
   EXISTS(SELECT REGI_Interno FROM POSAD_Registros 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND REGI_Estado = 'A' AND
      @pdteRESE_FechaInicio BETWEEN CONVERT(DATE,REGI_FechaHoraEntrada) AND CONVERT(DATE,ISNULL(REGI_FechaHoraSalidaReal,REGI_FechaHoraSalida)))) 
   BEGIN
      SET @pvchRESE_Mensaje = 'La habitacion se encuentra ocupada y/o reservada para la fecha ' + CONVERT(VARCHAR(25), @pdteRESE_FechaInicio,103) 
   END

   IF(EXISTS(SELECT RESE_Interno FROM POSAD_Reservaciones 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND RESE_Estado = 'A' AND
      @pdteRESE_FechaFin BETWEEN RESE_FechaInicio AND RESE_FechaFin) OR
   EXISTS(SELECT REGI_Interno FROM POSAD_Registros 
      WHERE 
      EMPR_Interno = @pbigEMPR_Interno AND SUCU_Interno = @pbigSUCU_Interno AND 
      HABI_Interno = @pbigHABI_Interno AND REGI_Estado = 'A' AND
      @pdteRESE_FechaFin BETWEEN CONVERT(DATE,REGI_FechaHoraEntrada) AND CONVERT(DATE,ISNULL(REGI_FechaHoraSalidaReal,REGI_FechaHoraSalida)))) 
   BEGIN
      SET @pvchRESE_Mensaje = 'La habitacion se encuentra ocupada y/o reservada para la fecha ' + CONVERT(VARCHAR(25), @pdteRESE_FechaFin,103) 
   END

   IF (@pvchRESE_Mensaje IS NULL)
   BEGIN
      SELECT @pbigRESE_Interno = (ISNULL(MAX(RESE_Interno), 0)+1)
        FROM [POSAD_Reservaciones]
       WHERE [EMPR_Interno]                   = @pbigEMPR_Interno
         AND [SUCU_Interno]                   = @pbigSUCU_Interno

      INSERT INTO [POSAD_Reservaciones]
           ( EMPR_Interno             , SUCU_Interno                   , RESE_Interno              , RESE_Estado               
           , HABI_Interno             , RESE_FechaInicio               , RESE_FechaFin             , RESE_FechaHoraRegistro    
           , RESE_HuespedId           , RESE_HuespedNombreCompleto     , RESE_HuespedDireccion     , RESE_Descripcion          
           , AUDI_UsuarioCreacion     , AUDI_FechaCreacion             )
      VALUES
           ( @pbigEMPR_Interno         , @pbigSUCU_Interno               , @pbigRESE_Interno          , @pchrRESE_Estado           
           , @pbigHABI_Interno         , @pdteRESE_FechaInicio           , @pdteRESE_FechaFin         , @pdtmRESE_FechaHoraRegistro
           , @pvchRESE_HuespedId       , @pvchRESE_HuespedNombreCompleto , @pvchRESE_HuespedDireccion , @pvchRESE_Descripcion      
           , @pvchAUDI_UsuarioCreacion , GETDATE() )
   END

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RESE_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RESE_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_RESE_ConsultaRegistro]
( 
     @pbigEMPR_Interno     BIGINT
   , @pbigSUCU_Interno     BIGINT
   , @pbigRESE_Interno     BIGINT 
) 
AS
BEGIN

   SELECT 
     RESE.EMPR_Interno                    AS [EMPR_Interno]
   , RESE.SUCU_Interno                    AS [SUCU_Interno]
   , RESE.RESE_Interno                    AS [RESE_Interno]
   , RESE.RESE_Estado                     AS [RESE_Estado]
   , RESE.HABI_Interno                    AS [HABI_Interno]
   , RESE.RESE_FechaInicio                AS [RESE_FechaInicio]
   , RESE.RESE_FechaFin                   AS [RESE_FechaFin]
   , RESE.RESE_FechaHoraRegistro          AS [RESE_FechaHoraRegistro]
   , RESE.RESE_HuespedId                  AS [RESE_HuespedId]
   , RESE.RESE_HuespedNombreCompleto      AS [RESE_HuespedNombreCompleto]
   , RESE.RESE_HuespedDireccion           AS [RESE_HuespedDireccion]
   , RESE.RESE_Descripcion                AS [RESE_Descripcion]
   , HABI.TABL_TablaPIS                   AS [TABL_TablaPIS]
   , HABI.TABL_InternoPIS                 AS [TABL_InternoPIS]
   , HABI.TABL_TablaTHA                   AS [TABL_TablaTHA]
   , HABI.TABL_InternoTHA                 AS [TABL_InternoTHA]
   FROM POSAD_Reservaciones AS RESE
   INNER JOIN POSAD_Habitaciones AS HABI ON HABI.EMPR_Interno = RESE.EMPR_Interno AND HABI.SUCU_Interno = RESE.SUCU_Interno AND
                                            HABI.HABI_Interno = RESE.HABI_Interno
   WHERE 
   RESE.EMPR_Interno= @pbigEMPR_Interno AND 
   RESE.SUCU_Interno= @pbigSUCU_Interno AND 
   RESE.RESE_Interno= @pbigRESE_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_RESE_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_RESE_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_RESE_ConsultaTodos] 
( 
     @pbigEMPR_Interno                 BIGINT
   , @pbigSUCU_Interno                 BIGINT
   , @pchrRESE_Estado                  CHAR(1) 
   , @pvchRESE_HuespedNombreCompleto   VARCHAR(250) 
   , @pchrHABI_Numero                  CHAR(3) 
)
AS
BEGIN

   SELECT 
     RESE.EMPR_Interno                 AS [EMPR_Interno]
   , RESE.SUCU_Interno                 AS [SUCU_Interno]
   , RESE.RESE_Interno                 AS [RESE_Interno]
   , RESE.RESE_Estado                  AS [RESE_Estado]
   , CASE RESE.RESE_Estado 
      WHEN 'A' THEN 'ACTIVO'
      WHEN 'X' THEN 'ANULADO'
      WHEN 'E' THEN 'ENTREGADO'
      END                              AS [RESE_NombreEstado]
   , RESE.HABI_Interno                 AS [HABI_Interno]
   , RESE.RESE_FechaInicio             AS [RESE_FechaInicio]
   , RESE.RESE_FechaFin                AS [RESE_FechaFin]
   , RESE.RESE_HuespedId               AS [RESE_HuespedId]
   , RESE.RESE_HuespedNombreCompleto   AS [RESE_HuespedNombreCompleto]
   , RESE.RESE_HuespedDireccion        AS [RESE_HuespedDireccion]  
   , HABI.HABI_Numero                  AS [RESE_Habitacion]
   , TTHA.TABL_Nombre                  AS [TABL_NombreTHA]
   , TPIS.TABL_Nombre                  AS [TABL_NombrePIS]
   FROM POSAD_Reservaciones AS RESE
   INNER JOIN POSAD_Habitaciones AS HABI ON HABI.EMPR_Interno = RESE.EMPR_Interno AND HABI.SUCU_Interno = RESE.SUCU_Interno AND
                                            HABI.HABI_Interno = RESE.HABI_Interno
   INNER JOIN POSAD_Tablas AS TTHA ON TTHA.EMPR_Interno = HABI.EMPR_Interno AND TTHA.TABL_Tabla = HABI.TABL_TablaTHA AND 
                                      TTHA.TABL_Interno = HABI.TABL_InternoTHA 
   INNER JOIN POSAD_Tablas AS TPIS ON TPIS.EMPR_Interno = HABI.EMPR_Interno AND TPIS.TABL_Tabla = HABI.TABL_TablaPIS AND 
                                      TPIS.TABL_Interno = HABI.TABL_InternoPIS 
   WHERE 
   RESE.EMPR_Interno = @pbigEMPR_Interno AND 
   RESE.SUCU_Interno = @pbigSUCU_Interno AND 
   RESE.RESE_Estado = @pchrRESE_Estado AND 
   RESE.RESE_HuespedNombreCompleto LIKE '%' + ISNULL(@pvchRESE_HuespedNombreCompleto,'') + '%' AND 
   HABI.HABI_Numero LIKE '%' + ISNULL(CONVERT(VARCHAR(3),RTRIM(LTRIM(@pchrHABI_Numero))),'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TURN_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TURN_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_TURN_Eliminar]
( 
     @pbigEMPR_Interno                 BIGINT
   , @pbigSUCU_Interno                 BIGINT
   , @pbigTURN_Interno                 BIGINT
) 
AS 
BEGIN

   DELETE FROM [POSAD_Turnos]
   WHERE 
   [EMPR_Interno]                 = @pbigEMPR_Interno AND 
   [SUCU_Interno]                 = @pbigSUCU_Interno AND 
   [TURN_Interno]                 = @pbigTURN_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TURN_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TURN_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_TURN_Actualizar]
( 
     @pbigEMPR_Interno                 BIGINT 
   , @pbigSUCU_Interno                 BIGINT 
   , @pbigTURN_Interno                 BIGINT 
   , @pvchTURN_Nominacion              VARCHAR(50) 
   , @ptimTURN_HoraInicio              DATETIME 
   , @ptimTURN_HoraFin                 DATETIME 
   , @pvchTURN_Descripcion             VARCHAR(250)
   , @pvchTURN_Color                   VARCHAR(20) 
   , @pbitTURN_Activo                  BIT
   , @pvchAUDI_UsuarioModificacion     VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Turnos]
      SET [TURN_Nominacion]            = @pvchTURN_Nominacion
        , [TURN_HoraInicio]            = CONVERT(TIME,@ptimTURN_HoraInicio)
        , [TURN_HoraFin]               = CONVERT(TIME,@ptimTURN_HoraFin)
        , [TURN_Descripcion]           = @pvchTURN_Descripcion
        , [TURN_Color]                 = @pvchTURN_Color
        , [TURN_Activo]                = @pbitTURN_Activo
        , [AUDI_UsuarioModificacion]   = @pvchAUDI_UsuarioModificacion
   WHERE 
   [EMPR_Interno]                 = @pbigEMPR_Interno AND 
   [SUCU_Interno]                 = @pbigSUCU_Interno AND 
   [TURN_Interno]                 = @pbigTURN_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TURN_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TURN_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_TURN_Insertar]
( 
    @pbigEMPR_Interno            BIGINT OUTPUT
   ,@pbigSUCU_Interno            BIGINT OUTPUT
   ,@pbigTURN_Interno            BIGINT OUTPUT
   ,@pvchTURN_Nominacion         VARCHAR(50) 
   ,@ptimTURN_HoraInicio         DATETIME
   ,@ptimTURN_HoraFin            DATETIME
   ,@pvchTURN_Descripcion        VARCHAR(250)
   ,@pvchTURN_Color              VARCHAR(20) 
   ,@pbitTURN_Activo             BIT
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigTURN_Interno = (ISNULL(MAX(TURN_Interno), 0)+1)
     FROM [POSAD_Turnos]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno

   INSERT INTO [POSAD_Turnos]
   ( EMPR_Interno       , SUCU_Interno          , TURN_Interno        , TURN_Nominacion    
   , TURN_HoraInicio    , TURN_HoraFin          , TURN_Descripcion    , TURN_Color         
   , TURN_Activo        , AUDI_UsuarioCreacion  , AUDI_FechaCreacion  )
   VALUES
   ( @pbigEMPR_Interno                    , @pbigSUCU_Interno                 , @pbigTURN_Interno        , @pvchTURN_Nominacion
   , CONVERT(TIME,@ptimTURN_HoraInicio)   , CONVERT(TIME,@ptimTURN_HoraFin)   , @pvchTURN_Descripcion    , @pvchTURN_Color     
   , @pbitTURN_Activo                     , @pvchAUDI_UsuarioCreacion         , GETDATE()                         )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TURN_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TURN_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_TURN_ConsultaRegistro]
( 
    @pbigEMPR_Interno      BIGINT
   ,@pbigSUCU_Interno      BIGINT
   ,@pbigTURN_Interno      BIGINT 
) 
AS
BEGIN

   SELECT 
        TURN.EMPR_Interno           AS [EMPR_Interno]
      , TURN.SUCU_Interno           AS [SUCU_Interno]
      , TURN.TURN_Interno           AS [TURN_Interno]
      , TURN.TURN_Nominacion        AS [TURN_Nominacion]
      , TURN.TURN_HoraInicio        AS [TURN_HoraInicio]
      , TURN.TURN_HoraFin           AS [TURN_HoraFin]
      , TURN.TURN_Descripcion       AS [TURN_Descripcion]
      , TURN.TURN_Color             AS [TURN_Color]
      , TURN.TURN_Activo            AS [TURN_Activo]
   FROM POSAD_Turnos AS TURN
   WHERE 
   TURN.EMPR_Interno= @pbigEMPR_Interno AND 
   TURN.SUCU_Interno= @pbigSUCU_Interno AND 
   TURN.TURN_Interno= @pbigTURN_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TURN_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TURN_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_TURN_ConsultaTodos] 
( 
    @pbigEMPR_Interno   BIGINT
   ,@pbigSUCU_Interno   BIGINT
)
AS
BEGIN

   SELECT 
        TURN.EMPR_Interno               AS [EMPR_Interno]
      , TURN.SUCU_Interno               AS [SUCU_Interno]
      , TURN.TURN_Interno               AS [TURN_Interno]
      , TURN.TURN_Nominacion            AS [TURN_Nominacion]
      , TURN.TURN_HoraInicio            AS [TURN_HoraInicio]
      , TURN.TURN_HoraFin               AS [TURN_HoraFin]
      , TURN.TURN_Activo                AS [TURN_Activo]
      , SUCU.SUCU_Nombre                AS [TURN_Sucursal]
   FROM POSAD_Turnos AS TURN
   INNER JOIN POSAD_Sucursales AS SUCU ON SUCU.EMPR_Interno = TURN.EMPR_Interno AND SUCU.SUCU_Interno = TURN.SUCU_Interno
   WHERE 
   TURN.EMPR_Interno = @pbigEMPR_Interno AND
   SUCU.SUCU_Interno = ISNULL(@pbigSUCU_Interno,SUCU.SUCU_Interno)

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_Eliminar]
( 
    @pbigEMPR_Interno  BIGINT
   ,@pbigSUCU_Interno  BIGINT
   ,@pbigHABI_Interno  BIGINT
) 
AS 
BEGIN

   DELETE FROM [POSAD_Habitaciones]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno
      AND [HABI_Interno]                 = @pbigHABI_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_Actualizar]
( 
    @pbigEMPR_Interno               BIGINT 
   ,@pbigSUCU_Interno               BIGINT 
   ,@pbigHABI_Interno               BIGINT 
   ,@pchrTABL_TablaPIS              CHAR(3)
   ,@pchrTABL_InternoPIS             CHAR(4)
   ,@pchrTABL_TablaTHA              CHAR(3)
   ,@pchrTABL_InternoTHA             CHAR(4)
   ,@pchrHABI_Numero                CHAR(3)
   ,@pbitHABI_Activo                BIT
   ,@pvchHABI_Descripcion           VARCHAR(250) 
   ,@pdecHABI_PrecioDia             DECIMAL(20,8)
   ,@pdecHABI_PrecioHora            DECIMAL(20,8)
   ,@pdecHABI_PrecioMinimo          DECIMAL(20,8)
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS
BEGIN

   UPDATE [POSAD_Habitaciones]
      SET [TABL_TablaPIS]            = @pchrTABL_TablaPIS
        , [TABL_InternoPIS]          = @pchrTABL_InternoPIS
        , [TABL_TablaTHA]            = @pchrTABL_TablaTHA
        , [TABL_InternoTHA]          = @pchrTABL_InternoTHA
        , [HABI_Numero]              = @pchrHABI_Numero
        , [HABI_Activo]              = @pbitHABI_Activo
        , [HABI_Descripcion]         = @pvchHABI_Descripcion
        , [HABI_PrecioDia]           = @pdecHABI_PrecioDia
        , [HABI_PrecioHora]          = @pdecHABI_PrecioHora
        , [HABI_PrecioMinimo]        = @pdecHABI_PrecioMinimo
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno
      AND [HABI_Interno]                 = @pbigHABI_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_Insertar]
( 
    @pbigEMPR_Interno         BIGINT OUTPUT
   ,@pbigSUCU_Interno         BIGINT OUTPUT
   ,@pbigHABI_Interno         BIGINT OUTPUT
   ,@pchrTABL_TablaPIS        CHAR(3)
   ,@pchrTABL_InternoPIS       CHAR(4)
   ,@pchrTABL_TablaTHA        CHAR(3)
   ,@pchrTABL_InternoTHA       CHAR(4)
   ,@pchrHABI_Numero          CHAR(3)
   ,@pbitHABI_Activo          BIT
   ,@pvchHABI_Descripcion     VARCHAR(250) 
   ,@pdecHABI_PrecioDia       DECIMAL(20,8)
   ,@pdecHABI_PrecioHora      DECIMAL(20,8)
   ,@pdecHABI_PrecioMinimo    DECIMAL(20,8)
   ,@pvchAUDI_UsuarioCreacion VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigHABI_Interno = (ISNULL(MAX(HABI_Interno), 0)+1)
     FROM [POSAD_Habitaciones]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno

   INSERT INTO [POSAD_Habitaciones]
        ( EMPR_Interno           , SUCU_Interno                 , HABI_Interno          , TABL_TablaPIS           
        , TABL_InternoPIS         , TABL_TablaTHA                , TABL_InternoTHA        , HABI_Numero             
        , HABI_Estado            , HABI_Limpio                  , HABI_Activo           , HABI_Descripcion        
        , HABI_PrecioDia         , HABI_PrecioHora              , HABI_PrecioMinimo     , AUDI_UsuarioCreacion    
        , AUDI_FechaCreacion     )
   VALUES
        ( @pbigEMPR_Interno       , @pbigSUCU_Interno             , @pbigHABI_Interno      , @pchrTABL_TablaPIS       
        , @pchrTABL_InternoPIS     , @pchrTABL_TablaTHA            , @pchrTABL_InternoTHA    , @pchrHABI_Numero         
        , 'L'                     , 1                             , @pbitHABI_Activo       , @pvchHABI_Descripcion    
        , @pdecHABI_PrecioDia     , @pdecHABI_PrecioHora          , @pdecHABI_PrecioMinimo , @pvchAUDI_UsuarioCreacion
        ,   GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_ConsultaRegistro]
( 
    @pbigEMPR_Interno   BIGINT
   ,@pbigSUCU_Interno   BIGINT
   ,@pbigHABI_Interno   BIGINT 
) 
AS
BEGIN

   SELECT HABI.EMPR_Interno         AS [EMPR_Interno]
        , HABI.SUCU_Interno         AS [SUCU_Interno]
        , HABI.HABI_Interno         AS [HABI_Interno]
        , HABI.TABL_TablaPIS        AS [TABL_TablaPIS]
        , HABI.TABL_InternoPIS       AS [TABL_InternoPIS]
        , HABI.TABL_TablaTHA        AS [TABL_TablaTHA]
        , HABI.TABL_InternoTHA       AS [TABL_InternoTHA]
        , HABI.HABI_Numero          AS [HABI_Numero]
        , HABI.HABI_Estado          AS [HABI_Estado]
        , HABI.HABI_Limpio          AS [HABI_Limpio]
        , HABI.HABI_Activo          AS [HABI_Activo]
        , HABI.HABI_Descripcion     AS [HABI_Descripcion]
        , HABI.HABI_PrecioDia       AS [HABI_PrecioDia]
        , HABI.HABI_PrecioHora      AS [HABI_PrecioHora]
        , HABI.HABI_PrecioMinimo    AS [HABI_PrecioMinimo]
     FROM POSAD_Habitaciones AS HABI
    WHERE 
   HABI.EMPR_Interno= @pbigEMPR_Interno AND 
   HABI.SUCU_Interno= @pbigSUCU_Interno AND 
   HABI.HABI_Interno= @pbigHABI_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_ConsultaLibres]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_ConsultaLibres]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_ConsultaLibres] 
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pchrTABL_TablaPIS     CHAR(3)
   ,@pchrTABL_InternoPIS   CHAR(4)
   ,@pchrTABL_TablaTHA     CHAR(3)
   ,@pchrTABL_InternoTHA   CHAR(4)
)
AS
BEGIN

   SELECT HABI.EMPR_Interno         AS [EMPR_Interno]
        , HABI.SUCU_Interno         AS [SUCU_Interno]
        , HABI.HABI_Interno         AS [HABI_Interno]
        , HABI.HABI_Numero          AS [HABI_Numero]
     FROM POSAD_Habitaciones AS HABI    
    WHERE 
    HABI.EMPR_Interno = @pbigEMPR_Interno AND
    HABI.SUCU_Interno = @pbigSUCU_Interno AND 
    HABI.TABL_TablaPIS = @pchrTABL_TablaPIS AND
    HABI.TABL_InternoPIS = ISNULL(@pchrTABL_InternoPIS,HABI.TABL_InternoPIS) AND
    HABI.TABL_TablaTHA = @pchrTABL_TablaTHA AND 
    HABI.TABL_InternoTHA = ISNULL(@pchrTABL_InternoTHA,HABI.TABL_InternoTHA) AND
    HABI.HABI_Estado = 'L' AND
    HABI.HABI_Activo = 1

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_HABI_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_HABI_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_HABI_ConsultaTodos] 
( 
    @pbigEMPR_Interno      BIGINT 
   ,@pbigSUCU_Interno      BIGINT 
   ,@pchrTABL_TablaPIS     CHAR(3)
   ,@pchrTABL_InternoPIS    CHAR(4)
   ,@pchrTABL_TablaTHA     CHAR(3)
   ,@pchrTABL_InternoTHA    CHAR(4)
)
AS
BEGIN

   SELECT HABI.EMPR_Interno         AS [EMPR_Interno]
        , HABI.SUCU_Interno         AS [SUCU_Interno]
        , HABI.HABI_Interno         AS [HABI_Interno]
        , HABI.TABL_TablaPIS        AS [TABL_TablaPIS]
        , HABI.TABL_InternoPIS       AS [TABL_InternoPIS]
        , TPIS.TABL_Nombre          AS [TABL_NombrePIS]
        , HABI.TABL_TablaTHA        AS [TABL_TablaTHA]
        , HABI.TABL_InternoTHA       AS [TABL_InternoTHA]
        , TTHA.TABL_Nombre          AS [TABL_NombreTHA]
        , HABI.HABI_Numero          AS [HABI_Numero]
        , HABI.HABI_Activo          AS [HABI_Activo]
     FROM POSAD_Habitaciones AS HABI    
    INNER JOIN POSAD_Tablas AS TPIS ON TPIS.EMPR_Interno = HABI.EMPR_Interno AND TPIS.TABL_Tabla = HABI.TABL_TablaPIS AND TPIS.TABL_Interno = HABI.TABL_InternoPIS
    INNER JOIN POSAD_Tablas AS TTHA ON TTHA.EMPR_Interno = HABI.EMPR_Interno AND TTHA.TABL_Tabla = HABI.TABL_TablaTHA AND TTHA.TABL_Interno = HABI.TABL_InternoTHA
    WHERE 
    HABI.EMPR_Interno = @pbigEMPR_Interno AND
    HABI.SUCU_Interno = @pbigSUCU_Interno AND 
    HABI.TABL_TablaPIS = @pchrTABL_TablaPIS AND
    HABI.TABL_InternoPIS = ISNULL(@pchrTABL_InternoPIS,HABI.TABL_InternoPIS) AND
    HABI.TABL_TablaTHA = @pchrTABL_TablaTHA AND 
    HABI.TABL_InternoTHA = ISNULL(@pchrTABL_InternoTHA,HABI.TABL_InternoTHA)
	ORDER BY HABI.TABL_TablaPIS, HABI.TABL_InternoPIS
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_FUNC_AsignarPorEntidad]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_FUNC_AsignarPorEntidad]
GO
CREATE PROCEDURE [dbo].[POSAD_FUNC_AsignarPorEntidad]
( 
    @pbigEMPR_Interno            BIGINT 
   ,@pbigENTI_Interno            BIGINT 
   ,@pchrTABL_TablaTEN           CHAR(3)
   ,@pchrTABL_InternoTEN          CHAR(4)
   ,@pbitFUNC_Primero            BIT
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN 
   
   IF (@pbitFUNC_Primero = 1)
   BEGIN
      DELETE FROM [POSAD_FuncionesEntidades]
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [ENTI_Interno] = @pbigENTI_Interno
   END 
      
   INSERT INTO [POSAD_FuncionesEntidades]
      (EMPR_Interno  ,TABL_TablaTEN          ,TABL_InternoTEN
      ,ENTI_Interno  ,AUDI_UsuarioCreacion   ,AUDI_FechaCreacion)
   VALUES
      (@pbigEMPR_Interno   ,@pchrTABL_TablaTEN        ,@pchrTABL_InternoTEN
      ,@pbigENTI_Interno   ,@pvchAUDI_UsuarioCreacion ,GETDATE())
END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_FUNC_ColsultaPorEntidad]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_FUNC_ColsultaPorEntidad]
GO
CREATE PROCEDURE [dbo].[POSAD_FUNC_ColsultaPorEntidad]
( 
    @pbigEMPR_Interno   BIGINT 
   ,@pbigENTI_Interno   BIGINT 
) 
AS 
BEGIN 
   
   SELECT 
    TTEN.TABL_Tabla                       AS [TABL_TablaTEN]
   ,TTEN.TABL_Interno                     AS [TABL_InternoTEN]
   ,TTEN.TABL_Nombre                      AS [FUNC_Nombre]
   ,TTEN.TABL_Descripcion                 AS [FUNC_Descripcion]
   ,CASE WHEN FUNC.TABL_TablaTEN IS NULL
         THEN CONVERT(BIT,0) 
         ELSE CONVERT(BIT,1)
         END                              AS [FUNC_Activo] 

   FROM POSAD_Tablas AS TTEN
   LEFT JOIN POSAD_FuncionesEntidades AS FUNC ON   FUNC.TABL_TablaTEN = TTEN.TABL_Tabla AND
                                                   FUNC.TABL_InternoTEN = TTEN.TABL_Interno AND
                                                   FUNC.EMPR_Interno = @pbigEMPR_Interno AND 
                                                   FUNC.ENTI_Interno = @pbigENTI_Interno
   WHERE 
   TTEN.TABL_Tabla = 'TEN' AND
   TTEN.TABL_Activo = 1

END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_Eliminar]
( 
    @pbigEMPR_Interno   BIGINT 
   ,@pbigENTI_Interno   BIGINT 
) 
AS 
BEGIN

   DELETE FROM [POSAD_FuncionesEntidades]
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [ENTI_Interno] = @pbigENTI_Interno

   DELETE FROM [POSAD_Entidades]
      WHERE 
      [EMPR_Interno] = @pbigEMPR_Interno AND 
      [ENTI_Interno] = @pbigENTI_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_Actualizar]
( 
    @pbigEMPR_Interno                                     BIGINT
   ,@pbigENTI_Interno                                     BIGINT
   ,@pchrTABL_TablaTDI                                    CHAR(3)
   ,@pchrTABL_InternoTDI                                   CHAR(4)
   ,@pvchENTI_Id                                          VARCHAR(50) 
   ,@pvchENTI_NombreCompleto                              VARCHAR(250)
   ,@pvchENTI_Direccion                                   VARCHAR(250)
   ,@pchrENTI_Sexo                                        CHAR(1) 
   ,@pintPAIS_Interno                                     INT
   ,@pintUBIG_Interno                                     INT
   ,@pvchAUDI_UsuarioModificacion                         VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Entidades]
      SET [TABL_TablaTDI]            = @pchrTABL_TablaTDI
        , [TABL_InternoTDI]           = @pchrTABL_InternoTDI
        , [ENTI_Id]                  = @pvchENTI_Id
        , [ENTI_NombreCompleto]      = @pvchENTI_NombreCompleto
        , [ENTI_Direccion]           = @pvchENTI_Direccion
        , [ENTI_Sexo]                = @pchrENTI_Sexo
        , [PAIS_Interno]             = @pintPAIS_Interno
        , [UBIG_Interno]             = @pintUBIG_Interno
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [ENTI_Interno]                 = @pbigENTI_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_Insertar]
( 
    @pbigEMPR_Interno                                     BIGINT OUTPUT
   ,@pbigENTI_Interno                                     BIGINT OUTPUT
   ,@pchrTABL_TablaTDI                                    CHAR(3)
   ,@pchrTABL_InternoTDI                                   CHAR(4)
   ,@pvchENTI_Id                                          VARCHAR(50) 
   ,@pvchENTI_NombreCompleto                              VARCHAR(250)
   ,@pvchENTI_Direccion                                   VARCHAR(250)
   ,@pchrENTI_Sexo                                        CHAR(1) 
   ,@pintPAIS_Interno                                     INT
   ,@pintUBIG_Interno                                     INT
   ,@pvchAUDI_UsuarioCreacion                             VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigENTI_Interno = (ISNULL(MAX(ENTI_Interno), 0)+1)
     FROM [POSAD_Entidades]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno

   INSERT INTO [POSAD_Entidades]
        ( EMPR_Interno                 , ENTI_Interno            , TABL_TablaTDI            , TABL_InternoTDI    
        , ENTI_Id                      , ENTI_NombreCompleto     , ENTI_Direccion           , ENTI_Sexo         
        , PAIS_Interno                 , UBIG_Interno            , AUDI_UsuarioCreacion     , AUDI_FechaCreacion)
   VALUES
        ( @pbigEMPR_Interno             , @pbigENTI_Interno        , @pchrTABL_TablaTDI        , @pchrTABL_InternoTDI
        , @pvchENTI_Id                  , @pvchENTI_NombreCompleto , @pvchENTI_Direccion       , @pchrENTI_Sexo     
        , @pintPAIS_Interno             , @pintUBIG_Interno        , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_ConsultaRegistro]
( 
    @pbigEMPR_Interno   BIGINT
   ,@pbigENTI_Interno   BIGINT 
) 
AS
BEGIN
                                                                     
   SELECT ENTI.EMPR_Interno            AS [EMPR_Interno]
        , ENTI.ENTI_Interno            AS [ENTI_Interno]
        , ENTI.TABL_TablaTDI           AS [TABL_TablaTDI]
        , ENTI.TABL_InternoTDI          AS [TABL_InternoTDI]
        , ENTI.ENTI_Id                 AS [ENTI_Id]
        , ENTI.ENTI_NombreCompleto     AS [ENTI_NombreCompleto]
        , ENTI.ENTI_Direccion          AS [ENTI_Direccion]
        , ENTI.ENTI_Sexo               AS [ENTI_Sexo]
        , ENTI.PAIS_Interno            AS [PAIS_Interno]
        , ENTI.UBIG_Interno            AS [UBIG_Interno]
        ,ISNULL(DEPA.UBIG_Interno,0)   AS [ENTI_Departamento]
        ,ISNULL(PROV.UBIG_Interno,0)   AS [ENTI_Provincia]
        ,ISNULL(DIST.UBIG_Interno,0)   AS [ENTI_Distrito]
   FROM POSAD_Entidades AS ENTI
   LEFT JOIN POSAD_Ubigeos AS DIST ON DIST.UBIG_Interno = ENTI.UBIG_Interno
   LEFT JOIN POSAD_Ubigeos AS PROV ON PROV.UBIG_Interno = DIST.UBIG_InternoPadre
   LEFT JOIN POSAD_Ubigeos AS DEPA ON DEPA.UBIG_Interno = PROV.UBIG_InternoPadre 
   WHERE 
   ENTI.EMPR_Interno= @pbigEMPR_Interno AND 
   ENTI.ENTI_Interno= @pbigENTI_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ENTI_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ENTI_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_ENTI_ConsultaTodos] 
(
    @pbigEMPR_Interno         BIGINT
   ,@pvchENTI_Id              VARCHAR(50) 
   ,@pvchENTI_NombreCompleto  VARCHAR(250)
)
AS
BEGIN

   SELECT ENTI.EMPR_Interno                                       AS [EMPR_Interno]
        , ENTI.ENTI_Interno                                       AS [ENTI_Interno]
        , ENTI.TABL_TablaTDI                                      AS [TABL_TablaTDI]
        , ENTI.TABL_InternoTDI                                    AS [TABL_InternoTDI]
        , TTDI.TABL_Nombre                                        AS [TABL_NombreTDI]
        , ENTI.ENTI_Id                                            AS [ENTI_Id]
        , ENTI.ENTI_NombreCompleto                                AS [ENTI_NombreCompleto]
   FROM POSAD_Entidades AS ENTI
   INNER JOIN POSAD_Tablas AS TTDI ON TTDI.TABL_Tabla = ENTI.TABL_TablaTDI AND TTDI.TABL_Interno = ENTI.TABL_InternoTDI
   WHERE 
   ENTI.ENTI_Id LIKE ISNULL(@pvchENTI_Id,'') + '%' AND 
   ENTI.ENTI_NombreCompleto LIKE ISNULL(@pvchENTI_NombreCompleto,'') + '%'
    
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CONF_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CONF_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_CONF_Actualizar]
( 
    @pbigEMPR_Interno               BIGINT 
   ,@pbigUSUA_Interno               BIGINT 
   ,@pchrCONF_Llave                 CHAR(5)
   ,@pvchCONF_Valor                 VARCHAR(50) 
   ,@pvchCONF_Descripcion           VARCHAR(250)
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Configuraciones]
      SET [CONF_Valor]               = @pvchCONF_Valor
        , [CONF_Descripcion]         = @pvchCONF_Descripcion
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [USUA_Interno]                 = @pbigUSUA_Interno
      AND [CONF_Llave]                   = @pchrCONF_Llave


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CONF_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CONF_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_CONF_Insertar]
( 
    @pbigEMPR_Interno            BIGINT OUTPUT
   ,@pbigUSUA_Interno            BIGINT OUTPUT 
   ,@pchrCONF_Llave              CHAR(5) OUTPUT
   ,@pvchCONF_Valor              VARCHAR(50) 
   ,@pvchCONF_Descripcion        VARCHAR(250)
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN

   INSERT INTO [POSAD_Configuraciones]
        ( EMPR_Interno        , USUA_Interno          , CONF_Llave            , CONF_Valor                  
        , CONF_Descripcion    , AUDI_UsuarioCreacion  , AUDI_FechaCreacion)
   VALUES
        ( @pbigEMPR_Interno      , @pbigUSUA_Interno           , @pchrCONF_Llave         , @pvchCONF_Valor              
        , @pvchCONF_Descripcion  , @pvchAUDI_UsuarioCreacion   , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CONF_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CONF_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_CONF_ConsultaRegistro]
( 
    @pbigEMPR_Interno      BIGINT
   ,@pbigUSUA_Interno      BIGINT
   ,@pchrCONF_Llave        CHAR(5) 
) 
AS
BEGIN

   SELECT CONF.EMPR_Interno                                       AS [EMPR_Interno]
        , CONF.USUA_Interno                                       AS [USUA_Interno]
        , CONF.CONF_Llave                                         AS [CONF_Llave]
        , CONF.CONF_Valor                                         AS [CONF_Valor]
        , CONF.CONF_Descripcion                                   AS [CONF_Descripcion]
   FROM POSAD_Configuraciones AS CONF
   WHERE 
   CONF.EMPR_Interno = @pbigEMPR_Interno AND 
   CONF.USUA_Interno = @pbigUSUA_Interno AND 
   CONF.CONF_Llave = @pchrCONF_Llave

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_CONF_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_CONF_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_CONF_ConsultaTodos]
( 
    @pbigEMPR_Interno      BIGINT
   ,@pbigUSUA_Interno      BIGINT
   ,@pchrCONF_Llave        CHAR(5) 
) 
AS
BEGIN

   SELECT CONF.EMPR_Interno                                       AS [EMPR_Interno]
        , CONF.USUA_Interno                                       AS [USUA_Interno]
        , CONF.CONF_Llave                                         AS [CONF_Llave]
        , CONF.CONF_Valor                                         AS [CONF_Valor]
        , CONF.CONF_Descripcion                                   AS [CONF_Descripcion]
   FROM POSAD_Configuraciones AS CONF
   WHERE 
   CONF.EMPR_Interno = @pbigEMPR_Interno AND 
   CONF.USUA_Interno = @pbigUSUA_Interno AND 
   CONF.CONF_Llave LIKE ISNULL(RTRIM(LTRIM(@pchrCONF_Llave)),'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PARA_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PARA_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_PARA_Actualizar]
( 
    @pbigEMPR_Interno               BIGINT 
   ,@pchrPARA_Llave                 CHAR(5)
   ,@pvchPARA_Valor                 VARCHAR(50) 
   ,@pvchPARA_Descripcion           VARCHAR(250)
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Parametros]
      SET [PARA_Valor]               = @pvchPARA_Valor
        , [PARA_Descripcion]         = @pvchPARA_Descripcion
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [PARA_Llave]                   = @pchrPARA_Llave


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PARA_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PARA_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_PARA_Insertar]
( 
    @pbigEMPR_Interno            BIGINT OUTPUT
   ,@pchrPARA_Llave              CHAR(5) OUTPUT
   ,@pvchPARA_Valor              VARCHAR(50) 
   ,@pvchPARA_Descripcion        VARCHAR(250)
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN


   INSERT INTO [POSAD_Parametros]
        ( EMPR_Interno             , PARA_Llave             , PARA_Valor                   , PARA_Descripcion    
        , AUDI_UsuarioCreacion     , AUDI_FechaCreacion     )
   VALUES
        ( @pbigEMPR_Interno         , @pchrPARA_Llave         , @pvchPARA_Valor               , @pvchPARA_Descripcion
        , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PARA_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PARA_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_PARA_ConsultaRegistro]
( 
    @pbigEMPR_Interno   BIGINT
   ,@pchrPARA_Llave     CHAR(5) 
) 
AS
BEGIN

   SELECT PARA.EMPR_Interno                                       AS [EMPR_Interno]
        , PARA.PARA_Llave                                         AS [PARA_Llave]
        , PARA.PARA_Valor                                         AS [PARA_Valor]
        , PARA.PARA_Descripcion                                   AS [PARA_Descripcion]
   FROM POSAD_Parametros AS PARA
   WHERE 
   PARA.EMPR_Interno= @pbigEMPR_Interno AND 
   PARA.PARA_Llave= @pchrPARA_Llave

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PARA_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PARA_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_PARA_ConsultaTodos] 
(
    @pbigEMPR_Interno   BIGINT 
   ,@pchrPARA_Llave     CHAR(5)
)
AS
BEGIN

   SELECT PARA.EMPR_Interno         AS [EMPR_Interno]
        , PARA.PARA_Llave           AS [PARA_Llave]
        , PARA.PARA_Valor           AS [PARA_Valor]
        , PARA.PARA_Descripcion     AS [PARA_Descripcion]
   FROM POSAD_Parametros AS PARA
   WHERE 
   PARA.EMPR_Interno = @pbigEMPR_Interno AND 
   PARA.PARA_Llave LIKE ISNULL(RTRIM(LTRIM(@pchrPARA_Llave)),'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_SUCU_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_SUCU_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_SUCU_Eliminar]
( 
    @pbigEMPR_Interno   BIGINT
   ,@pbigSUCU_Interno   BIGINT
) 
AS 
BEGIN

   DELETE FROM [POSAD_Sucursales]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_SUCU_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_SUCU_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_SUCU_Actualizar]
( 
    @pbigEMPR_Interno               BIGINT 
   ,@pbigSUCU_Interno               BIGINT 
   ,@pvchSUCU_Nombre                VARCHAR(250)
   ,@pvchSUCU_Direccion             VARCHAR(200)
   ,@pvchSUCU_Correo                VARCHAR(100)
   ,@pvchSUCU_Telefono              VARCHAR(100)
   ,@pvchSUCU_Web                   VARCHAR(100)
   ,@pbitSUCU_Principal             BIT 
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS 
BEGIN

   IF (@pbitSUCU_Principal = 1)
   BEGIN
      UPDATE [POSAD_Sucursales] SET [SUCU_Principal] = 0 WHERE [EMPR_Interno] = @pbigEMPR_Interno
   END

   UPDATE [POSAD_Sucursales]
      SET [SUCU_Nombre]              = @pvchSUCU_Nombre
        , [SUCU_Direccion]           = @pvchSUCU_Direccion
        , [SUCU_Correo]              = @pvchSUCU_Correo
        , [SUCU_Telefono]            = @pvchSUCU_Telefono
        , [SUCU_Web]                 = @pvchSUCU_Web
        , [SUCU_Principal]           = @pbitSUCU_Principal
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [SUCU_Interno]                 = @pbigSUCU_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_SUCU_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_SUCU_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_SUCU_Insertar]
( 
    @pbigEMPR_Interno               BIGINT OUTPUT
   ,@pbigSUCU_Interno               BIGINT OUTPUT
   ,@pvchSUCU_Nombre                VARCHAR(250)
   ,@pvchSUCU_Direccion             VARCHAR(200)
   ,@pvchSUCU_Correo                VARCHAR(100)
   ,@pvchSUCU_Telefono              VARCHAR(100)
   ,@pvchSUCU_Web                   VARCHAR(100)
   ,@pbitSUCU_Principal             BIT 
   ,@pvchAUDI_UsuarioCreacion       VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigSUCU_Interno = (ISNULL(MAX(SUCU_Interno), 0)+1)
     FROM [POSAD_Sucursales]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno

   IF (@pbitSUCU_Principal = 1)
   BEGIN
      UPDATE [POSAD_Sucursales] SET [SUCU_Principal] = 0 WHERE [EMPR_Interno] = @pbigEMPR_Interno
   END

   INSERT INTO [POSAD_Sucursales]
        ( EMPR_Interno             , SUCU_Interno           , SUCU_Nombre                  , SUCU_Direccion    
        , SUCU_Correo              , SUCU_Telefono          , SUCU_Web                     , SUCU_Principal    
        , AUDI_UsuarioCreacion     , AUDI_FechaCreacion     )
   VALUES
        ( @pbigEMPR_Interno         , @pbigSUCU_Interno       , @pvchSUCU_Nombre              , @pvchSUCU_Direccion
        , @pvchSUCU_Correo          , @pvchSUCU_Telefono      , @pvchSUCU_Web                 , @pbitSUCU_Principal
        , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_SUCU_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_SUCU_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_SUCU_ConsultaRegistro]
( 
    @pbigEMPR_Interno   BIGINT
   ,@pbigSUCU_Interno   BIGINT 
) 
AS
BEGIN

   SELECT SUCU.EMPR_Interno                                       AS [EMPR_Interno]
        , SUCU.SUCU_Interno                                       AS [SUCU_Interno]
        , SUCU.SUCU_Nombre                                        AS [SUCU_Nombre]
        , SUCU.SUCU_Direccion                                     AS [SUCU_Direccion]
        , SUCU.SUCU_Correo                                        AS [SUCU_Correo]
        , SUCU.SUCU_Telefono                                      AS [SUCU_Telefono]
        , SUCU.SUCU_Web                                           AS [SUCU_Web]
        , SUCU.SUCU_Principal                                     AS [SUCU_Principal]
   FROM POSAD_Sucursales AS SUCU
   WHERE 
   SUCU.EMPR_Interno= @pbigEMPR_Interno AND 
   SUCU.SUCU_Interno= @pbigSUCU_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_SUCU_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_SUCU_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_SUCU_ConsultaTodos] 
(
    @pbigEMPR_Interno   BIGINT
   ,@pvchSUCU_Nombre    VARCHAR(250)
)
AS
BEGIN

   SELECT SUCU.EMPR_Interno      AS [EMPR_Interno]
        , SUCU.SUCU_Interno      AS [SUCU_Interno]
        , SUCU.SUCU_Nombre       AS [SUCU_Nombre]
        , SUCU.SUCU_Direccion    AS [SUCU_Direccion]
        , SUCU.SUCU_Principal    AS [SUCU_Principal]
   FROM POSAD_Sucursales AS SUCU
   WHERE 
   SUCU.EMPR_Interno = @pbigEMPR_Interno AND 
   SUCU.SUCU_Nombre LIKE ISNULL(@pvchSUCU_Nombre,'') + '%'
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TABL_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TABL_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_TABL_Eliminar]
( 
	 @pbigEMPR_Interno		BIGINT 
	,@pchrTABL_Tabla        CHAR(3)
	,@pchrTABL_Interno       CHAR(4)
) 
AS 
BEGIN

   DELETE FROM [POSAD_Tablas]
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [TABL_Tabla]                   = @pchrTABL_Tabla
      AND [TABL_Interno]                  = @pchrTABL_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TABL_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TABL_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_TABL_Actualizar]
( 
	 @pbigEMPR_Interno					BIGINT 
	,@pchrTABL_Tabla					CHAR(3)
	,@pchrTABL_Interno					CHAR(4)
	,@pvchTABL_Nombre					VARCHAR(250)
	,@pvchTABL_Nomenclatura				VARCHAR(25) 
	,@pvchTABL_Descripcion				VARCHAR(250)
	,@pvchTABL_CodigoInternacional		VARCHAR(50) 
	,@pvchTABL_Codigo1					VARCHAR(50) 
	,@pvchTABL_Codigo2					VARCHAR(50) 
	,@pvchTABL_Codigo3					VARCHAR(50) 
	,@pintTABL_Numero1					INT 
	,@pdecTABL_Numero2					DECIMAL(20,2)
	,@pdecTABL_Numero3					DECIMAL(20,8)
	,@pbitTABL_Activo					BIT 
	,@pvchAUDI_UsuarioModificacion		VARCHAR(25) 
)
AS 
BEGIN

   UPDATE [POSAD_Tablas]
      SET [TABL_Nombre]              = @pvchTABL_Nombre
        , [TABL_Nomenclatura]        = @pvchTABL_Nomenclatura
        , [TABL_Descripcion]         = @pvchTABL_Descripcion
        , [TABL_CodigoInternacional] = @pvchTABL_CodigoInternacional
        , [TABL_Codigo1]             = @pvchTABL_Codigo1
        , [TABL_Codigo2]             = @pvchTABL_Codigo2
        , [TABL_Codigo3]             = @pvchTABL_Codigo3
        , [TABL_Numero1]             = @pintTABL_Numero1
        , [TABL_Numero2]             = @pdecTABL_Numero2
        , [TABL_Numero3]             = @pdecTABL_Numero3
        , [TABL_Activo]              = @pbitTABL_Activo
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE() 
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno
      AND [TABL_Tabla]                   = RTRIM(LTRIM(@pchrTABL_Tabla))
      AND [TABL_Interno]                 = RTRIM(LTRIM(@pchrTABL_Interno))


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TABL_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TABL_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_TABL_Insertar]
( 
	 @pbigEMPR_Interno					BIGINT OUTPUT
	,@pchrTABL_Tabla					CHAR(3) OUTPUT
	,@pchrTABL_Interno					CHAR(4) OUTPUT
	,@pvchTABL_Nombre					VARCHAR(250)
	,@pvchTABL_Nomenclatura				VARCHAR(25) 
	,@pvchTABL_Descripcion				VARCHAR(250)
	,@pvchTABL_CodigoInternacional		VARCHAR(50) 
	,@pvchTABL_Codigo1					VARCHAR(50) 
	,@pvchTABL_Codigo2					VARCHAR(50) 
	,@pvchTABL_Codigo3					VARCHAR(50) 
	,@pintTABL_Numero1					INT 
	,@pdecTABL_Numero2					DECIMAL(20,2)
	,@pdecTABL_Numero3					DECIMAL(20,8)
	,@pbitTABL_Activo					BIT 
	,@pvchAUDI_UsuarioCreacion			VARCHAR(25) 
)
AS 
BEGIN

   DECLARE @pintCodigo INT 
   SET @pintCodigo = ISNULL((SELECT MAX(CONVERT(INT,TABL_Interno)) FROM dbo.POSAD_Tablas WHERE EMPR_Interno = @pbigEMPR_Interno AND TABL_Tabla = @pchrTABL_Tabla),0) + 1
   SET @pchrTABL_Interno = RIGHT('0000' + CONVERT(VARCHAR(4),@pintCodigo),4)

   INSERT INTO [POSAD_Tablas]
        ( EMPR_Interno                 , TABL_Tabla           , TABL_Interno                 , TABL_Nombre           
        , TABL_Nomenclatura            , TABL_Descripcion     , TABL_CodigoInternacional     , TABL_Codigo1          
        , TABL_Codigo2                 , TABL_Codigo3         , TABL_Numero1                 , TABL_Numero2          
        , TABL_Numero3                 , TABL_Activo          , AUDI_UsuarioCreacion         , AUDI_FechaCreacion   )
   VALUES
        ( @pbigEMPR_Interno             , RTRIM(LTRIM(@pchrTABL_Tabla))       , RTRIM(LTRIM(@pchrTABL_Interno))   , @pvchTABL_Nombre       
        , @pvchTABL_Nomenclatura        , @pvchTABL_Descripcion               , @pvchTABL_CodigoInternacional     , @pvchTABL_Codigo1      
        , @pvchTABL_Codigo2             , @pvchTABL_Codigo3                   , @pintTABL_Numero1                 , @pdecTABL_Numero2      
        , @pdecTABL_Numero3             , @pbitTABL_Activo                    , @pvchAUDI_UsuarioCreacion         , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TABL_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TABL_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_TABL_ConsultaRegistro]
( 
	 @pbigEMPR_Interno		BIGINT
	,@pchrTABL_Tabla		CHAR(3)
	,@pchrTABL_Interno		CHAR(4) 
) 
AS
BEGIN

   SELECT TABL.EMPR_Interno                                       AS [EMPR_Interno]
        , RTRIM(LTRIM(TABL.TABL_Tabla))                           AS [TABL_Tabla]
        , RTRIM(LTRIM(TABL.TABL_Interno))                         AS [TABL_Interno]
        , TABL.TABL_Nombre                                        AS [TABL_Nombre]
        , TABL.TABL_Nomenclatura                                  AS [TABL_Nomenclatura]
        , TABL.TABL_Descripcion                                   AS [TABL_Descripcion]
        , TABL.TABL_CodigoInternacional                           AS [TABL_CodigoInternacional]
        , TABL.TABL_Codigo1                                       AS [TABL_Codigo1]
        , TABL.TABL_Codigo2                                       AS [TABL_Codigo2]
        , TABL.TABL_Codigo3                                       AS [TABL_Codigo3]
        , TABL.TABL_Numero1                                       AS [TABL_Numero1]
        , TABL.TABL_Numero2                                       AS [TABL_Numero2]
        , TABL.TABL_Numero3                                       AS [TABL_Numero3]
        , TABL.TABL_Activo                                        AS [TABL_Activo]
	FROM POSAD_Tablas AS TABL
	WHERE 
	TABL.EMPR_Interno	= @pbigEMPR_Interno AND
	TABL.TABL_Tabla	= @pchrTABL_Tabla AND
	TABL.TABL_Interno	= @pchrTABL_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_TABL_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_TABL_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_TABL_ConsultaTodos] 
(
	 @pbigEMPR_Interno	BIGINT
	,@pchrTABL_Tabla	   CHAR(3)
	,@pbitTABL_Activo	   BIT 
)
AS
BEGIN

   SELECT TABL.EMPR_Interno                                       AS [EMPR_Interno]
        , RTRIM(LTRIM(TABL.TABL_Tabla))                           AS [TABL_Tabla]
        , RTRIM(LTRIM(TABL.TABL_Interno))                         AS [TABL_Interno]
        , TABL.TABL_Nombre                                        AS [TABL_Nombre]
        , TABL.TABL_Activo                                        AS [TABL_Activo]
	FROM POSAD_Tablas AS TABL
	WHERE
	TABL.EMPR_Interno = @pbigEMPR_Interno AND 
	TABL.TABL_Tabla = @pchrTABL_Tabla AND
	TABL.TABL_Activo = ISNULL(@pbitTABL_Activo,TABL.TABL_Activo)

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_AsignarPorUsuario]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_AsignarPorUsuario]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_AsignarPorUsuario]
( 
    @pintUSUA_Interno         BIGINT
   ,@pintEMPR_Interno         BIGINT 
   ,@pbitEMPR_Primero         BIT
   ,@pvchAUDI_UsuarioCreacion VARCHAR(25) 
) 
AS 
BEGIN
   
   IF(@pbitEMPR_Primero = 1)
   BEGIN
      DELETE FROM POSAD_EmpresasUsuarios WHERE USUA_Interno = @pintUSUA_Interno
   END

   INSERT INTO POSAD_EmpresasUsuarios 
   (   EMPR_Interno           ,USUA_Interno        
      ,AUDI_UsuarioCreacion   ,AUDI_FechaCreacion  )
   VALUES
   (   @pintEMPR_Interno         ,@pintUSUA_Interno   
      ,@pvchAUDI_UsuarioCreacion ,GETDATE()           )

END
GO
   

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_ConsultaPorUsuarios]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_ConsultaPorUsuarios]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_ConsultaPorUsuarios]
( 
    @pintUSUA_Interno      BIGINT 
) 
AS 
BEGIN

   SELECT 
       @pintUSUA_Interno                  AS [ROLE_Interno]
      ,EMPR.EMPR_Interno                  AS [EMPR_Interno]
      ,EMPR.EMPR_Id                       AS [EMPR_Id]
      ,EMPR.EMPR_RazonSocial              AS [EMPR_RazonSocial]
      ,EMPR.EMPR_NombreComercial          AS [EMPR_NombreComercial]
      ,CASE WHEN EMUS.EMPR_Interno IS NULL
         THEN CONVERT(BIT,0) 
         ELSE CONVERT(BIT,1)
         END                              AS [EMPR_Activo] 
   FROM POSAD_Empresas AS EMPR
   LEFT JOIN POSAD_EmpresasUsuarios AS EMUS ON EMUS.EMPR_Interno = EMPR.EMPR_Interno AND EMUS.USUA_Interno = @pintUSUA_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_Insertar]
( 
    @pbigUSUA_Interno                  BIGINT OUTPUT
   ,@pintROLE_Interno                  INT 
   ,@pvchUSUA_NombreCompleto           VARCHAR(250) 
   ,@pvchUSUA_Usuario                  VARCHAR(25) 
   ,@pvchUSUA_Contrasena               VARCHAR(MAX) 
   ,@pvchUSUA_Correo                   VARCHAR(150)
   ,@pvchAUDI_UsuarioCreacion          VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigUSUA_Interno = (ISNULL(MAX(USUA_Interno), 0)+1)
     FROM [POSAD_Usuarios]

   INSERT INTO [POSAD_Usuarios]
        ( USUA_Interno                 , ROLE_Interno     , USUA_NombreCompleto     , USUA_Usuario          
        , USUA_Contrasena              , USUA_Correo      , AUDI_UsuarioCreacion    , AUDI_FechaCreacion)
   VALUES
        ( @pbigUSUA_Interno             , @pintROLE_Interno , @pvchUSUA_NombreCompleto    , @pvchUSUA_Usuario      
        , @pvchUSUA_Contrasena          , @pvchUSUA_Correo  , @pvchAUDI_UsuarioCreacion   , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_Actualizar]
( 
    @pbigUSUA_Interno                  BIGINT
   ,@pintROLE_Interno                  INT 
   ,@pvchUSUA_NombreCompleto           VARCHAR(250) 
   ,@pvchUSUA_Usuario                  VARCHAR(25) 
   ,@pvchUSUA_Contrasena               VARCHAR(MAX) 
   ,@pvchUSUA_Correo                   VARCHAR(150)
   ,@pvchAUDI_UsuarioModificacion      VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Usuarios]
      SET [ROLE_Interno]             = @pintROLE_Interno
        , [USUA_NombreCompleto]      = @pvchUSUA_NombreCompleto
        , [USUA_Usuario]             = @pvchUSUA_Usuario
        , [USUA_Contrasena]          = @pvchUSUA_Contrasena
        , [USUA_Correo]              = @pvchUSUA_Correo
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE()
    WHERE [USUA_Interno]                 = @pbigUSUA_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_Eliminar]
( 
    @pbigUSUA_Interno    BIGINT 
) 
AS 
BEGIN

   DELETE FROM [POSAD_EmpresasUsuarios]
   WHERE [USUA_Interno]                 = @pbigUSUA_Interno

   DELETE FROM [POSAD_Usuarios]
   WHERE [USUA_Interno]                 = @pbigUSUA_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_ConsultaRegistro]
( 
    @pbigUSUA_Interno   BIGINT 
) 
AS
BEGIN

   SELECT USUA.USUA_Interno                                       AS [USUA_Interno]
        , USUA.ROLE_Interno                                       AS [ROLE_Interno]
        , USUA.USUA_NombreCompleto                                AS [USUA_NombreCompleto]
        , USUA.USUA_Usuario                                       AS [USUA_Usuario]
        , USUA.USUA_Contrasena                                    AS [USUA_Contrasena]
        , USUA.USUA_Correo                                        AS [USUA_Correo]
   FROM POSAD_Usuarios AS USUA
   WHERE 
   USUA.USUA_Interno = @pbigUSUA_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_ConsultaPorEmpresa]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_ConsultaPorEmpresa]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_ConsultaPorEmpresa] 
(
    @pintEMPR_Interno   BIGINT 
)
AS
BEGIN

   SELECT USUA.USUA_Interno                                       AS [USUA_Interno]
        , USUA.ROLE_Interno                                       AS [ROLE_Interno]
        , USUA.USUA_Usuario                                       AS [USUA_Usuario]
        , USUA.USUA_NombreCompleto                                AS [USUA_NombreCompleto]
   FROM POSAD_Usuarios AS USUA
   INNER JOIN POSAD_EmpresasUsuarios AS EMUS ON EMUS.USUA_Interno = USUA.USUA_Interno
   WHERE 
   EMUS.EMPR_Interno = @pintEMPR_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_ConsultaTodos] 
(
    @pvchUSUA_Usuario   VARCHAR(25) 
)
AS
BEGIN

   SELECT USUA.USUA_Interno                                       AS [USUA_Interno]
        , USUA.ROLE_Interno                                       AS [ROLE_Interno]
        , USUA.USUA_Usuario                                       AS [USUA_Usuario]
        , USUA.USUA_NombreCompleto                                AS [USUA_NombreCompleto]
        , USUA.USUA_Correo                                        AS [USUA_Correo]
        , ROLES.ROLE_Nombre                                       AS [USUA_RolNombre]
   FROM POSAD_Usuarios AS USUA
   INNER JOIN POSAD_Roles AS ROLES ON ROLES.ROLE_Interno = USUA.ROLE_Interno
   WHERE 
   USUA.USUA_Usuario LIKE ISNULL(@pvchUSUA_Usuario,'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_Actualizar]
( 
	 @pbigEMPR_Interno				   BIGINT 
	,@pintPAIS_Interno				   INT 
	,@pvchEMPR_Id					      VARCHAR(50) 
	,@pvchEMPR_RazonSocial			   VARCHAR(250)
	,@pvchEMPR_Direccion			      VARCHAR(200)
	,@pvchEMPR_NombreComercial		   VARCHAR(250)
	,@pintUBIG_Interno				   INT 
	,@pvchEMPR_Correos				   VARCHAR(100)
	,@pvchEMPR_Telefonos			      VARCHAR(100)
	,@pvchEMPR_Web					      VARCHAR(100)
	,@pvchAUDI_UsuarioModificacion	VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Empresas]
      SET [PAIS_Interno]             = @pintPAIS_Interno
        , [EMPR_Id]                  = @pvchEMPR_Id
        , [EMPR_RazonSocial]         = @pvchEMPR_RazonSocial
        , [EMPR_Direccion]           = @pvchEMPR_Direccion
        , [EMPR_NombreComercial]     = @pvchEMPR_NombreComercial
        , [UBIG_Interno]             = @pintUBIG_Interno
        , [EMPR_Correos]             = @pvchEMPR_Correos
        , [EMPR_Telefonos]           = @pvchEMPR_Telefonos
        , [EMPR_Web]                 = @pvchEMPR_Web
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE()
    WHERE [EMPR_Interno]                 = @pbigEMPR_Interno


END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_Insertar]
( 
	 @pbigEMPR_Interno				BIGINT OUTPUT
	,@pintPAIS_Interno				INT 
	,@pvchEMPR_Id					   VARCHAR(50) 
	,@pvchEMPR_RazonSocial			VARCHAR(250)
	,@pvchEMPR_Direccion			   VARCHAR(200)
	,@pvchEMPR_NombreComercial		VARCHAR(250)
	,@pintUBIG_Interno				INT 
	,@pvchEMPR_Correos				VARCHAR(100)
	,@pvchEMPR_Telefonos			   VARCHAR(100)
	,@pvchEMPR_Web					   VARCHAR(100)
	,@pvchAUDI_UsuarioCreacion		VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pbigEMPR_Interno = (ISNULL(MAX(EMPR_Interno), 0)+1)
     FROM [POSAD_Empresas]

   INSERT INTO [POSAD_Empresas]
        ( EMPR_Interno                 , PAIS_Interno				, EMPR_Id                  , EMPR_RazonSocial    
        , EMPR_Direccion               , EMPR_NombreComercial		, UBIG_Interno             , EMPR_Correos        
        , EMPR_Telefonos               , EMPR_Web					, AUDI_UsuarioCreacion     , AUDI_FechaCreacion  )
   VALUES
        ( @pbigEMPR_Interno             , @pintPAIS_Interno         , @pvchEMPR_Id              , @pvchEMPR_RazonSocial
        , @pvchEMPR_Direccion           , @pvchEMPR_NombreComercial , @pintUBIG_Interno         , @pvchEMPR_Correos    
        , @pvchEMPR_Telefonos           , @pvchEMPR_Web             , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_ConsultaRegistro]
( 
	@pbigEMPR_Interno	BIGINT 
) 
AS
BEGIN

	SELECT 
		 EMPR.EMPR_Interno               AS [EMPR_Interno]
        ,EMPR.PAIS_Interno             AS [PAIS_Interno]
        ,EMPR.EMPR_Id                  AS [EMPR_Id]
        ,EMPR.EMPR_RazonSocial         AS [EMPR_RazonSocial]
        ,EMPR.EMPR_Direccion           AS [EMPR_Direccion]
        ,EMPR.EMPR_NombreComercial     AS [EMPR_NombreComercial]
        ,EMPR.UBIG_Interno             AS [UBIG_Interno]
        ,EMPR.EMPR_Correos             AS [EMPR_Correos]
        ,EMPR.EMPR_Telefonos           AS [EMPR_Telefonos]
        ,EMPR.EMPR_Web                 AS [EMPR_Web]
        ,ISNULL(DEPA.UBIG_Interno,0)   AS [EMPR_Departamento]
        ,ISNULL(PROV.UBIG_Interno,0)   AS [EMPR_Provincia]
        ,ISNULL(DIST.UBIG_Interno,0)   AS [EMPR_Distrito]
	FROM POSAD_Empresas AS EMPR
   LEFT JOIN POSAD_Ubigeos AS DIST ON DIST.UBIG_Interno = EMPR.UBIG_Interno
   LEFT JOIN POSAD_Ubigeos AS PROV ON PROV.UBIG_Interno = DIST.UBIG_InternoPadre
   LEFT JOIN POSAD_Ubigeos AS DEPA ON DEPA.UBIG_Interno = PROV.UBIG_InternoPadre 
	WHERE 
	EMPR.EMPR_Interno = @pbigEMPR_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_EMPR_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_EMPR_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_EMPR_ConsultaTodos] 
(
	 @pvchEMPR_Id			   VARCHAR(50) 
	,@pvchEMPR_RazonSocial  VARCHAR(250)
)
AS
BEGIN

	SELECT	
	 EMPR.EMPR_Interno          AS [EMPR_Interno]
	,EMPR.PAIS_Interno          AS [PAIS_Interno]
	,EMPR.EMPR_Id               AS [EMPR_Id]
	,EMPR.EMPR_RazonSocial      AS [EMPR_RazonSocial]
	,EMPR.EMPR_Direccion        AS [EMPR_Direccion]
	,PAIS.PAIS_Nombre			AS [EMPR_PaisNombre]
	FROM POSAD_Empresas AS EMPR
	INNER JOIN dbo.POSAD_Paises AS PAIS ON PAIS.PAIS_Interno = EMPR.PAIS_Interno
	WHERE 
	EMPR.EMPR_Id LIKE ISNULL(@pvchEMPR_Id,'') + '%' AND
	EMPR.EMPR_RazonSocial LIKE ISNULL(@pvchEMPR_RazonSocial,'') + '%'
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_OPCI_AsignarPorRoles]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_OPCI_AsignarPorRoles]
GO
CREATE PROCEDURE [dbo].[POSAD_OPCI_AsignarPorRoles]
( 
    @pintROLE_Interno         INT
   ,@pintOPCI_Interno         INT 
   ,@pbitOPCI_Primero         BIT
   ,@pvchAUDI_UsuarioCreacion VARCHAR(25) 
) 
AS 
BEGIN
   
   IF(@pbitOPCI_Primero = 1)
   BEGIN
      DELETE FROM POSAD_RolesOpciones WHERE ROLE_Interno = @pintROLE_Interno
   END

   INSERT INTO POSAD_RolesOpciones 
   (   ROLE_Interno           ,OPCI_Interno        ,ROPC_Activo
      ,AUDI_UsuarioCreacion   ,AUDI_FechaCreacion  )
   VALUES
   (   @pintROLE_Interno         ,@pintOPCI_Interno   ,1
      ,@pvchAUDI_UsuarioCreacion ,GETDATE()           )

END
GO
   

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_OPCI_ConsultaPorRoles]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_OPCI_ConsultaPorRoles]
GO
CREATE PROCEDURE [dbo].[POSAD_OPCI_ConsultaPorRoles]
( 
    @pintROLE_Interno      INT 
) 
AS 
BEGIN
   SELECT 
       @pintROLE_Interno                        AS [ROLE_Interno]
      ,OPCI.OPCI_Interno                        AS [OPCI_Interno]
      ,OPCI.OPCI_Nombre                         AS [OPCI_Nombre]
      ,OPCI.OPCI_Nomenclatura                   AS [OPCI_Nomenclatura]
      ,OPCI.OPCI_Descripcion                    AS [OPCI_Descripcion]
      ,OPCI.OPCI_InternoPadre                   AS [OPCI_InternoPadre]
      ,OPCI1.OPCI_Nombre                        AS [OPCI_NombrePadre]
      ,CONVERT(BIT,ISNULL(ROPC.ROPC_Activo,0))  AS [OPCI_Activo]
   FROM POSAD_Opciones AS OPCI
   LEFT JOIN POSAD_Opciones AS OPCI1 ON OPCI1.OPCI_Interno = OPCI.OPCI_InternoPadre
   LEFT JOIN POSAD_RolesOpciones AS ROPC ON ROPC.ROLE_Interno = @pintROLE_Interno AND ROPC.OPCI_Interno = OPCI.OPCI_Interno
   ORDER BY OPCI.OPCI_Interno
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ROLE_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ROLE_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_ROLE_Eliminar]
( 
    @pintROLE_Interno      INT 
) 
AS 
BEGIN

   DELETE FROM [POSAD_RolesOpciones] 
   WHERE [ROLE_Interno] = @pintROLE_Interno

   DELETE FROM [POSAD_Roles]
   WHERE [ROLE_Interno] = @pintROLE_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ROLE_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ROLE_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_ROLE_Actualizar]
( 
    @pintROLE_Interno               INT 
   ,@pvchROLE_Nombre                VARCHAR(25) 
   ,@pvchROLE_Descripcion           VARCHAR(250)
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Roles]
      SET [ROLE_Nombre]              = @pvchROLE_Nombre
        , [ROLE_Descripcion]         = @pvchROLE_Descripcion
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE()
    WHERE 
    [ROLE_Interno]  = @pintROLE_Interno


END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ROLE_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ROLE_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_ROLE_Insertar]
( 
    @pintROLE_Interno            INT OUTPUT
   ,@pvchROLE_Nombre             VARCHAR(25) 
   ,@pvchROLE_Descripcion        VARCHAR(250)
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pintROLE_Interno = (ISNULL(MAX(ROLE_Interno), 0)+1)
     FROM [POSAD_Roles]

   INSERT INTO [POSAD_Roles]
        ( ROLE_Interno           , ROLE_Nombre        , ROLE_Descripcion     
        , AUDI_UsuarioCreacion   , AUDI_FechaCreacion )
   VALUES
        ( @pintROLE_Interno         , @pvchROLE_Nombre   , @pvchROLE_Descripcion 
        , @pvchAUDI_UsuarioCreacion , GETDATE()          )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ROLE_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ROLE_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_ROLE_ConsultaRegistro]
( 
    @pintROLE_Interno      INT 
) 
AS
BEGIN

   SELECT ROLES.ROLE_Interno                                       AS [ROLE_Interno]
        , ROLES.ROLE_Nombre                                        AS [ROLE_Nombre]
        , ROLES.ROLE_Descripcion                                   AS [ROLE_Descripcion]
   FROM POSAD_Roles AS ROLES
   WHERE 
   ROLES.ROLE_Interno = @pintROLE_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_ROLE_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_ROLE_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_ROLE_ConsultaTodos] 
( 
    @pvchROLE_Nombre             VARCHAR(25) 
) 
AS
BEGIN

   SELECT ROLES.ROLE_Interno       AS [ROLE_Interno]
        , ROLES.ROLE_Nombre        AS [ROLE_Nombre]
        , ROLES.ROLE_Descripcion   AS [ROLE_Descripcion]
   FROM POSAD_Roles AS ROLES
   WHERE 
   ROLES.ROLE_Nombre LIKE ISNULL(@pvchROLE_Nombre,'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_ConsultaActivos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_ConsultaActivos]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_ConsultaActivos] 
AS
BEGIN

   SELECT PAIS.PAIS_Interno         AS [PAIS_Interno]
        , PAIS.PAIS_Nombre          AS [PAIS_Nombre]
        , PAIS.PAIS_CodigoAlfa3     AS [PAIS_CodigoAlfa3]
   FROM POSAD_Paises AS PAIS
   WHERE PAIS.PAIS_Activo = 1
   ORDER BY PAIS.PAIS_Nombre, PAIS.PAIS_CodigoAlfa3

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_PorNiveles]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_PorNiveles]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_PorNiveles] 
(
    @pintUBIG_InternoPadre    INT
   ,@pintPAIS_Interno         INT
)
AS
BEGIN

   SELECT UBIG.UBIG_Interno         AS [UBIG_Interno]
        , UBIG.PAIS_Interno         AS [PAIS_Interno]
        , UBIG.UBIG_Nombre          AS [UBIG_Nombre]
   FROM POSAD_Ubigeos AS UBIG
   WHERE 
   UBIG.UBIG_Activo = 1 AND
   ISNULL(UBIG.UBIG_InternoPadre,'') = ISNULL(@pintUBIG_InternoPadre,'') AND
   UBIG.PAIS_Interno = @pintPAIS_Interno
   ORDER BY UBIG.UBIG_Nombre

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_ConsultaPadresActivos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_ConsultaPadresActivos]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_ConsultaPadresActivos] 
AS
BEGIN

   SELECT UBIG.UBIG_Interno         AS [UBIG_Interno]
        , UBIG.PAIS_Interno         AS [PAIS_Interno]
        , UBIG.UBIG_Nombre          AS [UBIG_Nombre]
        , CASE LEN(ISNULL(UBIG.UBIG_Codigo1,'')) 
            WHEN 2 THEN 'DEPARTAMENTO'
            WHEN 5 THEN 'PROVINCIA'
            WHEN 8 THEN 'DISTRITO'
            ELSE 'INDEFINIDO' END   AS [UBIG_Tipo]
   FROM POSAD_Ubigeos AS UBIG
   WHERE 
   UBIG.UBIG_Activo = 1 AND
   LEN(ISNULL(UBIG.UBIG_Codigo1,'')) <= 5
   ORDER BY UBIG.UBIG_Nombre

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_Eliminar]
( 
    @pintUBIG_Interno   INT  
) 
AS 
BEGIN

   DELETE FROM [POSAD_Ubigeos]
    WHERE [UBIG_Interno]                 = @pintUBIG_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_Actualizar]
( 
    @pintUBIG_Interno               INT OUTPUT
   ,@pintPAIS_Interno               INT 
   ,@pvchUBIG_Nombre                VARCHAR(100) 
   ,@pvchUBIG_Nomenclatura          VARCHAR(25) 
   ,@pvchUBIG_Descripcion           VARCHAR(250)
   ,@pvchUBIG_Codigo1               VARCHAR(50) 
   ,@pvchUBIG_Codigo2               VARCHAR(50) 
   ,@pvchUBIG_Codigo3               VARCHAR(50) 
   ,@pbitUBIG_Activo                BIT 
   ,@pintUBIG_InternoPadre          INT 
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25)
) 
AS 
BEGIN

   UPDATE [POSAD_Ubigeos]
      SET [PAIS_Interno]             = @pintPAIS_Interno
        , [UBIG_Nombre]              = @pvchUBIG_Nombre
        , [UBIG_Nomenclatura]        = @pvchUBIG_Nomenclatura
        , [UBIG_Descripcion]         = @pvchUBIG_Descripcion
        , [UBIG_Codigo1]             = @pvchUBIG_Codigo1
        , [UBIG_Codigo2]             = @pvchUBIG_Codigo2
        , [UBIG_Codigo3]             = @pvchUBIG_Codigo3
        , [UBIG_Activo]              = @pbitUBIG_Activo
        , [UBIG_InternoPadre]        = @pintUBIG_InternoPadre
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE()
    WHERE [UBIG_Interno] = @pintUBIG_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_Insertar]
( 
    @pintUBIG_Interno            INT OUTPUT
   ,@pintPAIS_Interno            INT 
   ,@pvchUBIG_Nombre             VARCHAR(100) 
   ,@pvchUBIG_Nomenclatura       VARCHAR(25) 
   ,@pvchUBIG_Descripcion        VARCHAR(250)
   ,@pvchUBIG_Codigo1            VARCHAR(50) 
   ,@pvchUBIG_Codigo2            VARCHAR(50) 
   ,@pvchUBIG_Codigo3            VARCHAR(50) 
   ,@pbitUBIG_Activo             BIT 
   ,@pintUBIG_InternoPadre       INT 
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25)
) 
AS 
BEGIN

   SELECT @pintUBIG_Interno = (ISNULL(MAX(UBIG_Interno), 0)+1)
     FROM [POSAD_Ubigeos]

   INSERT INTO [POSAD_Ubigeos]
        ( UBIG_Interno                 , PAIS_Interno          , UBIG_Nombre              , UBIG_Nomenclatura    
        , UBIG_Descripcion             , UBIG_Codigo1          , UBIG_Codigo2             , UBIG_Codigo3         
        , UBIG_Activo                  , UBIG_InternoPadre     , AUDI_UsuarioCreacion     , AUDI_FechaCreacion )
   VALUES
        ( @pintUBIG_Interno             , @pintPAIS_Interno      , @pvchUBIG_Nombre          , @pvchUBIG_Nomenclatura
        , @pvchUBIG_Descripcion         , @pvchUBIG_Codigo1      , @pvchUBIG_Codigo2         , @pvchUBIG_Codigo3     
        , @pbitUBIG_Activo              , @pintUBIG_InternoPadre , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_ConsultaRegistro]
( 
    @pintUBIG_Interno   INT 
) 
AS
BEGIN

   SELECT UBIG.UBIG_Interno                                       AS [UBIG_Interno]
        , UBIG.PAIS_Interno                                       AS [PAIS_Interno]
        , UBIG.UBIG_Nombre                                        AS [UBIG_Nombre]
        , UBIG.UBIG_Nomenclatura                                  AS [UBIG_Nomenclatura]
        , UBIG.UBIG_Descripcion                                   AS [UBIG_Descripcion]
        , UBIG.UBIG_Codigo1                                       AS [UBIG_Codigo1]
        , UBIG.UBIG_Codigo2                                       AS [UBIG_Codigo2]
        , UBIG.UBIG_Codigo3                                       AS [UBIG_Codigo3]
        , UBIG.UBIG_Activo                                        AS [UBIG_Activo]
        , UBIG.UBIG_InternoPadre                                  AS [UBIG_InternoPadre]
   FROM POSAD_Ubigeos AS UBIG
   WHERE 
   UBIG.UBIG_Interno = @pintUBIG_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_UBIG_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_UBIG_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_UBIG_ConsultaTodos] 
( 
    @pvchUBIG_Nombre             VARCHAR(100) 
) 
AS
BEGIN

   SELECT UBIG.UBIG_Interno         AS [UBIG_Interno]
        , UBIG.PAIS_Interno         AS [PAIS_Interno]
        , UBIG.UBIG_Nombre          AS [UBIG_Nombre]
        , PAIS.PAIS_Nombre          AS [UBIG_NombrePais]
        , UBIG.UBIG_Nomenclatura    AS [UBIG_Nomenclatura]
        , UBIG.UBIG_Descripcion     AS [UBIG_Descripcion]
        , UBIG.UBIG_Codigo1         AS [UBIG_Codigo1]
        , UBIG.UBIG_Codigo2         AS [UBIG_Codigo2]
        , UBIG.UBIG_Codigo3         AS [UBIG_Codigo3]
        , UBIG.UBIG_Activo          AS [UBIG_Activo]
        , UBIG.UBIG_InternoPadre    AS [UBIG_InternoPadre]
        , UBIGP.UBIG_Nombre         AS [UBIG_NombrePadre]
   FROM POSAD_Ubigeos AS UBIG
   INNER JOIN POSAD_Paises AS PAIS ON PAIS.PAIS_Interno = UBIG.PAIS_Interno
   LEFT  JOIN POSAD_Ubigeos AS UBIGP ON UBIGP.UBIG_Interno = UBIG.UBIG_InternoPadre
   WHERE 
   UBIG.UBIG_Nombre LIKE ISNULL(@pvchUBIG_Nombre,'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_ConsultaRegistro]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_ConsultaRegistro]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_ConsultaRegistro]
( 
    @pintPAIS_Interno   INT 
) 
AS
BEGIN

   SELECT PAIS.PAIS_Interno               AS [PAIS_Interno]
        , PAIS.PAIS_Nombre                AS [PAIS_Nombre]
        , PAIS.PAIS_CodigoNumerico        AS [PAIS_CodigoNumerico]
        , PAIS.PAIS_CodigoAlfa2           AS [PAIS_CodigoAlfa2]
        , PAIS.PAIS_CodigoAlfa3           AS [PAIS_CodigoAlfa3]
        , PAIS.PAIS_Continente            AS [PAIS_Continente]
        , PAIS.PAIS_Descripcion           AS [PAIS_Descripcion]
        , PAIS.PAIS_Activo                AS [PAIS_Activo]
     FROM POSAD_Paises AS PAIS
    WHERE PAIS.PAIS_Interno = @pintPAIS_Interno

END
GO 

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_Insertar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_Insertar]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_Insertar]
(
    @pintPAIS_Interno            INT OUTPUT
   ,@pvchPAIS_Nombre             VARCHAR(100) 
   ,@pvchPAIS_CodigoNumerico     VARCHAR(5) 
   ,@pchrPAIS_CodigoAlfa2        CHAR(2) 
   ,@pchrPAIS_CodigoAlfa3        CHAR(3) 
   ,@pvchPAIS_Continente         VARCHAR(50)
   ,@pvchPAIS_Descripcion        VARCHAR(250) 
   ,@pbitPAIS_Activo             BIT 
   ,@pvchAUDI_UsuarioCreacion    VARCHAR(25) 
) 
AS 
BEGIN

   SELECT @pintPAIS_Interno = (ISNULL(MAX(PAIS_Interno), 0)+1)
     FROM [POSAD_Paises]

   INSERT INTO [POSAD_Paises]
        ( PAIS_Interno             , PAIS_Nombre            , PAIS_CodigoNumerico          , PAIS_CodigoAlfa2    
        , PAIS_CodigoAlfa3         , PAIS_Continente        , PAIS_Descripcion             , PAIS_Activo         
        , AUDI_UsuarioCreacion     , AUDI_FechaCreacion     )
   VALUES
        ( @pintPAIS_Interno         , @pvchPAIS_Nombre        , @pvchPAIS_CodigoNumerico      , @pchrPAIS_CodigoAlfa2
        , @pchrPAIS_CodigoAlfa3     , @pvchPAIS_Continente    , @pvchPAIS_Descripcion         , @pbitPAIS_Activo     
        , @pvchAUDI_UsuarioCreacion , GETDATE() )

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_Actualizar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_Actualizar]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_Actualizar]
(
    @pintPAIS_Interno               INT OUTPUT
   ,@pvchPAIS_Nombre                VARCHAR(100) 
   ,@pvchPAIS_CodigoNumerico        VARCHAR(5) 
   ,@pchrPAIS_CodigoAlfa2           CHAR(2) 
   ,@pchrPAIS_CodigoAlfa3           CHAR(3) 
   ,@pvchPAIS_Continente            VARCHAR(50)
   ,@pvchPAIS_Descripcion           VARCHAR(250) 
   ,@pbitPAIS_Activo                BIT 
   ,@pvchAUDI_UsuarioModificacion   VARCHAR(25) 
) 
AS 
BEGIN

   UPDATE [POSAD_Paises]
      SET [PAIS_Nombre]              = @pvchPAIS_Nombre
        , [PAIS_CodigoNumerico]      = @pvchPAIS_CodigoNumerico
        , [PAIS_CodigoAlfa2]         = @pchrPAIS_CodigoAlfa2
        , [PAIS_CodigoAlfa3]         = @pchrPAIS_CodigoAlfa3
        , [PAIS_Continente]          = @pvchPAIS_Continente
        , [PAIS_Descripcion]         = @pvchPAIS_Descripcion
        , [PAIS_Activo]              = @pbitPAIS_Activo
        , [AUDI_UsuarioModificacion] = @pvchAUDI_UsuarioModificacion
        , [AUDI_FechaModificacion]   = GETDATE()
    WHERE [PAIS_Interno]             = @pintPAIS_Interno


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_Eliminar]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_Eliminar]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_Eliminar]
( 
    @pintPAIS_Interno   INT 
) 
AS 
BEGIN

   DELETE FROM [POSAD_Paises]
    WHERE [PAIS_Interno] = @pintPAIS_Interno

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_PAIS_ConsultaTodos]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_PAIS_ConsultaTodos]
GO
CREATE PROCEDURE [dbo].[POSAD_PAIS_ConsultaTodos] 
(
   @pvchPAIS_Nombre      VARCHAR(100)
)
AS
BEGIN

   SELECT PAIS.PAIS_Interno         AS [PAIS_Interno]
        , PAIS.PAIS_Nombre          AS [PAIS_Nombre]
        , PAIS.PAIS_CodigoAlfa2     AS [PAIS_CodigoAlfa2]
        , PAIS.PAIS_CodigoAlfa3     AS [PAIS_CodigoAlfa3]
        , PAIS.PAIS_Activo          AS [PAIS_Activo]
   FROM POSAD_Paises AS PAIS
   WHERE PAIS.PAIS_Nombre LIKE ISNULL(@pvchPAIS_Nombre,'') + '%'

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_VerificarUsuario]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_VerificarUsuario]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_VerificarUsuario]
( 
    @pvchEMPR_Id           VARCHAR(50)
   ,@pvchUSUA_Contrasena   VARCHAR(MAX)
   ,@pvchUSUA_Usuario      VARCHAR(25)
) 
AS
BEGIN

   SELECT USUA.USUA_Interno            AS [USUA_Interno]
        , USUA.ROLE_Interno            AS [ROLE_Interno]
        , USUA.USUA_NombreCompleto     AS [USUA_NombreCompleto]
        , USUA.USUA_Usuario            AS [USUA_Usuario]
        , USUA.USUA_Contrasena         AS [USUA_Contrasena]
        , USUA.USUA_Correo             AS [USUA_Correo]
        , EMPR.EMPR_RazonSocial        AS [USUA_NombreEmpresa]
        , EMPR.EMPR_Interno            AS [USUA_InternoEmpresa]
   FROM POSAD_Usuarios AS USUA
   INNER JOIN POSAD_EmpresasUsuarios AS USUE ON USUE.USUA_Interno = USUA.USUA_Interno
   INNER JOIN POSAD_Empresas AS EMPR ON EMPR.EMPR_Interno = USUE.EMPR_Interno
   WHERE 
   USUA.USUA_Usuario = @pvchUSUA_Usuario AND
   USUA.USUA_Contrasena = @pvchUSUA_Contrasena AND
   EMPR.EMPR_Id = @pvchEMPR_Id

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POSAD_USUA_ConsultaUsuarioEmpresa]') AND type in (N'P', N'PC'))
   DROP PROCEDURE [dbo].[POSAD_USUA_ConsultaUsuarioEmpresa]
GO
CREATE PROCEDURE [dbo].[POSAD_USUA_ConsultaUsuarioEmpresa]
( 
    @pvchEMPR_Id        VARCHAR(50)
   ,@pbigUSUA_Interno   BIGINT 
) 
AS
BEGIN

   SELECT USUA.USUA_Interno            AS [USUA_Interno]
        , USUA.ROLE_Interno            AS [ROLE_Interno]
        , USUA.USUA_NombreCompleto     AS [USUA_NombreCompleto]
        , USUA.USUA_Usuario            AS [USUA_Usuario]
        , USUA.USUA_Contrasena         AS [USUA_Contrasena]
        , USUA.USUA_Correo             AS [USUA_Correo]
        , EMPR.EMPR_RazonSocial        AS [USUA_NombreEmpresa]
        , EMPR.EMPR_Interno            AS [USUA_InternoEmpresa]
        , SUCU.SUCU_Interno            AS [USUA_InternoSucursal]
        , SUCU.SUCU_Nombre             AS [USUA_NombreSucursal]
   FROM POSAD_Usuarios AS USUA
   INNER JOIN POSAD_EmpresasUsuarios AS USUE ON USUE.USUA_Interno = USUA.USUA_Interno
   INNER JOIN POSAD_Empresas AS EMPR ON EMPR.EMPR_Interno = USUE.EMPR_Interno
   INNER JOIN POSAD_Sucursales AS SUCU ON SUCU.EMPR_Interno = USUE.EMPR_Interno AND 
                                          SUCU.SUCU_Interno = CONVERT(BIGINT,( SELECT TOP 1 CONF_Valor FROM POSAD_Configuraciones 
                                          WHERE EMPR_Interno = EMPR.EMPR_Interno 
                                          AND USUA_Interno = USUA.USUA_Interno 
                                          AND CONF_Llave = 'SUCUR'))
   WHERE 
   USUA.USUA_Interno = @pbigUSUA_Interno AND 
   EMPR.EMPR_Id = @pvchEMPR_Id
    
END
GO

--**POSAD_Paises
--**POSAD_Ubigeos
--**POSAD_Empresas
--**POSAD_Sucursales
--POSAD_Tablas
--POSAD_Parametros
--POSAD_Entidades
--POSAD_FuncionesEntidades
--POSAD_Habitaciones
--**POSAD_Roles
--**POSAD_Usuarios
--**POSAD_EmpresasUsuarios
--**POSAD_Opciones
--**POSAD_RolesOpciones
--**POSAD_Procesos
--**POSAD_RolesProcesos
--POSAD_Configuraciones
--POSAD_Turnos
--POSAD_Registros