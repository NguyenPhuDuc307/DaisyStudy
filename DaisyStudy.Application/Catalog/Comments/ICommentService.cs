using DaisyStudy.ViewModels.Catalog.Comments;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Comments;

public interface ICommentService
{
    Task<int> Create(CommentCreateRequest request);
    Task<int> Update(CommentUpdateRequest request);
    Task<int> Delete(int CommentID);
    Task<ApiResult<CommentViewModel>> GetById(int NotificationID);
    Task<ApiResult<PagedResult<CommentViewModel>>> GetAllPaging(GetManageCommentPagingRequest request);
}