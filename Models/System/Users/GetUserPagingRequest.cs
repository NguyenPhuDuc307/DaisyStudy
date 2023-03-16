using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}