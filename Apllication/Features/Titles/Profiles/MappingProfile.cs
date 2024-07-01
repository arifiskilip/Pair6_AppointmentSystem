using Application.Features.Titles.Commands.Add;
using Application.Features.Titles.Commands.Update;
using Application.Features.Titles.Queries.GetAll;
using Application.Features.Titles.Queries.GetAllByPaginated;
using Application.Features.Titles.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Titles.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Title, AddTitleCommand>().ReverseMap();
            CreateMap<Title, AddTitleResponse>().ReverseMap();

            CreateMap<Title, UpdateTitleCommand>().ReverseMap();
            CreateMap<Title, UpdateTitleResponse>().ReverseMap();

            CreateMap<Title, TitleDto>().ReverseMap();

            CreateMap<Title, GetByIdTitleReponse>().ReverseMap();
            CreateMap<Title, GetAllTitleResponse>().ReverseMap();

        }
    }
}
