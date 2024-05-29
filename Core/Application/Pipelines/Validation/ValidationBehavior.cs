using Core.CrossCuttingConcers.Exceptions.Types;
using FluentValidation;
using MediatR;
using ValidationException = Core.CrossCuttingConcers.Exceptions.Types.ValidationException;
namespace Core.Application.Pipelines.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = _validators
                .Select(x => x.Validate(request))
                .SelectMany(result => result.Errors)
                .GroupBy(prop => prop.PropertyName)
                .Select(group => new ValidationExceptionModel
                {
                    Errors = group.Select(x => x.ErrorMessage).ToList(),
                    Property = group.Key
                });
            if (validationResult.Count() > 0) throw new ValidationException(validationResult);
            return await next();
        }
    }
}
