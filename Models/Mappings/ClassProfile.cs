using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Classes;

namespace DaisyStudy.Models.Mappings;
public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<Class, ClassViewModel>()
            .ForMember(dst => dst.Image, opt => opt.MapFrom(x => x.ImagePath));
        CreateMap<ClassViewModel, ClassUpdateRequest>();
    }
}