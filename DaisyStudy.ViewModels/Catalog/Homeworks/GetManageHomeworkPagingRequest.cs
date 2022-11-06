using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Homeworks
{
    public class GetManageHomeworkPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public int ClassID { get; set; }
    }
}