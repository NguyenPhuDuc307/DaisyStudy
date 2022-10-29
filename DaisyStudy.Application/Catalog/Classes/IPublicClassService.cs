using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Classes
{
    public interface IPublicClassService
    {
        Task<PagedResult<ClassViewModel>> GetAll(GetPublicClassPagingRequest request);
    }
}