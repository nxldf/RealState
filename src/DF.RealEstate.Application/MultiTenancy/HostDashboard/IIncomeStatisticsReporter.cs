using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DF.RealEstate.MultiTenancy.HostDashboard.Dto;

namespace DF.RealEstate.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}