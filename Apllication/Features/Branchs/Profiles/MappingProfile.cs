using Application.Features.Branchs.Commands.Add;
using Application.Features.Branchs.Queries.GetAll;
using Application.Features.Branchs.Queries.GetAllByPaginated;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Features.Branchs.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch,AddBranchCommand>().ReverseMap();
            CreateMap<Branch, AddBranchResponse>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();

            CreateMap<Branch, GetAllBranchResponse>().ReverseMap();

            CreateMap<Branch, GetAllByPaginatedBranchResponse>().ReverseMap();
        }
    }
}
