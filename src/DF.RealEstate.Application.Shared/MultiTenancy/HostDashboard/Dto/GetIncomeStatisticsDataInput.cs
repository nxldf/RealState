namespace DF.RealEstate.MultiTenancy.HostDashboard.Dto
{
    public class GetIncomeStatisticsDataInput : DashboardInputBase
    {
        public ChartDateInterval IncomeStatisticsDateInterval { get; set; }
    }
}