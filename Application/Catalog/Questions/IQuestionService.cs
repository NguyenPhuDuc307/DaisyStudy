using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Question;
using DaisyStudy.Models.Common;

namespace DaisyStudy.Application.Catalog.Questions;
public interface IQuestionService
{
    Task<int> Create(QuestionsCreateRequest request);
    Task<int> Update(QuestionUpdateRequest request);
    Task<int> Delete(int QuestionID);
    Task<QuestionViewModel> GetById(int QuestionID);
    Task<PagedResult<Question>> GetExamPaper(int ExamScheduleID);
    Task<PagedResult<QuestionViewModel>> GetAllPaging(GetManageQuestionPagingRequest request);
}