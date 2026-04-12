CREATE PROCEDURE SP_PerformTransaction
    @TransactionTypeID INT,
    @Amount DECIMAL(18,4),
    @Description NVARCHAR(200),
    @SenderAccountID INT = NULL, -- Use NULL or 0 for Deposits
    @ReceiverAccountID INT = NULL, -- Use NULL or 0 for Withdrawals
    @CreatedByUserID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Validation: Check Min/Max amounts from TransactionTypes table
        DECLARE @Min DECIMAL(18,4), @Max DECIMAL(18,4);
        SELECT @Min = MinAmount, @Max = MaxAmount FROM TransactionTypes WHERE TransactionTypeID = @TransactionTypeID;

        IF @Amount < @Min OR @Amount > @Max
        BEGIN
            THROW 50001, 'Transaction amount violates Min/Max limits.', 1;
        END

        -- 2. Logic for DEBIT (Withdrawal or Transfer)
        IF @TransactionTypeID IN (2, 3) -- Assuming 2=Withdraw, 3=Transfer
        BEGIN
            -- Check if sender has enough balance
            IF (SELECT Balance FROM Accounts WHERE AccountID = @SenderAccountID) < @Amount
            BEGIN
                THROW 50002, 'Insufficient funds for this operation.', 1;
            END
            
            -- Deduct money
            UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountID = @SenderAccountID;
        END

        -- 3. Logic for CREDIT (Deposit or Transfer)
        IF @TransactionTypeID IN (1, 3) -- Assuming 1=Deposit, 3=Transfer
        BEGIN
            UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountID = @ReceiverAccountID;
        END

        -- 4. Log the Transaction into the Transactions table
        INSERT INTO Transactions (TransactionTypeID, Amount, TransactionDate, [Description], SenderAccountID, ReceiverAccountID, CreatedByUserID)
        VALUES (@TransactionTypeID, @Amount, GETDATE(), @Description, 
                NULLIF(@SenderAccountID, 0), 
                NULLIF(@ReceiverAccountID, 0), 
                @CreatedByUserID);

        COMMIT TRANSACTION;
        SELECT SCOPE_IDENTITY() AS TransactionID;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO