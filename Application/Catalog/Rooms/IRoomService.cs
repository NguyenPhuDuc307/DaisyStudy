using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Classes;

namespace DaisyStudy.Application.Catalog.Rooms
{
    public interface IRoomService
    {
        Task<int> Create(string UserName, string ClassName);
    }
}