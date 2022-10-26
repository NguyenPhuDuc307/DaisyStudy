using DaisyStudy.ViewModel.Common;

namespace DaisyStudy.ViewModel.Catalog.Classes;

public class GetClassPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
}

