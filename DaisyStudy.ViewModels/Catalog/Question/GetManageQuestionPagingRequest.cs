using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Question;

public class GetManageQuestionPagingRequest : PagingRequestBase
{
    public string? Keyword { set; get; }

    public int ExamScheduleID { set; get; }
}