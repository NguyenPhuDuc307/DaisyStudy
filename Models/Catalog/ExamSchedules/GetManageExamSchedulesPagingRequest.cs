using DaisyStudy.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.Models.Catalog.ExamSchedules;

public class GetManageExamSchedulesPagingRequest : PagingRequestBase
{
    public string? Keyword { set; get; }

    public int ClassID { set; get; }

    public string? UserId { get; set; }
}