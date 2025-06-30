
ALTER PROC FI_SP_ConsBenef
    @IDCLIENTE BIGINT = NULL 
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF @IDCLIENTE IS NOT NULL AND @IDCLIENTE > 0
        BEGIN

            IF NOT EXISTS (SELECT 1 FROM CLIENTES WHERE ID = @IDCLIENTE)
            BEGIN
                RAISERROR('Cliente não encontrado', 16, 1);
                RETURN;
            END
            
            SELECT 
                ID,
                NOME, 
                CPF, 
                IDCLIENTE
            FROM BENEFICIARIOS 
            WHERE IDCLIENTE = @IDCLIENTE
            ORDER BY NOME;
        END
        ELSE
        BEGIN

            SELECT 
                ID,
                NOME, 
                CPF, 
                IDCLIENTE
            FROM BENEFICIARIOS
            ORDER BY NOME;
        END
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
