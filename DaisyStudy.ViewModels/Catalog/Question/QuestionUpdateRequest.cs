using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.Question;

public class QuestionUpdateRequest
{
    [Display(Name = "Mã câu hỏi")]
    public int QuestionID { set; get; }

    [Display(Name = "Nôi dung câu hỏi")]
    public string? QuestionString { set; get; }

    [Display(Name = "Điểm số")]
    public float Point { set; get; }

    public IFormFile? ThumbnailImage { get; set; }

    public long FileSize { set; get; }
}