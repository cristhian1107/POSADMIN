IF OBJECT_ID('dbo.GetDateCountry') IS NOT NULL
BEGIN
   DROP FUNCTION [dbo].[GetDateCountry]
END
GO

CREATE FUNCTION [dbo].[GetDateCountry](@myCountry VARCHAR(5)) 
RETURNS DATETIME
AS 
BEGIN

   DECLARE @DateCountry DATETIME

   SET @DateCountry = CASE @myCountry 
                        WHEN 'es-PE' THEN DATEADD(HOUR,-5,GETUTCDATE())
                        ELSE GETUTCDATE()
                        END

   RETURN @DateCountry
                        
END
GO