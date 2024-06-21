using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcers.Exceptions.HttpProblemDetails
{
    internal class BusinessProblemDetails : ProblemDetails
    {
        public BusinessProblemDetails(string detail)
        {
            Title = "Rule violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/business";
        }
    }
}
