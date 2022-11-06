using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DaisyStudy.ViewModels.Catalog.Submissions;

public class SubmissionCreateRequest
{
    public int HomeworkID { set; get; }

    public Guid StudentID { set; get; }

    [Display(Name = "Bài làm")]
    public string? Description { set; get; }
}

