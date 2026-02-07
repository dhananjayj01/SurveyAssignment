using MediatR;
using SurveyAssignment.Application.Dtos.SurveyDtos;
using SurveyAssignment.Domain.Common;

namespace SurveyAssignment.Application.Features.SurveyFeature.Query
{
    public record GetSurveyDefinitionQuery(int SurveyId)
    : IRequest<ApiResponse<SurveyDto>>;
}
