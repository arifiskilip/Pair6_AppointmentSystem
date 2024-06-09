using Domain.Dtos;


namespace Application.Features.Branchs.Queries.GetAll
{
    public class GetAllBranchResponse
    {
        public List<BranchDto> Branches { get; set; }
    }
}
