using DaisyStudy.Data.Enums;
using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Submissions;

public class GetManageSubmissionPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
    public int HomeworkID { get; set; }
    public string? UserId { get; set; }
    
}