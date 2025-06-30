CREATE PROC FI_SP_IncBenef
    @CPF VARCHAR(11),
    @NOME VARCHAR(50),
    @IDCLIENTE BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF @CPF IS NULL OR LEN(@CPF) != 11
        BEGIN
            RAISERROR('CPF deve ter exatamente 11 dígitos', 16, 1);
            RETURN;
        END
        
        IF @NOME IS NULL OR LTRIM(RTRIM(@NOME)) = ''
        BEGIN
            RAISERROR('Nome é obrigatório', 16, 1);
            RETURN;
        END
        
        IF @IDCLIENTE IS NULL OR @IDCLIENTE <= 0
        BEGIN
            RAISERROR('ID do Cliente é obrigatório e deve ser maior que zero', 16, 1);
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM CLIENTES WHERE ID = @IDCLIENTE)
        BEGIN
            RAISERROR('Cliente não encontrado', 16, 1);
            RETURN;
        END
        
        IF EXISTS (SELECT 1 FROM BENEFICIARIOS WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE)
        BEGIN
            RAISERROR('Já existe um beneficiário com este CPF para este cliente', 16, 1);
            RETURN;
        END
        
        INSERT INTO BENEFICIARIOS (CPF, NOME, IDCLIENTE)
        VALUES (@CPF, LTRIM(RTRIM(@NOME)), @IDCLIENTE);
        
        SELECT SCOPE_IDENTITY() AS ID_BENEFICIARIO_INSERIDO;
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END