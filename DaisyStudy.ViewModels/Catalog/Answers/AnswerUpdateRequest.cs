using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.Answers;

public class AnswerUpdateRequest
{
    [Display(Name = "Mã câu hỏi")]
    public int AnswerID { set; get; }

    [Display(Name = "Nôi dung câu trả lời")]
    public string? AnswerString { set; get; }

    public bool IsCorrect { set; get; }

    public IFormFile? ImagePath { get; set; }

    public long FileSize { set; get; }
}
