using Application.Features.Titles.Commands.Create;
using Application.Features.Titles.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Titles.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Title, CreateTitleCommand>().ReverseMap();
            CreateMap<Title, CreateTitleResponse>().ReverseMap();
            CreateMap<Title, GetListTitleResponse.TitleDto>().ReverseMap();

        }
    }
}
