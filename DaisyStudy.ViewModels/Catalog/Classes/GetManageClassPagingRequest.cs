using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Classes
{
    public class GetManageClassPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}