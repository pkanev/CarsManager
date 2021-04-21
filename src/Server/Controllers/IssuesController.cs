using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Dtos;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Issues;
using CarsManager.Application.Issues.Queries;
using CarsManager.Application.Issues.Queries.GetAllIssues;
using CarsManager.Application.Issues.Queries.GetIssuesReport;
using CarsManager.Application.Issues.Queries.GetIssuesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace CarsManager.Server.Controllers
{
    public class IssuesController : ApiControllerBase
    {
        private readonly IMapper mapper;

        public IssuesController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IssuesDto>> Get(
            int beltW,
            int beltA,
            int beltC,
            int brakeLiningsW,
            int brakeLiningsA,
            int brakeLiningsC,
            int brakeDisksW,
            int brakeDisksA,
            int brakeDisksC,
            int coolantW,
            int coolantA,
            int coolantC,
            int oilW,
            int oilA,
            int oilC,
            int motW,
            int motA,
            int civilLiabilityW,
            int civilLiabilityA,
            int carInsuranceW,
            int carInsuranceA,
            int vignetteW,
            int vignetteA)
            => await Mediator.Send(new GetAllIssuesQuery
            {
                BeltMileageAlertLimit = beltA,
                BeltMileageWarningLimit = beltW,
                BeltChangeMileage = beltC,
                BrakeLiningsMileageAlertLimit = brakeLiningsA,
                BrakeLiningsMileageWarningLimit = brakeLiningsW,
                BrakeLiningsChangeMileage = brakeLiningsC,
                BrakeDisksMileageAlertLimit = brakeDisksA,
                BrakeDisksMileageWarningLimit = brakeDisksW,
                BrakeDisksChangeMileage = brakeDisksC,
                CoolantMileageAlertLimit = coolantA,
                CoolantMileageWarningLimit = coolantW,
                CoolantChangeMileage = coolantC,
                OilMileageAlertLimit = oilA,
                OilMileageWarningLimit = oilW,
                OilChangeMileage = oilC,
                MotAlertLimit = motA,
                MotWarningLimit = motW,
                CivilLiabilityAlertLimit = civilLiabilityA,
                CivilLiabilityWarningLimit = civilLiabilityW,
                CarInsuranceAlertLimit = carInsuranceA,
                CarInsuranceWarningLimit = carInsuranceW,
                VignetteAlertLimit = vignetteA,
                VignetteWarningsimit = vignetteW,
            });

        [HttpGet("belts")]
        public async Task<IList<IssueReportDto>> GetBeltsReport()
            => await Mediator.Send(new GetIssuesReportQuery { IssueType = RepairIssueType.BeltMileage });

        [HttpGet("oil")]
        public async Task<IList<IssueReportDto>> GetOilReport()
            => await Mediator.Send(new GetIssuesReportQuery { IssueType = RepairIssueType.OilMileage });

        [HttpGet("linings")]
        public async Task<IList<IssueReportDto>> GetLiningsReport()
            => await Mediator.Send(new GetIssuesReportQuery { IssueType = RepairIssueType.BrakeLiningsMileage });

        [HttpGet("brakes")]
        public async Task<IList<IssueReportDto>> GetBrakesReport()
            => await Mediator.Send(new GetIssuesReportQuery { IssueType = RepairIssueType.BrakeDisksMileage });

        [HttpGet("coolant")]
        public async Task<IList<IssueReportDto>> GetCoolantReport()
            => await Mediator.Send(new GetIssuesReportQuery { IssueType = RepairIssueType.CoolantMileage });

        [HttpGet("belts/pages")]
        public async Task<ActionResult<PaginatedList<IssueReportDto>>> GetPagedBeltsReport([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetIssuesWithPaginationQuery>(dto);
            query.IssueType = RepairIssueType.BeltMileage;

            return await Mediator.Send(query);
        }

        [HttpGet("oil/pages")]
        public async Task<ActionResult<PaginatedList<IssueReportDto>>> GetPagedOilReport([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetIssuesWithPaginationQuery>(dto);
            query.IssueType = RepairIssueType.OilMileage;

            return await Mediator.Send(query);
        }

        [HttpGet("linings/pages")]
        public async Task<ActionResult<PaginatedList<IssueReportDto>>> GetPagedLiningsReport([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetIssuesWithPaginationQuery>(dto);
            query.IssueType = RepairIssueType.BrakeLiningsMileage;

            return await Mediator.Send(query);
        }

        [HttpGet("brakes/pages")]
        public async Task<ActionResult<PaginatedList<IssueReportDto>>> GetPagedBrakesReport([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetIssuesWithPaginationQuery>(dto);
            query.IssueType = RepairIssueType.BrakeDisksMileage;

            return await Mediator.Send(query);
        }

        [HttpGet("coolant/pages")]
        public async Task<ActionResult<PaginatedList<IssueReportDto>>> GetPagedCoolantReport([FromQuery] PaginationDto dto)
        {
            var query = mapper.Map<GetIssuesWithPaginationQuery>(dto);
            query.IssueType = RepairIssueType.CoolantMileage;

            return await Mediator.Send(query);
        }
    }
}
