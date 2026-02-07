

using SurveyAssignment.Application.Dtos.SaveSurveyAnswerDtos;
using SurveyAssignment.Application.Dtos.SurveyDtos;
using SurveyAssignment.Application.Features.SurveyFeature.Command;

namespace SurveyAssignment.Application.Contracts.Persistence
{
    public interface ISurveyRepository
    {
        Task<SurveyDto> GetSurveyDefinitionAsync(int surveyId);
        Task SaveSurveyAnswerAsync(SaveSurveyAnswerDto dto);
    }
}
