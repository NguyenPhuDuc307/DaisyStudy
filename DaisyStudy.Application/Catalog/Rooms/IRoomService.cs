using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Rooms;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace DaisyStudy.Application.Catalog.Rooms;

public interface IRoomService
{
    Task<IEnumerable<RoomViewModel>> Get();
    Task<Room> Get(int id);
    Task<Room> Get(string Name);
    Task<int> Create(RoomViewModel roomViewModel);
    Task<int> Edit(int id, RoomViewModel roomViewModel);
    Task<int> Delete(int id, string userName);
}