using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public short TitleId { get; set; }
        public short BranchId { get; set; }
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

                _mapper.Map(request, doctor);

                
                await _doctorRepository.UpdateAsync(doctor);

                doctor = await _doctorRepository.GetAsync(
                d => d.Id == request.Id,
                include: query => query.Include(d => d.Title).Include(d => d.Branch),
                enableTracking: false
                );
                // Geri dönecek response'u oluştur
                var response = _mapper.Map<UpdateDoctorResponse>(doctor);
                response.TitleName = doctor.Title?.Name;
                response.BranchName = doctor.Branch?.Name;

                return response;
            }
        }
    }
}
