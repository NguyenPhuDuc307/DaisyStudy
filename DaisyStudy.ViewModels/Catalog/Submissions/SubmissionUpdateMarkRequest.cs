using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.Catalog.Submissions;

public class SubmissionUpdateMarkRequest
{
    public int HomeworkID { set; get; }

    public Guid StudentID { set; get; }

    [Display(Name = "Điểm")]
    public float Mark { set; get; }

    [Display(Name = "Ghi chú")]
    public string? Note { set; get; }
}
