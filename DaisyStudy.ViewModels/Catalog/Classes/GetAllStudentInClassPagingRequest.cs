using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ViewModels.Catalog.Classes;

public class GetAllStudentInClassPagingRequest : PagingRequestBase
{
    public int? ClassID { get; set; }
}