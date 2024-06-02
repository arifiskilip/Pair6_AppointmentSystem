using Core.Domain;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Titles.Queries.GetList
{
    public class GetListTitleResponse
    {
        public IPaginate<TitleDto>? Titles { get; set; }

        public class TitleDto : IEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
    }
}
