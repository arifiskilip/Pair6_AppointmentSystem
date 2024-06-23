using Application.Features.Patients.Rules;
using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.Utilities.FileHelper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Patients.Commands.AddImage
{
    public class PatientAddImageCommand : IRequest<PatientAddImageResponse>, ITransactionalRequest, ISecuredRequest
    {
        public int PatientId { get; set; }
        public IFormFile File { get; set; }

        public string[] Roles => ["Patient"];

        public class PatientAddImageHandler : IRequestHandler<PatientAddImageCommand, PatientAddImageResponse>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly PatientBusinessRules _businessRules;

            public PatientAddImageHandler(IPatientRepository patientRepository, PatientBusinessRules businessRules)
            {
                _patientRepository = patientRepository;
                _businessRules = businessRules;
            }

            public async Task<PatientAddImageResponse> Handle(PatientAddImageCommand request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetAsync(x => x.Id == request.PatientId);
                _businessRules.IsSelectedEntityAvailable(patient);

                var newImageUrl = await FileHelper.AddAsync(request.File, FileTypeEnum.Image);

                if (!string.IsNullOrEmpty(patient.ImageUrl))
                {
                    FileHelper.Delete(patient.ImageUrl);
                }

                patient.ImageUrl = newImageUrl;
                await _patientRepository.UpdateAsync(patient);

                return new()
                {
                    Message = "Ekleme işlemi başarılı!"
                };
            }
        }
    }
}
