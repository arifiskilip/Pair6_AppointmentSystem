using Application.Features.Doctors.Rules;
using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.Utilities.FileHelper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Doctors.Commands.AddImage
{
    public class DoctorAddImageCommand : IRequest<DoctorAddImageResponse>, ITransactionalRequest, ISecuredRequest
    {
        public int DoctorId { get; set; }
        public IFormFile File { get; set; }
        public string[] Roles => ["Doctor"];



        public class DoctorAddImageHandler : IRequestHandler<DoctorAddImageCommand, DoctorAddImageResponse>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly DoctorBusinessRules _businessRules;

            public DoctorAddImageHandler(IDoctorRepository doctorRepository, DoctorBusinessRules businessRules)
            {
                _doctorRepository = doctorRepository;
                _businessRules = businessRules;
            }

            public async Task<DoctorAddImageResponse> Handle(DoctorAddImageCommand request, CancellationToken cancellationToken)
            {
                var patient = await _doctorRepository.GetAsync(x => x.Id == request.DoctorId);
                _businessRules.IsSelectedEntityAvailable(patient);

                var newImageUrl = await FileHelper.AddAsync(request.File, FileTypeEnum.Image);

                if (!string.IsNullOrEmpty(patient.ImageUrl))
                {
                    FileHelper.Delete(patient.ImageUrl);
                }

                patient.ImageUrl = newImageUrl;
                await _doctorRepository.UpdateAsync(patient);

                return new()
                {
                    Message = "Ekleme işlemi başarılı!"
                };
            }
        }
    }
}
