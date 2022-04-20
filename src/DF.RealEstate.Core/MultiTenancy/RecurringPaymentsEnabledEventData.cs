using Abp.Events.Bus;

namespace DF.RealEstate.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}