using DaisyStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.ViewModels.Catalog.Answers;

public class GetManageAnswerPagingRequest : PagingRequestBase
{
    public string? Keyword { set; get; }

    public int QuestionID { set; get; }
}