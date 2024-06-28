using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Doctors.Commands.Update
{
    public class UpdateDoctorCommand : IRequest<UpdateDoctorResponse>
    {
        public int Id { get; set; } = 1; // Şu an için Id'yi 1 olarak sabitliyoruz
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public short? TitleId { get; set; }
        public short? BranchId { get; set; }

        public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, UpdateDoctorResponse>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMapper _mapper;
           

            public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _mapper = mapper;
               
            }

            public async Task<UpdateDoctorResponse> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
            {
                // Doctor varlığını al
                var doctor = await _doctorRepository.GetAsync(
                    d => d.Id == request.Id,
                    include: query => query.Include(d => d.Title).Include(d => d.Branch)
                );
                // Null kontrolü yap ve mevcut değerleri koru
                if (!request.TitleId.HasValue)
                {
                    request.TitleId = doctor.TitleId;
                }

                if (!request.BranchId.HasValue)
                {
                    request.BranchId = doctor.BranchId;
                }
                _mapper.Map(request, doctor);              
                await _doctorRepository.UpdateAsync(doctor);
                // Geri dönecek response'u oluştur
                var response = _mapper.Map<UpdateDoctorResponse>(doctor);
                return response;
            }
        }
    }
}
