using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Contact;

public class GetAllPagingRequest : PagingRequestBase
{
    public string? Keyword { set; get; }
}