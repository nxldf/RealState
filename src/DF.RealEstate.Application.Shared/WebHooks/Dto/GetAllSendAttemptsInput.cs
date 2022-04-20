using DF.RealEstate.Dto;

namespace DF.RealEstate.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
