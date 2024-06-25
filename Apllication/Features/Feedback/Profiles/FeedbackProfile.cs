using Application.Features.Feedback.Commands.Add;
using Application.Features.Feedback.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Domain.Entities.Feedback,AddFeedbackCommand>().ReverseMap();
            CreateMap<Domain.Entities.Feedback, AddFeedBackResponse>().ReverseMap();
            CreateMap<Domain.Entities.Feedback, FeedbackPatientDto>().ReverseMap();
           
        }
    }
}
