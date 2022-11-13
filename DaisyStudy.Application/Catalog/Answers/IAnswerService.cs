using DaisyStudy.ViewModels.Catalog.Answers;
using DaisyStudy.ViewModels.Catalog.Question;
using DaisyStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.Application.Catalog.Answers
{
    public interface IAnswerService
    {
        Task<int> Create(AnswerCreateRequest request);
        Task<int> Update(AnswerUpdateRequest request);
        Task<int> Delete(int AnswerID);
        Task<ApiResult<AnswerViewModel>> GetById(int AnswerID);
        Task<ApiResult<PagedResult<AnswerViewModel>>> GetAllPaging(GetManageAnswerPagingRequest request);
    }
}
