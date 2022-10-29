using System;
using System.Collections.Generic;
using System.Text;
using DaisyStudy.ViewModel.Common;

namespace DaisyStudy.ViewModel.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}