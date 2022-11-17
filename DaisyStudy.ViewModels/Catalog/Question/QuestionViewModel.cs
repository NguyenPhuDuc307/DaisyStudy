using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.Question;

public class QuestionViewModel
{
    [Display(Name = "Mã câu hỏi")]
    public int QuestionID { set; get; }

    [Display(Name = "Mã kì thi")]
    public int ExamScheduleID { set; get; }

    [Display(Name = "Nội dung câu hỏi")]
    public string? QuestionString { set; get; }

    [Display(Name = "Điểm số")]
    public float Point { set; get; }

    public string? ImagePath { set; get; }

    public long FileSize { set; get; }
}
