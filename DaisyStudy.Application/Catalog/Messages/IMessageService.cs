using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.ViewModels.Catalog.Uploads;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Messages
{
    public interface IMessageService
    {
        Task<Message> Get(int id);
        Task<IEnumerable<MessageViewModel>> GetMessages(string roomName);
        Task<MessageViewModel> Create(MessageViewModel messageViewModel);
        Task<int> Delete(int id, string UserName);
    }
}