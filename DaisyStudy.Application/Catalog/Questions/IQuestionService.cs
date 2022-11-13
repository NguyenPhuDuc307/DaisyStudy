using DaisyStudy.ViewModels.Catalog.Question;
using DaisyStudy.ViewModels.Common;


namespace DaisyStudy.Application.Catalog.Questions;

public interface IQuestionService
{
    Task<int> Create(QuestionsCreateRequest request);
    Task<int> Update(QuestionUpdateRequest request);
    Task<int> Delete(int QuestionID);
    Task<ApiResult<QuestionViewModel>> GetById(int QuestionID);
    Task<ApiResult<PagedResult<QuestionViewModel>>> GetAllPaging(GetManageQuestionPagingRequest request);
}
