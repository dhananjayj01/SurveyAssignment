using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SurveyAssignment.Application.Contracts.Persistence;
using SurveyAssignment.Application.Dtos.ConfigurationDtos;
using SurveyAssignment.Application.Dtos.QuestionDtos;
using SurveyAssignment.Application.Dtos.QuestionOptionDtos;
using SurveyAssignment.Application.Dtos.SaveSurveyAnswerDtos;
using SurveyAssignment.Application.Dtos.SectionDtos;
using SurveyAssignment.Application.Dtos.SurveyDtos;
using SurveyAssignment.Persistence.Contexts;
using System.Data;

namespace SurveyAssignment.Persistence.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public SurveyRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            // Extract the connection string from the DbContext options
            _connectionString = _context.Database.GetConnectionString()
                                ?? throw new InvalidOperationException("Connection string not configured.");
        }

        // Retrieves complete survey definition
        public async Task<SurveyDto> GetSurveyDefinitionAsync(int surveyId)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetSurveyDefinition", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SurveyId", surveyId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            // Result Set 1: Survey
            SurveyDto survey = null;

            if (await reader.ReadAsync())
            {
                survey = new SurveyDto
                {
                    SurveyId = reader.GetInt32(0),
                    SurveyName = reader.GetString(1),
                    SurveyDesc = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Sections = new List<SectionDto>()
                };
            }

            if (survey == null)
                return null;

            // Result Set 2: Sections
            var sections = new Dictionary<int, SectionDto>();
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                var section = new SectionDto
                {
                    SectionId = reader.GetInt32(0),
                    SectionTitle = reader.GetString(2),
                    SectionDesc = reader.IsDBNull(3) ? null : reader.GetString(3),
                    DisplayOrder = reader.GetInt32(4),
                    IsSinglePage = reader.GetBoolean(5),
                    Questions = new List<QuestionDto>()
                };
                sections.Add(section.SectionId, section);
            }

            // Result Set 3: Questions
            var questions = new Dictionary<int, QuestionDto>();
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                var question = new QuestionDto
                {
                    QuestionId = reader.GetInt32(0),
                    QuestionText = reader.GetString(2),
                    QuestionTypeId = reader.GetInt32(3),
                    TypeCode = reader.GetString(4),
                    TypeName = reader.GetString(5),
                    IsMandatory = reader.GetBoolean(6),
                    DisplayOrder = reader.GetInt32(7),
                    AnswerMaxLength = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                    Options = new List<QuestionOptionDto>()
                };

                questions.Add(question.QuestionId, question);

                var sectionId = reader.GetInt32(1);
                if (sections.TryGetValue(sectionId, out var section))
                {
                    section.Questions.Add(question);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"SectionId {sectionId} not found for QuestionId {question.QuestionId}");
                }
            }

            // Result Set 4: Options
            await reader.NextResultAsync();

            while (await reader.ReadAsync())
            {
                var option = new QuestionOptionDto
                {
                    OptionId = reader.GetInt32(0),
                    OptionText = reader.GetString(2),
                    DisplayOrder = reader.GetInt32(3)
                };

                questions[reader.GetInt32(1)].Options.Add(option);
            }

            survey.Sections = sections.Values.OrderBy(x => x.DisplayOrder).ToList();

            // Result Set 5: Configuration
            await reader.NextResultAsync();

            var ratingConfig = new ConfigurationDto
            {
                MinRating = 1, 
                MaxRating = 5
            };

            while (await reader.ReadAsync())
            {
                var key = reader.GetString(0);
                var value = reader.GetString(1);

                if (key == "MinRating")
                    ratingConfig.MinRating = int.Parse(value);

                if (key == "MaxRating")
                    ratingConfig.MaxRating = int.Parse(value);
            }

            survey.RatingConfig = ratingConfig;

            return survey;
        }

        // Saves a single survey answer submitted by the user.
        public async Task SaveSurveyAnswerAsync(SaveSurveyAnswerDto dto)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SaveSurveyAnswer @SurveyId, @UserId, @QuestionId, @AnswerText, @AnswerOptionId, @RatingValue, @CreatedBy",
                new SqlParameter("@SurveyId", dto.SurveyId),
                new SqlParameter("@UserId", dto.UserId),
                new SqlParameter("@QuestionId", dto.QuestionId),
                new SqlParameter("@AnswerText", (object?)dto.AnswerText ?? DBNull.Value),
                new SqlParameter("@AnswerOptionId", (object?)dto.AnswerOptionId ?? DBNull.Value),
                new SqlParameter("@RatingValue", (object?)dto.RatingValue ?? DBNull.Value),
                new SqlParameter("@CreatedBy", dto.CreatedBy)
            );
        }
    }
}
