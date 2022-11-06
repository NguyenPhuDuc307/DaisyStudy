using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Submissions;

public class GetManageSubmissionPagingRequest : PagingRequestBase
{
    public Delay Delay { get; set; }
    public int HomeworkID { get; set; }
}