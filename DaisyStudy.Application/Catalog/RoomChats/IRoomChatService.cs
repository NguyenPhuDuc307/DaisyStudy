using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.RoomChats;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.RoomChats
{
    public interface IRoomChatService
    {
        Task<List<RoomChatViewModel>> GetAll();
        Task<RoomChat> GetByName(string RoomChatName);
        Task<RoomChat> GetById(int RoomChatID);
        Task<int> Create(RoomChatViewModel request);
        Task<int> Update(int RoomChatID, RoomChatViewModel request);
        Task<int> Delete(int RoomChatID);
    }
}