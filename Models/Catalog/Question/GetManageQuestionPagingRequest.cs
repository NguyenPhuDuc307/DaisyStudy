using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Question;

public class GetManageQuestionPagingRequest : PagingRequestBase
{
    public string? Keyword { set; get; }

    public int ExamScheduleID { set; get; }
}