using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.Catalog.Submissions;

public class SubmissionUpdateRequest
{
    public int HomeworkID { set; get; }

    public Guid StudentID { set; get; }

    [Display(Name = "Bài làm")]
    public string? Description { set; get; }
}
