using DaisyStudy.Data.Enums;

namespace DaisyStudy.Data;

public class ClassDetail
{
    public int ClassID { set; get; }
    public Class? Class { set; get; }
    public string? UserID { set; get; }
    public ApplicationUser? User { set; get; }
    public string? Note { set; get; }
    public Teacher IsTeacher { set; get; }
}