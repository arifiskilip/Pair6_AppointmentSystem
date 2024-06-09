using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using Application.Features.AppointmentInterval.Queries.GetById;
using Application.Repositories;
using Core.Domain;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AppointmentIntervalController : BaseController
    {
        private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;

        public AppointmentIntervalController(IAppointmentIntervalRepository appointmentIntervalRepository)
        {
            _appointmentIntervalRepository = appointmentIntervalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AppointmentIntervalsSearchByPaginatedQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdAppointmentIntervalQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var result = await _appointmentIntervalRepository.Test(1, null, DateTime.Parse("2024.06.06"), null, null, null);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Test2(int index, int size)
        {
            var query = await _appointmentIntervalRepository.GetListNotPagedAsync();
            var result = query.GroupBy(a => new { a.DoctorId, Date = a.IntervalDate.Date})
                .Select(g => new GroupedInterval
                {
                    Date = g.Key.Date,
                    DoctorId = g.Key.DoctorId,
                    IntervalDates = g.Select(x => new dot
                    {
                        IntervalDates = x.IntervalDate,
                        AppointmentIntervalId = x.Id
                    }).ToList()
                });
            var test = Paginate<GroupedInterval>.Create(result, index, size);
            return Ok(test);
        }

        [HttpGet]
        public async Task<IActionResult> Test3(int index, int size)
        {
            var query = await _appointmentIntervalRepository.GetListNotPagedAsync();
            var result = query.GroupBy(a => a.DoctorId)
                .Select(g=> g.OrderBy(a=> a.IntervalDate).FirstOrDefault())
                .ToList();
          
            return Ok(result);
        }
    }
    public class GroupedInterval : IEntity
    {
        public int DoctorId { get; set; }

        public DateTime Date { get; set; }
        public List<dot> IntervalDates { get; set; }
    }
    public class dot
    {
        public int AppointmentIntervalId { get; set; }
        public DateTime IntervalDates { get; set; }
    }

    public class Test3 : IEntity
    {
        public string DoctorName { get; set; }
        public DateTime IntervalDate { get; set; }
        public string DateMessage { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

}
