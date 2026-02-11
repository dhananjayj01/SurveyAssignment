using MediatR;
using SurveyAssignment.Application.Contracts.Persistence;
using SurveyAssignment.Application.Dtos.SurveyDtos;
using SurveyAssignment.Domain.Common;

namespace SurveyAssignment.Application.Features.SurveyFeature.Query
{
    public class GetSurveyDefinitionQueryHandler : IRequestHandler<GetSurveyDefinitionQuery, ApiResponse<SurveyDto>>
    {
        private readonly ISurveyRepository _repo;

        public GetSurveyDefinitionQueryHandler(ISurveyRepository repo)
        {
            _repo = repo;
        }

        // Handles loading the full survey definition
        public async Task<ApiResponse<SurveyDto>> Handle(
            GetSurveyDefinitionQuery request,
            CancellationToken cancellationToken)
        {
            var survey = await _repo.GetSurveyDefinitionAsync(request.SurveyId);

            if (survey == null)
            {
                return new ApiResponse<SurveyDto>
                {
                    Success = false,
                    Message = "Survey not found"
                };
            }

            return new ApiResponse<SurveyDto>
            {
                Success = true,
                Message = "Survey loaded successfully",
                Data = survey
            };
        }

    }
}
