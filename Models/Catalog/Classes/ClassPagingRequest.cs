using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Classes;

public class ClassPagingRequest : PagingRequestBase
{
    public string? Keyword { get; set; }
}