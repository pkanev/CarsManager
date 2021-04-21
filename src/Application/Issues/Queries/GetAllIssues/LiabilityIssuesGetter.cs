using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public class LiabilityIssuesGetter : ILiabilityIssuesGetter
    {
        public async Task<LiabilityIssuesCounts> GetLiabilityIssuesCounts(IApplicationDbContext context, RepairIssuesLimitsDto limits)
        {
            List<Meta> vehicles = await GetLiabilityIssueData(context);

            var motWarnings = vehicles.Where(
                v => v.Mot != null
                && IsWarning(v.Mot.EndDate, limits.MotWarningLimit, limits.MotAlertLimit));

            var motAlerts = vehicles.Where(v
                => v.Mot == null
                || IsAlert(v.Mot.EndDate, limits.MotAlertLimit));

            var civilLiabilityWarnings = vehicles.Where(
                v => v.CivilLiability != null 
                && IsWarning(v.CivilLiability.EndDate, limits.CivilLiabilityWarningLimit, limits.CivilLiabilityAlertLimit));

            var civilLiabilityAlerts = vehicles.Where(
                v => v.CivilLiability == null
                || IsAlert(v.CivilLiability.EndDate, limits.CivilLiabilityAlertLimit));

            var carInsuranceWarnings = vehicles.Where(
                v => v.CarInsurance != null
                && IsWarning(v.CarInsurance.EndDate, limits.CarInsuranceWarningLimit, limits.CarInsuranceAlertLimit));

            var carInsuranceAlerts = vehicles.Where(
                v => v.CarInsurance == null
                || IsAlert(v.CarInsurance.EndDate, limits.CarInsuranceAlertLimit));

            var vignetteWarnings = vehicles.Where(
                v => v.Vignette != null
                && IsWarning(v.Vignette.EndDate, limits.VignetteWarningLimit, limits.VignetteAlertLimit));

            var vignetteAlerts = vehicles.Where(
                v => v.Vignette != null
                && IsAlert(v.Vignette.EndDate, limits.VignetteAlertLimit));

            return new LiabilityIssuesCounts
            {
                MotWarnings = motWarnings.Count(),
                MotAlerts = motAlerts.Count(),
                CivilLiabilityWarnings = civilLiabilityWarnings.Count(),
                CivilLiabilityAlerts = civilLiabilityAlerts.Count(),
                CarInsuranceWarnings = carInsuranceWarnings.Count(),
                CarInsuranceAlerts = carInsuranceAlerts.Count(),
                VignetteWarnings = vignetteWarnings.Count(),
                VignetteAlerts = vignetteAlerts.Count(),
            };
        }

        private static async Task<List<Meta>> GetLiabilityIssueData(IApplicationDbContext context)
            => await context.Vehicles
                .Include(v => v.MOTs)
                .Include(v => v.CivilLiabilities)
                .Include(v => v.CarInsurances)
                .Include(v => v.Vignettes)
                .Select(v => new Meta
                {
                    Mot = v.MOTs.OrderByDescending(m => m.EndDate).FirstOrDefault(),
                    CivilLiability = v.CivilLiabilities.OrderByDescending(m => m.EndDate).FirstOrDefault(),
                    CarInsurance = v.CarInsurances.OrderByDescending(m => m.EndDate).FirstOrDefault(),
                    Vignette = v.Vignettes.OrderByDescending(m => m.EndDate).FirstOrDefault(),
                })
                .ToListAsync();

        private bool IsWarning(DateTime endDate, int warningLimit, int alertLimit)
        {
            int remainingDays = (int)(endDate - DateTime.Today).TotalDays;
            return warningLimit >= remainingDays && remainingDays > alertLimit;
        }

        private bool IsAlert(DateTime endDate, int alertLimit)
        {
            int remainingDays = (int)(endDate - DateTime.Today).TotalDays;
            return alertLimit >= remainingDays;
        }

        private class Meta
        {
            public MOT Mot { get; set; }
            public CivilLiability CivilLiability { get; set; }
            public CarInsurance CarInsurance { get; set; }
            public Vignette Vignette { get; set; }
        }
    }
}
