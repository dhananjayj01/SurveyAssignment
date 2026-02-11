using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyAssignment.Application.Dtos.SaveSurveyAnswerDtos;
using SurveyAssignment.Application.Features.SurveyFeature.Command;
using SurveyAssignment.Application.Features.SurveyFeature.Query;

namespace SurveyAssignment.API.Controllers
{
    [ApiController]
    [Route("api/surveys")]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Retrieves the complete survey definition including
        // sections, questions, options and configuration.
        [HttpGet("{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId)
        {
            var result = await _mediator.Send(
                new GetSurveyDefinitionQuery(surveyId));
            return Ok(result);
        }

        // Saves a single survey answer submitted by the user.
        [HttpPost("answer")]
        public async Task<IActionResult> SaveAnswer(SaveSurveyAnswerDto dto)
        {
            var response = await _mediator.Send(
                new SaveSurveyAnswerCommand(dto));

            return Ok(response);
        }
    }

}
