using Application.Features.Branchs.Commands.Add;
using Application.Features.Branchs.Queries.GetById;
using Application.Features.Branchs.Commands.Update;
using Application.Features.Branchs.Queries.GetAll;
using Application.Features.Branchs.Queries.GetAllByPaginated;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Features.Branchs.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch,AddBranchCommand>().ReverseMap();
            CreateMap<Branch, AddBranchResponse>().ReverseMap();


            CreateMap<Branch, GetByIdBranchResponse>().ReverseMap();

            CreateMap<Branch, UpdateBranchCommand>().ReverseMap();
            CreateMap<Branch, UpdateBranchResponse>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();


            CreateMap<Branch, GetAllBranchResponse>().ReverseMap();

            CreateMap<Branch, GetAllByPaginatedBranchResponse>().ReverseMap();

        }
    }
}
