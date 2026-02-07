using Microsoft.AspNetCore.Mvc;
using SurveyWebApplication.Common;
using SurveyWebApplication.Models.SurveyViewModels;
using System.Text.Json;

namespace SurveyWebApplication.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SurveyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<IActionResult> Index(
            int id,
            int sectionIndex = 0,
            int questionIndex = 0)
        {
            var client = _httpClientFactory.CreateClient("SurveyApi");

            var response =
                await client.GetFromJsonAsync<ApiResponse<SurveyViewModel>>(
                    $"api/surveys/{id}");

            if (response?.Data == null)
                return RedirectToAction("List");

            

            ViewBag.MinRating = response.Data.RatingConfig.MinRating;
            ViewBag.MaxRating = response.Data.RatingConfig.MaxRating;

            ViewBag.SectionIndex = sectionIndex;
            ViewBag.QuestionIndex = questionIndex;

            return View(response.Data);
        }


        public async Task<IActionResult> List()
        {
            var client = _httpClientFactory.CreateClient("SurveyApi");

            var surveys = new List<SurveyListViewModel>();

            int[] surveyIds = { 1, 3 };

            foreach (var id in surveyIds)
            {
                var response =
                    await client.GetFromJsonAsync<ApiResponse<SurveyViewModel>>(
                        $"api/surveys/{id}");

                if (response?.Data != null)
                {
                    surveys.Add(new SurveyListViewModel
                    {
                        SurveyId = response.Data.SurveyId,
                        SurveyName = response.Data.SurveyName,
                        SurveyDesc = response.Data.SurveyDesc
                    });
                }
            }

            return View(surveys);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitSection(
            int id,
            int sectionIndex,
            int questionIndex)
        {
            var client = _httpClientFactory.CreateClient("SurveyApi");

            int currentUserId = 1;

            // Get survey metadata (needed to know question types)
            var surveyResponse =
                await client.GetFromJsonAsync<ApiResponse<SurveyViewModel>>(
                    $"api/surveys/{id}");

            var survey = surveyResponse!.Data;

            foreach (var key in Request.Form.Keys)
            {
                if (!key.StartsWith("q_"))
                    continue;

                int questionId = int.Parse(key.Replace("q_", ""));
                string rawValue = Request.Form[key];

                if (string.IsNullOrWhiteSpace(rawValue))
                    continue;

                var question = survey.Sections
                    .SelectMany(s => s.Questions)
                    .First(q => q.QuestionId == questionId);

                var dto = new SaveSurveyAnswerDto
                {
                    SurveyId = id,
                    QuestionId = questionId,
                    UserId = currentUserId,
                    CreatedBy = 1
                };

                switch (question.TypeCode)
                {
                    case "SINGLE_SELECT":
                        dto.AnswerOptionId = int.Parse(rawValue);
                        break;

                    case "MULTI_SELECT":
                        foreach (var opt in rawValue.Split(','))
                        {
                            await client.PostAsJsonAsync(
                                "api/surveys/answer",
                                new SaveSurveyAnswerDto
                                {
                                    SurveyId = id,
                                    QuestionId = questionId,
                                    AnswerOptionId = int.Parse(opt),
                                    UserId = currentUserId,
                                    CreatedBy = 1
                                });
                        }
                        continue; // already saved

                    case "LONG_ANSWER":
                        dto.AnswerText = rawValue;
                        break;

                    case "STAR_RATING":
                        dto.RatingValue = int.Parse(rawValue);
                        break;
                }

                await client.PostAsJsonAsync("api/surveys/answer", dto);
            }

            // ===== NAVIGATION (UNCHANGED) =====
            var sections = survey.Sections
                .OrderBy(s => s.DisplayOrder)
                .ToList();

            var currentSection = sections[sectionIndex];
            var questions = currentSection.Questions
                .OrderBy(q => q.DisplayOrder)
                .ToList();

            if (currentSection.IsSinglePage)
            {
                // Always move to next section
                return RedirectToAction("Index", new
                {
                    id,
                    sectionIndex = sectionIndex + 1,
                    questionIndex = 0
                });
            }

            // Multi-page section
            if (questionIndex < questions.Count - 1)
            {
                return RedirectToAction("Index", new
                {
                    id,
                    sectionIndex,
                    questionIndex = questionIndex + 1
                });
            }

            // End of section
            return RedirectToAction("Index", new
            {
                id,
                sectionIndex = sectionIndex + 1,
                questionIndex = 0
            });

        }



    }
}
