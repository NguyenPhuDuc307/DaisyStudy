using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.ExamSchedules;

namespace DaisyStudy.Models.Mappings;
public class ExamScheduleProfile : Profile
{
    public ExamScheduleProfile()
    {
        CreateMap<ExamSchedule, ExamSchedulesViewModel>();
        CreateMap<ExamSchedulesViewModel, ExamSchedulesUpdateRequest>();
    }
}