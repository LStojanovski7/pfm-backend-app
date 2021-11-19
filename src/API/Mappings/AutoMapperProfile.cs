using API.Models;
using Data.Entities;
using AutoMapper;

namespace API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryModel>().ForMember(m => m.Code, args => args.MapFrom(s => s.Code))
                                                .ForMember(m => m.ParrentCode, args => args.MapFrom(s => s.ParrentCode))
                                                .ForMember(m => m.Name, args => args.MapFrom(s => s.Name));
        }
    }
}