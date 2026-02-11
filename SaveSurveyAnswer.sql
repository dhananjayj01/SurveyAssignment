USE [SurveySchema]
GO

/****** Object:  StoredProcedure [dbo].[sp_SaveSurveyAnswer]    Script Date: 2/11/2026 2:27:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SaveSurveyAnswer]
(
    @SurveyId INT,
    @UserId INT,
    @QuestionId INT,
    @AnswerText NVARCHAR(2000) = NULL,
    @AnswerOptionId INT = NULL,
    @RatingValue INT = NULL,
    @CreatedBy INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ResponseId INT;
    DECLARE @IsMandatory BIT;
    DECLARE @QuestionTypeId INT;

    /* 1️⃣ Get Question Details */
    SELECT
        @IsMandatory = IsMandatory,
        @QuestionTypeId = QuestionTypeId
    FROM Question
    WHERE QuestionId = @QuestionId
      AND IsDeleted = 0;

    IF @QuestionTypeId IS NULL
    BEGIN
        RAISERROR('Invalid QuestionId.', 16, 1);
        RETURN;
    END

    /* 2️⃣ Mandatory Validation */
    IF @IsMandatory = 1
    BEGIN
        IF (
            (@QuestionTypeId IN (1, 2) AND @AnswerOptionId IS NULL)
            OR (@QuestionTypeId = 3 AND ISNULL(@AnswerText, '') = '')
            OR (@QuestionTypeId = 4 AND @RatingValue IS NULL)
        )
        BEGIN
            RAISERROR('Mandatory question cannot be left unanswered.', 16, 1);
            RETURN;
        END
    END

    /* 3️⃣ Validate OptionId */
    IF @QuestionTypeId IN (1, 2) AND @AnswerOptionId IS NOT NULL
    BEGIN
        IF NOT EXISTS (
            SELECT 1
            FROM QuestionOption
            WHERE OptionId = @AnswerOptionId
              AND QuestionId = @QuestionId
              AND IsDeleted = 0
        )
        BEGIN
            RAISERROR('Invalid AnswerOptionId.', 16, 1);
            RETURN;
        END
    END

    /* 4️⃣ Get or Create SurveyResponse */
    SELECT @ResponseId = ResponseId
    FROM SurveyResponse
    WHERE SurveyId = @SurveyId
      AND UserId = @UserId
      AND IsDeleted = 0;

    IF @ResponseId IS NULL
    BEGIN
        INSERT INTO SurveyResponse (SurveyId, UserId, CreatedBy)
        VALUES (@SurveyId, @UserId, @CreatedBy);

        SET @ResponseId = SCOPE_IDENTITY();
    END

    /* 5️⃣ Cleanup only for NON-MULTI */
    IF @QuestionTypeId <> 2
    BEGIN
        DELETE FROM QuestionResponse
        WHERE ResponseId = @ResponseId
          AND QuestionId = @QuestionId;
    END

    /* 6️⃣ INSERT WITH DUPLICATE PROTECTION (🔥 FINAL FIX) */
    IF NOT EXISTS (
        SELECT 1
        FROM QuestionResponse
        WHERE ResponseId = @ResponseId
          AND QuestionId = @QuestionId
          AND (
                (@QuestionTypeId = 2 AND AnswerOptionId = @AnswerOptionId)
                OR (@QuestionTypeId <> 2)
              )
    )
    BEGIN
        INSERT INTO QuestionResponse
        (
            ResponseId,
            QuestionId,
            AnswerText,
            AnswerOptionId,
            RatingValue
        )
        VALUES
        (
            @ResponseId,
            @QuestionId,
            @AnswerText,
            @AnswerOptionId,
            @RatingValue
        );
    END
END;
GO


