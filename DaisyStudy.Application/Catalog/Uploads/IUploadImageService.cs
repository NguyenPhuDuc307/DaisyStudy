using DaisyStudy.ViewModels.Catalog.Messages;
using DaisyStudy.ViewModels.Catalog.Uploads;

namespace DaisyStudy.Application.Catalog.Uploads;

public interface IUploadImageService
{
    Task<MessageViewModel> Upload(UploadViewModel uploadViewModel);
}