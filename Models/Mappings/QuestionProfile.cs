using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Question;

namespace DaisyStudy.Models.Mappings;
public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionViewModel>();
        CreateMap<QuestionsCreateRequest, Question>();
        CreateMap<QuestionViewModel, QuestionUpdateRequest>();
    }
}