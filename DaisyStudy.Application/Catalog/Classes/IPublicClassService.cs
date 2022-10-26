using DaisyStudy.ViewModel.Catalog.Classes;
using DaisyStudy.ViewModel.Common;

namespace DaisyStudy.ViewModel.Catalog.Classes
{
    public interface IPublicClassService
    {
        Task<PagedResult<ClassViewModel>> GetAll(GetClassPagingRequest request);
        Task<List<ClassViewModel>> GetAllClass();
    }
}