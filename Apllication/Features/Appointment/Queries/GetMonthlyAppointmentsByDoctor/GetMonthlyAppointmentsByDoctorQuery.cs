using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetMonthlyAppointmentsByDoctor
{
    public class GetMonthlyAppointmentsByDoctorQuery : IRequest<GetMonthlyAppointmentsByDoctorResponse>, ISecuredRequest
    {
        public string[] Roles => ["Doctor", "Admin"];



        public class GetMonthlyAppointmentsByDoctorQueryHandler : IRequestHandler<GetMonthlyAppointmentsByDoctorQuery, GetMonthlyAppointmentsByDoctorResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IAuthService _authService;

            public GetMonthlyAppointmentsByDoctorQueryHandler(IAppointmentRepository appointmentRepository, IAuthService authService)
            {
                _appointmentRepository = appointmentRepository;
                _authService = authService;
            }

            public async Task<GetMonthlyAppointmentsByDoctorResponse> Handle(GetMonthlyAppointmentsByDoctorQuery request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                var getAppointments = await _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId &&
                                x.AppointmentInterval.IntervalDate >= DateTime.Now.AddMonths(-6),
                    include: x => x.Include(a => a.AppointmentInterval),
                    enableTracking: false);

                var appointmentsList = getAppointments.ToList();

                Dictionary<int, string> _turkishMonths = new Dictionary<int, string>
{
    { 1, "Ocak" },
    { 2, "Şubat" },
    { 3, "Mart" },
    { 4, "Nisan" },
    { 5, "Mayıs" },
    { 6, "Haziran" },
    { 7, "Temmuz" },
    { 8, "Ağustos" },
    { 9, "Eylül" },
    { 10, "Ekim" },
    { 11, "Kasım" },
    { 12, "Aralık" }
};

                // Geçmişten mevcut zamana kadar son 6 ayı içeren bir liste oluşturuyoruz
                var lastSixMonths = Enumerable.Range(0, 6)
                    .Select(i => DateTime.Now.AddMonths(-i))
                    .Select(date => new
                    {
                        Year = date.Year,
                        Month = date.Month,
                        MonthName = _turkishMonths[date.Month]
                    })
                    .OrderBy(m => new DateTime(m.Year, m.Month, 1))
                    .ToList();

                var monthlyAppointments = appointmentsList.GroupBy(a => new { a.AppointmentInterval.IntervalDate.Year, a.AppointmentInterval.IntervalDate.Month })
                                                          .Select(g => new
                                                          {
                                                              g.Key.Year,
                                                              g.Key.Month,
                                                              MonthName = _turkishMonths[g.Key.Month],
                                                              Count = g.Count()
                                                          })
                                                          .ToList();

                // Son 6 ayın her biri için randevu sayısını hesaplıyoruz
                var result = lastSixMonths.GroupJoin(monthlyAppointments,
                                                     month => new { month.Year, month.Month },
                                                     appointment => new { appointment.Year, appointment.Month },
                                                     (month, appointments) => new MonthlyAppointmentsDto
                                                     {
                                                         Month = month.MonthName,
                                                         Count = appointments.Sum(a => a.Count)
                                                     })
                                          .OrderBy(dto => new DateTime(DateTime.Now.Year, Array.IndexOf(_turkishMonths.Values.ToArray(), dto.Month) + 1, 1))
                                          .ToList();
                return new()
                {
                    MonthlyAppointments = result
                };
            }
        }
    }
}
