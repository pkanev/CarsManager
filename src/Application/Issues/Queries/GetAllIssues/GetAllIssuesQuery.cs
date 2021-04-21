using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using MediatR;

namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public class GetAllIssuesQuery : IRequest<IssuesDto>
    {
        public int BeltMileageWarningLimit { get; set; }
        public int BeltMileageAlertLimit { get; set; }
        public int BeltChangeMileage { get; set; }
        public int BrakeLiningsMileageWarningLimit { get; set; }
        public int BrakeLiningsMileageAlertLimit { get; set; }
        public int BrakeLiningsChangeMileage { get; set; }
        public int BrakeDisksMileageWarningLimit { get; set; }
        public int BrakeDisksMileageAlertLimit { get; set; }
        public int BrakeDisksChangeMileage { get; set; }
        public int CoolantMileageWarningLimit { get; set; }
        public int CoolantMileageAlertLimit { get; set; }
        public int CoolantChangeMileage { get; set; }
        public int OilMileageWarningLimit { get; set; }
        public int OilMileageAlertLimit { get; set; }
        public int OilChangeMileage { get; set; }
        public int MotWarningLimit { get; set; }
        public int MotAlertLimit { get; set; }
        public int CivilLiabilityWarningLimit { get; set; }
        public int CivilLiabilityAlertLimit { get; set; }
        public int CarInsuranceWarningLimit { get; set; }
        public int CarInsuranceAlertLimit { get; set; }
        public int VignetteWarningsimit { get; set; }
        public int VignetteAlertLimit { get; set; }
    }

    public class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, IssuesDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IRepairIssuesGetter repairIssuesGetter;
        private readonly ILiabilityIssuesGetter liabilityIssuesGetter;

        public GetAllIssuesQueryHandler(
            IApplicationDbContext context,
            IRepairIssuesGetter repairIssuesGetter,
            ILiabilityIssuesGetter liabilityIssuesGetter)
        {
            this.context = context;
            this.repairIssuesGetter = repairIssuesGetter;
            this.liabilityIssuesGetter = liabilityIssuesGetter;
        }

        public async Task<IssuesDto> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
        {
            var liabilityIssuesCounts = await liabilityIssuesGetter.GetLiabilityIssuesCounts(context, new RepairIssuesLimitsDto
            {
                CarInsuranceAlertLimit = request.CarInsuranceAlertLimit,
                CarInsuranceWarningLimit = request.CarInsuranceWarningLimit,
                CivilLiabilityAlertLimit = request.CivilLiabilityAlertLimit,
                CivilLiabilityWarningLimit = request.CivilLiabilityWarningLimit,
                MotAlertLimit = request.MotAlertLimit,
                MotWarningLimit = request.MotWarningLimit,
                VignetteAlertLimit = request.VignetteAlertLimit,
                VignetteWarningLimit = request.VignetteAlertLimit,
            });

            return new IssuesDto
            {
                BeltMileageAlerts = await repairIssuesGetter.GetBeltMileageAlerts(
                    context,
                    request.BeltChangeMileage,
                    alertLimit: request.BeltMileageAlertLimit),

                BeltMileageWarnings = await repairIssuesGetter.GetBeltMileageWarnings(
                    context,
                    request.BeltChangeMileage,
                    warningLimit: request.BeltMileageWarningLimit,
                    alertLimit: request.BeltMileageAlertLimit),

                BrakeDisksMileageAlerts = await repairIssuesGetter.GetBrakeDisksMileageAlerts(
                    context,
                    request.BrakeDisksChangeMileage,
                    alertLimit: request.BrakeDisksMileageAlertLimit),

                BrakeDisksMileageWarnings = await repairIssuesGetter.GetBrakeDisksMileageWarnings(
                    context,
                    request.BrakeDisksChangeMileage,
                    warningLimit: request.BrakeDisksMileageWarningLimit,
                    alertLimit: request.BrakeDisksMileageAlertLimit),

                BrakeLiningsMileageAlerts = await repairIssuesGetter.GetBrakeLiningsMileageAlerts(
                    context,
                    request.BrakeLiningsChangeMileage,
                    alertLimit: request.BrakeLiningsMileageAlertLimit),

                BrakeLiningsMileageWarnings = await repairIssuesGetter.GetBrakeLiningsMileageWarnings(
                    context,
                    request.BrakeLiningsChangeMileage,
                    warningLimit: request.BrakeLiningsMileageWarningLimit,
                    alertLimit: request.BrakeLiningsMileageAlertLimit),

                CoolantMileageAlerts = await repairIssuesGetter.GetCoolantMileageAlerts(
                    context,
                    request.CoolantChangeMileage,
                    alertLimit: request.CoolantMileageAlertLimit),

                CoolantMileageWarnings = await repairIssuesGetter.GetCoolantMileageWarnings(
                    context,
                    request.CoolantChangeMileage,
                    warningLimit: request.CoolantMileageWarningLimit,
                    alertLimit: request.CoolantMileageAlertLimit),

                OilMileageAlerts = await repairIssuesGetter.GetOilMileageAlerts(
                    context,
                    request.OilChangeMileage,
                    alertLimit: request.OilMileageAlertLimit),

                OilMileageWarnings = await repairIssuesGetter.GetOilMileageWarnings(
                    context,
                    request.OilChangeMileage,
                    warningLimit: request.OilMileageWarningLimit,
                    alertLimit: request.OilMileageAlertLimit),

                MotAlerts = liabilityIssuesCounts.MotAlerts,
                MotWarnings = liabilityIssuesCounts.MotWarnings,
                CivilLiabilityAlerts = liabilityIssuesCounts.CivilLiabilityAlerts,
                CivilLiabilityWarnings = liabilityIssuesCounts.CivilLiabilityWarnings,
                CarInsuranceAlerts = liabilityIssuesCounts.CarInsuranceAlerts,
                CarInsuranceWarnings = liabilityIssuesCounts.CarInsuranceWarnings,
                VignetteAlerts = liabilityIssuesCounts.VignetteAlerts,
                VignetteWarnings = liabilityIssuesCounts.VignetteWarnings,
            };
        }
    }
}
