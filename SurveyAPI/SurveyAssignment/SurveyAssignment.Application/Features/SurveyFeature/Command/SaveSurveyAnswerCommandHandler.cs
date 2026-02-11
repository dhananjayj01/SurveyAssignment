using MediatR;
using SurveyAssignment.Application.Contracts.Persistence;
using SurveyAssignment.Domain.Common;

namespace SurveyAssignment.Application.Features.SurveyFeature.Command
{

    public class SaveSurveyAnswerCommandHandler
        : IRequestHandler<SaveSurveyAnswerCommand, ApiResponse<bool>>
    {
        private readonly ISurveyRepository _repo;

        public SaveSurveyAnswerCommandHandler(ISurveyRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<bool>> Handle(
            SaveSurveyAnswerCommand request,
            CancellationToken cancellationToken)
        {
            await _repo.SaveSurveyAnswerAsync(request.SaveSurveyAnswerDto);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Survey answer saved successfully",
                Data = true
            };
        }
    }
}
