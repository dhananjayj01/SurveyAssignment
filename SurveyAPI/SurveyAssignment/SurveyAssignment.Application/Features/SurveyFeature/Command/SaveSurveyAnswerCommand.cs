using MediatR;
using SurveyAssignment.Application.Dtos.SaveSurveyAnswerDtos;
using SurveyAssignment.Domain.Common;

namespace SurveyAssignment.Application.Features.SurveyFeature.Command
{
    public record SaveSurveyAnswerCommand(SaveSurveyAnswerDto SaveSurveyAnswerDto
    ) : IRequest<ApiResponse<bool>>;
}
