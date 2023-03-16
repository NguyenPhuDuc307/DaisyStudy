using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Homeworks;

namespace DaisyStudy.Models.Mappings;
public class HomeworkProfile : Profile
{
    public HomeworkProfile()
    {
        CreateMap<Homework, HomeworkViewModel>();
        CreateMap<HomeworkViewModel, HomeworkUpdateRequest>();
    }
}