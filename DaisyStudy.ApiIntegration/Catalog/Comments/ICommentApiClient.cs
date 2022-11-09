using DaisyStudy.ViewModels.Catalog.Comments;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ApiIntegration.Catalog.Comments;

public interface ICommentApiClient
{
    Task<ApiResult<PagedResult<CommentViewModel>>> GetCommentPaging(GetManageCommentPagingRequest request);
    Task<bool> CreateComment(CommentCreateRequest request);
}