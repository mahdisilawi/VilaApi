using AutoMapper;
using Vila.WebApi.Dtos;
using Vila.WebApi.Models;
using Vila.WebApi.Utility;

namespace Vila.WebApi.Mapping
{
    public class ModelsMapper : Profile
    {
        public ModelsMapper()
        {
            CreateMap<Models.Vila, VilaDto>()
                .ForMember(x => x.BuildDate, d => d.MapFrom(des => des.BuildDate.ToPersainDate()))
                .ReverseMap()
                .ForMember(x => x.BuildDate, d => d.MapFrom(des => des.BuildDate.ToEnglishDateTime())); ;

            CreateMap<Detail, DetailDto>().ReverseMap();
            CreateMap<Models.Vila, VilaSearchDto>().
                  ForMember(x => x.BuildDate, d => d.MapFrom(des => des.BuildDate.ToPersainDate()));

        }
    }
}
