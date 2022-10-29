using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class GetClassPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
}

