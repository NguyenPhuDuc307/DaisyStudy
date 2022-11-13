using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.ExamSchedules;

public class ExamSchedulesViewModel
{
    public int ExamScheduleID { set; get; }

    [Display(Name = "Mã lớp học")]       
    public string? ClassID { set; get; }

    [Display(Name = "Tên lớp học")]
    public string? ClassName { get; set; }

    [Display(Name = "Tên kì thi")]
    public string? ExamScheduleName { set; get; }

    [Display(Name = "Ngày tạo")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateTimeCreated { set; get; }

    [Display(Name = "Ngày thi")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ExamDatetime { set; get; }

    [Display(Name = "Thời gian thi")]
    public int ExamTime { set; get; }
    
}
