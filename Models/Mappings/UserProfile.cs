using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.System.Users;

namespace DaisyStudy.Models.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserViewModel>();
        CreateMap<UserViewModel, ApplicationUser>();
    }
}