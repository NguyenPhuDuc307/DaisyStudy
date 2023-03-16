using DaisyStudy.Models.Common;

namespace DaisyStudy.Models.Catalog.Classes;

public class GetAllStudentInClassPagingRequest : PagingRequestBase
{
    public int? ClassID { get; set; }
}