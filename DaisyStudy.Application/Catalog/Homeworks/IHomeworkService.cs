using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.Homeworks;

namespace DaisyStudy.Application.Catalog.Homeworks
{
    public interface IHomeworkService
    {
        Task<int> Create(HomeworkCreateRequest request);
        Task<int> Update(HomeworkUpdateRequest request);
        Task<int> Delete(int ID);
        Task<HomeworkViewModel> GetById(int ID);
    }
}