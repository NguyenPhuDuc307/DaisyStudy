using System;
using DaisyStudy.Application.Catalog.Classes.Dtos;

namespace DaisyStudy.Application.Catalog.Classes
{
    public interface IClassService
    {
        int Create(ClassCreateRequest request);
        int Update(ClassUpdateRequest request);
        int Delete(int ClassID);
    }
}