using Application.Features.Branchs.Commands.Add;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch,AddBranchCommand>().ReverseMap();
            CreateMap<Branch, AddBranchResponse>().ReverseMap();
        }
    }
}
