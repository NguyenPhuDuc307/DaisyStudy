using AutoMapper;
using DaisyStudy.Application.Common.SignalR;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Submissions;

namespace DaisyStudy.Models.Mappings
{
    public class SubmissionProfile : Profile
    {
        public SubmissionProfile()
        {
            CreateMap<Submission, SubmissionViewModel>();
            CreateMap<SubmissionViewModel, SubmissionUpdateRequest>();
        }
    }
}
