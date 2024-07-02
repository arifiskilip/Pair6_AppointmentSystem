using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetAdminDashboardModel
{
    public class GetAdminDashboardModelQuery : IRequest<GetAdminDashboardModelResponse>, ISecuredRequest
    {
        public string[] Roles => ["Admin"];



        public class GetAdminDashboardModelQueryHandler : IRequestHandler<GetAdminDashboardModelQuery, GetAdminDashboardModelResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IPatientRepository _patientRepository;
            private readonly IBranchRepository _branchRepository;
            private readonly IDoctorRepository _doctorRepository;

            public GetAdminDashboardModelQueryHandler(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IBranchRepository branchRepository)
            {
                _appointmentRepository = appointmentRepository;
                _patientRepository = patientRepository;
                _doctorRepository = doctorRepository;
                _branchRepository = branchRepository;
            }

            public async Task<GetAdminDashboardModelResponse> Handle(GetAdminDashboardModelQuery request, CancellationToken cancellationToken)
            {
                int totalAppointments = _appointmentRepository.GetListNotPagedAsync().Result.Count();
                int totalBranches = _branchRepository.GetListNotPagedAsync().Result.Count();
                int totalDoctores = _doctorRepository.GetListNotPagedAsync().Result.Count();
                int totalPatients = _patientRepository.GetListNotPagedAsync().Result.Count();
                var getAppointmentCountsByBranch = _appointmentRepository.GetListNotPagedAsync(
                    include:x=> x.Include(i=>i.AppointmentInterval).ThenInclude(i=> i.Doctor).ThenInclude(i=> i.Branch))
                    .Result.GroupBy(a=> a.AppointmentInterval.Doctor.Branch.Name)
                    .Select(g=> new
                    {
                        branchName = g.Key,
                        Count = g.Count()
                    }).ToDictionary(d=> d.branchName,d=>d.Count);

                return new()
                {
                    TotalAppointments = totalAppointments,
                    TotalBranches = totalBranches,
                    TotalDoctores = totalDoctores,
                    TotalPatients = totalPatients,
                    GetAppointmentCountsByBranch = getAppointmentCountsByBranch
                };
            }
        }
    }
}
