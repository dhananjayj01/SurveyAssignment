USE [SurveySchema]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetSurveyDefinition]    Script Date: 2/11/2026 2:26:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_GetSurveyDefinition]
(
    @SurveyId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    /* 1️⃣ Survey */
    SELECT
        s.SurveyId,
        s.SurveyName,
        s.SurveyDesc,
        s.IsActive
    FROM Survey s
    WHERE s.SurveyId = @SurveyId
      AND s.IsDeleted = 0;

    /* 2️⃣ Sections */
    SELECT
        sec.SectionId,
        sec.SurveyId,
        sec.SectionTitle,
        sec.SectionDesc,
        sec.DisplayOrder,
        sec.IsSinglePage
    FROM Section sec
    WHERE sec.SurveyId = @SurveyId
      AND sec.IsDeleted = 0
    ORDER BY sec.DisplayOrder;

    /* 3️⃣ Questions */
    SELECT
        q.QuestionId,
        q.SectionId,
        q.QuestionText,
        q.QuestionTypeId,
        qt.TypeCode,
        qt.TypeName,
        q.IsMandatory,
        q.DisplayOrder,
        q.AnswerMaxLength
    FROM Question q
    INNER JOIN QuestionType qt
        ON qt.QuestionTypeId = q.QuestionTypeId
    WHERE q.SectionId IN (
        SELECT SectionId FROM Section WHERE SurveyId = @SurveyId AND IsDeleted = 0
    )
      AND q.IsDeleted = 0
    ORDER BY q.SectionId, q.DisplayOrder;

    /* 4️⃣ Question Options (Only for Single & Multi Select) */
    SELECT
        qo.OptionId,
        qo.QuestionId,
        qo.OptionText,
        qo.DisplayOrder
    FROM QuestionOption qo
    WHERE qo.QuestionId IN (
        SELECT QuestionId FROM Question
        WHERE SectionId IN (
            SELECT SectionId FROM Section WHERE SurveyId = @SurveyId
        )
        AND QuestionTypeId IN (1, 2) -- Single Select, Multi Select
        AND IsDeleted = 0
    )
      AND qo.IsDeleted = 0
    ORDER BY qo.QuestionId, qo.DisplayOrder;

	/* 5 Configuration */
	SELECT
        ConfigKey,
        ConfigValue
    FROM Configuration
	WHERE ConfigKey IN ('MinRating', 'MaxRating');

END;
GO


