using System.ComponentModel.DataAnnotations;
using DaisyStudy.Data.Entities;

namespace DaisyStudy.ViewModels.Catalog.Submissions;

public class SubmissionViewModel
{
    public int HomeworkID { set; get; }

    public Guid StudentID { set; get; }

    [Display(Name = "Tên")]
    public string? FirstName { get; set; }

    [Display(Name = "Họ")]
    public string? LastName { get; set; }

    [Display(Name = "Số điện thoại")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Tên bài tập")]
    public string? HomeworkName { set; get; }

    [Display(Name = "Mã lớp học")]
    public string? ClassID { set; get; }

    [Display(Name = "Tên lớp học")]
    public string? ClassName { set; get; }

    [Display(Name = "Mô tả")]
    public string? Description { set; get; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateTimeCreated { set; get; }

    [Display(Name = "Hạn nộp bài")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Deadline { set; get; }

    [Display(Name = "Điểm số")]
    public float Mark { set; get; }

    [Display(Name = "Ghi chú")]
    public string? Note { set; get; }

    [Display(Name = "Bài làm")]
    public string? DescriptionSubmission { set; get; }

    [Display(Name = "Thời gian nộp bài")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime SubmissionDateTime { set; get; }

    [Display(Name = "Thời gian cập nhật")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? DateTimeUpdated { set; get; }

    [Display(Name = "Nộp trễ")]
    public Delay Delay { set; get; }
}

