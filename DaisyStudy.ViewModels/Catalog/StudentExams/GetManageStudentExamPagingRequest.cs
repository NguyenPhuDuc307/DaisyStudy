using DaisyStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.StudentExams
{
    public class GetManageStudentExamPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public int ExamScheduleID { get; set; }
    }
}