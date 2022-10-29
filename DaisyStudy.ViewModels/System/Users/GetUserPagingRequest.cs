using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}