using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Messages
{
    public interface IMessageService
    {
        Task<List<MessageViewModel>> GetAll(string roomName);
        Task<Message> GetById(int MessageID);
        Task<int> Create(MessageViewModel request);
        Task<int> UploadFile(UploadViewModel request);
        Task<int> Delete(int MessageID);
    }
}